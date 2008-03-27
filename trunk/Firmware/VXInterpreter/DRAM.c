#include <Globals.h>
#include <Config.h>
#include "DRAM.h"
#include <Kernel/Kernel.h>
#include <Kernel/KernelInternals.h>
#include <Peripherals/UART.h>


#define DRAM_PRESCALER													12
#define NUMBER_OF_ROWS													1024

#define DATA_IN																	PIND
#define DATA_OUT																PORTD
#define ADR_PORT																PORTA
#define CTRL_PORT																PORTC

#define DIR_IN																	DDRD = 0x00
#define DIR_OUT																	DDRD = 0xff

#define CAS_HIGH																CTRL_PORT |= (1<<7)
#define CAS_LOW																	CTRL_PORT &= ~(1<<7)
#define RAS_HIGH																CTRL_PORT |= (1<<5)
#define RAS_LOW																	CTRL_PORT &= ~(1<<5)
#define WE_HIGH																	CTRL_PORT |= (1<<6)
#define WE_LOW																	CTRL_PORT &= ~(1<<6)


typedef struct
{
	bool free;
	unsigned long size;
	unsigned long next;
} block;


static void ReadBlock(block* b, unsigned long address);
static void ReadFirstBlock(block* b);
static void WriteBlock(block* b, unsigned long address);
static void JoinFreeAdjacentBlocks();


void DRAM_Init()
{
block b;

	b.free = true;
	b.size = DRAM_SIZE - sizeof(block);
	b.next = null;

	WriteBlock(&b, 0);
}


void DRAM_Refresh()
{
static unsigned char prescaler = 0;
unsigned short i;

	if(prescaler == 0)
	{
		for(i=0;i<NUMBER_OF_ROWS;i++)
		{
			CAS_LOW;
			__no_operation();
			RAS_LOW;
			__no_operation();
			CAS_HIGH;
			__no_operation();
			RAS_HIGH;
		}
		prescaler = DRAM_PRESCALER;
	}
	else
	{
		prescaler--;
	}
}


dram DRAM_Allocate(unsigned long size)
{
block b;
block split;
dram a1 = 0, a2;

	ReadFirstBlock(&b);

	while((b.free != true || b.size < size) && b.next != null)
	{
		a1 = b.next;
		ReadBlock(&b, a1);
	}
	
	if(b.free != true || b.size < size)																									// Found a valid block?
	{
		return null;																																					// No
	}
	
	if(b.size > (size + sizeof(block)))																									// Split block, return first part and mark last part as free
	{
		a2 = a1 + sizeof(block) + size;
		split.free = true;
		split.size = b.size - size - sizeof(block);
		split.next = b.next;
		WriteBlock(&split, a2);
		
		b.free = false;
		b.size = size;
		b.next = a2;
	}
	else																																									// Block is too small to split - return all
	{
		b.free = false;
	}

	WriteBlock(&b, a1);
	
	return (dram)(a1 + sizeof(block));
}


void DRAM_Deallocate(dram chunk)
{
block b;
dram a1 = 0, a2 = (chunk - sizeof(block));

	ReadFirstBlock(&b);

	while(a1 != a2 && b.next != null)
	{
		a1 = b.next;
		ReadBlock(&b, a1);
	}
	
	if(a1 != a2)
	{
		return;																																								// Invalid pointer
	}
	
	b.free = true;																																					// deallocate it
	WriteBlock(&b, a1);

	JoinFreeAdjacentBlocks();
}


unsigned long DRAM_GetFreeHeapSpace()
{
block b;
dram a = 0;
unsigned long space = 0;

	do
	{
		ReadBlock(&b, a);

		if(b.free)
		{
			space += b.size;
		}
		
		a = b.next;
	} while(b.next != null);
	
	return space;
}

/*
unsigned long space = DRAM_SIZE - sizeof(block);
block b;
dram a = 0;

	ReadFirstBlock(&b);
	
	while(b.next != null)
	{
		if(b.free == true)
		{
			space -= (b.size + sizeof(block));
		}
		a = b.next;
		ReadBlock(&b, a);
	}
	
	return space;
}
*/



unsigned char DRAM_ReadByte(unsigned long address)
{
unsigned char data;

	Critical();
	
	ADR_PORT = address;
	CTRL_PORT = ((address >> 8) & 0x03) | 0xe0;
	__no_operation();
	RAS_LOW;
	ADR_PORT = (address >> 10);
	CTRL_PORT = ((address >> 18) & 0x03) | 0xc0;
	__no_operation();
	CAS_LOW;
	
	__no_operation();
	data = DATA_IN;
	
	CTRL_PORT = 0xe0;
	
	NonCritical();
	
	return data;
}

void DRAM_WriteByte(unsigned long address, unsigned char data)
{
	Critical();
	
	DIR_OUT;
	DATA_OUT = data;
	
	WE_LOW;
	
	ADR_PORT = address;
	CTRL_PORT = ((address >> 8) & 0x03) | 0xa0;
	__no_operation();
	RAS_LOW;
	ADR_PORT = (address >> 10);
	CTRL_PORT = ((address >> 18) & 0x03) | 0x80;
	__no_operation();
	CAS_LOW;
	
	__no_operation();
	
	CTRL_PORT = 0xe0;
	
	DIR_IN;
	DATA_OUT = 0x00;
	
	NonCritical();
}

unsigned long DRAM_ReadLong(unsigned long address)
{
unsigned long value;

	DRAM_ReadBytes((unsigned char*)&value, address, 4);
	
	return value;
}

void DRAM_WriteLong(unsigned long address, unsigned long value)
{
	DRAM_ReadBytes((unsigned char*)&value, address, 4);
}

void DRAM_ReadBytes(unsigned char* data, unsigned long address, unsigned long length)
{
	while(length--)
	{
		*data++ = DRAM_ReadByte(address++);
	}
}

void DRAM_WriteBytes(unsigned char* data, unsigned long address, unsigned long length)
{
	while(length--)
	{
		DRAM_WriteByte(address++, *data++);
	}
}

void ReadBlock(block* b, unsigned long address)
{
unsigned char i;
unsigned char* pb = (unsigned char*)b;

	for(i = 0; i < sizeof(block); i++)
	{
		*pb++ = DRAM_ReadByte(address++);
	}
}

void ReadFirstBlock(block* b)
{
unsigned char i;
unsigned char* pb = (unsigned char*)b;
unsigned long address = 0;

	for(i = 0; i < sizeof(block); i++)
	{
		*pb++ = DRAM_ReadByte(address++);
	}
}

void WriteBlock(block* b, unsigned long address)
{
unsigned char i;
unsigned char* pb = (unsigned char*)b;

	for(i = 0; i < sizeof(block); i++)
	{
		DRAM_WriteByte(address++, *pb++);
	}
}

static void JoinFreeAdjacentBlocks()
{
block b, next;
dram a = 0;

	ReadFirstBlock(&b);

	while(b.next != null)
	{
		ReadBlock(&next, b.next);
		if(b.free == true && next.free == true)
		{
			b.size += next.size + sizeof(block);
			b.next = next.next;
			WriteBlock(&b, a);
		}
		else
		{
			a = b.next;
			ReadBlock(&b, a);
		}
	}
}



void DRAM_PrintBlockList()
{
block b;
dram a = 0;

	do
	{
		ReadBlock(&b, a);

		UART_WriteByte(b.free+'0');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(b.size,7,' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(b.next,7,' ');
		UART_WriteString_P("\n");
		
		a = b.next;
	} while(b.next != null);
}
