#include <Kernel.h>


#define TASK_STATE_READY												0																				// Ready to be executed
#define TASK_STATE_SUSPENDED										(1<<0)																	// Suspended by a call to Suspend


/*
	Function-like macro encapsulating functionality to aquire the currently executing task.
	
	task CurrentTask();
*/
#define CurrentTask()														(taskQueue[currentTaskIndex].theTask)



typedef struct
{
	task theTask;
	unsigned char state;
	unsigned long executionTime;
	unsigned short timer;
	messageQueue* blockingMessageQueue;
	semaphore* blockingSemaphore;
} taskStruct;


extern volatile taskStruct taskQueue[MAX_NUMBER_OF_TASKS];
extern unsigned char currentTaskIndex;
extern unsigned short executionTimer;
extern unsigned long totalExecutionTime;


/***************************************************************************************************************************
	Local functions
***************************************************************************************************************************/
extern signed char TaskToIndex(task t);
extern void SystemTimer(void);
