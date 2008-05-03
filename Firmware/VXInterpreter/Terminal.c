#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Kernel/MemoryManagement.h>
#include <Peripherals/UART.h>
#include <Commander/Commander.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>
#include <VX/VX.h>
#include <DRAM.h>


void HelpScreen(char* line);
void ListFiles(char* line);
void ViewFile(char* line);
void RunProgram(char* line);
void StepProgram(char* line);
void StopProgram(char* line);
void LoadFileToDisc(char* line);
void TestDRAM(char* line);
void PrintHeaps(char* line);
void KillProcess(char* line);
__flash command commands[] = {
	{"help", HelpScreen, "This help screen."},
	{"list", ListFiles, "List files on disk."},
	{"view", ViewFile, "Print specified file to the console."},
	{"run", RunProgram, "Set process state to running."},
	{"step", StepProgram, "Perform one instruction of the process."},
	{"stop", StopProgram, "Stop the process."},
	{"load", LoadFileToDisc, "Load image to disc."},
	{"testdram", TestDRAM, 0},
	{"mem", PrintHeaps, "Print internal and external heap usage."},
	{"pros", VX_ListProcesses, "List all processes."},
	{"kill", KillProcess, "Kills the process specified by its ID."} };

unsigned short NumberOfCommands;

bool ExecuteProgram(char* line);


void Terminal_Init()
{
	Kernel_CreateTask(Commander_Run);
	NumberOfCommands = sizeof(commands) / sizeof(command);
	defaultHandler = ExecuteProgram;
}

void HelpScreen(char* line)
{
unsigned char i;

	for(i = 0; i < NumberOfCommands; i++)
	{
		UART_WriteString_P(commands[i].name);
		if(commands[i].helpText != null)
		{
			UART_WriteString_P(" - ");
			UART_WriteString_P(commands[i].helpText);
		}
		UART_WriteString_P("\n");
	}
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
		UART_WriteString_P("Size...... Name\n");
		do
		{
			FileStore_GetFileName(&f, name, 50);
			UART_WriteValueUnsigned(f.size, 10, ' ');
			UART_WriteByte(' ');
			UART_WriteString(name);
			UART_WriteString_P("\n");
			totalBytes += f.size;
			totalFiles++;
		}	while(FileStore_GetNextFileEntry(&f, false));
	}
	
	UART_WriteString_P("\n");
	UART_WriteValueUnsigned(totalFiles,0,0);
	UART_WriteString_P(" files ");
	UART_WriteValueUnsigned(totalBytes,0,0);
	UART_WriteString_P(" bytes\n");
}

