#include "UART1.h"


#if defined(BUFFERED_UART)
static unsigned char RxBuffer[RX_BUFFER_SIZE];
static unsigned char RxBufferIn=0;
static unsigned char RxBufferOut=0;
static unsigned char RxBufferFull=0;

static unsigned char TxBuffer[TX_BUFFER_SIZE];
static unsigned char TxBufferIn=0;
static unsigned char TxBufferOut=0;
static unsigned char TxBufferFull=0;
#endif



static void WriteHexDigit(unsigned char value);


/**************************************************************

**************************************************************/
void UART_Init(unsigned short baudrate)
{
#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	UBRR1H=(baudrate>>8);
	UBRR1L=(baudrate&0xff);
	UCSR1A=(1<<U2X1);
#elif defined(__IOM32_H) || defined(__IOM8_H)
	UBRRH=(baudrate>>8);
	UBRRL=(baudrate&0xff);
	UCSRA=(1<<U2X);
#else
#error Module UART does not support the selected processor!
#endif

#if defined(BUFFERED_UART)

#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	UCSR1B=(1<<RXCIE1)|(1<<RXEN1)|(1<<TXEN1);
#elif defined(__IOM32_H) || defined(__IOM8_H)
	UCSRB=(1<<RXCIE)|(1<<RXEN)|(1<<TXEN);
#else
#error Module UART does not support the selected processor!
#endif

#else

#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	UCSR1B=(1<<RXEN1)|(1<<TXEN1);
#elif defined(__IOM32_H) || defined(__IOM8_H)
	UCSRB=(1<<RXEN)|(1<<TXEN);
#else
#error Module UART does not support the selected processor!
#endif

#endif

}


/**************************************************************

**************************************************************/
void UART_SetBaudrate(unsigned short baudrate)
{
#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	UBRR1H=(baudrate>>8);
	UBRR1L=(baudrate&0xff);
#elif defined(__IOM32_H) || defined(__IOM8_H)
	UBRRH=(baudrate>>8);
	UBRRL=(baudrate&0xff);
#else
#error Module UART does not support the selected processor!
#endif
}

/**************************************************************

**************************************************************/
unsigned char UART_BytesReady(void)
{
#if defined(BUFFERED_UART)
	if(RxBufferFull)
		return RX_BUFFER_SIZE;
	else
	{
		if(RxBufferIn==RxBufferOut)
			return 0;
		else if(RxBufferIn>RxBufferOut)
			return (RxBufferIn-RxBufferOut);
		else
			return (RX_BUFFER_SIZE-(RxBufferOut-RxBufferIn));
	}

#else

#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	return ((UCSR1A&(1<<RXC1))!=0);
#elif defined(__IOM32_H) || defined(__IOM8_H)
	return ((UCSRA&(1<<RXC))!=0);
#else
#error Module UART does not support the selected processor!
#endif

#endif
}


/**************************************************************

**************************************************************/
unsigned char UART_ReadByte(void)
{
#if defined(BUFFERED_UART)
unsigned char Temp;

	while(UART_BytesReady()==0);

	Temp=RxBuffer[RxBufferOut++];
	if(RxBufferOut>=RX_BUFFER_SIZE)
		RxBufferOut=0;
	RxBufferFull=0;
	return Temp;

#else

#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	while(!(UCSR1A&(1<<RXC1)));
	return UDR1;
#elif defined(__IOM32_H) || defined(__IOM8_H)
	while(!(UCSRA&(1<<RXC)));
	return UDR;
#else
#error Module UART does not support the selected processor!
#endif

#endif
}


/**************************************************************

**************************************************************/
void UART_ReadBytes(unsigned char *buffer, unsigned char length)
{
	while(length)
	{
		*buffer++=UART_ReadByte();
		length--;
	}
}


/**************************************************************

**************************************************************/
void UART_WriteByte(unsigned char data)
{
#if defined(BUFFERED_UART)

	while(TxBufferFull);

//	if(UCSRA&(1<<UDRE))
//		UDR=Data;
//	else
//	{
		TxBuffer[TxBufferIn++]=data;
		if(TxBufferIn>=TX_BUFFER_SIZE)
			TxBufferIn=0;
		if(TxBufferIn==TxBufferOut)
			TxBufferFull=1;
		else
			TxBufferFull=0;
		UCSR0B|=(1<<UDRIE0);
//		UCSRB|=(1<<UDRIE);
//	}

#else

#if defined(__IOM64_H) || defined(__IOM162_H) || defined(__IOM168_H)
	while(!(UCSR1A&(1<<UDRE1)));
	UDR1=data;
#elif defined(__IOM32_H) || defined(__IOM8_H)
	while(!(UCSRA&(1<<UDRE)));
	UDR=data;
#else
#error Module UART does not support the selected processor!
#endif

#endif
}


/**************************************************************

**************************************************************/
void putc(unsigned char data)
{
	UART_WriteByte(data);
}
int putchar(int data)
{
	UART_WriteByte(data);
	return 0;
}


