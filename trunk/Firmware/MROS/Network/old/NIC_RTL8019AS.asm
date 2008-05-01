NAME RTL8019AS_LowLevel_Driver


#include "NIC_RTL8019AS.h"
#include "Assist.h"


//**************************************************************************************************************************
//**************************************************************************************************************************
// NIC Register and bit definitions
//**************************************************************************************************************************
//**************************************************************************************************************************

// Page 0
#define CR								0			// RW
#define CLDA0							1			// R
#define PSTART						1			// W
#define CLDA1							2			// R
#define PSTOP							2			// W
#define BNRY							3			// RW
#define TSR								4			// R
#define TPSR							4			// W
#define NCR								5			// R
#define TBCR0							5			// W
#define FIFO							6			// R
#define TBCR1							6			// W
#define ISR								7			// RW
#define CRDA0							8			// R
#define RSAR0							8			// W
#define CRDA1							9			// R
#define RSAR1							9			// W
#define _8019ID0					10		// R
#define RBCR0							10		// W
#define _8019ID1					11		// R
#define RBCR1							11		// W
#define RSR								12		// R
#define RCR								12		// W
#define CNTR0							13		// R
#define TCR								13		// W
#define CNTR1							14		// R
#define DCR								14		// W
#define CNTR2							15		// R
#define IMR								15		// W
// Page 1
#define PAR0							1			// RW
#define PAR1							2			// RW
#define PAR2							3			// RW
#define PAR3							4			// RW
#define PAR4							5			// RW
#define PAR5							6			// RW
#define CURR							7			// RW
#define MAR0							8			// RW
#define MAR1							9			// RW
#define MAR2							10		// RW
#define MAR3							11		// RW
#define MAR4							12		// RW
#define MAR5							13		// RW
#define MAR6							14		// RW
#define MAR7							15		// RW
// Page 2
#define PSTART						1			// R
#define PSTOP							2			// R
#define TPSR							4			// R
#define RCR								12		// R
#define TCR								13		// R
#define DCR								14		// R
#define IMR								15		// R

#define DMA_PORT					0x10
#define RESET_PORT				0x1f

// CR register
#define CR_PAGE0					0x00
#define CR_PAGE1					0x40
#define CR_PAGE2					0x80
#define CR_PAGE3					0xc0
#define CR_READ						0x08
#define CR_WRITE					0x10
#define CR_SEND						0x18
#define CR_ABORT					0x20
#define CR_TXP						0x04
#define CR_STA						0x02
#define CR_STP						0x01
// ISR register
#define ISR_RST						0x80
#define ISR_RDC						0x40
#define ISR_CNT						0x20
#define ISR_OVW						0x10
#define ISR_TXE						0x08
#define ISR_RXE						0x04
#define ISR_PTX						0x02
#define ISR_PRX						0x01
// IMR register
#define IMR_RDCE					0x40
#define IMR_CNTE					0x20
#define IMR_OVWE					0x10
#define IMR_TXEE					0x08
#define IMR_RXEE					0x04
#define IMR_PTXE					0x02
#define IMR_PRXE					0x01
// DCR register
#define DCR_FT1						0x80|0x40
#define DCR_FT0						0x80|0x20
#define DCR_ARM						0x80|0x10
#define DCR_LS						0x80|0x08
#define DCR_LAS						0x80|0x04
#define DCR_BOS						0x80|0x02
#define DCR_WTS						0x80|0x01
// TCR register
#define TCR_OFST					0xe0|0x10
#define TCR_ATD						0xe0|0x08
#define TCR_LB1						0xe0|0x04
#define TCR_LB0						0xe0|0x02
#define TCR_CRC						0xe0|0x01
// TSR register
#define TSR_OWC						0x22|0x80
#define TSR_CDH						0x22|0x40
#define TSR_CRS						0x22|0x10
#define TSR_ABT						0x22|0x08
#define TSR_COL						0x22|0x04
#define TSR_PTX						0x22|0x01
// RCR register
#define RCR_MON						0xc0|0x20
#define RCR_PRO						0xc0|0x10
#define RCR_AM						0xc0|0x08
#define RCR_AB						0xc0|0x04
#define RCR_AR						0xc0|0x02
#define RCR_SEP						0xc0|0x01
// RSR register
#define RSR_DFR						0x08|0x80
#define RSR_DIS						0x08|0x40
#define RSR_PHY						0x08|0x20
#define RSR_MPA						0x08|0x10
#define RSR_FAE						0x08|0x04
#define RSR_CRC						0x08|0x02
#define RSR_PRX						0x08|0x01


