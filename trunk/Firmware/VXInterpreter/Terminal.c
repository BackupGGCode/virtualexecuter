#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <Commander/Commander.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>
#include "VX.h"


void HelpScreen(char* line);
void ListFiles(char* line);
void PrintFile(char* line);
void RunProgram(char* line);
void StepProgram(char* line);
void StartProgram(char* line);
void StopProgram(char* line);
void LoadFileToDisc(char* line);
__flash command commands[] = {{"help", HelpScreen}, {"list", ListFiles}, {"print", PrintFile}, {"run", RunProgram}, {"step", StepProgram}, {"start", StartProgram}, {"stop", StopProgram}, {"load", LoadFileToDisc}};


void Terminal_Init()
{
	Kernel_CreateTask(Commander_Run);
}

void HelpScreen(char* line)
{
	UART_WriteString_P("help - This help screen\n");
	UART_WriteString_P("list - List files\n");
	UART_WriteString_P("print <file> - Print file\n");
	UART_WriteString_P("run <file> - Run VX program\n");
	UART_WriteString_P("load <size> <blocksize> - Load image to disc\n");
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
char* word;
	
	word = GetNextWord(line);
	if(FileStore_OpenFile(word, &f) == false)
	{
		UART_WriteString_P("File not found");
		return;
	}
	
	if(VX_Load(&f) == false)
	{
		UART_WriteString_P("Unable to load program");
		return;
	}
	
	UART_WriteString_P("Program loaded ");
	
	word = GetNextWord(word);
	if(*word == 'b')
	{
		// dont run program now. Enables stepping.
		UART_WriteString_P("but not startet.\n");
	}
	else
	{
		VX_Start();
		UART_WriteString_P("and startet.\n");
	}
}

void StepProgram(char* line)
{
unsigned long count = ReadInteger(GetNextWord(line));

	if(count == 0)
	{
		count = 1;
	}

	while(count > 0)
	{
		VX_Step();
		count--;
	}
}

void StartProgram(char* line)
{
	VX_Start();
}

void StopProgram(char* line)
{
	VX_Stop();
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
