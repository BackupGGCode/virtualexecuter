.include "m64def.inc"


.equ BOOTLOADER_START_ADDRESS										= FIRSTBOOTSTART
.equ CRYSTAL																		= 11059200															; processor clock in Hz
.equ START_DELAY																= 614400																; number of ticks to wait for trigger. Roughly 18 cycles per tick (depending on duration of ComDataReady())
.equ BAUDRATE																		= 11


;#define USING_RAMPZ
#define USE_MEMORY_MAPPED_SPMCSR
;#define VERIFY_AFTER_WRITE
;#define USING_INTERRUPTS


#define HookInit							sbi DDRB, 7\ sbi PORTB, 7
#define HookDeinit						cbi DDRB, 7\ cbi PORTB, 7
;#define HookEnterBootloader		sbi PORTB, 7
;#define HookCycleStart				
;#define HookGetPageNumber			sbi PORTB, 6
;#define HookErasePage					cbi PORTB, 6\sbi PORTB, 7
;#define HookGetPageData				sbi PORTB, 6\sbi PORTB, 7
;#define HookLoadPageData			sbi PORTB, 6\cbi PORTB, 7
;#define HookProgramPage				
;#define HookVerifyPage				
;#define HookAcknowledge
;#define HookNotAcknowledge
;#define HookCycleDone					


#define COM_DRIVER .include"UART_m64_UART0.asm"
