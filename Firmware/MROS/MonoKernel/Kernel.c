#include "Kernel.h"
#include "KernelInternals.h"
#include "Timer.h"


volatile taskStruct taskQueue[MAX_NUMBER_OF_TASKS];
unsigned char currentTaskIndex = ~0;


/***************************************************************************************************************************
	Removes all tasks from the task queue
***************************************************************************************************************************/
void Kernel_InitScheduler(void)
{
unsigned short index;

	// Clearing the task queue this way takes up less code space than using initializer ( ={0} )!
	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)																	// Clear all task index'
	{
		taskQueue[index].theTask = null;
		taskQueue[index].state = TASK_STATE_READY;
		taskQueue[index].blockingMessageQueue = null;
		taskQueue[index].blockingSemaphore = null;
		taskQueue[index].timer = 0;
	}
	Kernel_InitTimer(SystemTimer);
	NonCritical();
}


/***************************************************************************************************************************
	Loops through the task queue and calls the tasks
***************************************************************************************************************************/
void Kernel_RunScheduler(void)
{
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

		NonCritical();
		
		taskQueue[currentTaskIndex].theTask();																							// Execute the task
		
		Critical();
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
