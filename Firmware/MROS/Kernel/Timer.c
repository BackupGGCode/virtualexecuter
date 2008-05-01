#include "Timer.h"
#include "KernelInternals.h"


volatile static unsigned short timerTicks = 0;

static void (*timerEventHandler)(void) = null;


void Kernel_InitTimer(void (*handler)(void))
{
#if defined(__IOM8_H)
	TCCR0 = TIMER_PRESCALER;
	TCNT0 = (unsigned char)(0 - TIMER_RELOAD);
	TIMSK |= (1 << TOIE0);
#elif defined(__IOM32_H) || defined(__IOM162_H)
	TCCR0 = (1<<WGM01) | TIMER_PRESCALER;
	OCR0 = TIMER_RELOAD;
	TIMSK |= (1 << OCIE0);
#elif defined(__IOM128_H) || defined(__IOM64_H)
	TCCR0 = (1 << WGM01) | TIMER_PRESCALER;
	OCR0 = TIMER_RELOAD;
	TIMSK |= (1 << OCIE0);
#elif defined(__IOM168_H)
	TCCR0A = (1<<WGM01);
	TCCR0B = TIMER_PRESCALER;
	OCR0A = TIMER_RELOAD;
	TIMSK0 |= (1 << OCIE0A);
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif
	timerEventHandler = handler;
}


void Kernel_Delay(unsigned short time)
{
#if TASKER_DEBUG_LEVEL > 0
	Kernel_DebuggerEvent(DEBUG_EVENT_DELAY, null, DEBUG_STATUS_OK, time);
#endif

	Critical();
	
	timerTicks = time;
	while(timerTicks > 0)
	{
		NonCritical();
		__no_operation();
		Critical();
	}
	NonCritical();
}


#if defined(__IOM8_H)
#pragma vector=TIMER0_OVF_vect
#elif defined(__IOM32_H) || defined(__IOM128_H) || defined(__IOM64_H) || defined(__IOM162_H)
#pragma vector=TIMER0_COMP_vect
#elif defined(__IOM168_H)
#pragma vector=TIMER0_COMPA_vect
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif
__interrupt void Kernel_TimerInterruptHandler(void)
{
#if defined(__IOM8_H)
	TCNT0 = (unsigned char)(0-TIMER_RELOAD);
#endif

	if(timerTicks > 0)
	{
		timerTicks--;
	}

	if(timerEventHandler != null)
	{
		timerEventHandler();
	}
}
