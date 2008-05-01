#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include "Commander.h"
#include <Peripherals/UART.h>
#include <strings.h>


char prompt[COMMANDER_PROMPT_LENGTH] = ">";

bool (*defaultHandler)(char* line);


void Commander_Run()
{
	static unsigned char index = 0;
	static char buffer[COMMANDER_MAX_LINE_LENGTH+1];
	static char oldBuffer[COMMANDER_MAX_LINE_LENGTH+1];
	static unsigned char oldIndex = 0;
	unsigned char data;
	unsigned char i;
	bool commandFound=false;
	
	if(UART_BytesReady())
	{
		data = UART_ReadByte();
		
		if(data == 0xff || data == 0)		// This is where all the naughty keys go... TO IGNORE LAND!
		{
			return;
		}
		else if(data == 0x0d)
		{
			UART_WriteByte(0x0d);
		}
		else if(data == 8)
		{
			if(index > 0)
			{
				UART_WriteString_P("\b \b");
				index--;
			}
		}
		else if(data == ',')
		{
			if(oldIndex > 0)
			{
				Strings_Copy(oldBuffer, buffer);
				index = oldIndex;
				oldIndex = 0;
				UART_WriteByte('\r');
				UART_WriteString(prompt);
				UART_WriteString(buffer);
			}
			else
			{
				Strings_Copy(buffer, oldBuffer);
				oldIndex = index;
				if(index > 0)
				{
					while(--index)
					{
						UART_WriteString_P("\b \b");
					}
				}
				UART_WriteByte('\r');
				UART_WriteString(prompt);
			}
		}
		else if(data == 0x0a)
		{
			LED_GREEN_OFF
			UART_WriteByte(0x0a);
			
			if(index > 0)
			{
				buffer[index++] = 0;	// add zero termination to command line string
	
				Strings_Copy(buffer, oldBuffer);
				oldIndex = index;
				
				for(i = 0; i < NumberOfCommands && commandFound == false; i++)
				{					
					if(Strings_Compare_P(commands[i].name, buffer))
					{
						commands[i].handler(buffer);
						commandFound=true;
					}
				}
				if(commandFound == false)
				{
					if(defaultHandler == null || defaultHandler(buffer) == false)
					{
						UART_WriteString_P("Unknown command\n");
					}
				}
			}
			
			index=0;
			LED_GREEN_ON
			UART_WriteString(prompt);
		}
		else
		{
			if(index < COMMANDER_MAX_LINE_LENGTH)
			{
				buffer[index++] = data;
				UART_WriteByte(data);
			}
		}
	}
}
