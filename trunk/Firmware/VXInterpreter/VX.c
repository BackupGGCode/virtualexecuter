#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <FileStore/FileStore.h>
#include "VX.h"


//--force_switch_type 1


unsigned char GetNextByte();
void PushSingle(unsigned char value);
unsigned char PopSingle();
void PushDouble(unsigned short value);
unsigned short PopDouble();
void PushQuad(unsigned long value);
unsigned long PopQuad();
void PushFloat(float value);
float PopFloat();


unsigned char* heap;
unsigned long instructionPointer;
unsigned long stackPointer;
bool carry;
bool zero;
bool negative;

bool VX_Load(fsFile* file)
{
	if(file->size > VX_HEAP_SIZE)
	{
		return false;
	}
	
	heap = Kernel_Allocate(file->size);			// IKKE file->size!
	
	
	VX_Reset();
	
	return true;
}

void VX_Reset()
{
	instructionPointer = 0;
	stackPointer = 0;
	carry = false;
	zero = false;
	negative = false;
}

void VX_Unload()
{
	Kernel_DeleteTask(VX_Step);	
	Kernel_Deallocate(heap);
}

void VX_Start()
{
	Kernel_CreateTask(VX_Step);
}

void VX_Stop()
{
	Kernel_DeleteTask(VX_Step);	
}


void VX_Step()
{
unsigned char uc;
unsigned short us;
unsigned long ul, ul2, ul3, ul4;
float f;
	
	switch(GetNextByte())
	{
		case   1: // adds
							us = PopSingle();
							us += PopSingle();
							if(us > 0xff)
							{
								carry = true;
							}
							else
							{
								carry = false;
							}
							PushSingle(us & 0xff);
							break;
		case   2: // addd
							ul = PopDouble();
							ul += PopDouble();
							if(ul > 0xffff)
							{
								carry = true;
							}
							else
							{
								carry = false;
							}
							PushDouble(ul & 0xffff);
							break;
		case   3: // addq
							ul = PopQuad();
							ul2 = PopQuad();
							ul3 = (ul & 0xffff) + (ul2 & 0xffff);
							ul3 >>= 16;
							ul3 += (ul >> 16) + (ul2 >> 16);
							if(ul3 > 0x0000ffff)
							{
								carry = true;
							}
							else
							{
								carry = false;
							}
							PushQuad(ul + ul2);
							break;
		case   4: // addf
							f = PopFloat();
							f += PopFloat();
							PushFloat(f);
							break;
	}
}


unsigned char GetNextByte()
{
	return heap[instructionPointer++];
}

void PushSingle(unsigned char value)
{
	heap[stackPointer++] = value;
}

unsigned char PopSingle()
{
	return heap[stackPointer--];
}

void PushDouble(unsigned short value)
{
	heap[stackPointer++] = value;
}

unsigned short PopDouble()
{
	return heap[stackPointer--];
}

void PushQuad(unsigned long value)
{
	heap[stackPointer++] = value;
}

unsigned long PopQuad()
{
	return heap[stackPointer--];
}

void PushFloat(float value)
{
	heap[stackPointer++] = value;
}

float PopFloat()
{
	return heap[stackPointer--];
}
