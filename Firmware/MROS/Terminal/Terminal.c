#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <Commander/Commander.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>


void ListFiles(char* line);
void PrintFile(char* line);
void RunProgram(char* line);
void LoadFileToDisc(char* line);
__flash command commands[] = {{"ls", ListFiles}, {"prn", PrintFile}, {"run", RunProgram}, {"load", LoadFileToDisc}};


void Terminal_Init()
{
	Kernel_CreateTask(Commander_Run);
}

void ListFiles(char* line)
{
	fsFile f;
	char name[50];
	unsigned long totalBytes=0;
	unsigned short totalFiles=0;
	
	if(FileStore_GetNextFileEntry(&f, true) == false)
	{
		UART_WriteString_P("No files\n");
	}
	else
	{
		do
		{
			FileStore_GetFileName(&f, name, 50);
			UART_WriteString(name);
			UART_WriteByte(' ');
			UART_WriteValueUnsigned(f.size);
			UART_WriteString_P("\n");
			totalBytes += f.size;
			totalFiles++;
		}	while(FileStore_GetNextFileEntry(&f, false));
	}
	
	UART_WriteString_P("\n");
	UART_WriteValueUnsigned(totalFiles);
	UART_WriteString_P(" files ");
	UART_WriteValueUnsigned(totalBytes);
	UART_WriteString_P(" bytes");
}

void PrintFile(char* line)
{
	fsFile f;
	unsigned char bytes;
	unsigned char buf[20];
	
	if(FileStore_OpenFile(GetNextWord(line), &f) == false)
	{
		UART_WriteString_P("File not found");
		return;
	}
	
	do
	{
		bytes = FileStore_ReadBytes(&f, buf, 20);
		UART_WriteBytes(buf, bytes);
	} while(bytes == 20);
}

void RunProgram(char* line)
{
	fsFile f;
	
	if(FileStore_OpenFile(GetNextWord(line), &f) == false)
	{
		UART_WriteString_P("File not found");
		return;
	}
	
	// pass it on to the VX loader
}

#define BLOCK_SIZE 16
#define DISC_SIZE 1024
void LoadFileToDisc(char* line)
{
unsigned char buf[BLOCK_SIZE];
unsigned char length;
unsigned long blockSize, size, current=0;
char* word;

	word = GetNextWord(line);
	size = ReadInteger(word);
	word = GetNextWord(word);
	blockSize = ReadInteger(word);
	
	if(blockSize > BLOCK_SIZE)
	{
		blockSize = BLOCK_SIZE;
	}
	
	if(size > DISC_SIZE)
	{
		UART_WriteString_P("N 0\n");
		return;
	}
	else
	{
		UART_WriteByte('A');
		UART_WriteByte(' ');
		UART_WriteValueUnsigned(blockSize);
		UART_WriteString_P("\n");
	}
	
	while(current < size)
	{
		if((current + blockSize) >= size)
		{
			length = size - current;
		}
		else
		{
			length = blockSize;
		}
		UART_ReadBytes(buf, length);
		InternalEEPROM_WriteBytes(current, buf, length);
		UART_WriteByte('*');
		current += length;
	}
	UART_WriteByte('!');
}
