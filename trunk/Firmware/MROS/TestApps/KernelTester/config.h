/*
	Debugger levels
		0 - Disabled. No debugger code will be implemented.
		1 - All kernel API functions sends an event message.
		2 - Same as 1 but also generates a DEBUG_EVENT_TASKINFO message every TASKER_DEBUG_RATE tick.
*/
#define TASKER_DEBUG_LEVEL											0
#define TASKER_DEBUG_RATE												10
#define DEBUGGER_BAUDRATE												11			// 115200 @ 11.0592 MHz

#define BUFFERED_UART
#define RX_BUFFER_SIZE													10
#define TX_BUFFER_SIZE													300

#define SYSTEM_CLOCK_FREQUENCY									11059200
#define SYSTEM_TICKS_PER_SECOND									1000

#define MAX_NUMBER_OF_TASKS											8

#define MESSAGE_QUEUE_SIZE											8


/*
	To enable dynamic memory allocation set HEAP_SIZE to a value greater than 0.
	The memory blocks used for maintaining the heap also take up heap space:
		Small allocation units take up 3 bytes.
		Big allocation units take up 4 bytes.
*/
#define HEAP_SIZE																1000
