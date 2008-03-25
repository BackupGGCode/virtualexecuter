#define BUFFERED_UART
#define RX_BUFFER_SIZE													20
#define TX_BUFFER_SIZE													60

#define SYSTEM_CLOCK_FREQUENCY									11059200
#define SYSTEM_TICKS_PER_SECOND									1000

#define MAX_NUMBER_OF_TASKS											20

#define MESSAGE_QUEUE_SIZE											8


// Commander config
#define COMMANDER_MAX_LINE_LENGTH								50
//#define COMMANDER_COMMAND_COUNT									12

#define SEND_CRLF																1

#define HEAP_SIZE																500

#define VX_HEAP_SIZE														HEAP_SIZE

#define LED_GREEN_ON														{PORTB|=(1<<6);}
#define LED_GREEN_OFF														{PORTB&=~(1<<6);}
#define LED_GREEN_TOGGLE												{PORTB^=(1<<6);}
#define LED_YELLOW_ON														{PORTB|=(1<<7);}
#define LED_YELLOW_OFF													{PORTB&=~(1<<7);}
#define LED_YELLOW_TOGGLE												{PORTB^=(1<<7);}
#define LED_RED_ON															{PORTG|=(1<<3);}
#define LED_RED_OFF															{PORTG&=~(1<<3);}
#define LED_RED_TOGGLE													{PORTG^=(1<<3);}
