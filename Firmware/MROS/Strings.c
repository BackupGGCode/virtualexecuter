#include "Globals.h"


bool Strings_Compare(char* string1, char* string2)
{
	while(*string1 != 0)
	{
		if(*string1 != *string2)
		{
			return false;
		}
		string1++;
		string2++;
	}
	
	if(*string2 != 0 && *string2 != ' ')
	{
		return false;
	}
	
	return true;
}

bool Strings_Compare_P(string* string1, char* string2)
{
	while(*string1 != 0)
	{
		if(*string1 != *string2)
		{
			return false;
		}
		string1++;
		string2++;
	}
	
	if(*string2 != 0 && *string2 != ' ')
	{
		return false;
	}
	
	return true;
}


void Strings_Copy(char* source, char* destination)
{
	while(*source != 0)
	{
		*destination = *source;
		destination++;
		source++;
	}
}

void Strings_Copy_P(string* source, char* destination)
{
	while(*source != 0)
	{
		*destination = *source;
		destination++;
		source++;
	}
}


char* Strings_GetNextWord(char* string1)
{
	while(*string1)
	{
		if(*string1 == ' ')
		{
			while(*string1 == ' ')
			{
				string1++;
			}
			
			return string1;
		}
		
		string1++;
	}
	
	return null;
}

unsigned char Strings_Length(char* string1)
{
unsigned char length = 0;

	while(*string1 != 0)
	{
		length++;
		string1++;
	}
	
	return length;
}


unsigned char Strings_Length_P(string* string1)
{
unsigned char length = 0;

	while(*string1 != 0)
	{
		length++;
		string1++;
	}
	
	return length;
}


unsigned long Strings_ReadValueUnsigned(char* string1)
{
unsigned char i = 0;
unsigned long value = 0;
	
	while('9' >= string1[i] && string1[i] >= '0')
	{
		value *= 10;
		value += (string1[i] - '0');
		i++;
	}
	
	return value;
}