#define TX_RING_BUFFER_START		0x40
#define TX_RING_BUFFER_SIZE			6
#define RX_RING_BUFFER_START		TX_RING_BUFFER_START+TX_RING_BUFFER_SIZE
#define RX_RING_BUFFER_STOP			0x60-1



#define NIC_PORT					PORTC
#define NIC_DDR						DDRC
#define NIC_PIN						PINC

#define NIC_ADR						PORTB

#define NIC_WR_PORT				PORTA
#define NIC_WR_BIT				5
#define NIC_RD_PORT				PORTA
#define NIC_RD_BIT				6
#define NIC_RESET_PORT		PORTA
#define NIC_RESET_BIT			4


//**************************************************************************************************************************
//**************************************************************************************************************************
// End of NIC Register and bit definitions
//**************************************************************************************************************************
//**************************************************************************************************************************



//**************************************************************************************************************************
//**************************************************************************************************************************
// Public symbols
//**************************************************************************************************************************
//**************************************************************************************************************************

// The main NIC functions

//unsigned char NIC_Read(unsigned char Register)
Public NIC_Read
//void NIC_Write(unsigned char Register, unsigned char Data)
Public NIC_Write
//signed char NIC_Init(unsigned char *pMAC)
Public NIC_Init
//signed short NIC_Get(unsigned char *Buffer, unsigned short MaxLength)
Public NIC_Get
//signed char NIC_SendBlocking(unsigned char *Buffer, unsigned short Length)
Public NIC_SendBlocking
//signed char NIC_Send(unsigned char *Buffer, unsigned short Length)
Public NIC_Send


// unsigned char mac[6];
Public mac
// unsigned char globalBuffer[GLOBAL_BUFFER_SIZE];
//Public globalBuffer

//**************************************************************************************************************************
//**************************************************************************************************************************
// MAC
//**************************************************************************************************************************
//**************************************************************************************************************************

RSEG NEAR_Z :DATA

mac:
	DS8		6																				// Local MAC address
//globalBuffer:
//	DS8		GLOBAL_BUFFER_SIZE											// The global buffer

//**************************************************************************************************************************
//**************************************************************************************************************************
// End of MAC
//**************************************************************************************************************************
//**************************************************************************************************************************



//**************************************************************************************************************************
//**************************************************************************************************************************
// Macros
//**************************************************************************************************************************
//**************************************************************************************************************************


//**************************************************************************************************************************
// void mNIC_Delay(unsigned char kCycles)
//**************************************************************************************************************************
mNIC_Delay	MACRO
	Local mNIC_Delay_InnerLoop
	Local mNIC_Delay_OuterLoop

mNIC_Delay_OuterLoop:
	ldi		r17, 250
mNIC_Delay_InnerLoop:
	nop
	dec		r17
	brne	mNIC_Delay_InnerLoop
	dec		r16
	brne	mNIC_Delay_OuterLoop
	ENDM
//**************************************************************************************************************************

	
//**************************************************************************************************************************
// unsigned char NIC_Read(unsigned char Register)
//**************************************************************************************************************************
mNIC_Read		MACRO
	out		NIC_ADR, r16
	cbi		NIC_RD_PORT, NIC_RD_BIT
	cbi		NIC_RD_PORT, NIC_RD_BIT
	in		r16, NIC_PIN
	sbi		NIC_RD_PORT, NIC_RD_BIT
	ENDM
