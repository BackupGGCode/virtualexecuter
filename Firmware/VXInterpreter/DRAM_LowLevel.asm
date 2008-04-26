#include "ioavr.h"


NAME SlicerKernel


/***************************************************************************************************************************
	Public symbols
***************************************************************************************************************************/

// unsigned long TimerTicks;
//PUBLIC TimerTicks


/***************************************************************************************************************************
	TaskQueue structure:
		TaskID ID
		unsigned int ContextPointer
		unsigned int SleepTimer
***************************************************************************************************************************/

#define TASK_QUEUE_ENTRY_SIZE											(1+2+2)
#define TASK_ID_OFFSET														0						// Size in bytes: 1
#define TASK_CONTEXT_OFFSET												1						//                2
#define TASK_SLEEP_OFFSET													3						//                2

#define TASK_ENTRY_EMPTY													0
/*
RSEG NEAR_Z :DATA

TaskQueue:
	DS8		(__MAXIMUM_NUMBER_OF_TASKS*TASK_QUEUE_ENTRY_SIZE)
NextTaskID:																	// First TaskID to test when creating a new task
	DS8		1
NextTaskIndex:															// Next available Task Queue entry
	DS8		1
NumberOfTasks:															// Number of tasks in the Task Queue
	DS8		1
CurrentTaskIndex:														// Index in the Task Queue of current task
	DS8		1
pCurrentTask:																// Pointer to the current task in the Task Queue
	DS16	1
TimerTicks:																	// TimerTicks since kernel was started - max 49 days at 1000 ticks/second
	DS32	1

// Add in version 2
LockList:																		// List of locks
	DS8		__MAXIMUM_NUMBER_OF_LOCKS
*/
/***************************************************************************************************************************
	Macros
***************************************************************************************************************************/
/*
mSaveContext MACRO
	push	r0																	// Use post-decrement
	in		r0, SREG
	cli
	push	r0
	push	r1
	push	r2
	push	r3
	push	r4
	push	r5
	push	r6
	push	r7
	push	r8
	push	r9
	push	r10
	push	r11
	push	r12
	push	r13
	push	r14
	push	r15
	push	r16
	push	r17
	push	r18
	push	r19
	push	r20
	push	r21
	push	r22
	push	r23
	push	r24
	push	r25
	push	r26
	push	r27
	push	r28
	push	r29
	push	r30																	// Skip Y - saved in the Task Queue
	push	r31
	
	// Point to context structure and save hardware stack pointer to TCF
	lds		ZL, pCurrentTask										// Point to current task entry
	lds		ZH, pCurrentTask+1
	in		r0, SPL
	in		r1, SPH
	std		Z+TASK_CONTEXT_OFFSET, r0						// Save current hardware stack pointer to Task Context Frame
	std		Z+(TASK_CONTEXT_OFFSET+1), r1
	ENDM



mLoadContext MACRO
	lds		ZL, pCurrentTask										// Point to current task entry
	lds		ZH, pCurrentTask+1
	ldd		r0, Z+TASK_CONTEXT_OFFSET						// Load previous stack pointer from Task Context Frame
	ldd		r1, Z+(TASK_CONTEXT_OFFSET+1)
	out		SPL, r0
	out		SPH, r1

	pop		r31
	pop		r30
	pop		r29
	pop		r28
	pop		r27
	pop		r26
	pop		r25
	pop		r24
	pop		r23
	pop		r22
	pop		r21
	pop		r20
	pop		r19
	pop		r18
	pop		r17
	pop		r16
	pop		r15
	pop		r14
	pop		r13
	pop		r12
	pop		r11
	pop		r10
	pop		r9
	pop		r8
	pop		r7
	pop		r6
	pop		r5
	pop		r4
	pop		r3
	pop		r2
	pop		r1
	pop		r0
	out		SREG, r0
	pop		r0
	ENDM


// Convert TaskID (r16) to a pointer into the TaskQueue (Z). If the task was found then (r17!=0) and (r16==r18)
// Modifies: r17, r18, r30, r31
mGetTask	MACRO
	ldi		r17, __MAXIMUM_NUMBER_OF_TASKS
	ldi		ZL, low(TaskQueue)
	ldi		ZH, high(TaskQueue)
mGetTask_Compare:
	ld		r18, Z
	cp		r16, r18
	breq	mGetTask_Found
mGetTask_TryNext:
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE
	dec		r17
	brne	mGetTask_Compare
mGetTask_Found:
	ENDM

// Convert TaskID (r16) to a pointer into the TaskQueue (Z). If the task was found then (r17!=0) and (r16==r18)
// Modifies: r17, r18, r30, r31
mGetTask2	MACRO
	ldi		r17, __MAXIMUM_NUMBER_OF_TASKS
	ldi		ZL, low(TaskQueue)
	ldi		ZH, high(TaskQueue)
mGetTask2_Compare:
	ld		r18, Z
	cp		r16, r18
	breq	mGetTask2_Found
mGetTask2_TryNext:
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE
	dec		r17
	brne	mGetTask2_Compare
mGetTask2_Found:
	ENDM
*/
/***************************************************************************************************************************
	Interrupt vector for the RTC
***************************************************************************************************************************/
/*COMMON INTVEC :CODE
ORG TIMER0_COMP_vect

kernelRTC_InterruptVector:
	jmp		kernelRTC
*/
/***************************************************************************************************************************
	Program code
***************************************************************************************************************************/
RSEG CODE :CODE