/**************************************************************

**************************************************************/
unsigned char getc(void)
{
	return UART_ReadByte();
}


/**************************************************************

**************************************************************/
void UART_WriteBytes(unsigned char *buffer, unsigned char length)
{
	while(length)
	{
		length--;
		UART_WriteByte(*buffer++);
	}
}


/**************************************************************

**************************************************************/
#if defined(BUFFERED_UART)
#if defined(__IOM162_H) || defined(__IOM168_H)
#pragma vector=USART1_RXC_vect
#elif
#error Module 'UART' does not support the selected processor!
//#pragma vector=USART_RXC_vect
#endif
__interrupt void UART_RX_Interrupt(void)
{
unsigned char temp;

#if defined(__IOM64_H) || defined(__IOM162_h)
	temp=UDR1;
#elif defined(__IOM32_H) || defined(__IOM8_H)
	temp=UDR;
#else
#error Module 'UART' does not support the selected processor!
#endif
	if(!RxBufferFull)
	{
		RxBuffer[RxBufferIn++]=temp;
		if(RxBufferIn>=RX_BUFFER_SIZE)
			RxBufferIn=0;
		if(RxBufferIn==RxBufferOut)
			RxBufferFull=1;
		else
			RxBufferFull=0;
	}
}
#endif


/**************************************************************

**************************************************************/
#if defined(BUFFERED_UART)
#if defined(__IOM162_H) || defined(__IOM168_H)
#pragma vector=USART1_UDRE_vect
#elif
#error Module 'UART' does not support the selected processor!
//#pragma vector=USART_UDRE_vect
#endif
__interrupt void UART_UDRE_Interrupt(void)
{
	if(TxBufferFull || (TxBufferIn!=TxBufferOut))
	{
#if defined(__IOM64_H) || defined(__IOM162_h)
		UDR1=TxBuffer[TxBufferOut++];
#elif defined(__IOM32_H) || defined(__IOM8_H)
		UDR=TxBuffer[TxBufferOut++];
#else
#error Module UART does not support the selected processor!
#endif
		if(TxBufferOut>=TX_BUFFER_SIZE)
			TxBufferOut=0;
		TxBufferFull=0;
	}
	else
#if defined(__IOM64_H) || defined(__IOM162_h)
		UCSR1B&=~(1<<UDRIE1);
#elif defined(__IOM32_H) || defined(__IOM8_H)
		UCSRB&=~(1<<UDRIE);
#else
#error Module UART does not support the selected processor!
#endif
}
#endif


/**************************************************************

**************************************************************/
void UART_WriteString(char* str)
{
unsigned char temp;

	while(*str)
	{
		temp=*str++;
#if (SEND_CRLF==1)
		if(temp==10)
			UART_WriteByte(13);
#endif
		UART_WriteByte(temp);
	}
}


/**************************************************************

**************************************************************/
void UART_WriteString_P(string* str)
{
unsigned char temp;

	while(*str)
	{
		temp=*str++;
#if (SEND_CRLF==1)
		if(temp==10)
			UART_WriteByte(13);
#endif
		UART_WriteByte(temp);
	}
}



/**************************************************************

**************************************************************/
void UART_WriteValueUnsigned(unsigned long value)
{
	if(value > 999999)
	{
		UART_WriteByte(((value / 1000000) % 10) + '0');
	}
	if(value > 99999)
	{
		UART_WriteByte(((value / 100000) % 10) + '0');
	}
	if(value > 9999)
	{
		UART_WriteByte(((value / 10000) % 10) + '0');
	}
	if(value > 999)
	{
		UART_WriteByte(((value / 1000) % 10) + '0');
	}
	if(value > 99)
	{
		UART_WriteByte(((value / 100) % 10) + '0');
	}
	if(value > 9)
	{
		UART_WriteByte(((value / 10) % 10) + '0');
	}
	UART_WriteByte((value % 10) + '0');
}


void UART_WritePointer(void* pointer)
{
	WriteHexDigit((unsigned short)pointer >> 12);
	WriteHexDigit((unsigned short)pointer >> 8);
	WriteHexDigit((unsigned short)pointer >> 4);
	WriteHexDigit((unsigned short)pointer);
}


static void WriteHexDigit(unsigned char value)
{
	value &= 0x0f;
	
	if(value > 9)
		UART_WriteByte(value + 'A' - 10);
	else
		UART_WriteByte(value + '0');
}

/**************************************************************

**************************************************************/
/*
void UART_WriteShort(unsigned short Data)
{
	if(Data>9999)
		UART_WriteByte((Data/10000)%10+'0');
	if(Data>999)
		UART_WriteByte((Data/1000)%10+'0');
	if(Data>99)
		UART_WriteByte((Data/100)%10+'0');
	if(Data>9)
		UART_WriteByte((Data/10)%10+'0');
	UART_WriteByte(Data%10+'0');
}
*/

/**************************************************************

**************************************************************/
/*
void Flush(void)
{
	while(UART_BytesReady())
		UART_ReadByte();
}
*/
