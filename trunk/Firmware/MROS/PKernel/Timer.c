#include <globals.h>
#include <Timer.h>
#include <KernelInternals.h>


volatile static unsigned short timerTicks = 0;

static void (*timerEventHandler)(void) = null;


void KernelInitTimer(void (*handler)(void))
{
#if defined(__IOM8_H)
	TCCR0 = TIMER_PRESCALER;
	TCNT0 = (unsigned char)(0-TIMER_RELOAD);
	TIMSK |= (1<<TOIE0);
#elif defined(__IOM32_H)
	TCCR0 = (1<<WGM01)|TIMER_PRESCALER;
	OCR0 = TIMER_RELOAD;
	TIMSK |= (1<<OCIE0);
#elif defined(__IOM128_H) || defined(__IOM64_H)
	TCCR0 = (1<<WGM01)|TIMER_PRESCALER;
	OCR0 = TIMER_RELOAD;
	TIMSK |= (1<<OCIE0);
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif
	timerEventHandler = handler;
}


void KernelDelay(unsigned short time)
{
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
#elif defined(__IOM32_H) || defined(__IOM128_H) || defined(__IOM64_H)
#pragma vector=TIMER0_COMP_vect
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif
__interrupt void KernelTimerInterruptHandler(void)
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
