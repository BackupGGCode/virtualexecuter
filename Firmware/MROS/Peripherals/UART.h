#include <globals.h>


#define __BAUDRATE__(baudrate)									((SYSTEM_CLOCK_FREQUENCY/(8L*(baudrate)))-1L)


extern void UART_Init(unsigned short baudrate);
extern void UART_SetBaudrate(unsigned short baudrate);
extern unsigned char UART_BytesReady(void);
extern unsigned char UART_ReadByte(void);
extern void UART_ReadBytes(unsigned char *buffer, unsigned char length);
extern void UART_WriteByte(unsigned char data);
extern void UART_WriteBytes(unsigned char *buffer, unsigned char length);

extern void putc(unsigned char Data);
extern int putchar(int data);
extern unsigned char getc(void);

extern void UART_WriteString(char* str);
extern void UART_WriteString_P(string* str);


//extern void UART_WriteValueUnsigned(unsigned long value);
extern void UART_WriteValueUnsigned(unsigned long value, unsigned char width, char padChar);
extern void UART_WritePointer(void* pointer);
/*
extern void UARTWriteShort(unsigned short Data);
extern void UARTWriteHex(unsigned char Data);

extern void Flush(void);
*/