void ViewFile(char* line)
{
fsFile f;
unsigned char bytes;
unsigned char buf[20];
	
	if(FileStore_OpenFile(Strings_GetNextWord(line), &f) == false)
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
vx_pid id = Strings_ReadValueUnsigned(Strings_GetNextWord(line));

	if(VX_SetProcessState(id, Run))
	{
		UART_WriteString_P("Process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P(" is now running\n");
	}
	else
	{
		UART_WriteString_P("Unable to run process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P("!\n");
	}
}

void StepProgram(char* line)
{
vx_pid id = Strings_ReadValueUnsigned(Strings_GetNextWord(line));

	if(VX_SetProcessState(id, Step))
	{
		UART_WriteString_P("Stepping process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P(" one tick\n");
	}
	else
	{
		UART_WriteString_P("Unable to step process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P("!\n");
	}
}


void StopProgram(char* line)
{
vx_pid id = Strings_ReadValueUnsigned(Strings_GetNextWord(line));

	if(VX_SetProcessState(id, Stop))
	{
		UART_WriteString_P("Process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P(" is now stopped\n");
	}
	else
	{
		UART_WriteString_P("Unable to stop process ");
		UART_WriteValueUnsigned(id, 0, 0);
		UART_WriteString_P("!\n");
	}
}


void LoadFileToDisc(char* line)
{
unsigned char* buffer;
//unsigned char length;
unsigned long current;
unsigned long size;
unsigned short blockSize;
char* word;

	word = Strings_GetNextWord(line);
	size = Strings_ReadValueUnsigned(word);
	word = Strings_GetNextWord(word);
	blockSize = Strings_ReadValueUnsigned(word);
	
	if(blockSize > IMAGE_LOAD_BLOCK_SIZE)
	{
		blockSize = IMAGE_LOAD_BLOCK_SIZE;
	}
	
	buffer = Kernel_Allocate(blockSize);
	
	if(size > FileStore_GetDiscSize() || buffer == null)
	{
		UART_WriteString_P("N 0\n");
		return;
	}
	else
	{
		UART_WriteString_P("A ");
		UART_WriteValueUnsigned(blockSize, 0, 0);
		UART_WriteString_P("\n");
	}
	
	current = 0;
	while(current < size)
	{
		if((current + blockSize) > size)
		{
			blockSize = size - current;
		}

		UART_ReadBytes(buffer, blockSize);
		FileStore_WriteBytes(current, buffer, blockSize);
		UART_WriteByte('*');
		current += blockSize;
	}
	
	UART_WriteByte('!');
	
	Kernel_Deallocate(buffer);
}


/*
#define BLOCK_SIZE 16
#define DISC_SIZE (E2END + 1)																														// Reserve the entire EEPROM for the disc
void LoadFileToDisc(char* line)
{
unsigned char buf[BLOCK_SIZE];
unsigned char length;
unsigned long blockSize, size, current=0;
char* word;

	word = Strings_GetNextWord(line);
	size = Strings_ReadValueUnsigned(word);
	word = Strings_GetNextWord(word);
	blockSize = Strings_ReadValueUnsigned(word);
	
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
		UART_WriteString_P("A ");
		UART_WriteValueUnsigned(blockSize,0,0);
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
		//InternalEEPROM_WriteBytes(current, buf, length);
		FileStore_WriteBytes(current, buf, length);
		UART_WriteByte('*');
		current += length;
	}
	UART_WriteByte('!');
}
*/

void TestDRAM(char* line)
{
unsigned long current;
unsigned long max = DRAM_SIZE;
unsigned long step = 1;
unsigned char value = 123;
unsigned short sent;

	UART_WriteString_P("Clearing memory...\n");

	sent=0;
	for(current = 0; current < max; current += step)
	{
		DRAM_WriteByte(current, 0);
		sent+=step;
		if(sent>=1024)
		{
			UART_WriteValueUnsigned(current / 1024, 0, 0);
			UART_WriteString_P(" kB\r");
			sent-=1024;
		}
		value += 234;
	}
	
	UART_WriteString_P("\nDone\nTesting memory...\n");

	sent=0;
	for(current = 0; current < max; current += step)
	{
		if(DRAM_ReadByte(current) != 0)
		{
			UART_WriteValueUnsigned(DRAM_ReadByte(current),0,0);
			UART_WriteString_P("\nNot cleared");
			break;
		}
		DRAM_WriteByte(current, value);
		if(DRAM_ReadByte(current) != value)
		{
			UART_WriteString_P("\nReadback error");
			break;
		}
		sent+=step;
		if(sent>=1024)
		{
			UART_WriteValueUnsigned(current / 1024, 0, 0);
			UART_WriteString_P(" kB\r");
			sent-=1024;
		}
		value += 234;
	}
	
	UART_WriteString_P("\nDone - all is well :)\n");
}

void PrintHeaps(char* line)
{
unsigned long space = Kernel_GetFreeHeapSpace();

	UART_WriteString_P("Internal heap usage:\n");
	Kernel_PrintHeap();
	UART_WriteString_P("Allocated/free: ");
	UART_WriteValueUnsigned(HEAP_SIZE - space, 0, 0);
	UART_WriteByte('/');
	UART_WriteValueUnsigned(space, 0, 0);
	UART_WriteString_P(" bytes\n\n");


	space = DRAM_GetFreeHeapSpace();

	UART_WriteString_P("External heap usage:\n");
	DRAM_PrintBlockList();
	UART_WriteString_P("Allocated/free: ");
	UART_WriteValueUnsigned(DRAM_SIZE - space, 0, 0);
	UART_WriteByte('/');
	UART_WriteValueUnsigned(space, 0, 0);
	UART_WriteString_P(" bytes\n");
}

void KillProcess(char* line)
{
	vx_pid id = Strings_ReadValueUnsigned(Strings_GetNextWord(line));
	
	if(VX_KillProcess(id))
	{
		UART_WriteString_P("Process died\n");
	}
	else
	{
		UART_WriteString_P("Process not found\n");
	}
}

bool ExecuteProgram(char* line)
{
vx_pid id;
unsigned char i = 0;

	while(line[i] != 0 && line[i] != ' ')
	{
		i++;
	}
	line[i] = 0;

	if(VX_CreateProcessFromFile(line, &id) == false)
	{
		UART_WriteString_P("Unknown command\n");
		return true;
	}
	
	VX_SetProcessState(id, Stop);
	
	UART_WriteString_P("Process created with ID ");
	UART_WriteValueUnsigned(id, 0, 0);
	UART_WriteString_P("\n");
	
	return true;
}
