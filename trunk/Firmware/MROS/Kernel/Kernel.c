#include "Kernel.h"
#include "KernelInternals.h"
#include "Timer.h"
#include "Debugger.h"


volatile taskStruct taskQueue[MAX_NUMBER_OF_TASKS];
unsigned char currentTaskIndex = ~0;
unsigned short executionTimer;
unsigned long totalExecutionTime;
#if TASKER_DEBUG_LEVEL > 1
unsigned short debugTimer = 0;
#endif

static void (*systemTimerHook)(void) = null;


/***************************************************************************************************************************
	Removes all tasks from the task queue
***************************************************************************************************************************/
void Kernel_Init(void)
{
unsigned short index;

#if TASKER_DEBUG_LEVEL > 0
	Kernel_InitDebugger();
#endif

#if defined(HEAP_SIZE)
	Kernel_InitHeap();
#endif

	// Clearing the task queue this way takes up less code space than using initializer ( ={0} )!
	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)																	// Clear all task index'
	{
		taskQueue[index].theTask = null;
		taskQueue[index].executionTime = 0;
		taskQueue[index].state = TASK_STATE_READY;
		taskQueue[index].blockingMessageQueue = null;
		taskQueue[index].blockingSemaphore = null;
		taskQueue[index].timer = 0;
	}
	
	Kernel_InitTimer(SystemTimer);
	
	NonCritical();
	
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_INITSCHEDULER, null, DEBUG_STATUS_OK, 0);
#endif
}


/***************************************************************************************************************************
	Loops through the task queue and calls the tasks
***************************************************************************************************************************/
void Kernel_Run(void)
{
#if TASKER_DEBUG_LEVEL > 1
static unsigned char debugIndex = 0;
#endif

static unsigned char tcnt;

#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_RUNSCHEDULER, null, DEBUG_STATUS_OK, 0);
#endif
		
	while(true)												// Run scheduler forever
	{
//		Critical();
		
		do
		{
			NonCritical();
			__no_operation();
			Critical();
			
			currentTaskIndex++;
			
			if(currentTaskIndex >= MAX_NUMBER_OF_TASKS || taskQueue[currentTaskIndex].theTask == null)
			{
				currentTaskIndex = 0;
			}
		} while(taskQueue[currentTaskIndex].state != TASK_STATE_READY || taskQueue[currentTaskIndex].timer > 0 ||
						taskQueue[currentTaskIndex].blockingMessageQueue != null || taskQueue[currentTaskIndex].blockingSemaphore != null );
		
		executionTimer = 0;
		tcnt = TCNT0;
		
		NonCritical();
		
		taskQueue[currentTaskIndex].theTask();																							// Execute the task
		
		Critical();
		
#if defined(__IOM8_H)

#elif defined(__IOM168_H)
		
#else
		executionTimer *= OCR0;
#endif

#if defined(__IOM8_H)

#elif defined(__IOM168_H)

#else
		if(tcnt >= TCNT0)
		{
			executionTimer += ((OCR0 + 1) - tcnt);
			executionTimer += TCNT0;
		}
		else
		{
			executionTimer += TCNT0 - tcnt;
		}
#endif
		
		totalExecutionTime += executionTimer;
		
		taskQueue[currentTaskIndex].executionTime += executionTimer;

#if TASKER_DEBUG_LEVEL > 1
		if(debugTimer == 0)
		{
			debugTimer=TASKER_DEBUG_RATE;

			Kernel_DebuggerEvent(DEBUG_EVENT_TASKINFO, null, DEBUG_STATUS_OK, taskQueue[debugIndex].executionTime);
			debugIndex++;			
			if(taskQueue[debugIndex].theTask == null || debugIndex >= MAX_NUMBER_OF_TASKS)
				debugIndex=0;
		}
#endif

  }
}


/***************************************************************************************************************************
	
***************************************************************************************************************************/
void SystemTimer(void)
{
unsigned char index;

	// Tick all active task timers
	for(index=0;index<MAX_NUMBER_OF_TASKS;index++)				// For all task
	{
		if(taskQueue[index].timer > 0)													// check if timer is active
		{
			taskQueue[index].timer--;
		}
	}
	
	executionTimer++;
	totalExecutionTime++;
	
#if TASKER_DEBUG_LEVEL > 1
	if(debugTimer > 0)
		debugTimer--;
#endif
	

	if(systemTimerHook != null)
	{
		systemTimerHook();
	}
}


void Kernel_SystemTimerHook(void(*hook)(void))
{
	systemTimerHook = hook;
}

/***************************************************************************************************************************
	Returns the specfied tasks index in the task queue. If the task is not in the queue -1 is returned.
***************************************************************************************************************************/
signed char TaskToIndex(task t)
{
unsigned char index;

	for(index=0;index<MAX_NUMBER_OF_TASKS;index++)
	{
		if(taskQueue[index].theTask==t)
			return index;
	}

	return -1;
}
