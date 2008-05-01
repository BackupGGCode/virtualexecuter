#include <ioavr.h>
#include <config.h>


#ifndef SYSTEM_CLOCK_FREQUENCY
#error 'SYSTEM_CLOCK_FREQUENCY' must be defined when using MROS_Kernel_Timer!
#endif

#ifndef SYSTEM_TICKS_PER_SECOND
#error 'SYSTEM_TICKS_PER_SECOND' must be defined when using MROS_Kernel_Timer!
#endif

#if defined(__IOM8_H) || defined(__IOM32_H)
#define TIMER_PRESCALER_1												0x01
#define TIMER_PRESCALER_8												0x02
#define TIMER_PRESCALER_64											0x03
#define TIMER_PRESCALER_256											0x04
#define TIMER_PRESCALER_1024										0x05
#elif defined(__IOM128_H) || defined(__IOM64_H)
#define TIMER_PRESCALER_1												0x01
#define TIMER_PRESCALER_8												0x02
#define TIMER_PRESCALER_32											0x03
#define TIMER_PRESCALER_64											0x04
#define TIMER_PRESCALER_128											0x05
#define TIMER_PRESCALER_256											0x06
#define TIMER_PRESCALER_1024										0x07
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif


#if defined(__IOM8_H) || defined(__IOM32_H)
#if ((SYSTEM_CLOCK_FREQUENCY/1L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_1
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/1L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/8L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_8
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/8L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/64L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_64
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/64L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/256L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_256
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/256L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/1024L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_1024
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/1024L)/SYSTEM_TICKS_PER_SECOND)
#else
#error No prescaler/reload setting could be found for MROS_Kernel_Timer! Check 'SYSTEM_CLOCK_FREQUENCY' and 'SYSTEM_TICKS_PER_SECOND' settings.
#endif
#elif defined(__IOM128_H) || defined(__IOM64_H)
#if ((SYSTEM_CLOCK_FREQUENCY/1L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_1
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/1L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/8L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_8
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/8L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/32L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_32
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/32L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/64L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_64
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/64L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/128L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_128
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/128L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/256L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_256
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/256L)/SYSTEM_TICKS_PER_SECOND)
#elif ((SYSTEM_CLOCK_FREQUENCY/1024L)/SYSTEM_TICKS_PER_SECOND)<256L
#define TIMER_PRESCALER													TIMER_PRESCALER_1024
#define TIMER_RELOAD														((SYSTEM_CLOCK_FREQUENCY/1024L)/SYSTEM_TICKS_PER_SECOND)
#else
#error No prescaler/reload setting could be found for MROS_Kernel_Timer! Check 'SYSTEM_CLOCK_FREQUENCY' and 'SYSTEM_TICKS_PER_SECOND' settings.
#endif
#else
#error MROS_Kernel_Timer does not support the selected processor!
#endif


extern void Kernel_InitTimer(void (*handler)(void));
extern void Kernel_Delay(unsigned short time);
