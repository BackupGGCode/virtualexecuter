#include <globals.h>
#include <Kernel.h>
#include <KernelInternals.h>
#include <Timer.h>


volatile taskStruct taskQueue[MAX_NUMBER_OF_TASKS];
unsigned char currentTaskIndex = ~0;
unsigned short executionTimer;
unsigned long totalExecutionTime;


/***************************************************************************************************************************
	Removes all tasks from the task queue
***************************************************************************************************************************/
void KernelInitScheduler(void)
{
unsigned short index;

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
	KernelInitTimer(SystemTimer);
	NonCritical();
}


/***************************************************************************************************************************
	Loops through the task queue and calls the tasks
***************************************************************************************************************************/
void KernelRunScheduler(void)
{
static unsigned char tcnt;

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
		
		executionTimer *= OCR0;
		if(tcnt >= TCNT0)
			executionTimer += ((OCR0+1)-tcnt)+TCNT0;
		else
			executionTimer += TCNT0-tcnt;
		
		totalExecutionTime += executionTimer;
		
		taskQueue[currentTaskIndex].executionTime += executionTimer;
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