/***************************************************************************************************************************
RTC Interrupt rutine
***************************************************************************************************************************/
/*kernelRTC:
	push	r31
	push	r30
	push	r29
	push	r28
	push	r27
	push	r26
	lds		r28, TimerTicks
	lds		r29, TimerTicks+1
	lds		r30, TimerTicks+2
	lds		r31, TimerTicks+3
	ldi		r26, 1
	ldi		r27, 0
	add		r28, r26
	adc		r29, r27
	adc		r30, r27
	adc		r31, r27
	sts		TimerTicks, r28
	sts		TimerTicks+1, r29
	sts		TimerTicks+2, r30
	sts		TimerTicks+3, r31
	pop		r26
	pop		r27
	pop		r28
	pop		r29
	pop		r30
	pop		r31

	mSaveContext
	
kernelRTC_SleepTimers:
	ldi		ZL, low(TaskQueue)									// Point to first entry
	ldi		ZH, high(TaskQueue)
	ldi		r18, __MAXIMUM_NUMBER_OF_TASKS
	clr		r19
kernelRTC_SleepTimersCompare:
	ldd		r24, Z+TASK_SLEEP_OFFSET
	ldd		r25, Z+TASK_SLEEP_OFFSET+1
	cp		r24, r19
	cpc		r25, r19
	breq	kernelRTC_SleepTimersNext
	sbiw	r25:r24, 1
	std		Z+TASK_SLEEP_OFFSET, r24
	std		Z+TASK_SLEEP_OFFSET+1, r25
kernelRTC_SleepTimersNext:
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE
	dec		r18
	brne	kernelRTC_SleepTimersCompare
	
kernelRTC_FindNextTask:
	lds		ZL, pCurrentTask										// Point to current task entry
	lds		ZH, pCurrentTask+1
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE				// Point to next Task Entry
	lds		r17, CurrentTaskIndex
	inc		r17

	ld		r16, Z
	cpi		r16, TASK_ENTRY_EMPTY								// End of Task Queue reached?
	brne	kernelRTC_NextEntryFound						// 
kernelRTC_PointToFirstEntry:
	ldi		ZL, low(TaskQueue)									// Point to first entry
	ldi		ZH, high(TaskQueue)
	ldi		r17, 0
	rjmp	kernelRTC_StoreEntry
kernelRTC_NextEntryFound:
	cpi		r17, __MAXIMUM_NUMBER_OF_TASKS
	breq	kernelRTC_PointToFirstEntry
kernelRTC_StoreEntry:
	sts		pCurrentTask, ZL										// Update CurrentTaskPointer
	sts		pCurrentTask+1, ZH
	sts		CurrentTaskIndex, r17

kernelRTC_CheckSleep:
	clr		r19
	ldd		r24, Z+TASK_SLEEP_OFFSET
	ldd		r25, Z+TASK_SLEEP_OFFSET+1
	cp		r24, r19
	cpc		r25, r19
	brne	kernelRTC_FindNextTask

kernelRTC_LoadNewContext:
	mLoadContext

	reti*/
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelInit(void);
***************************************************************************************************************************/
/*kernelInit:
// Clear the TaskQueue
	ldi		r16, 1															// Next avaibable TaskID is 1 [1...MAX]
	sts		NextTaskID, r16
	clr		r16
	sts		NextTaskIndex, r16									// Next task is to be placed at index 0
	sts		NumberOfTasks, r16									// No tasks right now
	sts		CurrentTaskIndex, r16								// Set to first entry in TaskQueue
	ldi		ZL, low(TaskQueue)									// Set task pointer to start of task queue
	ldi		ZH, high(TaskQueue)
	sts		pCurrentTask, ZL
	sts		pCurrentTask+1, ZH
	ldi		r17, (__MAXIMUM_NUMBER_OF_TASKS*TASK_QUEUE_ENTRY_SIZE)
kernelInit_ClearingTaskQueue:
	st		Z+, r16
	dec		r17
	brne	kernelInit_ClearingTaskQueue

// Prepare the RTC - *** HARDWARE DEPENDENT ***
	ldi		r16, (1<<WGM01)|__PRESCALER
	out		TCCR0, r16
	ldi		r16, __RTC_RELOAD
	out		OCR0, r16
	in		r16, TIMSK
	sbr		r16, (1<<OCIE0)
	out		TIMSK, r16
	
// Clear LockList - Add in version 2
kernelInit_ClearLockList:
	clr		r16
	ldi		r17, __MAXIMUM_NUMBER_OF_LOCKS
	ldi		ZL, low(LockList)
	ldi		ZH, high(LockList)
kernelInit_ClearLockList_Loop:
	st		Z+, r16
	dec		r17
	brne	kernelInit_ClearLockList_Loop
	
	ret*/
