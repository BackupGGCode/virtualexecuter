#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <Commander/Commander.h>
#include <strings.h>
#include <FileStore/FileStore.h>
#include <Peripherals/InternalEEPROM.h>
#include <VX/VX.h>
#include <DRAM.h>


void HelpScreen(char* line);
void ListFiles(char* line);
void PrintFile(char* line);
void RunProgram(char* line);
void StepProgram(char* line);
void StartProgram(char* line);
void StopProgram(char* line);
void LoadFileToDisc(char* line);
void TestDRAM(char* line);
void AllocateChunk(char* line);
void DeallocateChunk(char* line);
void PrintFreeHeap(char* line);
//void CreateProcess(char* line);
void KillProcess(char* line);
__flash command commands[] = {
	{"help", HelpScreen, "This help screen."},
	{"list", ListFiles, "List files on disk."},
	{"print", PrintFile, "Print specified file."},
	{"run", RunProgram, "Load a VX program to memory. Use option s to auto start the program."},
	{"step", StepProgram, "Execute one instruction of the currently loaded program."},
	{"start", StartProgram, "Starts execution of the currently loaded program."},
	{"stop", StopProgram, "Stops execution of the currently loaded program."},
	{"load", LoadFileToDisc, "Load image to disc."},
	{"testdram", TestDRAM, 0},
	{"a", AllocateChunk, 0},
	{"d", DeallocateChunk, 0},
	{"h", PrintFreeHeap, 0},
	{"pro", VX_ListProcesses, "List all processes."},
//	{"create", CreateProcess, "Creates a new process from the file specified."},
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
unsigned long count = ReadValueUnsigned(GetNextWord(line));

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
#define DISC_SIZE (E2END + 1)																														// Reserve the entire EEPROM for the disc
void LoadFileToDisc(char* line)
{
unsigned char buf[BLOCK_SIZE];
unsigned char length;
unsigned long blockSize, size, current=0;
char* word;

	word = GetNextWord(line);
	size = ReadValueUnsigned(word);
	word = GetNextWord(word);
	blockSize = ReadValueUnsigned(word);
	
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
		InternalEEPROM_WriteBytes(current, buf, length);
		UART_WriteByte('*');
		current += length;
	}
	UART_WriteByte('!');
}


void TestDRAM(char* line)
{
unsigned long current;
unsigned long max = DRAM_SIZE;
unsigned long step = 57;
unsigned char value = 123;

	UART_WriteString_P("Clearing memory...\n");

	for(current = 0; current < max; current += step)
	{
		DRAM_WriteByte(current, 0);
	}
	
	UART_WriteString_P("Done\nTesting memory...\n");

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
		UART_WriteValueUnsigned(current / 1024,0,0);
		UART_WriteString_P(" kB\r");
		value += 234;
	}
	
	UART_WriteString_P("\nDone");
}


void AllocateChunk(char* line)
{
unsigned long size = ReadValueUnsigned(GetNextWord(line));	
dram d = DRAM_Allocate(size);

	if(d)
	{
		UART_WriteString_P("Allocation succeeded. Location: ");
		UART_WriteValueUnsigned(d,0,0);
		UART_WriteString_P("\n");
	}
	else
	{
		UART_WriteString_P("Allocation failed!\n");
	}
}

void DeallocateChunk(char* line)
{
unsigned long address = ReadValueUnsigned(GetNextWord(line));	

	DRAM_Deallocate(address);

	UART_WriteString_P("Done\n");
}

void PrintFreeHeap(char* line)
{
unsigned long space = DRAM_GetFreeHeapSpace();

	DRAM_PrintBlockList();
	UART_WriteString_P("Allocated/free: ");
	UART_WriteValueUnsigned(DRAM_SIZE - space, 0, 0);
	UART_WriteByte('/');
	UART_WriteValueUnsigned(space, 0, 0);
	UART_WriteString_P(" bytes\n");
}

/*
void CreateProcess(char* line)
{
char* filename = GetNextWord(line);
fsFile file;
vx_pid id;

	if(FileStore_OpenFile(filename, &file) == false)
	{
		UART_WriteString_P("Unable to open file.\n");
		return;
	}
	
	if(VX_CreateProcessFromFile(&file, &id) == false)
	{
		UART_WriteString_P("Unable to create process.\n");
		return;
	}
	
	UART_WriteString_P("Process created with ID: ");
	UART_WriteValueUnsigned(id, 0, 0);
	UART_WriteString_P("\n");
}
*/

void KillProcess(char* line)
{
	vx_pid id = ReadValueUnsigned(GetNextWord(line));
	
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
fsFile file;
vx_pid id;

	if(FileStore_OpenFile(line, &file) == false)
	{
		return false;
	}
	
	if(VX_CreateProcessFromFile(&file, &id) == false)
	{
		UART_WriteString_P("Unable to create process.\n");
		return true;
	}
	
	
	
	UART_WriteString_P("Process created with ID ");
	UART_WriteValueUnsigned(id, 0, 0);
	UART_WriteString_P("\n");
	
	return true;
}
