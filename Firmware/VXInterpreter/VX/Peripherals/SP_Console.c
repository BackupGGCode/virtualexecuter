#include "VX/VX_SoftPeripherals.h"
#include "Peripherals/UART.h"


unsigned char SP_Console_Read()
{
unsigned char value = 0;

	if(UART_BytesReady())
	{
		value = UART_ReadByte();
	}

  return value;
}

void SP_Console_Write(unsigned char value)
{
	UART_WriteByte(value);
}
