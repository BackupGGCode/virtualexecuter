#include <Globals.h>
#include "Kernel.h"
#include "KernelInternals.h"


bool Kernel_PostMessage(unsigned char msg, messageQueue* mq)
{
unsigned char index;

	if(mq->full)
	{
		return false;
	}
	
	mq->data[mq->head] = msg;
	
	if(++mq->head >= MESSAGE_QUEUE_SIZE)
	{
		mq->head = 0;
	}
	
	for(index = 0; index < MAX_NUMBER_OF_TASKS; index++)
	{
		if(taskQueue[index].blockingMessageQueue == mq)
		{
			taskQueue[index].blockingMessageQueue = null;
			return true;
		}
	}
	
	return true;
}


bool Kernel_GetMessage(unsigned char* msg, messageQueue* mq)
{
	if(mq->full == false && mq->head == mq->tail)
	{
		taskQueue[currentTaskIndex].blockingMessageQueue = mq;
		return false;
	}
	
	*msg = mq->data[mq->tail];
	
	if(++mq->tail >= MESSAGE_QUEUE_SIZE)
	{
		mq->tail = 0;
	}
	mq->full = false;
	
	return true;
}
