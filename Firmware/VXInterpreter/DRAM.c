#include <Globals.h>
#include <Config.h>
#include "DRAM.h"
#include <Kernel/Kernel.h>
#include <Kernel/KernelInternals.h>

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
	unsigned short size;
	unsigned long next;
} block;


static void JoinFreeAdjacentBlocks();


static unsigned long dramSize;


void DRAM_Init(unsigned long size)
{
	dramSize = size;

	/*
block* b = (block*)heap;


	b->free = true;
	b->size = dramSize - sizeof(block);
	b->next = null;
*/
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

/*
void* Kernel_Allocate(unsigned short size)
{
block* b = (block*)heap;
block* split;

	while((b->free != true || b->size < size) && b->next != null)
	{
		b = (block*)b->next;
	}
	
	if(b->free != true || b->size < size)																									// Found a valid block?
	{
		return null;																																					// No
	}
	
	if(b->size > (size + sizeof(block)))																									// Split block, return first part and mark last part as free
	{
		split = (block*)((unsigned char*)b + sizeof(block) + size);
		split->free = true;
		split->size = b->size - size - sizeof(block);
		split->next = b->next;
		
		b->free = false;
		b->size = size;
		b->next = split;
		
		return (void*)((unsigned short)b + sizeof(block));
	}
	else																																									// Block is too small to split - return all
	{
		b->free = false;
		return (void*)((unsigned short)b + sizeof(block));
	}
}


void Kernel_Deallocate(void* pointer)
{
block* b = (block*)heap;
block* p = (block*)((unsigned short)pointer - sizeof(block));
	
	while(b != p && b->next != null)
	{
		b = (block*)b->next;
	}
	
	if(b != p)
	{
		return;																																								// Invalid pointer
	}
	
	b->free = true;

	JoinFreeAdjacentBlocks();
}
*/

unsigned char DRAM_ReadByte(unsigned long address)
{
unsigned char data;

	Critical();
	
	ADR_PORT = address;
	CTRL_PORT = ((address >> 8) & 0x0f) | 0xe0;
	__no_operation();
	RAS_LOW;
	ADR_PORT = (address >> 12);
	CTRL_PORT = ((address >> 20) & 0x0f) | 0xc0;
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
	CTRL_PORT = ((address >> 8) & 0x0f) | 0xa0;
	__no_operation();
	RAS_LOW;
	ADR_PORT = (address >> 12);
	CTRL_PORT = ((address >> 20) & 0x0f) | 0x80;
	__no_operation();
	CAS_LOW;
	
	__no_operation();
	
	CTRL_PORT = 0xe0;
	
	DIR_IN;
	DATA_OUT = 0x00;
	
	NonCritical();
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

/*
static void JoinFreeAdjacentBlocks()
{
block* b = (block*)heap;
block* next;

	while(b->next != null)
	{
		next = (block*)b->next;
		if(b->free == true && next->free == true)
		{
			b->size += next->size + sizeof(block);
			b->next = next->next;
		}
		else
		{
			b = (block*)b->next;
		}
	}
}
*/