//**************************************************************************************************************************


//**************************************************************************************************************************
// unsigned char NIC_ReadLoop(void)
//**************************************************************************************************************************
mNIC_ReadLoop		MACRO
	cbi		NIC_RD_PORT, NIC_RD_BIT
	cbi		NIC_RD_PORT, NIC_RD_BIT
	in		r16, NIC_PIN
	sbi		NIC_RD_PORT, NIC_RD_BIT
	ENDM
//**************************************************************************************************************************


//**************************************************************************************************************************
// void NIC_Write(unsigned char Register, unsigned char Data)
//**************************************************************************************************************************
mNIC_Write	MACRO
	out		NIC_ADR, r16
	ldi		r16, 0xff
	out		NIC_DDR, r16
	out		NIC_PORT, r17
	ldi		r17, 0x00
	cbi		NIC_WR_PORT, NIC_WR_BIT
	cbi		NIC_WR_PORT, NIC_WR_BIT
	sbi		NIC_WR_PORT, NIC_WR_BIT
	out		NIC_DDR, r17
	out		NIC_PORT, r16
	ENDM
//**************************************************************************************************************************


//**************************************************************************************************************************
// void NIC_WriteQuick(unsigned char Register, unsigned char Data)
//**************************************************************************************************************************
mNIC_WriteQuick	MACRO
	out		NIC_ADR, r16
	out		NIC_PORT, r17
	cbi		NIC_WR_PORT, NIC_WR_BIT
	cbi		NIC_WR_PORT, NIC_WR_BIT
	sbi		NIC_WR_PORT, NIC_WR_BIT
	ENDM
//**************************************************************************************************************************


//**************************************************************************************************************************
// void NIC_WriteQuickLoop(unsigned char Data)
//**************************************************************************************************************************
mNIC_WriteQuickLoop	MACRO
	out		NIC_PORT, r16
	cbi		NIC_WR_PORT, NIC_WR_BIT
	cbi		NIC_WR_PORT, NIC_WR_BIT
	sbi		NIC_WR_PORT, NIC_WR_BIT
	ENDM
//**************************************************************************************************************************



//**************************************************************************************************************************
//**************************************************************************************************************************
// End of Macros
//**************************************************************************************************************************
//**************************************************************************************************************************



//**************************************************************************************************************************
// Program code
//**************************************************************************************************************************
RSEG CODE :CODE


//**************************************************************************************************************************
//**************************************************************************************************************************
// Main Functions
//**************************************************************************************************************************
//**************************************************************************************************************************


//**************************************************************************************************************************
// unsigned char NIC_Read(unsigned char Register)
//**************************************************************************************************************************
NIC_Read:
	out		NIC_ADR, r16
	cbi		NIC_RD_PORT, NIC_RD_BIT
	cbi		NIC_RD_PORT, NIC_RD_BIT
	in		r16, NIC_PIN
	sbi		NIC_RD_PORT, NIC_RD_BIT
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void NIC_Write(unsigned char Register, unsigned char Data)
//**************************************************************************************************************************
NIC_Write:
	out		NIC_ADR, r16
	ldi		r16, 0xff
	out		NIC_DDR, r16
	out		NIC_PORT, r17
	ldi		r17, 0x00
	cbi		NIC_WR_PORT, NIC_WR_BIT
	cbi		NIC_WR_PORT, NIC_WR_BIT
	sbi		NIC_WR_PORT, NIC_WR_BIT
	out		NIC_DDR, r17
	out		NIC_PORT, r16
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// signed char NIC_Init(unsigned char *pMAC)
//**************************************************************************************************************************

