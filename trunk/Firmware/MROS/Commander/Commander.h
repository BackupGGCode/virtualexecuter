#include <Globals.h>


#define COMMANDER_PROMPT_LENGTH									4


typedef struct
{
	string* name;
	void (*handler)(char* line);
	string* helpText;
} command;

extern __flash command commands[];
extern unsigned short NumberOfCommands;
extern bool (*defaultHandler)(char* line);

extern char prompt[];


extern void Commander_Run();
