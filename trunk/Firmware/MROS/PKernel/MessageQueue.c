#include <globals.h>
#include <Kernel.h>
#include <KernelInternals.h>


bool KernelPostMessage(unsigned char msg, messageQueue* mq)
{
unsigned char index;

	if(mq->full)
	{
#if TASKER_DEBUG_LEVEL > 0
		KernelDebuggerEvent(DEBUG_EVENT_POSTMESSAGE, null, DEBUG_STATUS_NO_ROOM, (unsigned long)mq);
#endif
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
#if TASKER_DEBUG_LEVEL > 0
			KernelDebuggerEvent(DEBUG_EVENT_POSTMESSAGE, null, DEBUG_STATUS_OK, (unsigned long)mq);
#endif
			return true;
		}
	}
	
#if TASKER_DEBUG_LEVEL > 0
	KernelDebuggerEvent(DEBUG_EVENT_POSTMESSAGE, null, DEBUG_STATUS_OK, (unsigned long)mq);
#endif
	return true;
}


bool KernelGetMessage(unsigned char* msg, messageQueue* mq)
{
	if(mq->full == false && mq->head == mq->tail)
	{
		taskQueue[currentTaskIndex].blockingMessageQueue = mq;
#if TASKER_DEBUG_LEVEL > 0
		KernelDebuggerEvent(DEBUG_EVENT_GETMESSAGE, null, DEBUG_STATUS_NO_MESSAGE, (unsigned long)mq);
#endif
		return false;
	}
	
	*msg = mq->data[mq->tail];
	
	if(++mq->tail >= MESSAGE_QUEUE_SIZE)
	{
		mq->tail = 0;
	}
	mq->full = false;
	
#if TASKER_DEBUG_LEVEL > 0
	KernelDebuggerEvent(DEBUG_EVENT_GETMESSAGE, null, DEBUG_STATUS_OK, (unsigned long)mq);
#endif
	return true;
}
