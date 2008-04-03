#include <Globals.h>
#include <Config.h>
#include "DRAM.h"
#include <Kernel/Kernel.h>
#include <Kernel/KernelInternals.h>
#include <Peripherals/UART.h>


#define DRAM_PRESCALER													14
#define NUMBER_OF_ROWS													1024
#define CAS_RAS_PER_CYCLE												8
#define REFRESH_CYCLES													(NUMBER_OF_ROWS / CAS_RAS_PER_CYCLE)

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


static void ReadBlock(block* b, dram address);
static void ReadFirstBlock(block* b);
static void WriteBlock(block* b, dram address);
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
unsigned char i;

	if(prescaler == 0)
	{
		for(i = 0; i < REFRESH_CYCLES; i++)
		{
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
			RAS_HIGH;
			CAS_LOW;
			RAS_LOW;
			CAS_HIGH;
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


/*
	This assembles to 60 bytes of code executing in 30 clocks
*/
unsigned char DRAM_ReadByte(dram address)
{
unsigned char data;

	Critical();
	
	ADR_PORT = address;
	address >>= 8;
	CTRL_PORT = (address & 0x03) | 0xe0;
	__no_operation();
	RAS_LOW;
	address >>= 2;
	ADR_PORT = address;
	address >>= 8;
	CTRL_PORT = (address & 0x03) | 0xc0;
	__no_operation();
	CAS_LOW;
	
	__no_operation();
	data = DATA_IN;
	
	CTRL_PORT = 0xe0;
	
	NonCritical();
	
	return data;
}

/*
	This assembles to 70 bytes of code executing in 35 clocks
*/
void DRAM_WriteByte(dram address, unsigned char data)
{
	Critical();
	
	DIR_OUT;
	DATA_OUT = data;
	
	ADR_PORT = address;
	address >>= 8;
	CTRL_PORT = (address & 0x03) | 0xa0;
	__no_operation();
	RAS_LOW;
	address >>= 2;
	ADR_PORT = address;
	address >>= 8;
	CTRL_PORT = (address & 0x03) | 0x80;
	__no_operation();
	CAS_LOW;
	
	__no_operation();
	
	CTRL_PORT = 0xe0;
	
	DIR_IN;
	DATA_OUT = 0x00;
	
	NonCritical();
}

unsigned long DRAM_ReadLong(dram address)
{
unsigned long value;

	DRAM_ReadBytes(address, (unsigned char*)&value, 4);
	
	return value;
}

void DRAM_WriteLong(dram address, unsigned long value)
{
	DRAM_ReadBytes(address, (unsigned char*)&value, 4);
}

void DRAM_ReadBytes(dram address, unsigned char* data, unsigned long length)
{
	while(length--)
	{
		*data++ = DRAM_ReadByte(address++);
	}
}

void DRAM_WriteBytes(dram address, unsigned char* data, unsigned long length)
{
	while(length--)
	{
		DRAM_WriteByte(address++, *data++);
	}
}

void ReadBlock(block* b, dram address)
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

void WriteBlock(block* b, dram address)
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

	UART_WriteString_P("F Size    Next    This\n");
	
	do
	{
		ReadBlock(&b, a);

		UART_WriteByte(b.free+'0');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(b.size, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(b.next, 7, ' ');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(a + sizeof(block), 7, ' ');
		UART_WriteString_P("\n");
		
		a = b.next;
	} while(b.next != null);
}
