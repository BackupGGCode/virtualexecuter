/*


		Verify that the kernel doesn't crash when no tasks are ready to run i.e. all tasks are waiting for something.


*/


#pragma once

#include <Config.h>
#include <Globals.h>


typedef void(*task)(void);

typedef unsigned char semaphore;


typedef struct
{
	unsigned char head;
	unsigned char tail;
	bool full;
	unsigned char data[MESSAGE_QUEUE_SIZE];
} messageQueue;

#define Critical()															__disable_interrupt()
#define NonCritical()														__enable_interrupt()


#define YieldBegin															static unsigned char __state=0; unsigned char __step=0; if(__state==__step++){
#define YieldWhile(condition)										{if((condition)) return; else __state++; } } if(__state==__step++){
#define YieldUntil(condition)										{if(!(condition)) return; else __state++; } } if(__state==__step++){
#define YieldWait(time)													{ Kernel_Sleep((time)); __state++; return;} } if(__state==__step++){
#define Yield																		{ __state++; return;} } if(__state==__step++){
#define YieldEnd																{ __state=0; return;} }


#include "TaskManagement.h"
#include "Semaphore.h"
#include "MessageQueue.h"
#include "MemoryManagement.h"
#include "Timer.h"


extern void Kernel_Init(void);
extern void Kernel_Run(void);
extern void Kernel_SystemTimerHook(void(*hook)(void));
