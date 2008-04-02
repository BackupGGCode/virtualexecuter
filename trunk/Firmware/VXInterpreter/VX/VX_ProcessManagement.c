#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include "VX.h"
#include "DRAM.h"
#include "VX_ProcessManagement.h"
#include <mem.h>


typedef struct
{
	vx_pid id;
	vx_pstate state;
	unsigned long ticks;
	unsigned char flags;
	dram ip;
	dram sp;
	dram code;
	unsigned long code_size;
	dram constant;
	unsigned long constant_size;
	dram data;
	unsigned long data_size;
	dram stack;
	unsigned long stack_size;
	dram next;
	char name[PROCES_NAME_LENGTH + 1];
} process;

dram processList = null;

#define WriteProcess(address, proc)							DRAM_WriteBytes(address, (unsigned char*)proc, sizeof(process))
#define ReadProcess(address, proc)							DRAM_ReadBytes(address, (unsigned char*)proc, sizeof(process))


string* vxPStateText[] ={" Stop", " Run ", " Step", "Crash"};


void VX_InitProcesses()
{
	
}

#define BLOCK_SIZE															32

bool VX_CreateProcessFromFile(char* filename, vx_pid* id)
{
unsigned char* buffer = Kernel_Allocate(21);
unsigned long codeSize, constantSize, dataSize, stackSize;
dram codeStart, constantStart, i;
process* proc;
dram newProcess;
fsFile file;
unsigned char length;

	if(FileStore_OpenFile(filename, &file) == false)
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	FileStore_ReadBytes(&file, buffer, 21);
	if(buffer[0] != 'V' || buffer[1] != 'X' || buffer[2] != 'E' || buffer[3] != 'X' || buffer[4] != 'E')
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	Copy(buffer + 5, &codeSize, 4);
	Copy(buffer + 9, &constantSize, 4);
	Copy(buffer + 13, &dataSize, 4);
	Copy(buffer + 17, &stackSize, 4);
	Kernel_Deallocate(buffer);
	
	newProcess = DRAM_Allocate(sizeof(process) + codeSize + constantSize + dataSize + stackSize);
	if(newProcess == 0)
	{
		return false;
	}
	
	proc = Kernel_Allocate(sizeof(process));
	
	proc->id = newProcess;
	proc->state = Stop;
	proc->ticks = 0;
	proc->flags = 0;
	proc->ip = 0;
	proc->sp = 0;
	codeStart = newProcess + sizeof(process);
	proc->code = codeStart;
	proc->code_size = codeSize;
	constantStart = proc->code + proc->code_size;
	proc->constant = constantStart;
	proc->constant_size = constantSize;
	proc->data = proc->constant + proc->constant_size;
	proc->data_size = dataSize;
	proc->stack = proc->data + proc->data_size;
	proc->stack_size = stackSize;
	proc->next = processList;
	length = 0;
	do
	{
		proc->name[length] = filename[length];
		length++;
	} while(length < PROCES_NAME_LENGTH && filename[length] != 0);
	proc->name[length] = 0;
	
	WriteProcess(newProcess, proc);
	
	processList = newProcess;
	
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
		DRAM_WriteBytes(newProcess + codeStart + i, buffer, length);
		i += length;
	}
	
	length = BLOCK_SIZE;
	i = 0;
	while(i < constantSize)
	{
		if((constantSize - i) < length)
		{
			length = constantSize - i;
		}
		if(FileStore_ReadBytes(&file, buffer, length) != length)
		{
			Kernel_Deallocate(buffer);
			DRAM_Deallocate(newProcess);
			return false;		
		}
		DRAM_WriteBytes(newProcess + constantStart + i, buffer, length);
		i += length;
	}
	
	Kernel_Deallocate(buffer);
	
	*id = newProcess;
	
	return true;
}

bool VX_KillProcess(vx_pid id)
{
process* proc1;
process* proc2;
dram current = processList;

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
			proc->state = state;
			WriteProcess(id, proc);
			Kernel_Deallocate(proc);
			return true;
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
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(sizeof(process) + p->code_size + p->constant_size + p->data_size + p->stack_size, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteString(p->name);
		UART_WriteString_P("\n");
		address = p->next;
	} while(p->next != null);
	
	Kernel_Deallocate(p);
}
