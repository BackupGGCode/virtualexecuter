#define MAX_STRING_LENGTH


typedef struct
{
	string command;
	void (*handler)(unsigned char*);
} CommandHandler;


const CommandHandler commands[NUMBER_OF_COMMANDS]={"dir", CmdDir};


void CLI()
{
static unsigned char index=0;
static unsigned char buffer[MAX_STRING_LENGTH];

unsigned char temp;

	if(DataReady())
	{
		temp=getc();
		
		if(index < MAX_STRING_LENGTH)
			buffer[index++]=temp;
		
		if(temp==13)
		{
			
		}
	}
}
