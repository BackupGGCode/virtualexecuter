#pragma once

typedef enum {Stopped, Running, Dead} vx_pstate;
#define PROCESS_STATE_STOPPED 

typedef unsigned long vx_pid;


extern void VX_InitProcesses();
extern bool VX_CreateProcess(fsFile* file, vx_pid* id);
extern bool VX_KillProcess(vx_pid* id);
extern void VX_ListProcesses(char* line);
