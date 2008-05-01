#pragma once


#include <config.h>
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


/***************************************************************************************
	Kernel_ initialization
***************************************************************************************/
extern void Kernel_InitScheduler(void);
extern void Kernel_RunScheduler(void);


/***************************************************************************************
	Task management
***************************************************************************************/
extern bool Kernel_CreateTask(task t, unsigned char priority);
extern bool Kernel_DeleteTask(task t);
extern void Kernel_Delete(void);

extern bool Kernel_SuspendTask(task t);
extern void Kernel_Suspend(void);
extern bool Kernel_ResumeTask(task t);

extern bool Kernel_SleepTask(task t, unsigned short time);
extern void Kernel_Sleep(unsigned short time);
extern bool Kernel_WakeTask(task t);
extern void Kernel_Delay(unsigned short time);


/***************************************************************************************
	Semaphores
***************************************************************************************/
extern bool Kernel_SemaphoreWait(semaphore* s);
extern void Kernel_SemaphoreSignal(semaphore* s);


/***************************************************************************************
	Message queues
***************************************************************************************/
extern bool Kernel_PostMessage(unsigned char msg, messageQueue* mq);
extern bool Kernel_GetMessage(unsigned char* msg, messageQueue* mq);
