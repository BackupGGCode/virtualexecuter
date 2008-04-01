/*
 If TASKER_DEBUG_LEVEL is define to 1 kernel debuging will be enabled. This must be done in order to use the kernel event monitor.
*/
#define TASKER_DEBUG_LEVEL											0
#define TASKER_DEBUGGER_BAUDRATE								11			// 115200 @ 11.0592 MHz
#define BUFFERED_UART
#define RX_BUFFER_SIZE													10
#define TX_BUFFER_SIZE													300


#define MAX_NUMBER_OF_TASKS											8

#define MESSAGE_QUEUE_SIZE											8

#define SYSTEM_CLOCK_FREQUENCY									11059200
#define SYSTEM_TICKS_PER_SECOND									1000
