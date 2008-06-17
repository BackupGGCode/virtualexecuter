#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>
#include <Terminal.h>
#include "DRAM.h"
#include "VX/VX.h"
#include "Commander/Commander.h"

extern unsigned char DRAM_ReadByte(dram address);
extern void DRAM_WriteByte(dram address, unsigned char value);
extern unsigned long DRAM_ReadLong(dram address);
extern void DRAM_WriteLong(dram address, unsigned long value);
extern void DRAM_ReadBytes(dram address, unsigned char* data, unsigned short length);
extern void DRAM_WriteBytes(dram address, unsigned char* data, unsigned short length);

/*
void ProcessTimerTask()
{
	if(processTimer < 0xffffffff)
	{
		processTimer++;
	}
	Kernel_Sleep(1);
}
*/

void main()
{
dram ramDiscBase;
unsigned long ramDiscSize = (unsigned long)RAM_DISC_SIZE;
unsigned short eepromDiscBase = EEPROM_DISC_OFFSET;
unsigned long eepromDiscSize = EEPROM_DISC_SIZE;

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
	PORTF=0xbf;
	DDRG=0x08;
	PORTG=0x17;
	
	LED_YELLOW_OFF
	LED_RED_OFF
	
	Kernel_Init();

//	UART_Init(__BAUDRATE__(115200));
//	UART_Init(__BAUDRATE__(230400));
	UART_Init(__BAUDRATE__(460800));
	
	UART_WriteString_P("\f\n---::: V I R T U A L   E X E C U T E R :::---\n\n");

	UART_WriteString_P("IO ports initialized.\n");

	UART_WriteString_P("MROS kernel initialized.\n");
	
	UART_WriteString_P("Com port initialized.\n");

	Kernel_SystemTimerHook(DRAM_Refresh);
	DRAM_Init();
	UART_WriteString_P("DRAM controller initialized.\n");
	
	UART_WriteString_P("Initializing storage system...");

#if defined(USE_EEPROM_FOR_DISC)
	UART_WriteString_P("\n  Medium: EEPROM. Trying to allocate ");
	UART_WriteValueUnsigned(eepromDiscSize, 0, 0);
	UART_WriteString_P(" bytes...");
	FileStore_Init(InternalEEPROM_ReadByte, InternalEEPROM_ReadBytes, InternalEEPROM_ReadLong, InternalEEPROM_WriteBytes, eepromDiscBase, eepromDiscSize);
	UART_WriteString_P("Succes\n");
#elif defined(USE_RAM_FOR_DISC)
	UART_WriteString_P("\n  Medium: DRAM. Trying to allocate ");
	UART_WriteValueUnsigned(ramDiscSize, 0, 0);
	UART_WriteString_P(" bytes...");
	ramDiscBase = DRAM_Allocate(ramDiscSize);
	if(ramDiscBase != null)
	{
		FileStore_Init(DRAM_ReadByte, DRAM_ReadBytes, DRAM_ReadLong, DRAM_WriteBytes, ramDiscBase, ramDiscSize);
		UART_WriteString_P("Succes\n");
	}
	else
	{
		UART_WriteString_P("Failed!\n  No disc available!");
	}
#else
	UART_WriteString_P("No disc configured for system.\n");
#endif

	Terminal_Init();	
	UART_WriteString_P("Terminal interface initialized.\n");
	
	VX_Init();
	UART_WriteString_P("Virtual machine initialized.\n");

	LED_GREEN_ON
	
	UART_WriteString_P("System initialized - now running. yay.\n\n");
	
	UART_WriteString(prompt);
	
//	Kernel_CreateTask(ProcessTimerTask);
	
	Kernel_Run();
}
