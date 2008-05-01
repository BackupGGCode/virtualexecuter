#include <ioavr.h>
#include <Kernel.h>


#define led0																		PORTB_Bit0
#define led1																		PORTB_Bit1
#define led2																		PORTB_Bit2
#define led3																		PORTB_Bit3
#define led4																		PORTB_Bit4
#define led5																		PORTB_Bit5
#define led6																		PORTB_Bit6
#define led7																		PORTB_Bit7
#define sw0																			PINA_Bit0
#define sw1																			PINA_Bit1
#define sw2																			PINA_Bit2
#define sw3																			PINA_Bit3
#define sw4																			PINA_Bit4
#define sw5																			PINA_Bit5
#define sw6																			PINA_Bit6
#define sw7																			PINA_Bit7

#define ON																			0             // for LED control
#define OFF																			1             // for LED control

// Button states
#define UP		1
#define DOWN	0

// Application specific messages for turning on and of the LEDS
#define TURN_LED_ON															1
#define TURN_LED_OFF														2


messageQueue ledmsgs;

semaphore semaFlip=0;


/*
	A task that takes up a lot of time
*/
void workhard()
{
/*
double d=0;

	while(d<250)
		d+=0.1;
	led5=!led5;
*/
}


/*
	A task demonstrating KernelSleep() (alternativly KernelDelay())
*/
void blink()
{
	led4=!led4;
	KernelSleep(777);
//	KernelDelay(1000);
}


void LEDControl1(void)
{
	if(sw6==DOWN)
		led6=ON;
	else
		led6=OFF;
}


void LEDControl2(void)
{
	if(sw7==DOWN)
		led7=ON;
	else
		led7=OFF;
}


/*
	Demonstation of task creation and deletion.
	Also demonstrates task suspension and resumption.
*/
void UserInput(void)
{
	if(sw0==DOWN)											//if sw0 pressed
	{
		KernelCreateTask(LEDControl1);
		KernelDeleteTask(LEDControl2);
		KernelSuspendTask(blink);
		led0=ON;
		led1=OFF;
	}
	else if(sw1==DOWN)									//if sw1 pressed
	{
		KernelDeleteTask(LEDControl1);
		KernelCreateTask(LEDControl2);
		KernelResumeTask(blink);
		led0=OFF;
		led1=ON;
	}

	if(sw5==DOWN)
		KernelCreateTask(workhard);
}


/*
	Demonstrates posting messages to message queues
*/
void ledposter()
{
static bool state=false;

	if(state==false)
	{
		if(sw7==DOWN)
		{
			state=true;
			KernelPostMessage(TURN_LED_ON, &ledmsgs);
		}
	}
	else
	{
		if(sw6==DOWN)
		{
			state=false;
			KernelPostMessage(TURN_LED_OFF, &ledmsgs);
		}
	}
}


/*
	Demonstates getting messages from message queues
*/
void ledcontroller()
{
unsigned char data;

	if(KernelGetMessage(&data, &ledmsgs))
	{
		switch(data)
		{
			case TURN_LED_ON:		led5=ON;
													break;
			case TURN_LED_OFF:	led5=OFF;
													break;
		}
	}
}

void AlternativeLedController()
{
unsigned char data;

	YieldBegin
	YieldUntil(KernelGetMessage(&data, &ledmsgs))
	switch(data)
	{
		case TURN_LED_ON:		led5=ON;
												break;
		case TURN_LED_OFF:	led5=OFF;
												break;
	}
	YieldEnd
}



/*
	Demonstrates how semaphores can be used to signal from task to task
*/

void ledFlipper()
{
	if(KernelSemaphoreWait(&semaFlip))
	{
		led3=!led3;
	}
}


void AlternativeLedFlipper()
{
	YieldBegin
	YieldUntil(KernelSemaphoreWait(&semaFlip))
	led3=!led3;
	YieldEnd
}


void SwitchFlipper()
{
static bool state=false;

	if(state)
	{
		if(sw3==UP)
		{
			state=false;
			KernelSleep(20);
		}
	}
	else
	{
		if(sw3==DOWN)
		{
			state=true;
			KernelSemaphoreSignal(&semaFlip);
			KernelSleep(20);
		}
	}
}

void AlternativeSwitchFlipper()
{
	YieldBegin
	YieldUntil(sw3==DOWN)																																	// wait until key is pressed
	KernelSemaphoreSignal(&semaFlip);																											// send signal
	YieldWait(20)
	YieldWhile(sw3==DOWN)																																	// wait while key is pressed
	YieldWait(20)
	YieldEnd
}


__C_task void main(void)
{
	DDRB=0xff;                  // PORT B is outputs
	DDRA=0x00;                  // PORT A is inputs
	PORTB=0xff;                 // all LEDs off

	KernelInitScheduler();

	KernelCreateTask(UserInput);				// Add a task to queue (must have at least one task obviously)
	KernelCreateTask(blink);

	KernelCreateTask(ledposter);
//	KernelCreateTask(ledcontroller);
	KernelCreateTask(AlternativeLedController);

//	KernelCreateTask(ledFlipper);
	KernelCreateTask(AlternativeLedFlipper);
//	KernelCreateTask(SwitchFlipper);
	KernelCreateTask(AlternativeSwitchFlipper);

	KernelRunScheduler();
}