NIC_Init:
	movw	R31:r30, R17:r16												// Move MAC pointer
	sbi		NIC_RESET_PORT, NIC_RESET_BIT						// HW reset

	ldi		r16, 255
	mNIC_Delay

	cbi		NIC_RESET_PORT, NIC_RESET_BIT

	ldi		r16, 255
	mNIC_Delay

	ldi		r16, RESET_PORT													// SW reset
	mNIC_Read
	mov		r17, r16
	ldi		r16, RESET_PORT
	mNIC_Write

	ldi		r16, 255
	mNIC_Delay

	ldi		r16, ISR
	mNIC_Read
	sbrc	r16, 7
	rjmp	NIC_Init_ResetCompleted
	ldi		r16, -1																	// return -1 if ISR_RST == 0
	ret

NIC_Init_ResetCompleted:

	ldi		r16, 0xff																// Set NIC_PORT for output -> prepare for mNIC_WriteQuick
	out		NIC_DDR, r16

	ldi		r16, CR																	// Stop NIC, DMA abort, Page 0
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STP
	mNIC_WriteQuick

// FIFO threshold, no loop-back, 8 bit mode, Send Packet is executed
	ldi		r16, DCR
	ldi		r17, DCR_ARM|DCR_FT1|DCR_LS
	mNIC_WriteQuick

	ldi		r16, RBCR0															// Remote DMA byte count
	ldi		r17, 0
	mNIC_WriteQuick

	ldi		r16, RBCR1															// -||-
	ldi		r17, 0
	mNIC_WriteQuick

	ldi		r16, RCR																// Monitor mode (for now) - 0x00 in AN-874!
	ldi		r17, RCR_MON
	mNIC_WriteQuick

	ldi		r16, TCR																// Internal loop-back mode (for now)
	ldi		r17, TCR_LB0
	mNIC_WriteQuick

	ldi		r16, TPSR																// Tx Ring Buffer Start
	ldi		r17, TX_RING_BUFFER_START
	mNIC_WriteQuick

	ldi		r16, PSTART															// Rx RB start
	ldi		r17, RX_RING_BUFFER_START
	mNIC_WriteQuick

	ldi		r16, PSTOP															// Rx RB stop
	ldi		r17, RX_RING_BUFFER_STOP
	mNIC_WriteQuick

	ldi		r16, BNRY																// Rx RB boundary
	ldi		r17, RX_RING_BUFFER_START
	mNIC_WriteQuick

	ldi		r16, CR																	// Switch to page 1
	ldi		r17, CR_PAGE1|CR_ABORT|CR_STP
	mNIC_WriteQuick

NIC_Init_LoadPAR_Select:												// NULL pointer => load default (0a:0b:0c:0d:0e:0f)
	mov		r16, r30
	or		r16, r31
	breq	NIC_Init_LoadPAR_Internal

NIC_Init_LoadPAR_External:
	ldi		r18, 6																	// Store MAC in the stack (the MAC array) and in the NIC
	ldi		r16, PAR0
	movw	r23:r22, r27:r26
	ldi		r26, low(mac)
	ldi		r27, high(mac)
NIC_Init_LoadPAR_External_Loop:
	ld		r17, Z+
	st		X+, r17
	mNIC_WriteQuick
	inc		r16
	dec		r18
	brne	NIC_Init_LoadPAR_External_Loop
	movw	r27:r26, r23:r22
	rjmp	NIC_Init_LoadPAR_Done

NIC_Init_LoadPAR_Internal:
	ldi		r18, 6																	// Store MAC in the stack (the MAC array) and in the NIC
	ldi		r16, PAR0
	movw	r23:r22, r27:r26
	ldi		r26, low(mac)
	ldi		r27, high(mac)
	ldi		r17, 0x0a
NIC_Init_LoadPAR_Internal_Loop:
	st		X+, r17
	mNIC_WriteQuick
	inc		r16
	inc		r17
	dec		r18
	brne	NIC_Init_LoadPAR_Internal_Loop
	movw	r27:r26, r23:r22

NIC_Init_LoadPAR_Done:


	ldi		r18, 8																	// "Accept all" for multicast mode
	ldi		r16, MAR0
	ldi		r17, 0xff
