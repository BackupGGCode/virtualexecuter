NAME SlicerKernel

/*
#define DATA_IN																	PIND
#define DATA_OUT																PORTD
#define ADR_PORT																PORTA
#define CTRL_PORT																PORTC

#define DIR_IN																	DDRD = 0x00
#define DIR_OUT																	DDRD = 0xff

#define CAS_HIGH																CTRL_PORT |= (1<<7)
#define CAS_LOW																	CTRL_PORT &= ~(1<<7)
#define RAS_HIGH																CTRL_PORT |= (1<<5)
#define RAS_LOW																	CTRL_PORT &= ~(1<<5)
#define WE_HIGH																	CTRL_PORT |= (1<<6)
#define WE_LOW																	CTRL_PORT &= ~(1<<6)
*/


/***************************************************************************************************************************
	Public symbols
***************************************************************************************************************************/

// unsigned long TimerTicks;
PUBLIC TimerTicks


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

/***************************************************************************************************************************
	Macros
***************************************************************************************************************************/

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

/***************************************************************************************************************************
	Interrupt vector for the RTC
***************************************************************************************************************************/
COMMON INTVEC :CODE
ORG TIMER0_COMP_vect

kernelRTC_InterruptVector:
	jmp		kernelRTC

/***************************************************************************************************************************
	Program code
***************************************************************************************************************************/
RSEG CODE :CODE


/***************************************************************************************************************************
RTC Interrupt rutine
***************************************************************************************************************************/
kernelRTC:
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

	reti
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelInit(void);
***************************************************************************************************************************/
kernelInit:
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
	
	ret
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelRun(void);
***************************************************************************************************************************/
kernelRun:
	lds		r16, NumberOfTasks									// Skip if no tasks in queue
	tst		r16
	breq	kernelRun_DontRun
kernelRun_StartRunning:
	mLoadContext
	reti
kernelRun_DontRun:
	rjmp	kernelRun_DontRun

/**************************************************************************************************************************/



/***************************************************************************************************************************
	TaskID kernelCreateNewTask(void(*FunctionPointer)(void), TaskStack *RStack, unsigned char RStackSize, TaskStack *CStack, unsigned char CStackSize;
	r16                     r17:r16                       r19:r18            r20                       r23:r22             r21
***************************************************************************************************************************/
kernelCreateNewTask:
	in		r0, SREG
	cli
	push	YL
	push	YH
	movw	r3:r2, r17:r16

		// ***** OK TO USE r1, r16, r17, r28, r29, r30, r31 *****

// Room for more tasks?
kernelCreateTask_RoomForMoreTasks:
	lds		r16, NumberOfTasks									// Get current number of tasks
	cpi		r16, __MAXIMUM_NUMBER_OF_TASKS			// Test if all task entries has been used
	brne	kernelCreateTask_IncNumberOfTasks		// If not then find next free TaskID
	rjmp	kernelCreateTask_Failed							// Else return with r16=0
kernelCreateTask_IncNumberOfTasks:					// Grap a task space
	inc		r16
	sts		NumberOfTasks, r16
	
		// ***** OK TO USE r1, r16, r17, r28, r29, r30, r31 *****

// Find new free task ID
kernelCreateTask_FindNextFreeTaskID:
	lds		r29, NextTaskID											// Get first TaskID to test	
kernelCreateTask_FindNextFreeTaskID_WhileNotFound:
	ldi		r28, __MAXIMUM_NUMBER_OF_TASKS			// Number of times the compare must "fail" to accept the TaskID
	ldi		ZL, low(TaskQueue)									// Point to TaskQueue
	ldi		ZH, high(TaskQueue)
kernelCreateTask_FindNextFreeTaskID_ForAllEntries:
	ld		r16, Z															// Get TaskID of entry pointed to by Z
	cp		r16, r29														// Test if TaskID is already in use
	breq	kernelCreateTask_FindNextFreeTaskID_TryNext					// It was so try next TaskID
	dec		r28																	// Decrement counter to see if all entries has been compared
	breq	kernelCreateTask_TaskIDFound				// Free TaskID found (r29)
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE				// Point to next TaskQueue entry to compare to
	rjmp	kernelCreateTask_FindNextFreeTaskID_ForAllEntries		// And jump to compare code
kernelCreateTask_FindNextFreeTaskID_TryNext:
	inc		r29																	// Next TaskID to try
	cpi		r29, __ME														// If last avaiable ID has been passed
	brne	kernelCreateTask_FindNextFreeTaskID_WhileNotFound
	ldi		r29, 1															// Go to first possible ID
	rjmp	kernelCreateTask_FindNextFreeTaskID_WhileNotFound
