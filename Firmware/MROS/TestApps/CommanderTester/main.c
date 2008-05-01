#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <FileStore/FileStore.h>
#include <FileStore/InternalEEPROM.h>
#include <Terminal/Terminal.h>

void blink()
{
	PORTB^=(1<<7);
	Kernel_Sleep(500);
}

void main()
{
	DDRB=0xff;
	PORTB=0xff;
	
	Kernel_Init();

	UART_Init(__BAUDRATE__(115200));
	FileStore_Init(InternalEEPROM_ReadByte, InternalEEPROM_ReadBytes, InternalEEPROM_ReadLong);
	Terminal_Init();	
	Kernel_CreateTask(blink);
	
	Kernel_Run();
}
