#include <FileStore/FileStore.h>
#include <FileStore/InternalEEPROM.h>
#include <Peripherals/UART.h>


fsfile f;
unsigned char buf[100];


string txt1[]="Hello\n";
string txt2[]="Error opening file!";
string txt3[]="\nDone :)";


void main()
{
unsigned long bytes;

	FileStore_Init(InternalEEPROM_ReadByte, InternalEEPROM_ReadBytes, InternalEEPROM_ReadLong);

	UART_Init(11);

	UART_WriteString_P(txt1);

	if(FileStore_OpenFile("text.bat", &f) == false)
	{
		UART_WriteString_P(txt2);
		while(true);
	}

	do
	{
		bytes = FileStore_ReadBytes(&f, buf, 20);
		UART_WriteBytes(buf, bytes);
	} while(bytes == 20);
	
	UART_WriteString_P(txt3);
	
	while(true);
}
