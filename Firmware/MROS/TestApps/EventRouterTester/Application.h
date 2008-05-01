#include <EventRouter.h>


#define SYSTEM_CLOCK_FREQUENCY		11059200
#define SYSTEM_TICKS_PER_SECOND		1000


// events
#define Timer2							0
#define Button1							1
#define EventDummy					2

#define ACTIONS_PER_EVENT		3

#define NUMBER_OF_TIMERS		1


extern void Initialization();


extern __flash action actions[EventDummy][ACTIONS_PER_EVENT];
extern timer timers[NUMBER_OF_TIMERS];
