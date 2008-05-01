#include <globals.h>
#include <Kernel.h>
#include <KernelInternals.h>


bool KernelSemaphoreWait(semaphore* s)
{
bool succes;

	Critical();
	
	if(*s == 0)
	{
		taskQueue[currentTaskIndex].blockingSemaphore=s;
		succes = false;
	}
	else
	{
		*s = *s-1;
		succes = true;
	}

	NonCritical();

#if TASKER_DEBUG_LEVEL > 0
	if(succes)
	{
		KernelDebuggerEvent(DEBUG_EVENT_SEMAPHOREWAIT, null, DEBUG_STATUS_OK, 0);
	}
	else
	{
		KernelDebuggerEvent(DEBUG_EVENT_SEMAPHOREWAIT, null, DEBUG_STATUS_SEMAPHORE_BLOCK, 0);
	}
#endif

	return succes;
}


void KernelSemaphoreSignal(semaphore* s)
{
unsigned char index;

	Critical();
	
	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].blockingSemaphore == s)
		{
			taskQueue[index].blockingSemaphore = null;
		}
	}
	
	*s = *s+1;
	
	NonCritical();

#if TASKER_DEBUG_LEVEL > 0
	KernelDebuggerEvent(DEBUG_EVENT_SEMAPHORESIGNAL, null, DEBUG_STATUS_OK, 0);
#endif
}
