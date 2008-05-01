#include <Globals.h>
#include "Kernel.h"
#include "KernelInternals.h"


bool Kernel_SemaphoreWait(semaphore* s)
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

	return succes;
}


void Kernel_SemaphoreSignal(semaphore* s)
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
}