kernelCreateTask_TaskIDFound:								// r29 now contains the new TaskID to be used
	mov		r28, r29
	inc		r28
	cpi		r28, __ME
	brne	kernelCreateTask_SaveNextTaskID
	ldi		r28, 1
kernelCreateTask_SaveNextTaskID:
	sts		NextTaskID, r28											// Save NextTaskID for next 'kernelCreateTask' call

// Now find the next unused Task entry in the Task Queue
kernelCreateTask_FindNextFreeTaskEntry:
	lds		r28, NumberOfTasks									// Number of task + 1 (the one we're adding)
	ldi		ZL, low(TaskQueue)									// Point to TaskQueue
	ldi		ZH, high(TaskQueue)
kernelCreateTask_FindNextFreeTaskEntry_Adding:
	dec		r28																	// Decrement counter
	breq	kernelCreateTask_TaskEntryFound
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE				// Point to next TaskQueue entry
	rjmp	kernelCreateTask_FindNextFreeTaskEntry_Adding

// Z points to the Task entry in the Task Queue where the new task will be placed
kernelCreateTask_TaskEntryFound:
	st		Z, r29															// Save the new TaskID to the Task entry we just found
	mov		r16, r29

// Setup stack pointers	
	clr		r17
//	dec		r20																	// Point to top of RStack
	add		r18, r20														// RStackL = r18
	adc		r19, r17														// RStackH = r19
	movw	YH:YL, r19:r18											// RStack moved to Y
	dec		r21																	// Point to top of CStack
	add		r22, r21														// CStackL = r22
	adc		r23, r17														// CStackH = r23
	
	// ***** OK TO USE r1, r16, r17, r18, r19, r20, r21, r30, r31 *****

// Generate the context for the new task
	st		Y, r2																// PCL - Use pre-decrement since we're pointing beyond the stack space
	st		-Y, r3															// PCH
	st		-Y, r17															// r0 = 0x00
	st		-Y, r17															// SREG - Global Interrupt not enabled -> Use 'reti' to save an 'ldi r17, 0x80'
	st		-Y, r17															// r1
	st		-Y, r17															// r2
	st		-Y, r17															// r3
	st		-Y, r17															// r4
	st		-Y, r17															// r5
	st		-Y, r17															// r6
	st		-Y, r17															// r7
	st		-Y, r17															// r8
	st		-Y, r17															// r9
	st		-Y, r17															// r10
	st		-Y, r17															// r11
	st		-Y, r17															// r12
	st		-Y, r17															// r13
	st		-Y, r17															// r14
	st		-Y, r17															// r15
	st		-Y, r17															// r16
	st		-Y, r17															// r17
	st		-Y, r17															// r18
	st		-Y, r17															// r19
	st		-Y, r17															// r20
	st		-Y, r17															// r21
	st		-Y, r17															// r22
	st		-Y, r17															// r23
	st		-Y, r17															// r24
	st		-Y, r17															// r25
	st		-Y, r17															// r26
	st		-Y, r17															// r27
	st		-Y, r22															// r28 - YL
	st		-Y, r23															// r29 - YH
	st		-Y, r17															// r30
	st		-Y, r17															// r31

	sbiw	YH:YL, 1
	std		Z+TASK_CONTEXT_OFFSET, YL						// Save context pointer to Task Queue
	std		Z+TASK_CONTEXT_OFFSET+1, YH

kernelCreateTask_Exit:
	pop		YH
	pop		YL
	out		SREG, r0
	ret

kernelCreateTask_Failed:										// Return with error notification if we couldn't create the task
	clr		r16
	rjmp	kernelCreateTask_Exit
/**************************************************************************************************************************/
	


/***************************************************************************************************************************
	Added in version 2
	unsigned char kernelDeleteTask (TaskID *TaskIDToDelete);
	r16															r17:r16
***************************************************************************************************************************/
kernelDeleteTask:
	cli
	mov		ZL, r16
	mov		r22, r16
	mov		ZH, r17
	mov		r23, r17
	ld		r16, Z
	mGetTask2
	tst		r17
	breq	kernelDeleteTask_Failed					// Task doesn't exist
	lds		r18, CurrentTaskIndex
	ldi		r19, __MAXIMUM_NUMBER_OF_TASKS
	sub		r19, r17
	cp		r19, r18
	breq	kernelDeleteTask_Failed					// Can't kill self
kernelDeleteTask_CleanUp:
	lds		r18, NumberOfTasks
	dec		r18
	sts		NumberOfTasks, r18
	lds		r18, NextTaskIndex
	dec		r18
	sts		NextTaskIndex, r18
	lds		r18, CurrentTaskIndex
	dec		r18
	sub		r19, r18
	brpl	kernelDeleteTask_CleanUp_DontDecCurrentTaskIndex
	sts		CurrentTaskIndex, r18