NIC_Init_LoadMAR:
	mNIC_WriteQuick
	inc		r16
	dec		r18
	brne	NIC_Init_LoadMAR

	ldi		r16, CURR																// First Rx Page - AN-874 error! - CURR must NOT equal BNRY
	ldi		r17, RX_RING_BUFFER_START
	mNIC_WriteQuick

	ldi		r16, CR																	// Switch to page 0
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STP
	mNIC_WriteQuick

	ldi		r16, RCR																// Accept broadcasts
	ldi		r17, RCR_AB
	mNIC_WriteQuick

	ldi		r16, ISR																// Clear any pending interrupts
	ldi		r17, 0xff
	mNIC_WriteQuick

	ldi		r16, IMR																// Mask out all interrupts
	ldi		r17, 0
	mNIC_WriteQuick

	ldi		r16, TCR																// No loop-back
	ldi		r17, 0x00
	mNIC_WriteQuick

	ldi		r16, CR																	// Start NIC
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STA
	mNIC_WriteQuick

	ldi		r16, 0x00																// Clean up after mNIC_WriteQuick and return 0
	ldi		r17, 0xff
	out		NIC_DDR, r16
	out		NIC_PORT, r17

	ret

//**************************************************************************************************************************



//**************************************************************************************************************************
// signed short NIC_Get(unsigned char *Buffer, unsigned short MaxLength)
//**************************************************************************************************************************
NIC_Get:
	movw	r31:r30, r17:r16												// Move *Buffer to Z
	ldi		r16, CR																	// Switch to page 1
	ldi		r17, CR_PAGE1|CR_ABORT|CR_STA
	mNIC_Write

	ldi		r16, CURR
	mNIC_Read
	mov		r20, r16

	ldi		r16, CR																	// Switch to page 0
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STA
	mNIC_Write

	ldi		r16, BNRY
	mNIC_Read

	cp		r16, r20
	brne	NIC_GetGoForIt

	ldi		r16, low(-1)														// No packets
	ldi		r17, high(-1)
	ret

NIC_GetGoForIt:
	ldi		r16, ISR																// Clear any pending PRX interrupt
	ldi		r17, ISR_PRX
	mNIC_Write

	ldi		r16, CR																	// DMA Send packet command
	ldi		r17, CR_PAGE0|CR_SEND|CR_STA
	mNIC_Write
	
	ldi		r16, DMA_PORT														// Get the NIC level header and store packet length
	out		NIC_ADR, r16
	mNIC_ReadLoop																	// Dump the first two
	mNIC_ReadLoop																	// ...info bytes
	mNIC_ReadLoop																	// ...but get 
	mov		r20, r16
	mNIC_ReadLoop																	// ...packet length
	mov		r21, r16
	movw	r23:r22, r25:r24												// Back up r25:r24

	movw	r25:r24, r19:r18												// Move MaxLength to r25:r24 to support sbiw
	movw	r19:r18, r27:r26												// Back up r27:r26
	movw	r27:r26, r21:r20												// Move packet length to r27:r26 to support sbiw

NIC_GetLoop:
	mNIC_ReadLoop																	// Get byte
	sbiw	r25:r24, 0
	breq	NIC_GetLoop_DontStore										// Dump it if more than MaxLength bytes received
	st		Z+, r16
NIC_GetLoop_DontStore:
	sbiw	r25:r24, 1
	sbiw	r27:r26, 1
	brne	NIC_GetLoop

	ldi		r16, ISR
	out		NIC_ADR, r16
NIC_GetWait:
	mNIC_ReadLoop
	sbrs	r16, 6																	// Loop till ISR_RDC get set
	rjmp	NIC_GetWait

	ldi		r16, CR
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STA
	mNIC_Write
	
	movw	r27:r26, r19:r18												// Restore r27:r26
	movw	r25:r24, r23:r22												// Restore r25:r24
	movw	r17:r16, r21:r20												// Return packet length
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// signed char NIC_SendBlocking(unsigned char *Buffer, unsigned short Length)
// signed char NIC_Send(unsigned char *Buffer, unsigned short Length)
//**************************************************************************************************************************
NIC_SendBlocking:
	movw	r31:r30, r17:r16												// We need to back up r16 but we save time and copy *buffer to Z at once
