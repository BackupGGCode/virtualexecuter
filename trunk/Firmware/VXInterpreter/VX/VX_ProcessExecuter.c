#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <FileStore/FileStore.h>
#include "VX.h"


//--force_switch_type 1 => jump table


process p;

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
unsigned long stackFramePointer;
bool zero;
bool negative;


void VX_Executer()
{
	
}


void VX_ExecuteInstruction()
{
	switch(GetNextByte())
	{
		case   1: // adds
							PushSingle(PopSingle() + PopSingle());
							break;
		case   2: // addd
							PushDouble(PopDouble() + PopDouble());
							break;
		case   3: // addq
							PushQuad(PopQuad() + PopQuad());
							break;
		case   4: // addf
							PushFloat(PopFloat() + PopFloat());
							break;
		case   5: // subs
							PushSingle(PopSingle() - PopSingle());
							break;
		case   6: // subd
							PushDouble(PopDouble() - PopDouble());
							break;
		case   7: // subq
							PushQuad(PopQuad() - PopQuad());
							break;
		case   8: // subf
							PushFloat(PopFloat() - PopFloat());
							break;
		case   9: // muls
							PushDouble(PopSingle() * PopSingle());
							break;
		case  10: // muld
							PushQuad(PopDouble() * PopDouble());
							break;
		case  11: // mulq
							
							break;
		case  12: // mulf
							PushFloat(PopFloat() * PopFloat());
							break;
		case  13: // divs
							
							break;
		case  14: // divd
							
							break;
		case  15: // divq
							
							break;
		case  16: // divf
							PushFloat(PopFloat() / PopFloat());
							break;
		case  17: // incs
							PushSingle(PopSingle() + 1);
							break;
		case  18: // incd
							PushDouble(PopDouble() + 1);
							break;
		case  19: // incq
							PushQuad(PopQuad() + 1);
							break;
		case  20: // incf
							PushFloat(PopFloat() + 1);
							break;
		case  21: // decs
							PushSingle(PopSingle() - 1);
							break;
		case  22: // decd
							PushDouble(PopDouble() - 1);
							break;
		case  23: // decq
							PushQuad(PopQuad() - 1);
							break;
		case  24: // decf
							PushFloat(PopFloat() - 1);
							break;
							
							
		case  25: // adds
							PushSingle(PopSingle() + PopSingle());
							break;
		case   2: // addd
							PushDouble(PopDouble() + PopDouble());
							break;
		case   3: // addq
							PushQuad(PopQuad() + PopQuad());
							break;
		case   4: // addf
							PushFloat(PopFloat() + PopFloat());
							break;
	}
}


unsigned char GetNextByte()
{
unsigned char value = DRAM_ReadByte(p.codeStart + p.ip);
	p.ip++;
	return value;
}

void PushSingle(unsigned char value)
{
	DRAM_WriteByte(p.stackStart + p.sp, value);
	p.sp++;
}

unsigned char PopSingle()
{
unsigned char value = DRAM_ReadByte(p.stackStart + p.sp);
	p.sp--;
	return value;
}

void PushDouble(unsigned short value)
{
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 2);
	p.sp+=2;
}

unsigned short PopDouble()
{
unsigned short value;

	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)&value, 2);
	p.sp-=2;
	return value;
}

void PushQuad(unsigned long value)
{
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);
	p.sp+=4;
}

unsigned long PopQuad()
{
unsigned long value;

	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);
	p.sp-=4;
	return value;
}

void PushFloat(float value)
{
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);
	p.sp+=4;
}

float PopFloat()
{
float value;

	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);
	p.sp-=4;
	return value;
}
