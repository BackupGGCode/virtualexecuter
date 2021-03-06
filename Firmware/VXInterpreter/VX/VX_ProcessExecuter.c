#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <FileStore/FileStore.h>
#include "VX_ProcessManagement.h"
#include "VX.h"
#include "VX_SoftPeripherals.h"
#include <Peripherals/UART.h>


#define DISABLE_PROCESS_TIMER										0xffffffff


unsigned long processTimer = DISABLE_PROCESS_TIMER;


//--force_switch_type 1 => jump table

bool VX_ExecuteInstruction();

#define GetAddress(pValue) 	do{DRAM_ReadBytes(p.codeStart + p.ip, (unsigned char*)pValue, 4);	p.ip += 4;}while(0)

//bool GetAddress(unsigned long* pValue);
bool GetSingle(unsigned char* pValue);
bool GetDouble(unsigned short* pValue);
bool GetQuad(unsigned long* pValue);
bool GetFloat(float* pValue);
bool GetGlobalSingle(unsigned long address, unsigned char* pValue);
bool GetGlobalDouble(unsigned long address, unsigned short* pValue);
bool GetGlobalQuad(unsigned long address, unsigned long* pValue);
bool GetGlobalFloat(unsigned long address, float* pValue);
bool PushSingle(unsigned char value);
bool PopSingle(unsigned char* pValue);
bool PushDouble(unsigned short value);
bool PopDouble(unsigned short* pValue);
bool PushQuad(unsigned long value);
bool PopQuad(unsigned long* pValue);
bool PushFloat(float value);
bool PopFloat(float* pValue);


static process p;


