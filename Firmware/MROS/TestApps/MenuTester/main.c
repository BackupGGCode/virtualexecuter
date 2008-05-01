#include <Menu/Menu.h>
#include <UART.h>
#include <Kernel/Kernel.h>


unsigned char testValue=0;

bool inc()
{
	if(testValue<10)
	{
		testValue++;
		return true;
	}
	else
	{
		return false;
	}
}


MenuItem mainMenu[], settingsMenu[], infoMenu[], extrasMenu[];

MenuItem mainMenu[]={			{"Settings",&mainMenu[2],&mainMenu[1],null,&settingsMenu[0],VOID,null,null,null},
													{"Info",&mainMenu[0],&mainMenu[2],null,&infoMenu[0],VOID,null,null,null},
													{"Extras",&mainMenu[1],&mainMenu[0],null,&extrasMenu[0],VOID,null,null,null}};

MenuItem settingsMenu[]={	{"Contrast",null,&settingsMenu[1],&mainMenu[0],null,VOID,null,null,null},
													{"Volume",&settingsMenu[0],&settingsMenu[2],&mainMenu[0],null,VOID,null,null,null},
													{"Key timeout",&settingsMenu[1],null,&mainMenu[0],null,VOID,null,null,null}};

MenuItem infoMenu[]={			{"Power cycles",null,&infoMenu[1],&mainMenu[1],null,VOID,null,null,null},
													{"Firmware version",&infoMenu[0],&infoMenu[2],&mainMenu[1],null,VOID,null,null,null},
													{"Production date",&infoMenu[1],null,&mainMenu[1],null,VOID,null,null,null}};

MenuItem extrasMenu[]={		{"Stuff",null,&extrasMenu[1],&mainMenu[2],null,VOID,null,null,null},
													{"Time",&extrasMenu[0],&extrasMenu[2],&mainMenu[2],null,VOID,null,null,null},
													{"Date",&extrasMenu[1],null,&mainMenu[2],null,UINT8,&testValue,&inc,null}};


void CheckKeys()
{
	YieldBegin
	YieldUntil(UART_BytesReady())
	switch(UART_ReadByte())
	{
		case '+':						Menu_Increment();
												break;
		case '-':						Menu_Decrement();
												break;
		case '8':						Menu_Previous();
												break;
		case '2':						Menu_Next();
												break;
		case '6':						Menu_Enter();
												break;
		case '4':						Menu_Exit();
												break;
	}
	YieldEnd
}


void MenuUpdate(MenuItem* menuItem)
{
//	UART_WriteByte(27);
	UART_WriteString_P(menuItem->text);
	switch(menuItem->type)
	{
		case UINT8:		UART_WriteValueUnsignedChar(*((unsigned char*)menuItem->variable));
									break;
	}
}


void main()
{
	UART_Init(11);
	Menu_Init(mainMenu, MenuUpdate);

//	UART_WriteString_P("YAYYAY");

	Kernel_InitScheduler();
	Kernel_CreateTask(CheckKeys);
	Kernel_RunScheduler();
}
