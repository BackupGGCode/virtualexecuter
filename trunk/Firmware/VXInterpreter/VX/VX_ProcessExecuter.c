#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <Peripherals/UART.h>
#include <FileStore/FileStore.h>
#include "VX_ProcessManagement.h"
#include "VX.h"


//--force_switch_type 1 => jump table

bool VX_ExecuteInstruction();

bool GetCodeByte(unsigned char* pValue);
bool PushSingle(unsigned char* pValue);
bool PopSingle(unsigned char* pValue);
bool PushDouble(unsigned short* pValue);
bool PopDouble(unsigned short* pValue);
bool PushQuad(unsigned long* pValue);
bool PopQuad(unsigned long* pValue);
bool PushFloat(float* pValue);
bool PopFloat(float* pValue);


process p;


// Give all started processes a tick
void VX_ProcessExecuter_Tick()
{
dram currentProcess = processList;	

	if(currentProcess != null)
	{
		ReadProcess(currentProcess, &p);
		if(p.state == Run || p.state == Step)
		{
			if(VX_ExecuteInstruction() == false)
			{
				p.state = Crash;
			}
			else
			{
				p.ticks++;
				if(p.state == Step)
				{
					p.state = Stop;
				}
			}
			WriteProcess(p.id, &p);
//			UpdateProcess(p);
		}
		currentProcess = p.next;
	}
}


bool VX_ExecuteInstruction()
{
unsigned char instruction;
unsigned char uc1, uc2;
unsigned short us1, us2;
unsigned long ul1, ul2;
float f1, f2;

	if(GetCodeByte(&instruction) == false)
	{
		return false;
	}

	switch(instruction)
	{

// Arithmetic
		
		case   1:	// adds
							if(PopSingle(&uc1) == false || PopSingle(&uc2) == false)
							{
								return false;
							}
							uc1 += uc2;
							if(PushSingle(&uc1) == false)
							{
								return false;
							}
							break;
							
// Transfer
							
		case  2:	// loads
							if(GetCodeByte(&uc1) == false)
							{
								return false;
							}
							if(PushSingle(&uc1) == false)
							{
								return false;
							}
							break;

// Unknown instructions

		default:	// Unknown instruction => crash
							return false;
	}
	
	return true;
}


bool GetCodeByte(unsigned char* pValue)
{
	UART_WriteString_P("GetCodeByte");
	
	if(p.ip >= p.codeSize)
	{
		p.state = Crash;
		return false;
	}
	
	*pValue = DRAM_ReadByte(p.codeStart + p.ip);

	p.ip += 1;
	
	UART_WriteString_P("*\n");
	
	return true;
}

bool PushSingle(unsigned char* pValue)
{
	UART_WriteString_P("PushSingle");

	if((p.sp + 1) > p.codeSize)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_WriteByte(p.stackStart + p.sp, *pValue);

	p.sp += 1;

	UART_WriteString_P("*\n");
	
	return true;
}

bool PopSingle(unsigned char* pValue)
{
	UART_WriteString_P("PopSingle");
	
	if(p.sp < 1)
	{
		p.state = Crash;
		return false;
	}
	
	*pValue = DRAM_ReadByte(p.stackStart + p.sp);

	p.sp -= 1;
	
	UART_WriteString_P("*\n");
	
	return true;
}

bool PushDouble(unsigned short* pValue)
{
	if((p.sp + 2) > p.codeSize)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)pValue, 2);

	p.sp += 2;
	
	return true;
}

bool PopDouble(unsigned short* pValue)
{
	if(p.sp < 2)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 2);

	p.sp -= 2;
	
	return true;
}

bool PushQuad(unsigned long* pValue)
{
	if((p.sp + 4) > p.codeSize)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	p.sp += 4;
	
	return true;
}

bool PopQuad(unsigned long* pValue)
{
	if(p.sp < 4)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	p.sp -= 4;
	
	return true;
}

bool PushFloat(float* pValue)
{
	if((p.sp + 4) > p.codeSize)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	p.sp += 4;
	
	return true;
}

bool PopFloat(float* pValue)
{
	if(p.sp < 4)
	{
		p.state = Crash;
		return false;
	}
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	p.sp -= 4;
	
	return true;
}
