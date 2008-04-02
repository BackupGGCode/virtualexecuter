#pragma once

#define PROCES_NAME_LENGTH											20

typedef enum {Stop, Run, Step, Crash} vx_pstate;
typedef unsigned long vx_pid;

extern string* vxPStateText[];

extern void VX_InitProcesses();
extern bool VX_CreateProcessFromFile(char* filename, vx_pid* id);
extern bool VX_KillProcess(vx_pid id);
extern bool VX_SetProcessState(vx_pid id, vx_pstate state);
extern void VX_ListProcesses(char* line);
