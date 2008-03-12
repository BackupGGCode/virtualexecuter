#include <Globals.h>
#include <Config.h>
#include "DRAM.h"
#include "Kernel.h"
#include "KernelInternals.h"


typedef struct
{
	bool free;
	unsigned short size;
	void* next;
} block;


static unsigned char heap[HEAP_SIZE];


static void JoinFreeAdjacentBlocks();


void Kernel_InitHeap()
{
block* b = (block*)heap;

	b->free = true;
	b->size = HEAP_SIZE - sizeof(block);
	b->next = null;
}


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


void DRAM_Read(void* data, unsigned long address, unsigned long length)
{
	
}

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
