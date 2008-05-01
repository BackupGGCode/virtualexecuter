#include <Menu/Menu.h>


MenuItem* currentMenuItem;
void(*update)(MenuItem* menuItem);


void Menu_Init(MenuItem* theMenu, void(*menuWriter)(MenuItem* menuItem))
{
	currentMenuItem = theMenu;
	update = menuWriter;
	update(currentMenuItem);
}


void Menu_Refresh()
{
	update(currentMenuItem);
}


void Menu_Next()
{
	if((*currentMenuItem).next != null)
	{
		currentMenuItem = (MenuItem*)((*currentMenuItem).next);
		update(currentMenuItem);
	}
}


void Menu_Previous()
{
	if((*currentMenuItem).previous != null)
	{
		currentMenuItem = (MenuItem*)((*currentMenuItem).previous);
		update(currentMenuItem);
	}
}


void Menu_Enter()
{
	if((*currentMenuItem).child != null)
	{
		currentMenuItem = (MenuItem*)((*currentMenuItem).child);
		update(currentMenuItem);
	}
}


void Menu_Exit()
{
	if((*currentMenuItem).parent != null)
	{
		currentMenuItem = (MenuItem*)((*currentMenuItem).parent);
		update(currentMenuItem);
	}
}

	
void Menu_Increment()
{
bool refresh = true;

	if(currentMenuItem->incrementFunction != null)
	{
		refresh = currentMenuItem->incrementFunction();
	}
	else
	{
		switch(currentMenuItem->type)
		{
			case SINT8:		*((signed char*)(currentMenuItem->variable))+=1;
										break;
			case UINT8:		*((unsigned char*)(currentMenuItem->variable))+=1;
										break;
			case SINT16:	*((signed short*)(currentMenuItem->variable))+=1;
										break;
			case UINT16:	*((unsigned short*)(currentMenuItem->variable))+=1;
										break;
			case SINT32:	*((signed long*)(currentMenuItem->variable))+=1;
										break;
			case UINT32:	*((unsigned long*)(currentMenuItem->variable))+=1;
										break;
			case FLOAT:		*((float*)(currentMenuItem->variable))+=1;
										break;
			case DOUBLE:	*((double*)(currentMenuItem->variable))+=1;
										break;
		}
	}
	
	if(refresh)
	{
		update(currentMenuItem);
	}
}


void Menu_Decrement()
{
bool refresh = false;

	if(currentMenuItem->decrementFunction != null)
	{
		refresh = currentMenuItem->decrementFunction();
	}
	else
	{
		switch(currentMenuItem->type)
		{
			case SINT8:		*((signed char*)(currentMenuItem->variable))-=1;
										break;
			case UINT8:		*((unsigned char*)(currentMenuItem->variable))-=1;
										break;
			case SINT16:	*((signed short*)(currentMenuItem->variable))-=1;
										break;
			case UINT16:	*((unsigned short*)(currentMenuItem->variable))-=1;
										break;
			case SINT32:	*((signed long*)(currentMenuItem->variable))-=1;
										break;
			case UINT32:	*((unsigned long*)(currentMenuItem->variable))-=1;
										break;
			case FLOAT:		*((float*)(currentMenuItem->variable))-=1;
										break;
			case DOUBLE:	*((double*)(currentMenuItem->variable))-=1;
										break;
		}
	}
	
	if(refresh)
	{
		update(currentMenuItem);
	}
}