kernelDeleteTask_CleanUp_DontDecCurrentTaskIndex:
	push	r26
	push	r27
	movw	r27:r26, r31:r30
	adiw	r31:r30, TASK_QUEUE_ENTRY_SIZE
	clr		r18
	dec		r17
	breq	kernelDeleteTask_Finish
	ldi		r19, TASK_QUEUE_ENTRY_SIZE
kernelDeleteTask_CleanUpInnerLoop:
	ld		r16, Z+
	st		X+, r16
	dec		r19
	brne	kernelDeleteTask_CleanUpInnerLoop
	adiw	r27:r26, TASK_QUEUE_ENTRY_SIZE
	adiw	r31:r30, TASK_QUEUE_ENTRY_SIZE
	dec		r17
	brne	kernelDeleteTask_CleanUp
kernelDeleteTask_Finish:
	ldi		r17, TASK_QUEUE_ENTRY_SIZE
kernelDeleteTask_FinishLoop:
	st		X+, r18
	dec		r17
	brne	kernelDeleteTask_FinishLoop
	mov		ZL, r22
	mov		ZH, r23
	clr		r16
	st		Z, r16
	ldi		r16, 1
	pop		r27
	pop		r26
	sei
	ret

kernelDeleteTask_Failed:
	clr		r16
	sei
	ret
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelSleepTask(TaskID Task, unsigned int Time);
											 r16					r19:r18
***************************************************************************************************************************/
kernelSleepTask:
	cli
	cpi		r16, __ME
	brne	kernelSleepTask_NotMe

	lds		ZL, pCurrentTask										// Point to current task entry
	lds		ZH, pCurrentTask+1
	std		Z+TASK_SLEEP_OFFSET, r18
	std		Z+TASK_SLEEP_OFFSET+1, r19
	rjmp	kernelYield

kernelSleepTask_NotMe:
	movw	r21:r20, r19:r18
	mGetTask
	tst		r17
	brne	kernelSleepTask_TaskFound
	sei
	ret
kernelSleepTask_TaskFound:
	std		Z+TASK_SLEEP_OFFSET, r20
	std		Z+TASK_SLEEP_OFFSET+1, r21
	sei
	ret
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelYield(void);
***************************************************************************************************************************/
kernelYield:
	mSaveContext
	
	// Find next Task
	adiw	ZH:ZL, TASK_QUEUE_ENTRY_SIZE				// Point to next Task Entry
	lds		r17, CurrentTaskIndex
	inc		r17

	ld		r16, Z
	cpi		r16, TASK_ENTRY_EMPTY								// End of Task Queue reached?
	brne	kernelYield_NextEntryFound						// 
kernelYield_PointToFirstEntry:
	ldi		ZL, low(TaskQueue)									// Point to first entry
	ldi		ZH, high(TaskQueue)
	ldi		r17, 0
	rjmp	kernelYield_StoreEntry
kernelYield_NextEntryFound:
	cpi		r17, __MAXIMUM_NUMBER_OF_TASKS
	breq	kernelYield_PointToFirstEntry
kernelYield_StoreEntry:
	sts		pCurrentTask, ZL										// Update CurrentTaskPointer
	sts		pCurrentTask+1, ZH
	sts		CurrentTaskIndex, r17

kernelYield_LoadNewContext:
	mLoadContext

	reti
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelLock(unsigned char LockNr, TaskID Task);
	                r16                 , r17
	Added in version 2
***************************************************************************************************************************/
kernelLock:
	cli
	ldi		ZL, low(LockList)
	ldi		ZH, high(LockList)
	mov		r18, r17
	clr		r17
	add		ZL, r16
	adc		ZH, r17
kernelLock_LoadLock:
	ld		r17, Z
	tst		r17
	brne	kernelLock_AlreadyLocked
kernelLock_LockIt:
	st		Z, r18
	sei
	ret
kernelLock_AlreadyLocked
	sei

	nop
	// REPLACE nop WITH rcall kernelYield !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	cli
	rjmp	kernelLock_LoadLock
/**************************************************************************************************************************/



/***************************************************************************************************************************
	void kernelUnLock(unsigned char LockNr);
	                  r16
	Added in version 2
***************************************************************************************************************************/
kernelUnLock:
	cli
	ldi		ZL, low(LockList)
	ldi		ZH, high(LockList)
	clr		r17
	add		ZL, r16
	adc		ZH, r17
	st		Z, r17
	sei
	ret
/**************************************************************************************************************************/



/***************************************************************************************************************************
	DEBUGGER
	Added in version 2
***************************************************************************************************************************/

PutC:
	sbis	UCSRA, UDRE
	rjmp	PutC
	out		UDR, r16
	ret
	