/**************************************************************************************************************************/

#define DATA_IN																	PIND
#define DATA_OUT																PORTD
#define DATA_DDR																DDRD
#define ADR_PORT																PORTA
#define CTRL_PORT																PORTC
#define RAS_NUMBER															5
#define CAS_NUMBER															7
#define WR_NUMBER																6

//public DRAM_ReadByte

// unsigned char DRAM_ReadByte(dram address)
__DRAM_ReadByte:
; critical
  cli
; adr_port = address & 0xff
  out		ADR_PORT, r16
; address >>= 8
; ctrl_port = (address & 0x03) | 0xe0
	mov		r20, r17
	andi	r20, 0x03
	ori		r20, 0xe0
	out		CTRL_PORT, r20
; no op
;	nop
; address >>= 2
	
	movw	r17:r16, r19:r18
	lsr		r17
	ror		r16
; RAS_LOW - PORTn, m
	cbi		CTRL_PORT, RAS_NUMBER
; address >>= 8
	lsr		r17
	ror		r16
; adr_port = address
	out		ADR_PORT, r16
; address &= 0x03

; ctrl_port = address | 0xc0
	andi	r16, 0x03
	ori		r16, 0xc0
	out		CTRL_PORT, r16
; no op
	nop
; CAS_LOW - PORTn, m
	cbi		CTRL_PORT, CAS_NUMBER
; no op
	nop
; data = DATA_IN
	in		r16, DATA_IN
; CTRL_PORT = 0xe0
	ldi		r17, 0xe0
	out		CTRL_PORT, r17
; non critical
;	sei
; return data
;	ret
	reti


//public DRAM_WriteByte

// void DRAM_WriteByte(dram address, unsigned char value)

__DRAM_WriteByte:
; cli
	cli
; DIR_OUT
	ldi		r21, 0xff
	out		DATA_DDR, r21
; DATA_OUT = data
	out		DATA_OUT, r20
; ADR_PORT = address
	out		ADR_PORT, r16
; address >>= 8
	mov		r16, r17
	mov		r17, r18
	mov		r18, r19
	ldi		r19, 0
; CTRL_PORT = (address & 0x03) | 0xa0
	mov		r20, r16
	andi	r20, 0x03
	ori		r20, 0xa0
	out		CTRL_PORT, r20
; no op
;	nop
	mov		r16, r17
	mov		r17, r18
	lsr		r17
; address >>= 2

; RAS_LOW
	cbi		CTRL_PORT, RAS_NUMBER
; address >>= 8
	ror		r16
	lsr		r17
	ror		r16
; ADR_PORT = address
	out		ADR_PORT, r16
; address &= 0x03

; CTRL_PORT = address | 0x80
	andi	r16, 0x03
	ori		r16, 0x80
	out		CTRL_PORT, r16
; no op
	nop
; CAS_LOW
	cbi		CTRL_PORT, CAS_NUMBER
; no op
	nop
; CTRL_PORT = 0xe0
	ldi		r16, 0xe0
	out		CTRL_PORT, r16
; DIR_IN
	out		DATA_DDR, r19
; DATA_OUT = 0x00
	out		DATA_OUT, r19
; sei
;	sei
; return
;	ret
	reti
	
END
