#pragma once

#include "DRAM.h"

#define PROCES_NAME_LENGTH											20


typedef enum {Stop, Run, Step, Crash} vx_pstate;
typedef unsigned long vx_pid;

typedef struct
{
	vx_pid id;
	vx_pstate state;
	unsigned long ticks;
	unsigned char flags;
	dram ip;
	dram sp;
	dram code;
	unsigned long code_size;
	dram constant;
	unsigned long constant_size;
	dram data;
	unsigned long data_size;
	dram stack;
	unsigned long stack_size;
	dram next;
	char name[PROCES_NAME_LENGTH + 1];
} process;


extern string* vxPStateText[];

extern void VX_InitProcesses();
extern bool VX_CreateProcessFromFile(char* filename, vx_pid* id);
extern bool VX_KillProcess(vx_pid id);
extern bool VX_SetProcessState(vx_pid id, vx_pstate state);
extern void VX_ListProcesses(char* line);
