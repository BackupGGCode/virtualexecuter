#pragma once

#include "DRAM.h"

#define PROCES_NAME_LENGTH											20

#define WriteProcess(address, proc)							DRAM_WriteBytes(address, (unsigned char*)proc, sizeof(process))
#define ReadProcess(address, proc)							DRAM_ReadBytes(address, (unsigned char*)proc, sizeof(process))
#define UpdateProcess(proc)											DRAM_WriteBytes((dram)proc->id, proc->ticks, 17)


typedef enum {Stop, Run, Step, Crash} vx_pstate;
typedef unsigned long vx_pid;

typedef struct
{
	vx_pid id;
	vx_pstate state;
	dram next;
	char name[PROCES_NAME_LENGTH + 1];
	dram codeStart;
	dram dataStart;
	dram stackStart;
	unsigned long codeSize;
	unsigned long dataSize;
	unsigned long stackSize;
	unsigned long ticks;
	unsigned char flags;
	dram ip;
	dram sp;
	dram sfp;
} process;


extern string* vxPStateText[];

extern void VX_InitProcesses();
extern bool VX_CreateProcessFromFile(char* filename, vx_pid* id);
extern bool VX_KillProcess(vx_pid id);
extern bool VX_SetProcessState(vx_pid id, vx_pstate state);
extern void VX_ListProcesses(char* line);
