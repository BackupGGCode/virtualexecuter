// Debug events
#define DEBUG_EVENT_INITSCHEDULER								0			// Scheduler
#define DEBUG_EVENT_RUNSCHEDULER								1
#define DEBUG_EVENT_TASKINFO										2			// Task management
#define DEBUG_EVENT_CREATETASK									3
#define DEBUG_EVENT_DELETETASK									4
#define DEBUG_EVENT_SUSPENDTASK									5
#define DEBUG_EVENT_RESUMETASK									6
#define DEBUG_EVENT_SLEEPTASK										7
#define DEBUG_EVENT_WAKETASK										8
#define DEBUG_EVENT_DELAY												9
#define DEBUG_EVENT_SEMAPHOREWAIT								10		// Semaphores
#define DEBUG_EVENT_SEMAPHORESIGNAL							11
#define DEBUG_EVENT_POSTMESSAGE									12		// Message queues
#define DEBUG_EVENT_GETMESSAGE									13


// Debug status codes
#define DEBUG_STATUS_OK													0
#define DEBUG_STATUS_ERROR											1
#define DEBUG_STATUS_ALREADY_RUNNING						2
#define DEBUG_STATUS_NO_ROOM										3
#define DEBUG_STATUS_NO_TASK										4
#define DEBUG_STATUS_ALREADY_SUSPENDED					5
//#define DEBUG_STATUS_NOT_SUSPENDED							6
#define DEBUG_STATUS_ALREADY_SLEEPING						7
//#define DEBUG_STATUS_NOT_SLEEPING								8
#define DEBUG_STATUS_SEMAPHORE_BLOCK						9
#define DEBUG_STATUS_NO_MESSAGE									10


extern void Kernel_InitDebugger();
extern void Kernel_DebuggerEvent(unsigned char event, task target, unsigned char status, unsigned long extended);
