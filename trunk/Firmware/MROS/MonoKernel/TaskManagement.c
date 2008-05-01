#include "Timer.h"
#include "Kernel.h"
#include "KernelInternals.h"


bool Kernel_CreateTask(task t, unsigned char priority)
{
unsigned char index;

	if(TaskToIndex(t) >= 0)																																// Abort if task is already in task queue
	{
		return false;
	}

	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == null)
		{
			taskQueue[index].theTask = t;																											// Add task at the end of the queue
			taskQueue[index].priority = priority;
			taskQueue[index].state = TASK_STATE_READY;
			taskQueue[index].blockingMessageQueue = null;
			taskQueue[index].blockingSemaphore = null;
			taskQueue[index].timer = 0;

			return true;
		}
	}
	
  return false;
}


bool Kernel_DeleteTask(task t)
{
unsigned char index;

	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == t)																										// The task has been found
		{
			while((index+1) < MAX_NUMBER_OF_TASKS && taskQueue[index].theTask != null)					// and move the rest of the queue one step up
			{
				taskQueue[index] = taskQueue[index+1];																								// move it
				index++;
			}
			taskQueue[index].theTask = null;																										// clear the last task entry
			taskQueue[index].priority = 0;
			taskQueue[index].state = TASK_STATE_READY;
			taskQueue[index].blockingMessageQueue = null;
			taskQueue[index].blockingSemaphore = null;
			taskQueue[index].timer = 0;
			
			return true;
		}
	}

	return false;
}


bool Kernel_SuspendTask(task t)
{
signed char index = TaskToIndex(t);

	if(index < 0)
	{
		return false;
	}
	
	taskQueue[index].state |= TASK_STATE_SUSPENDED;
	
	return true;
}


void Kernel_Suspend(void)
{
	taskQueue[currentTaskIndex].state |= TASK_STATE_SUSPENDED;
}


bool Kernel_ResumeTask(task t)
{
signed char index = TaskToIndex(t);

	if(index < 0)
	{
		return false;
	}

	taskQueue[index].state &= ~TASK_STATE_SUSPENDED;
	
	return true;
}


bool Kernel_SleepTask(task t, unsigned short time)
{
unsigned char index;

	for(index=0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == t)
		{
			if(taskQueue[index].timer < time)
			{
				taskQueue[index].timer = time;
			}
			return true;
		}
	}
	
	return false;
}


void Kernel_Sleep(unsigned short time)
{
	if(taskQueue[currentTaskIndex].timer < time)
	{
		taskQueue[currentTaskIndex].timer = time;
	}
}


bool Kernel_WakeTask(task t)
{
unsigned char index;

	for(index=0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == t)
		{
			taskQueue[index].timer = 0;
			return true;
		}
	}
	
	return false;
}
