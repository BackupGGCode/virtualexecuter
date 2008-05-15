;******************************************************************************
;
;	Generic Firmware Updater
;
;******************************************************************************


;******************************************************************************
;
; History:
;
;		18-03-2008 - Project created from an existing project that included
;                a bootloader.
;
;******************************************************************************



;******************************************************************************
.include "config.inc"
;******************************************************************************

.equ PAGE_COUNT																	= ((BOOTLOADER_START_ADDRESS * 2) / (PAGESIZE * 2))



;******************************************************************************
; SRAM variables
;******************************************************************************
.dseg

.equ PAGE_BUFFER_SIZE														=	(PAGESIZE * 2)
PageBuffer:	.byte	PAGE_BUFFER_SIZE


;******************************************************************************
; Register definitions
;******************************************************************************

.def temp												= r16
.def temp2											= r17
.def PageNumber0								= r18
.def PageNumber1								= r19
.def PageNumber2								= r20


;******************************************************************************
; Macroes
;******************************************************************************

#ifdef USE_MEMORY_MAPPED_SPMCSR
#define ReadSPMCR(reg)		lds reg, SPMCSR
#define WriteSPMCR(reg)		sts SPMCSR, reg
#else
#define ReadSPMCR(reg)		in reg, SPMCR
#define WriteSPMCR(reg)		out SPMCR, reg
#endif

.macro SetPCPAGE
	movw	ZH:ZL, PageNumber1:PageNumber0
#ifdef USING_RAMPZ
	out		RAMPZ, PageNumber2
#endif
.endmacro


;******************************************************************************

.cseg
.org 0
	jmp	0

.cseg
.org BOOTLOADER_START_ADDRESS


BooloaderStartAddress:

; Initialize SP
	ldi 	temp, low(RAMEND)
	out 	SPL, temp
	ldi 	temp, high(RAMEND)
	out 	SPH, temp

; Initialize the selected communication channel
	rcall	ComInit

#ifdef HookInit
	HookInit
#endif

CheckForBootloaderTrigger:

	ldi		PageNumber0, low(START_DELAY)
	ldi		PageNumber1, byte2(START_DELAY)
	ldi		PageNumber2, byte3(START_DELAY)

CheckForBootloaderTrigger_Loop:

	rcall	ComDataReady
	tst		temp
	breq	CheckForBootloaderTrigger_Nothing
	rcall	ComGet
	cpi		temp, '*'
	breq	EnterBootloader
	rjmp	RunApplication

CheckForBootloaderTrigger_Nothing:

	subi	PageNumber0, 1
	sbci	PageNumber1, 0
	sbci	PageNumber2, 0
	brne	CheckForBootloaderTrigger_Loop
	rjmp	RunApplication


EnterBootloader:

#ifdef HookEnterBootloader
	HookEnterBootloader
#endif

SendDeviceInfo:
	ldi		temp, low(PAGESIZE)
	rcall	ComPut
	ldi		temp, high(PAGESIZE)
	rcall	ComPut
	ldi		temp, low(PAGE_COUNT)
	rcall	ComPut
	ldi		temp, high(PAGE_COUNT)
	rcall	ComPut


MainLoop:

	rcall	ComDataReady
	tst		temp
	breq	MainLoop


StartProgrammingCycle:

#ifdef HookCycleStart
	HookCycleStart
#endif


GetPageNumberFromHost:

#ifdef HookGetPageNumber
	HookGetPageNumber
#endif

	rcall	ComGet
	mov		PageNumber0, temp
	rcall	ComGet
	mov		PageNumber1, temp
	rcall	ComGet
	mov		PageNumber2, temp

;	cpi		PageNumber2, 0xff
;	rjmp	BootloaderSpecialOperation


ErasePage:

#ifdef HookErasePage
	HookErasePage
#endif

	SetPCPAGE
	ldi		temp, (1<<PGERS)|(1<<SPMEN)
	rcall	ExecuteSPM


GetPageDataFromHost:

#ifdef HookGetPageData
	HookGetPageData
#endif

	ldi		ZL, low(PageBuffer)
	ldi		ZH, high(PageBuffer)
	ldi		YL, low(PAGE_BUFFER_SIZE)
	ldi		YH, high(PAGE_BUFFER_SIZE)
GetPageDataFromHost_Loop:
	rcall	ComGet
	st		Z+, temp
	sbiw	YH:YL, 1
	brne	GetPageDataFromHost_Loop


LoadPageData:

#ifdef HookLoadPageData
	HookLoadPageData
#endif

	SetPCPAGE
	ldi		XL, low(PageBuffer)
	ldi		XH, high(PageBuffer)
	ldi		YL, low(PAGESIZE)
	ldi		YH, high(PAGESIZE)
LoadPageData_Loop:
	ld		r0, X+
	ld		r1, X+
	ldi		temp, (1<<SPMEN)
	rcall	ExecuteSPM
	adiw	ZH:ZL, 2
	sbiw	YH:YL, 1
	brne	LoadPageData_Loop


ProgramPage:

#ifdef HookProgramPage
	HookProgramPage
#endif

	SetPCPAGE
	ldi		temp, (1<<PGWRT)|(1<<SPMEN)
	rcall	ExecuteSPM

/*
#ifdef VERIFY_AFTER_WRITE
VerifyPage:

#ifdef HookVerifyPage
	HookVerifyPage
#endif

	ldi		temp, (1<<RWWSRE)|(1<<SPMEN)
	rcall	ExecuteSPM

	SetPCPAGE
	ldi		XL, low(PageBuffer)
	ldi		XH, high(PageBuffer)
	ldi		YL, low(PAGESIZE * 2)
	ldi		YH, high(PAGESIZE * 2)
VerifyPage_Loop:
	lpm		r0, Z+
	ld		r1, X+
	cp		r0, r1
	brne	SendNotAcknowledge
	sbiw	YH:YL, 1
	brne	VerifyPage_Loop
	brne	SendAcknowledge


SendNotAcknowledge:

#ifdef HookNotAcknowledge
	HookNotAcknowledge
#endif

	ldi		temp, 'N'
	rjmp	SendReply

#endif
*/

SendAcknowledge:

#ifdef HookAcknowledge
	HookAcknowledge
#endif

	ldi		temp, 'A'


SendReply:

	rcall	ComPut

#ifdef HookCycleDone
	HookCycleDone
#endif

	rjmp	MainLoop


;******************************************************************************


ExecuteSPM:

WaitForPreviousSPMToComplete:
	ReadSPMCR(temp2)
	sbrc	temp2, SPMEN
	rjmp	WaitForPreviousSPMToComplete
#ifdef USING_INTERRUPTS
	in		temp2, SREG
	cli
#endif
WaitForEEPROMWriteToComplete:
	sbic	EECR, EEWE
	rjmp	WaitForEEPROMWriteToComplete
DoTheSPMThing:
	WriteSPMCR(temp)
	spm
#ifdef USING_INTERRUPTS
	out		SREG, temp2
#endif
	ret


;******************************************************************************
/*
BootloaderSpecialOperation:


	rjmp	MainLoop
*/
;******************************************************************************

RunApplication:
	
#ifdef HookDeinit
	HookDeinit
#endif
	
	jmp		0x0000

;******************************************************************************

COM_DRIVER
