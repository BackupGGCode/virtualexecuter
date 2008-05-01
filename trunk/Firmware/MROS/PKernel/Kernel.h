#pragma once


#include <config.h>
#include <globals.h>


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


//#define TASK(

#define YieldBegin															static unsigned char __state=0; unsigned char __step=0; if(__state==__step++){
#define YieldWhile(condition)										{if((condition)) return; else __state++; } } if(__state==__step++){
#define YieldUntil(condition)										{if(!(condition)) return; else __state++; } } if(__state==__step++){
#define YieldWait(time)													{ KernelSleep((time)); __state++; return;} } if(__state==__step++){
#define Yield																		{ __state++; return;} } if(__state==__step++){
#define YieldEnd																{ __state=0; return;} }


/***************************************************************************************
	Kernel initialization
***************************************************************************************/
// Debug: time=0, DEBUG_EVENT_INITSCHEDULER, null, OK, 0
extern void KernelInitScheduler(void);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RUNSCHEDULER, null, OK/NO_TASKS, 0
extern void KernelRunScheduler(void);


/***************************************************************************************
	Task management
***************************************************************************************/
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_CREATETASK, taskID, OK/TASKS_ALREADY_RUNNING/NO_ROOM, callingTask
extern bool KernelCreateTask(task t);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_DELETETASK, taskID, OK/NO_TASKS, callingTask
extern bool KernelDeleteTask(task t);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_DELETETASK, taskID, OK/NO_TASKS, callingTask
extern void KernelDelete(void);

// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SUSPENDTASK, null, OK/NO_TASKS, 0
extern bool KernelSuspendTask(task t);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SUSPENDTASK, null, OK/NO_TASKS, 0
extern void KernelSuspend(void);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RESUMETASK, null, OK/NO_TASKS, 0
extern bool KernelResumeTask(task t);

// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SLEEPTASK, null, OK/NO_TASKS, 0
extern bool KernelSleepTask(task t, unsigned short time);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SLEEPTASK, null, OK/NO_TASKS, 0
extern void KernelSleep(unsigned short time);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_WAKETASK, null, OK/NO_TASKS, 0
extern bool KernelWakeTask(task t);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_DELAY, null, OK/NO_TASKS, 0
extern void KernelDelay(unsigned short time);


/***************************************************************************************
	Timer management
***************************************************************************************/
/*
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RUNSCHEDULER, null, OK/NO_TASKS, 0
extern bool taskerCreateIntervalTimer(task t, unsigned short time);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RUNSCHEDULER, null, OK/NO_TASKS, 0
extern bool taskerCreateOnShotTimer(task t, unsigned short time);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RUNSCHEDULER, null, OK/NO_TASKS, 0
extern bool taskerDeleteTimer(task t);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_RUNSCHEDULER, null, OK/NO_TASKS, 0
//extern void taskerDelete(void);
*/


/***************************************************************************************
	Semaphores
***************************************************************************************/
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SEMAPHOREWAIT, null, OK/NO_TASKS, 0
extern bool KernelSemaphoreWait(semaphore* s);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_SEMAPHORESIGNAL, null, OK/NO_TASKS, 0
extern void KernelSemaphoreSignal(semaphore* s);


/***************************************************************************************
	Message queues
***************************************************************************************/
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_POSTMESSAGE, null, OK/NO_TASKS, 0
extern bool KernelPostMessage(unsigned char msg, messageQueue* mq);
// Debug: time=TIME_SINCE_INIT, DEBUG_EVENT_GETMESSAGE, null, OK/NO_TASKS, 0
extern bool KernelGetMessage(unsigned char* msg, messageQueue* mq);
