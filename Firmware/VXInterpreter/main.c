#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>
#include <Terminal.h>
#include "DRAM.h"


void main()
{
	DDRA=0xff;
	PORTA=0x00;
	DDRB=0xff;
	PORTB=0xff;
	DDRC=0xef;
	PORTC=0xf0;
	DDRD=0x00;
	PORTD=0x00;
	DDRE=0x02;
	PORTE=0xff;
	DDRF=0x00;
	PORTF=0xff;
	DDRG=0x08;
	PORTG=0x17;
	
	LED_YELLOW_OFF
	LED_RED_OFF
		
	Kernel_Init();
	
	UART_Init(__BAUDRATE__(230400));
	FileStore_Init(InternalEEPROM_ReadByte, InternalEEPROM_ReadBytes, InternalEEPROM_ReadLong);
	Terminal_Init();	
	
	LED_GREEN_ON
		
	Kernel_SystemTimerHook(DRAM_Refresh);
	DRAM_Init();
	
	Kernel_Run();
}
