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
} process;

dram processList = null;

#define WriteProcess(proc, address)							DRAM_WriteBytes((unsigned char*)proc, address, sizeof(process))
#define ReadProcess(proc, address)							DRAM_ReadBytes((unsigned char*)proc, address, sizeof(process))


void VX_InitProcesses()
{
	
}

bool VX_CreateProcessFromFile(fsFile* file, vx_pid* id)
{
unsigned char* buffer = Kernel_Allocate(21);
unsigned long codeSize, constantSize, dataSize, stackSize;
process* proc;
dram newProcess;

	FileStore_ReadBytes(file, buffer, 21);
	if(buffer[0] != 'V' || buffer[1] != 'X' || buffer[2] != 'E' || buffer[3] != 'X' || buffer[4] != 'E')
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	Copy(buffer + 5, &codeSize, 4);
	Copy(buffer + 9, &constantSize, 4);
	Copy(buffer + 13, &dataSize, 4);
	Copy(buffer + 17, &stackSize, 4);
	
	if(codeSize == 0)
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	newProcess = DRAM_Allocate(sizeof(process) + codeSize + constantSize + dataSize + stackSize);
	if(newProcess == 0)
	{
		Kernel_Deallocate(buffer);
		return false;
	}
	
	proc = Kernel_Allocate(sizeof(process));
	
	proc->id = newProcess;
	proc->state = Stopped;
	proc->ticks = 0;
	proc->flags = 0;
	proc->ip = 0;
	proc->sp = 0;
	proc->code = newProcess + sizeof(vx_pid) + sizeof(vx_pstate) + sizeof(unsigned long) + sizeof(unsigned char) +  + sizeof(dram) +  + sizeof(dram);
	proc->code_size = codeSize;
	proc->constant = proc->code + proc->code_size;
	proc->constant_size = constantSize;
	proc->data = proc->constant + proc->constant_size;
	proc->data_size = dataSize;
	proc->stack = proc->data + proc->data_size;
	proc->stack_size = stackSize;
	proc->next = processList;
	WriteProcess(proc, newProcess);
	processList = newProcess;
	
	Kernel_Deallocate(proc);
	
	*id = newProcess;
	
	return true;
}

bool VX_KillProcess(vx_pid id)
{
process* proc1;
process* proc2;
dram current = processList;

	proc1 = Kernel_Allocate(sizeof(process));
	proc2 = Kernel_Allocate(sizeof(process));
	if(proc1 == null || proc2 == null)
	{
		return false;
	}
	
	ReadProcess(proc1, processList);
	
	if(processList == id)
	{
		processList = proc1->next;
		DRAM_Deallocate(id);
	}
	else
	{
		current = processList;
		while(proc1->next != null)
		{
			if(proc1->next == id)
			{
				ReadProcess(proc2, id);
				proc1->next = proc2->next;
				WriteProcess(proc1, current);
				DRAM_Deallocate(id);
				Kernel_Deallocate(proc1);
				Kernel_Deallocate(proc2);
				return true;
			}
			
			current = proc1->next;
			ReadProcess(proc1, current);
		}
	}
	
	Kernel_Deallocate(proc1);
	Kernel_Deallocate(proc2);
	
	return false;
}

bool VX_SetProcessState(vx_pid id, vx_pstate state)
{
process* proc1;
dram current = processList;

	proc = Kernel_Allocate(sizeof(process));
	if(proc == null)
	{
		return false;
	}
	
	ReadProcess(proc, processList);
	
	if(processList == id)
	{
		proc->state = state;
		WriteProcess(proc, id);
	}
	else
	{
		current = processList;
		while(proc->next != null)**************************************************************************************
		{
			if(proc->next == id)
			{
				ReadProcess(proc, id);
				proc->next = proc->next;
				WriteProcess(proc, current);
				DRAM_Deallocate(id);
				Kernel_Deallocate(proc);
				Kernel_Deallocate(proc);
				return true;
			}
			
			current = proc->next;
			ReadProcess(proc, current);
		}
	}
	
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
	
	UART_WriteString_P("PID.... State Ticks..... IP..... SP..... Size...\n");
	
	do
	{
		ReadProcess(p, address);
		UART_WriteValueUnsigned(p->id, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->state, 5, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ticks, 10, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ip, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->sp, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->code_size + p->constant_size + p->data_size + p->stack_size, 7, ' ');
		UART_WriteString_P("\n");
		address = p->next;
	} while(p->next != null);
	
	Kernel_Deallocate(p);
}
