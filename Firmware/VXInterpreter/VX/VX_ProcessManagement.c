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
unsigned char processCount = 0;
vx_pid nextID = 0;


void ReadProcess(process* p, dram address);
void WriteProcess(process* p, dram address);
//bool FindProcess(process* p, vx_pid id);

void VX_InitProcesses()
{
	
}

bool VX_CreateProcess(fsFile* file, vx_pid* id)
{
/*
	Get process size from file
	Allocate chunk on DRAM heap
	Allocation fail => return false
	Create process information
	Save process information to DRAM
	Copy .code and .const to DRAM
*/
	
unsigned char i=0;

process* p = (process*)Kernel_Allocate(sizeof(process));;

	while(FindProcess(p, nextID))
	{
		if(i == 255)
		{
			Kernel_Deallocate(p);
			return false;
		}
		nextID++;
		i++;
	}
	
	p->id = nextID;
	p->stack = Stopped;
	p->ticks = 0;
	p->flags = 0;
	p->ip = 0;
	p->sp = 0;
	p->code = 0;
	p->code_size = 0;
	p->constant = 0;
	p->constant_size = 0;
	p->data = 0;
	p->data_size = 0;
	p->stack = 0;
	p->stack_size = 0;
	p->next = 0;
	WriteProcess(p, a);
	
	Kernel_Deallocate(p);
	
	*id = nextID;
	nextID++;
	
	return true;
}

/*
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
*/
bool VX_KillProcess(vx_pid id)
{

	return false;	
}

bool VX_SetProcessState(vx_pid id, vx_pstate state)
{
	
	return false;
}

void VX_ListProcesses(char* line)
{
process* p;
dram address = 0;

	if(processCount == 0)
	{
		UART_WriteString_P("No processes created\n");
		return;
	}
	
	p = (process*)Kernel_Allocate(sizeof(process));

	UART_WriteString_P("PID        Ticks      IP         SP         Size\n");
	
	do
	{
		ReadProcess(p, address);
		UART_WriteValueUnsigned(p->id,0,' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ticks,0,' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->ip,0,' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->sp,0,' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(p->code_size+p->constant_size+p->data_size+p->stack_size,0,' ');
		UART_WriteString_P("\n");
		address = p->next;
	} while(p->next != null);
	Kernel_Deallocate(p);
}


//*****************************************************************************


void ReadProcess(process* p, dram address)
{
unsigned char i;
unsigned char* pp = (unsigned char*)p;

	for(i = 0; i < sizeof(process); i++)
	{
		*pp++ = DRAM_ReadByte(address++);
	}
}


void WriteProcess(process* p, dram address)
{
unsigned char i;
unsigned char* pp = (unsigned char*)p;

	for(i = 0; i < sizeof(process); i++)
	{
		DRAM_WriteByte(address++, *pp++);
	}
}


/*
bool FindProcess(process* p, vx_pid id)
{
process* p2;
dram address = 0;
bool processFound = false;

	if(processCount == 0)
	{
		return false;
	}
	
	p2 = (process*)Kernel_Allocate(sizeof(process));
	
	do
	{
		ReadProcess(p2, address);
		
		if(p2->id == id)
		{
			processFound = true;
			Copy(p2, p, sizeof(process));
		}
		
		address = p->next;
	} while(p->next != null && processFound == false);	

	Kernel_Deallocate(p2);

	return processFound;
}
*/
