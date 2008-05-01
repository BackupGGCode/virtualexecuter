#include <ioavr.h>
#include "UART.h"
#include "Timer0.h"
#include "Ethernet.h"
#include "NICl.h"
#include "IP.h"
#include "UDP.h"
#include "App.h"
#include "ARP.h"


#define KEY							PIND_Bit3
#define LED_RED					PORTD_Bit5
#define LED_GREEN				PORTD_Bit6

#define LED_1						PORTD_Bit4
#define LED_2						PORTD_Bit5
#define LED_SYS					PORTD_Bit6


#define LED_ON_TIME			10
unsigned char LedRxTimer=0;
unsigned char LedTxTimer=0;
unsigned char LedSysTimer=0;


unsigned char b[APP_UDP_OVERHEAD+30];


void heartbeat(void)
{
	LED_SYS=!LED_SYS;
}

void ledcontrol(void)
{
	if(LedRxTimer)
	{
		LedRxTimer--;
		LED_1=0;
	}
	else
		LED_1=1;

	if(LedTxTimer)
	{
		LedTxTimer--;
		LED_2=0;
	}
	else
		LED_2=1;
/*
	if(LedSysTimer)
	{
		LedSysTimer--;
		LED_SYS=0;
	}
	else
		LED_SYS=1;
*/
}

/*
void TextServer(unsigned char *Buffer)
{
	LED_1=!LED_1;
	UART_WriteBuffer(Buffer+UDP_DATA,1);
//	UDP_Send(getlong(Buffer+IP_SOURCE),getshort(Buffer+UDP_SOURCE),getshort(Buffer+UDP_DESTINATION),buf,6);
	GlobalBuffer[UDP_DATA]='*';
	UDP_Send(Num2IP(10,0,0,2),10000,10000,GlobalBuffer,1);
}


void TextServerupcase(unsigned char *Buffer)
{
	LED_2=!LED_2;
	UART_WriteChar(Buffer[UDP_DATA]&~0x20);
}
*/

void ComReceiver(unsigned char *Buffer)
{
	LedTxTimer=LED_ON_TIME;
	UART_WriteBuffer(Buffer+UDP_DATA,getshort(Buffer+UDP_LENGTH)-UDP_HEADER_SIZE);
}


void ComTransmit(void)
{
unsigned char count=UART_InBuffer();

	if(count)
	{
		LedRxTimer=LED_ON_TIME;
//		UART_ReadBuffer(b+APP_UDP_OVERHEAD,count);
//		UART_ReadBuffer(b,count);
//		b[APP_UDP_OVERHEAD]=UART_ReadChar();
//		count=1;
//		if(b[APP_UDP_OVERHEAD]!='-')
		UART_ReadChar();
	}
}


__C_task void main(void)
{
//unsigned char buffer[APP_UDP_OVERHEAD+30];

	PORTA=0x7c;
	PORTB=0x00;
	PORTC=0x00;
	PORTD=0xff;
	DDRA=0xf0;
	DDRB=0x1f;
	DDRC=0x00;
	DDRD=0x72;
	
	UART_Init(BAUDRATE_115200);
	TIMER0_Init(PRESCALER_64, 173);
	SREG|=0x80;
	
	IP=Num2IP(10,0,0,64);

	if(NIC_Init(0))
	{
		LED_SYS=0;
		while(1);
	}
/*
	UDP_RegisterPort(10000,TextServer);
	UDP_RegisterPort(10001,TextServerupcase);
*/
	UDP_RegisterPort(10000,ComReceiver);

//	UART_Flush();

	TIMER0_AddEvent(ARP_Tick,1000);
//	TIMER0_AddEvent(ComTransmit,25);
//	TIMER0_AddEvent(ledcontrol,10);
	TIMER0_AddEvent(heartbeat,500);

	while(1)
	{
		ETHERNET_Receive();

		if(!KEY)
			UDP_Send(0x0a000002,10002,15000,GlobalBuffer,1);

		/*
		if(action)
		{
			action--;
			buffer[APP_UDP_OVERHEAD]='A';
			UDP_Send(Num2IP(10,0,0,2),10000,10000,buffer,1);
		}
		*/
	}
}
