#include <globals.h>
#include <EventRouter.h>
#include <Application.h>
#include <Timer.h>


static unsigned char ticks=0;


void EventEngine();
void TimerEngine();

bool EventReady();
event GetEvent();


void TimerTicker()
{
	ticks++;
}


void main()
{
	KernelInitTimer(TimerTicker);

	Initialization();
	
	SREG|=0x80;
	
	while(true)
	{
		EventEngine();
		
		if(ticks)
		{
			ticks--;
			TimerEngine();
		}
	}
}


void EventEngine()
{
event e;
unsigned char i;

	while(EventReady())
	{
		e=GetEvent();
		
		for(i=0;i<ACTIONS_PER_EVENT;i++)
		{
			if(actions[e][i])
				actions[e][i](e);
			else
				i=ACTIONS_PER_EVENT;
		}
	}
}


void TimerEngine()
{
unsigned char i;

	for(i=0;i<NUMBER_OF_TIMERS;i++)
	{
		if(timers[i].current>0)
		{
			if(--timers[i].current==0)
			{
				timers[i].current=timers[i].interval;
				PostEvent(timers[i].theEvent);
			}
		}
	}
}







#define EVENT_QUEUE_SIZE		20
event eventQueue[EVENT_QUEUE_SIZE];
unsigned char eventQueueIn=0;
unsigned char eventQueueOut=0;
bool eventQueueFull=false;


void PostEvent(event e)
{
	if(eventQueueFull)
		return;
	eventQueue[eventQueueIn]=e;
	if(++eventQueueIn==EVENT_QUEUE_SIZE)
		eventQueueIn=0;
	if(eventQueueIn==eventQueueOut)
		eventQueueFull=true;
}

bool EventReady()
{
	if(eventQueueFull || (eventQueueIn != eventQueueOut))
		return true;
	else
		return false;
}

event GetEvent()
{
event e=255;

	if(EventReady())
	{
		e=eventQueue[eventQueueOut];
		eventQueueFull=false;
		if(++eventQueueOut==EVENT_QUEUE_SIZE)
			eventQueueOut=0;
	}
	
	return e;
}
