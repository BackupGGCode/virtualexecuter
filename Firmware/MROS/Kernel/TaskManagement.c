#include "Timer.h"
#include "Kernel.h"
#include "KernelInternals.h"


bool Kernel_CreateTask(task t)
{
unsigned char index;

	if(TaskToIndex(t) >= 0)																																// Abort if task is already in task queue
	{
#if TASKER_DEBUG_LEVEL > 0
		Kernel_DebuggerEvent(DEBUG_EVENT_CREATETASK, t, DEBUG_STATUS_ALREADY_RUNNING, 0);
#endif
		return false;
	}

	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == null)
		{
			taskQueue[index].theTask = t;																											// Add task at the end of the queue
			taskQueue[index].state = TASK_STATE_READY;
			taskQueue[index].executionTime = 0;
			taskQueue[index].blockingMessageQueue = null;
			taskQueue[index].blockingSemaphore = null;
			taskQueue[index].timer = 0;

#if TASKER_DEBUG_LEVEL > 0
			Kernel_DebuggerEvent(DEBUG_EVENT_CREATETASK, t, DEBUG_STATUS_OK, 0);
#endif

			return true;
		}
	}
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_CREATETASK, t, DEBUG_STATUS_NO_ROOM, 0);
#endif
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
			taskQueue[index].executionTime = 0;
			taskQueue[index].state = TASK_STATE_READY;
			taskQueue[index].blockingMessageQueue = null;
			taskQueue[index].blockingSemaphore = null;
			taskQueue[index].timer = 0;
#if TASKER_DEBUG_LEVEL > 0
			Kernel_DebuggerEvent(DEBUG_EVENT_DELETETASK, t, DEBUG_STATUS_OK, 0);
#endif
			return true;
		}
	}

#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_DELETETASK, t, DEBUG_STATUS_NO_TASK, 0);
#endif

	return false;
}


bool Kernel_SuspendTask(task t)
{
signed char index = TaskToIndex(t);

	if(index < 0)
	{
#if TASKER_DEBUG_LEVEL > 0
		Kernel_DebuggerEvent(DEBUG_EVENT_SUSPENDTASK, t, DEBUG_STATUS_NO_TASK, 0);
#endif
		return false;
	}
	
	taskQueue[index].state |= TASK_STATE_SUSPENDED;
	
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_SUSPENDTASK, t, DEBUG_STATUS_OK, 0);
#endif
	return true;
}


void Kernel_Suspend(void)
{
	taskQueue[currentTaskIndex].state |= TASK_STATE_SUSPENDED;
	
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_SUSPENDTASK, CurrentTask(), DEBUG_STATUS_OK, 0);
#endif
}


bool Kernel_ResumeTask(task t)
{
signed char index = TaskToIndex(t);

	if(index < 0)
	{
#if TASKER_DEBUG_LEVEL > 0
		Kernel_DebuggerEvent(DEBUG_EVENT_RESUMETASK, t, DEBUG_STATUS_NO_TASK, 0);
#endif
		return false;
	}

	taskQueue[index].state &= ~TASK_STATE_SUSPENDED;
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_RESUMETASK, t, DEBUG_STATUS_OK, 0);
#endif
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
#if TASKER_DEBUG_LEVEL > 0
			Kernel_DebuggerEvent(DEBUG_EVENT_SLEEPTASK, t, DEBUG_STATUS_OK, time);
#endif
			return true;
		}
	}
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_SLEEPTASK, t, DEBUG_STATUS_NO_TASK, time);
#endif
	return false;
}


void Kernel_Sleep(unsigned short time)
{
	if(taskQueue[currentTaskIndex].timer < time)
	{
		taskQueue[currentTaskIndex].timer = time;
	}

#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_SLEEPTASK, CurrentTask(), DEBUG_STATUS_OK, 0);
#endif
}


bool Kernel_WakeTask(task t)
{
unsigned char index;

	for(index=0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].theTask == t)
		{
			taskQueue[index].timer = 0;
#if TASKER_DEBUG_LEVEL > 0
			Kernel_DebuggerEvent(DEBUG_EVENT_WAKETASK, t, DEBUG_STATUS_OK, 0);
#endif
			return true;
		}
	}
	
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_WAKETASK, t, DEBUG_STATUS_NO_TASK, 0);
#endif
	return false;
}
