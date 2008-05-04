#pragma once

#include "DRAM.h"

#define PROCES_NAME_LENGTH											20

#define WriteProcess(address, proc)							DRAM_WriteBytes(address, (unsigned char*)proc, sizeof(process))
#define ReadProcess(address, proc)							DRAM_ReadBytes(address, (unsigned char*)proc, sizeof(process))
//#define UpdateProcess(proc)											DRAM_WriteBytes(proc.id + 53, (unsigned char*)(&(proc.state)), 22)
#define UpdateProcess(proc)											DRAM_WriteBytes(proc.id + ((unsigned short)&proc.state - (unsigned short)&proc.id), (unsigned char*)&proc.state, sizeof(process) - ((unsigned short)&proc.state - (unsigned short)&proc.id))

#define FLAG_ZERO																(1 << 0)
#define FLAG_NEGATIVE														(1 << 1)
#define FLAG_POSITIVE														(FLAG_NEGATIVE | FLAG_ZERO)


extern dram processList;
typedef enum {Stop, Run, Step, Crash} vx_pstate;
typedef unsigned long vx_pid;

/*
	New static fields must be inserted before 'state'. New dynamic fields must be inserted after 'state'.
*/
typedef struct
{
	vx_pid id;//0
	unsigned long options;//4
	char name[PROCES_NAME_LENGTH + 1];//8
	dram codeStart;//29
	dram dataStart;//33
	dram stackStart;//37
	unsigned long codeSize;//41
	unsigned long dataSize;//45
	unsigned long stackSize;//49
	vx_pstate state;//53
	dram next;//54
	unsigned long ticks;//58
	unsigned char flags;//62
	dram ip;//63
	dram sp;//67
	dram sfp;//71
} process;


extern string* vxPStateText[];

extern bool VX_CreateProcessFromFile(char* filename, vx_pid* id);
extern bool VX_KillProcess(vx_pid id);
extern bool VX_SetProcessState(vx_pid id, vx_pstate state);
extern void VX_ListProcesses(char* line);
