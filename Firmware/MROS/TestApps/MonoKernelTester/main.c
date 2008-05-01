#include "Globals.h"
#include <MonoKernel/Kernel.h>


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
	A task demonstrating Kernel_Sleep() (alternativly Kernel_Delay())
*/
void blink()
{
	led4=!led4;
	Kernel_Sleep(777);
//	Kernel_Delay(1000);
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
		Kernel_CreateTask(LEDControl1,10);
		Kernel_DeleteTask(LEDControl2);
		Kernel_SuspendTask(blink);
		led0=ON;
		led1=OFF;
	}
	else if(sw1==DOWN)									//if sw1 pressed
	{
		Kernel_DeleteTask(LEDControl1);
		Kernel_CreateTask(LEDControl2,11);
		Kernel_ResumeTask(blink);
		led0=OFF;
		led1=ON;
	}

	if(sw5==DOWN)
		Kernel_CreateTask(workhard, 12);
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
			Kernel_PostMessage(TURN_LED_ON, &ledmsgs);
		}
	}
	else
	{
		if(sw6==DOWN)
		{
			state=false;
			Kernel_PostMessage(TURN_LED_OFF, &ledmsgs);
		}
	}
}


/*
	Demonstates getting messages from message queues
*/
void ledcontroller()
{
unsigned char data;

	if(Kernel_GetMessage(&data, &ledmsgs))
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
	YieldUntil(Kernel_GetMessage(&data, &ledmsgs))
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
	if(Kernel_SemaphoreWait(&semaFlip))
	{
		led3=!led3;
	}
}


void AlternativeLedFlipper()
{
	YieldBegin
	YieldUntil(Kernel_SemaphoreWait(&semaFlip))
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
			Kernel_Sleep(20);
		}
	}
	else
	{
		if(sw3==DOWN)
		{
			state=true;
			Kernel_SemaphoreSignal(&semaFlip);
			Kernel_Sleep(20);
		}
	}
}

void AlternativeSwitchFlipper()
{
	YieldBegin
	YieldUntil(sw3==DOWN)																																	// wait until key is pressed
	Kernel_SemaphoreSignal(&semaFlip);																											// send signal
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

	Kernel_InitScheduler();

	Kernel_CreateTask(UserInput,1);				// Add a task to queue (must have at least one task obviously)
	Kernel_CreateTask(blink,2);

	Kernel_CreateTask(ledposter,3);
//	Kernel_CreateTask(ledcontroller,4);
	Kernel_CreateTask(AlternativeLedController,5);

//	Kernel_CreateTask(ledFlipper,6);
	Kernel_CreateTask(AlternativeLedFlipper,7);
//	Kernel_CreateTask(SwitchFlipper,8);
	Kernel_CreateTask(AlternativeSwitchFlipper,9);

	Kernel_RunScheduler();
}
