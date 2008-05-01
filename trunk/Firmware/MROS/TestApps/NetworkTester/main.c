#include <Kernel/kernel.h>
#include <Network/Network.h>
#include <Peripherals/UART.h>
#include <Peripherals/SPI.h>


string txt[]="Hello\n";
string txtGo[]="Go\n";
string txtRevision[]="NIC revision: ";



void send()
{
	YieldBegin
	YieldWait(1000)
	IP_Send(Num2IP(192,168,0,1), IP_PROTOCOL_UDP, globalBuffer, 40);
	YieldEnd
}

void dummy()
{

}

void main()
{
	DDRB=0xb0;
	PORTB|=0x10;

	Kernel_InitScheduler();

	UART_Init(11);
	SPI_Init(SPI_PRESCALER_32);

	NIC_Init(null);

	UART_WriteString_P(txtRevision);
	UART_WriteByte(GetVersion() + '0');
	UART_WriteByte(10);
	UART_WriteByte(13);
	
	UART_WriteString_P(txt);
/*
	Network_Init(192, 168, 0, 70);

	UART_WriteString_P(txtGo);

	Kernel_CreateTask(send);
	*/
	
	Kernel_CreateTask(dummy);
	
	Kernel_RunScheduler();
}
