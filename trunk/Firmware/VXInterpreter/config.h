#define BUFFERED_UART
#define RX_BUFFER_SIZE													20
#define TX_BUFFER_SIZE													20

#define SYSTEM_CLOCK_FREQUENCY									11059200
#define SYSTEM_TICKS_PER_SECOND									1000

#define MAX_NUMBER_OF_TASKS											10

#define MESSAGE_QUEUE_SIZE											8


// Commander config
#define COMMANDER_MAX_LINE_LENGTH								50
#define COMMANDER_COMMAND_COUNT									8

#define SEND_CRLF																1

#define HEAP_SIZE																500

#define VX_HEAP_SIZE														HEAP_SIZE
