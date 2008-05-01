#include <Globals.h>
#include <Kernel/Kernel.h>


unsigned char InternalEEPROM_ReadByte(unsigned long address)
{
	while(EECR & (1<<EEWE));
	EEAR = address;
	EECR |= (1 << EERE);
	return EEDR;
}


void InternalEEPROM_WriteByte(unsigned long address, unsigned char value)
{
	Critical();
	while(EECR & (1<<EEWE));
	EEAR = address;
	EEDR = value;
	EECR |= (1 << EEMWE);
	EECR |= (1 << EEWE);
	NonCritical();
}


void InternalEEPROM_ReadBytes(unsigned long address, unsigned char* data, unsigned short length)
{
	while(length--)
	{
		*data++ = InternalEEPROM_ReadByte(address++);
	}
}


unsigned long InternalEEPROM_ReadLong(unsigned long address)
{
unsigned long t;
	
	InternalEEPROM_ReadBytes(address, (unsigned char*)&t, 4);
	
	return t;
}


void InternalEEPROM_WriteBytes(unsigned long address, unsigned char* data, unsigned short length)
{
	while(length--)
	{
		InternalEEPROM_WriteByte(address++, *data++);
	}
}