NIC_SendBlocking_Loop:
	ldi		r16, CR
	mNIC_Read
	sbrs	r16, 2																	// Block while the transmitter is busy
	rjmp	NIC_Send_GoodToGo
	rjmp	NIC_SendBlocking_Loop

NIC_Send:
	movw	r31:r30, r17:r16												// We need to back up r16 but we save time and copy *buffer to Z at once
	ldi		r16, CR
	mNIC_Read
	sbrs	r16, 2																	// return -1 if CR_TXP is set
	rjmp	NIC_Send_GoodToGo
	ldi		r16, low(-1)
	ldi		r17, high(-1)
	ret

NIC_Send_GoodToGo:
	movw	r23:r22, r25:r24												// Back up r25:r24
	movw	r25:r24, r19:r18												// ...and move Length to r25:r24
	sbrc	r24, 0																	// Make sure Length is even
	adiw	r25:r24, 1
	movw	r19:r18, r25:r24												// back up the new length

	ldi		r16, 0xff																// Set NIC_PORT for output -> prepare for mNIC_WriteQuick
	out		NIC_DDR, r16

	ldi		r16, RBCR0															// Set buffer length
	mov		r17, r18
	mNIC_WriteQuick
	ldi		r16, RBCR1
	mov		r17, r19
	mNIC_WriteQuick

	ldi		r16, RSAR0															// Set Remote Page address
	ldi		r17, 0
	mNIC_WriteQuick

	ldi		r16, RSAR1
	ldi		r17, TX_RING_BUFFER_START
	mNIC_WriteQuick

	ldi		r16, CR																	// Start DMA write
	ldi		r17, CR_PAGE0|CR_WRITE|CR_STA
	mNIC_WriteQuick

	adiw	r25:r24, 1															// Adjust for post-testing
	ldi		r16, DMA_PORT
	out		NIC_ADR, r16
NIC_Send_Loop:
	ld		r16, Z+
	mNIC_WriteQuickLoop
	sbiw	r25:r24, 1
	brne	NIC_Send_Loop

	ldi		r16, 0x00																// Clean up after mNIC_WriteQuick
	ldi		r17, 0xff
	out		NIC_DDR, r16
	out		NIC_PORT, r17

NIC_Send_WaitDMA:																// Wait for DMA complete
	ldi		r16, ISR
	mNIC_Read
	andi	r16, ISR_RDC
	breq	NIC_Send_WaitDMA

	ldi		r16, 0xff																// Set NIC_PORT for output -> prepare for mNIC_WriteQuick
	out		NIC_DDR, r16

	ldi		r16, ISR																// Clear DMA complete interrupt flag
	ldi		r17, ISR_RDC
	mNIC_WriteQuick
	
// Now tell the NIC to send the packet that is stored at the place we just filled up
	ldi		r16, TPSR															// Tell which page the packet starts at
	ldi		r17, TX_RING_BUFFER_START
	mNIC_WriteQuick
	
	ldi		r16, TBCR0															// Set the length of the packet
	mov		r17, r18
	mNIC_WriteQuick
	ldi		r16, TBCR1
	mov		r17, r19
	mNIC_WriteQuick

	ldi		r16, CR																	// Tell the NIC to start transmitting the packet
	ldi		r17, CR_PAGE0|CR_ABORT|CR_STA|CR_TXP
	mNIC_WriteQuick

	ldi		r16, 0x00																// Clean up after mNIC_WriteQuick and return 0
	ldi		r17, 0xff
	out		NIC_DDR, r16
	out		NIC_PORT, r17

	ret
//**************************************************************************************************************************



END
