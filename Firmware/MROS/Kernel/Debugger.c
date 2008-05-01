#include <Peripherals/UART.h>
#include "KernelInternals.h"
#include "Kernel.h"


#define SOP																			0x00
#define EOP																			0x01


void Hexify(unsigned char value, unsigned char* destination)
{
	destination[0] = value&0x0f;
	destination[1] = (value>>4)&0x0f;
}


void Kernel_InitDebugger()
{
	UART_Init(DEBUGGER_BAUDRATE);
}


void Kernel_DebuggerEvent(unsigned char event, task target, unsigned char status, unsigned long extended)
{
unsigned char buffer[2*14];
unsigned char i, current, count;
unsigned short caller = (unsigned short)CurrentTask();

	putc(SOP);

// Put into buffer as non ASCII hex values using two bytes per byte

	Hexify(event, buffer+0);
	
	Hexify(totalExecutionTime, buffer+2);
	Hexify(totalExecutionTime>>8, buffer+4);
	Hexify(totalExecutionTime>>16, buffer+6);
	Hexify(totalExecutionTime>>24, buffer+8);
	
	Hexify(caller, buffer+10);
	Hexify(caller>>8, buffer+12);

	Hexify((unsigned short)target, buffer+14);
	Hexify(((unsigned short)target)>>8, buffer+16);

	Hexify(status, buffer+18);
	
	Hexify(extended, buffer+20);
	Hexify(extended>>8, buffer+22);
	Hexify(extended>>16, buffer+24);
	Hexify(extended>>24, buffer+26);


// Compress (RLE) the buffer and send the compressed frame
	current = buffer[0];
	count = 1;
	
	for(i = 1; i < (2*14); i++)
	{
		if(buffer[i] == current && count < 15)
		{
			count++;
		}
		else
		{
			putc( ((count)<<4) | current );
			current = buffer[i];
			count = 1;
		}
	}
	putc( ((count)<<4) | current );
	
	putc(EOP);
}
