#include <Globals.h>
#include <Kernel/Kernel.h>
#include "DataFlash.h"


void DataFlash_Init()
{
	
}


unsigned char DataFlash_ReadByte(unsigned long address)
{

	return 0;	
}


void DataFlash_WriteByte(unsigned long address, unsigned char value)
{
	
}


void DataFlash_ReadPage(unsigned long address, unsigned char* buffer)
{
	
}


void DataFlash_WritePage(unsigned long address, unsigned char* buffer)
{
	
}


void DataFlash_ReadBytes(unsigned long address, unsigned char* data, unsigned short length)
{
	while(length--)
	{
		*data++ = DataFlash_ReadByte(address++);
	}
}


unsigned long DataFlash_ReadLong(unsigned long address)
{
unsigned long t;
	
	DataFlash_ReadBytes(address, (unsigned char*)&t, 4);
	
	return t;
}


void DataFlash_WriteBytes(unsigned long address, unsigned char* data, unsigned short length)
{
	while(length--)
	{
		DataFlash_WriteByte(address++, *data++);
	}
}
