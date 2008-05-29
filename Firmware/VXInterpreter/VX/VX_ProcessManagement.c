#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include "VX.h"
#include "DRAM.h"
#include "VX_ProcessManagement.h"
#include <Memory.h>


dram processList = null;

string* vxPStateText[] ={" Stop", " Run ", " Step", "Crash", " Done"};


#define BLOCK_SIZE															32

bool VX_CreateProcessFromFile(char* filename, vx_pid* id)
{
unsigned char* buffer;
unsigned long codeSize, dataSize, stackSize;
unsigned long options;
dram codeStart, i;
process* proc;
dram newProcess;
fsFile file;
unsigned char length;

	if(FileStore_OpenFile(filename, &file) == false)
	{
		return false;
	}
	
	buffer = Kernel_Allocate(21);
	FileStore_ReadBytes(&file, buffer, 21);
	
	if(Strings_StartsWith_P(buffer, "VXEXE") == false)
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	Memory_Copy_Tiny(buffer + 5, &options, 4);
	Memory_Copy_Tiny(buffer + 9, &codeSize, 4);
	Memory_Copy_Tiny(buffer + 13, &dataSize, 4);
	Memory_Copy_Tiny(buffer + 17, &stackSize, 4);
	Kernel_Deallocate(buffer);
	
	newProcess = DRAM_Allocate(sizeof(process) + codeSize + dataSize + stackSize);
	if(newProcess == 0)
	{
		return false;
	}
	
	proc = Kernel_Allocate(sizeof(process));
	
	proc->id = newProcess;
	proc->options = options;
	length = 0;
	do
	{
		proc->name[length] = filename[length];
		length++;
	} while(length < PROCES_NAME_LENGTH && filename[length] != 0);
	proc->name[length] = 0;

	codeStart = newProcess + sizeof(process);
	proc->codeStart = codeStart;
	proc->codeSize = codeSize;
	proc->dataStart = proc->codeStart + proc->codeSize;
	proc->dataSize = dataSize;
	proc->stackStart = proc->dataStart + proc->dataSize;
	proc->stackSize = stackSize;
	proc->next = processList;
	proc->state = Stop;
	proc->ticks = 0;
	proc->flags = 0;
	proc->ip = 0;
	proc->sp = 0;
	proc->sfp = 0;
	
	WriteProcess(newProcess, proc);
	Kernel_Deallocate(proc);
	
	buffer = Kernel_Allocate(BLOCK_SIZE);
	if(buffer == null)
	{
		DRAM_Deallocate(newProcess);
		return false;
	}
	
	length = BLOCK_SIZE;
	i = 0;
	while(i < codeSize)
	{
		if((codeSize - i) < length)
		{
			length = codeSize - i;
		}
		
		if(FileStore_ReadBytes(&file, buffer, length) != length)
		{
			Kernel_Deallocate(buffer);
			DRAM_Deallocate(newProcess);
			return false;		
		}
		DRAM_WriteBytes(codeStart + i, buffer, length);
		i += length;
	}
	
	Kernel_Deallocate(buffer);
	
	*id = newProcess;
	processList = newProcess;
	
	PORTF |= (1 << 6);
	
	return true;
}

bool VX_KillProcess(vx_pid id)
{
process* proc1;
process* proc2;
dram current;

	if(processList == null)
	{
		return false;
	}

	proc1 = Kernel_Allocate(sizeof(process));
	if(proc1 == null)
	{
		return false;
	}
	proc2 = Kernel_Allocate(sizeof(process));
	if(proc2 == null)
	{
		Kernel_Deallocate(proc1);
		return false;
	}
	
	ReadProcess(processList, proc1);
	
	if(processList == id)
	{
		processList = proc1->next;
		DRAM_Deallocate(id);
		Kernel_Deallocate(proc1);
		Kernel_Deallocate(proc2);
		return true;
	}
	else
	{
		current = processList;
		while(proc1->next != null)
		{
			if(proc1->next == id)
			{
				ReadProcess(id, proc2);
				proc1->next = proc2->next;
				WriteProcess(current, proc1);
				DRAM_Deallocate(id);
				Kernel_Deallocate(proc1);
				Kernel_Deallocate(proc2);
				return true;
			}
			
			current = proc1->next;
			ReadProcess(current, proc1);
		}
	}
	
	Kernel_Deallocate(proc1);
	Kernel_Deallocate(proc2);
	
	return false;
}

bool VX_SetProcessState(vx_pid id, vx_pstate state)
{
process* proc;
dram current = processList;

	proc = Kernel_Allocate(sizeof(process));
	if(proc == null)
	{
		return false;
	}
	
	do
	{
		ReadProcess(current, proc);
		if(proc->id == id)
		{
			if(proc->state != Crash && proc->state != Done)
			{
				proc->state = state;
				WriteProcess(id, proc);
				Kernel_Deallocate(proc);
				return true;
			}
			else
			{
				Kernel_Deallocate(proc);
				return false;
			}
		}
		current = proc->next;
	} while(current != null);

	Kernel_Deallocate(proc);
	
	return false;
}

void VX_ListProcesses(char* line)
{
process* p;
dram address = processList;

	if(processList == null)
	{
		UART_WriteString_P("No processes created\n");
		return;
	}
	
	p = (process*)Kernel_Allocate(sizeof(process));
	
	//UART_WriteString_P("PID.... State Ticks..... IP..... SP..... SFP.... Size... Name\n");
	UART_WriteString_P("PID.... State Ticks..... IP..... SP..... Size... Name\n");
	
	do
	{
		ReadProcess(address, p);
		UART_WriteValueUnsigned(p->id, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteString_P(vxPStateText[p->state]);
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ticks, 10, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ip, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->sp, 7, ' ');
//		UART_WriteByte(' ');
//		UART_WriteValueUnsigned(p->sfp, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(sizeof(process) + p->codeSize + p->dataSize + p->stackSize, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteString(p->name);
		UART_WriteString_P("\n");
		address = p->next;
	} while(p->next != null);
	
	Kernel_Deallocate(p);
}
