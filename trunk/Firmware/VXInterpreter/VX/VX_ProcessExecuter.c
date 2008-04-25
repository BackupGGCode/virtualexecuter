#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <Peripherals/UART.h>
#include <FileStore/FileStore.h>
#include "VX_ProcessManagement.h"
#include "VX.h"
#include "VX_SoftPeripherals.h"


//--force_switch_type 1 => jump table

bool VX_ExecuteInstruction();

bool GetSingle(unsigned char* pValue);
bool GetDouble(unsigned short* pValue);
bool GetQuad(unsigned long* pValue);
bool GetFloat(float* pValue);
bool PushSingle(unsigned char* pValue);
bool PopSingle(unsigned char* pValue);
bool PushDouble(unsigned short* pValue);
bool PopDouble(unsigned short* pValue);
bool PushQuad(unsigned long* pValue);
bool PopQuad(unsigned long* pValue);
bool PushFloat(float* pValue);
bool PopFloat(float* pValue);

void PrintStack();

process p;


// Give all started processes a tick
void VX_ProcessExecuter_Tick()
{
dram currentProcess = processList;	

	while(currentProcess != null)
	{
		ReadProcess(currentProcess, &p);
		
		if(p.state == Run || p.state == Step)
		{
//			PrintStack();
			if(VX_ExecuteInstruction())
			{
				p.ticks++;
				if(p.state == Step)
				{
					p.state = Stop;
				}
			}
			else
			{
				p.state = Crash;
			}
//			WriteProcess(currentProcess, &p);
			UpdateProcess(p);
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

	if(GetSingle(&instruction) == false)
	{
		return false;
	}

	switch(instruction)
	{

// No operation
		
		case   0:	// nop
							break;

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
							
		case   2:	// addd
							if(PopDouble(&us1) == false || PopDouble(&us2) == false)
							{
								return false;
							}
							us1 += us2;
							if(PushDouble(&us1) == false)
							{
								return false;
							}
							break;
							
		case   3:	// addq
							if(PopQuad(&ul1) == false || PopQuad(&ul2) == false)
							{
								return false;
							}
							ul1 += ul2;
							if(PushQuad(&ul1) == false)
							{
								return false;
							}
							break;
							
		case   4:	// addf
							if(PopFloat(&f1) == false || PopFloat(&f2) == false)
							{
								return false;
							}
							f1 += f2;
							if(PushFloat(&f1) == false)
							{
								return false;
							}
							break;
							
// Transfer
							
		case  51:	// loads - 0x33
							if(GetSingle(&uc1) == false)
							{
								return false;
							}
							if(PushSingle(&uc1) == false)
							{
								return false;
							}
							break;
							
		case  52:	// loadd - 0x34
							if(GetDouble(&us1) == false)
							{
								return false;
							}
							if(PushDouble(&us1) == false)
							{
								return false;
							}
							break;

		case  53:	// loadq - 0x35
							if(GetQuad(&ul1) == false)
							{
								return false;
							}
							if(PushQuad(&ul1) == false)
							{
								return false;
							}
							break;

		case  54:	// loadf - 0x36
							if(GetFloat(&f1) == false)
							{
								return false;
							}
							if(PushFloat(&f1) == false)
							{
								return false;
							}
							break;

// Branches

		case  71:	// jmp - 0x47
							if(GetQuad(&ul1) == false)
							{
								return false;
							}
							if(ul1 >= p.codeSize)
							{
								return false;
							}
							p.ip = ul1;
							break;

// IO

		case  75:	// input - 0x4b
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
              uc1 = VX_SoftPeripherals_Read(uc1);
              PushSingle(&uc1);
							break;

		case  76:	// output - 0x4c
							if(PopSingle(&uc1) == false || PopSingle(&uc2) == false)
							{
								return false;
							}
              VX_SoftPeripherals_Write(uc1, uc2);
							break;
              
// Unknown instructions

		default:	// Unknown instruction => crash
							return false;
	}
	
	return true;
}


bool GetSingle(unsigned char* pValue)
{
	if(p.ip >= p.codeSize)
	{
		return false;
	}
	
	*pValue = DRAM_ReadByte(p.codeStart + p.ip);

	p.ip += 1;
	
	return true;
}

bool GetDouble(unsigned short* pValue)
{
	if((p.ip + 1) >= p.codeSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.codeStart + p.ip, (unsigned char*)pValue, 2);

	p.ip += 2;
	
	return true;
}

bool GetQuad(unsigned long* pValue)
{
	if((p.ip + 3) >= p.codeSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.codeStart + p.ip, (unsigned char*)pValue, 4);

	p.ip += 4;
	
	return true;
}

bool GetFloat(float* pValue)
{
	if((p.ip + 3) >= p.codeSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.codeStart + p.ip, (unsigned char*)pValue, 4);

	p.ip += 4;
	
	return true;
}

bool PushSingle(unsigned char* pValue)
{
	if((p.sp + 1) > p.stackSize)
	{
		return false;
	}
	
	DRAM_WriteByte(p.stackStart + p.sp, *pValue);

	p.sp += 1;
	
	return true;
}

bool PopSingle(unsigned char* pValue)
{
	if(p.sp < 1)
	{
		return false;
	}
	
	p.sp -= 1;
	
	*pValue = DRAM_ReadByte(p.stackStart + p.sp);

	return true;
}

bool PushDouble(unsigned short* pValue)
{
	if((p.sp + 2) > p.stackSize)
	{
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
		return false;
	}
	
	p.sp -= 2;
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 2);

	return true;
}

bool PushQuad(unsigned long* pValue)
{
	if((p.sp + 4) > p.stackSize)
	{
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
		return false;
	}
	
	p.sp -= 4;
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	return true;
}

bool PushFloat(float* pValue)
{
	if((p.sp + 4) > p.stackSize)
	{
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
		return false;
	}
	
	p.sp -= 4;
	
	DRAM_ReadBytes(p.stackStart + p.sp, (unsigned char*)pValue, 4);

	return true;
}


void PrintStack()
{
unsigned char i, t;

	for(i=0; i<p.stackSize; i++)
	{
		t = DRAM_ReadByte(p.stackStart + i);
		UART_WriteValueUnsigned(t, 3, '0');
		UART_WriteByte(' ');
	}
	UART_WriteString_P("\n");
}