// Give all started processes their ticks
void VX_ProcessExecuter_Tick()
{
dram currentProcess = processList;	
unsigned char processTicks;
bool modified;

	while(currentProcess != null)
	{
		ReadProcess(currentProcess, &p);

		modified = false;
		processTicks = MAX_TICKS_PER_PROCESS;
		
		while(processTicks > 0)
		{				
			if(p.state == Run)
			{
				if(VX_ExecuteInstruction())
				{
					p.ticks++;
				}
				else
				{
					p.state = Crash;
				}
				modified = true;
				processTicks--;
			}
			else if(p.state == Step)
			{
				if(VX_ExecuteInstruction())
				{
					p.ticks++;
					p.state = Stop;
				}
				else
				{
					p.state = Crash;
				}
				modified = true;
				processTicks--;
			}
			else
			{
				processTicks = 0;
			}
		}

		if(modified)
		{
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
		
		case   0:	// nop - 0x00
							break;

// Arithmetic

		case   1:	// adds - 0x01
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 += uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   2:	// addd - 0x02
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 += us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   3:	// addq - 0x03
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 += ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   4:	// addf - 0x04
							if(PopFloat(&f2) == false || PopFloat(&f1) == false)
							{
								return false;
							}
							f1 += f2;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;

		case   5:	// subs - 0x05
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 -= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   6:	// subd - 0x06
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 -= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   7:	// subq - 0x07
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 -= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   8:	// subf - 0x08
							if(PopFloat(&f2) == false || PopFloat(&f1) == false)
							{
								return false;
							}
							f1 -= f2;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
		case   9:	// muls - 0x09
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 *= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   10:	// muld - 0x0a
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 *= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   11:	// mulq - 0x0b
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 *= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   12:	// mulf - 0x0c
							if(PopFloat(&f2) == false || PopFloat(&f1) == false)
							{
								return false;
							}
							f1 *= f2;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
		case   13:	// divs - 0x0d
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 /= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   14:	// divd - 0x0e
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 /= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   15:	// divq - 0x0f
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 /= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   16:	// divf - 0x10
							if(PopFloat(&f2) == false || PopFloat(&f1) == false)
							{
								return false;
							}
							f1 /= f2;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
		case   17:	// incs - 0x11
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 += 1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   18:	// incd - 0x12
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 += 1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   19:	// incq - 0x13
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 += 1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   20:	// incf - 0x014
							if(PopFloat(&f1) == false)
							{
								return false;
							}
							f1 += 1.0;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
		case   21:	// decs - 0x15
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 -= 1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   22:	// decd - 0x16
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 -= 1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   23:	// decq - 0x17
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 -= 1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   24:	// decf - 0x18
							if(PopFloat(&f1) == false)
							{
								return false;
							}
							f1 -= 1.0;
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
// Logical

		case   25:	// ands - 0x19
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 &= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   26:	// andd - 0x1a
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 &= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   27:	// andq - 0x1b
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 &= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   28:	// ors - 0x1c
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 |= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   29:	// ord - 0x1d
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 |= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   30:	// orq - 0x1e
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 |= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   31:	// xors - 0x1f
							if(PopSingle(&uc2) == false || PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 ^= uc2;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   32:	// xord - 0x20
							if(PopDouble(&us2) == false || PopDouble(&us1) == false)
							{
								return false;
							}
							us1 ^= us2;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   33:	// xorq - 0x21
							if(PopQuad(&ul2) == false || PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 ^= ul2;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   34:	// coms - 0x22
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 = ~uc1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   35:	// comd - 0x23
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 = ~us1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   36:	// comq - 0x24
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 = ~ul1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   37:	// negs - 0x25
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 = 0 - uc1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   38:	// negd - 0x26
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 = 0 - us1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   39:	// negq - 0x27
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 = 0 - ul1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   40:	// shfls - 0x28
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 <<= 1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   41:	// shfld - 0x29
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 <<= 1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   42:	// shflq - 0x2a
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 <<= 1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
		case   43:	// shfrs - 0x2b
							if(PopSingle(&uc1) == false)
							{
								return false;
							}
							uc1 >>= 1;
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case   44:	// shfrd - 0x2c
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							us1 >>= 1;
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;
							
		case   45:	// shfrq - 0x2d
							if(PopQuad(&ul1) == false)
							{
								return false;
							}
							ul1 >>= 1;
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;
							
// Transfer
							
		case  46:	// loads - 0x2e
							if(GetSingle(&uc1) == false)
							{
								return false;
							}
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case  47:	// loadd - 0x2f
							if(GetDouble(&us1) == false)
							{
								return false;
							}
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;

		case  48:	// loadq - 0x30
							if(GetQuad(&ul1) == false)
							{
								return false;
							}
							if(PushQuad(ul1) == false)
							{
								return false;
							}
							break;

		case  49:	// loadf - 0x31
							if(GetFloat(&f1) == false)
							{
								return false;
							}
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;
							
/*
			PUSH LOCALS
*/							
							
		case  54:	// pushs - 0x36
							GetAddress(&ul1);
							
							uc1 = DRAM_ReadByte(p.dataStart + ul1);
	
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;
							
		case  55:	// pushd - 0x37
							GetAddress(&ul1);
							
							DRAM_ReadBytes(p.dataStart + ul1, (unsigned char*)&us1, 2);
	
							if(PushDouble(us1) == false)
							{
								return false;
							}
							break;

		case  56:	// pushq - 0x38
							GetAddress(&ul1);
							
							DRAM_ReadBytes(p.dataStart + ul1, (unsigned char*)&ul2, 4);
	
							if(PushQuad(ul2) == false)
							{
								return false;
							}
							break;

		case  57:	// pushf - 0x39
							GetAddress(&ul1);
							
							DRAM_ReadBytes(p.dataStart + ul1, (unsigned char*)&f1, 4);
	
							if(PushFloat(f1) == false)
							{
								return false;
							}
							break;

/*
		POP LOCAL
*/

		case  62:	// pops - 0x3e
							GetAddress(&ul1);
							
							if(PopSingle(&uc1) == false)
							{
								return false;
							}

							DRAM_WriteByte(p.dataStart + ul1, uc1);
							
							break;
							
		case  63:	// popd - 0x3f
							GetAddress(&ul1);
							
							if(PopDouble(&us1) == false)
							{
								return false;
							}

							DRAM_WriteBytes(p.dataStart + ul1, (unsigned char*)&us1, 2);
							break;
							
		case  64:	// popq - 0x40
							GetAddress(&ul1);
							
							if(PopQuad(&ul2) == false)
							{
								return false;
							}

							DRAM_WriteBytes(p.dataStart + ul1, (unsigned char*)&ul2, 4);
							break;
							
		case  65:	// popf - 0x41
							GetAddress(&ul1);
							
							if(PopFloat(&f1) == false)
							{
								return false;
							}

							DRAM_WriteBytes(p.dataStart + ul1, (unsigned char*)&f1, 4);
							break;

// Branches

		case  66:	// jmp - 0x42
							GetAddress(&ul1);
							p.ip = ul1;
							break;
							
		case  67:	// jmpz - 0x43
							if((p.flags & FLAG_ZERO) == FLAG_ZERO)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

		case  68:	// jmpnz - 0x44
							if((p.flags & FLAG_ZERO) != FLAG_ZERO)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

		case  69:	// jmpn - 0x45
							if((p.flags & FLAG_NEGATIVE) == FLAG_NEGATIVE)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

		case  70:	// jmpnn - 0x46
							if((p.flags & FLAG_NEGATIVE) != FLAG_NEGATIVE)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

		case  71:	// jmpp - 0x47
							if((p.flags & FLAG_POSITIVE) == FLAG_POSITIVE)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

		case  72:	// jmpnp - 0x48
							if((p.flags & FLAG_POSITIVE) != FLAG_POSITIVE)
							{
								GetAddress(&ul1);
								p.ip = ul1;
							}
							else
							{
								p.ip += 4;
							}
							break;

// Methods

		case  73:	// call - 0x49
							GetAddress(&ul1);
							if(PushQuad(p.ip) == false)
							{
								return false;
							}
							p.ip = ul1;
							break;

		case  74:	// return - 0x4a
							if(PopQuad(&ul1) == false)
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
							if(PushSingle(uc1) == false)
							{
								return false;
							}
							break;

		case  76:	// output - 0x4c
							if(PopSingle(&uc1) == false || PopSingle(&uc2) == false)
							{
								return false;
							}
							VX_SoftPeripherals_Write(uc1, uc2);
							break;
							
// Misc
							
		case  77:	// wait - 0x4d
							if(PopDouble(&us1) == false)
							{
								return false;
							}
							// wait us1 milliseconds
							break;
							
		case  78:	// exit - 0x4e
							p.state = Done;
							if(processTimer < DISABLE_PROCESS_TIMER)
							{
								UART_WriteString_P("Process finished in ");
								UART_WriteValueUnsigned(processTimer, 0, 0);
								UART_WriteString_P(" ms.\n");
								processTimer = DISABLE_PROCESS_TIMER;
							}
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

bool GetGlobalSingle(unsigned long address, unsigned char* pValue)
{
	if(address >= p.dataSize)
	{
		return false;
	}
	
	*pValue = DRAM_ReadByte(p.dataStart + address);
	
	return true;
}

bool GetGlobalDouble(unsigned long address, unsigned short* pValue)
{
	if((address + 1) >= p.dataSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.dataStart + address, (unsigned char*)pValue, 2);
	
	return true;
}

bool GetGlobalQuad(unsigned long address, unsigned long* pValue)
{
	if((address + 3) >= p.dataSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.dataStart + address, (unsigned char*)pValue, 4);
	
	return true;
}

bool GetGlobalFloat(unsigned long address, float* pValue)
{
	if((address + 3) >= p.dataSize)
	{
		return false;
	}
	
	DRAM_ReadBytes(p.dataStart + address, (unsigned char*)pValue, 4);
	
	return true;
}

bool PushSingle(unsigned char value)
{
	if((p.sp + 1) > p.stackSize)
	{
		return false;
	}
	
	DRAM_WriteByte(p.stackStart + p.sp, value);

	p.sp += 1;
	
	if(value == 0)
	{
		p.flags |= FLAG_ZERO;
	}
	else
	{
		p.flags &= ~FLAG_ZERO;
	}
	
	if(value & 0x80)
	{
		p.flags |= FLAG_NEGATIVE;
	}
	else
	{
		p.flags &= ~FLAG_NEGATIVE;
	}
	
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

bool PushDouble(unsigned short value)
{
	if((p.sp + 2) > p.stackSize)
	{
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 2);

	p.sp += 2;
	
	if(value == 0)
	{
		p.flags |= FLAG_ZERO;
	}
	else
	{
		p.flags &= ~FLAG_ZERO;
	}
	
	if(value & 0x8000)
	{
		p.flags |= FLAG_NEGATIVE;
	}
	else
	{
		p.flags &= ~FLAG_NEGATIVE;
	}
	
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

bool PushQuad(unsigned long value)
{
	if((p.sp + 4) > p.stackSize)
	{
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);

	p.sp += 4;
	
	if(value == 0)
	{
		p.flags |= FLAG_ZERO;
	}
	else
	{
		p.flags &= ~FLAG_ZERO;
	}
	
	if(value & 0x80000000)
	{
		p.flags |= FLAG_NEGATIVE;
	}
	else
	{
		p.flags &= ~FLAG_NEGATIVE;
	}
	
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

bool PushFloat(float value)
{
	if((p.sp + 4) > p.stackSize)
	{
		return false;
	}
	
	DRAM_WriteBytes(p.stackStart + p.sp, (unsigned char*)&value, 4);

	p.sp += 4;
	
	if(value == 0.0)
	{
		p.flags |= FLAG_ZERO;
	}
	else
	{
		p.flags &= ~FLAG_ZERO;
	}
	
	if(value < 0.0)
	{
		p.flags |= FLAG_NEGATIVE;
	}
	else
	{
		p.flags &= ~FLAG_NEGATIVE;
	}
	
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