/***************************************************************************************************************************
	void kernelDebugDump(void);
	Added in version 2
***************************************************************************************************************************/
kernelDebugDump:
	cli
	ldi		r16, 255
	rcall	PutC
	lds		r16, TaskQueue
	rcall	PutC
	lds		r16, TaskQueue+TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+2*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+3*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+4*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+5*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+6*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	lds		r16, TaskQueue+7*TASK_QUEUE_ENTRY_SIZE
	rcall	PutC
	ldi		r16, 100
	rcall	PutC
	lds		r16, NextTaskID
	rcall	PutC
	lds		r16, NextTaskIndex
	rcall	PutC
	lds		r16, NumberOfTasks
	rcall	PutC
	ldi		r16, 255
	rcall	PutC
	sei
	ret
/**************************************************************************************************************************/




// unsigned char DRAM_ReadByte(dram address)

DRAMLL_ReadByte:
; critical
  cli
; adr_port = address & 0xff
  out		ADR_PORT, r16
; ctrl_port = (address & 0x03) | 0xe0
	mov		r20, r17
	andi	r20, 0x03
	ori		r20, 0xe0
	out		CTRL_PORT, r20
; no op
	nop
; address >>= 2
	
; RAS_LOW - PORTn, m
	cbi		
; address >>= 8
	movw	r17:r16, r19:r18
	lsr		r17
	ror		r16
	lsr		r17
	ror		r16
; adr_port = address
	out		CTRL_PORT, r16
; address &= 0x03

; ctrl_port = address | 0xc0
	andi	r16, 0x03
	ori		r16, 0xc0
	out		CTRL_PORT, r16
; no op
	nop
; CAS_LOW - PORTn, m
	cbi		
; no op
	nop
; data = DATA_IN
	in		r16, DATA_IN_PINS
; CTRL_PORT = 0xe0
	ldi		r17, 0xe0
	out		CTRL_PORT, r17
; non critical
	sei
; return data
	ret



// void DRAM_WriteByte(dram address, unsigned char value)

DRAMLL_WriteByte:

   \   00000000   94F8               CLI
    258          	
    259          	DIR_OUT;
   \   00000002   EF5F               LDI     R21, 255
   \   00000004   BB51               OUT     0x11, R21
    260          	DATA_OUT = data;
   \   00000006   BB42               OUT     0x12, R20
    261          	
    262          	ADR_PORT = address;
   \   00000008   BB0B               OUT     0x1B, R16
    263          	address >>= 8;
   \   0000000A   2F01               MOV     R16, R17
   \   0000000C   2F12               MOV     R17, R18
   \   0000000E   2F23               MOV     R18, R19
   \   00000010   E030               LDI     R19, 0
    264          	CTRL_PORT = (address & 0x03) | 0xa0;
   \   00000012   2F40               MOV     R20, R16
   \   00000014   7043               ANDI    R20, 0x03
   \   00000016   6A40               ORI     R20, 0xA0
   \   00000018   BB45               OUT     0x15, R20
    265          	__no_operation();
   \   0000001A   0000               NOP
    266           	address >>= 2;
    267          	RAS_LOW;
   \   0000001C   98AD               CBI     0x15, 0x05
    268          //	address >>= 2;
    269          	address >>= 8;
   \   0000001E   2F01               MOV     R16, R17
   \   00000020   2F12               MOV     R17, R18
   \   00000022   9516               LSR     R17
   \   00000024   9507               ROR     R16
   \   00000026   9516               LSR     R17
   \   00000028   9507               ROR     R16
    270          	ADR_PORT = address;
   \   0000002A   BB0B               OUT     0x1B, R16
    271          //	address >>= 8;
    272            address &= 0x03;
    273          //	CTRL_PORT = (address & 0x03) | 0x80;
    274          	CTRL_PORT = address | 0x80;
   \   0000002C   7003               ANDI    R16, 0x03
   \   0000002E   6800               ORI     R16, 0x80
   \   00000030   BB05               OUT     0x15, R16
    275          	__no_operation();
   \   00000032   0000               NOP
    276          	CAS_LOW;
   \   00000034   98AF               CBI     0x15, 0x07
    277          	
    278          	__no_operation();
   \   00000036   0000               NOP
    279          	
    280          	CTRL_PORT = 0xe0;
   \   00000038   EE00               LDI     R16, 224
   \   0000003A   BB05               OUT     0x15, R16
    281          	
    282          	DIR_IN;
   \   0000003C   BB31               OUT     0x11, R19
    283          	DATA_OUT = 0x00;
   \   0000003E   BB32               OUT     0x12, R19
    284          	
    285          	NonCritical();
   \   00000040   9478               SEI
    286          }
   \   00000042   9508               RET
  ret

END
