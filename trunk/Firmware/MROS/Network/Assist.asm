NAME AssistFunctions


//**************************************************************************************************************************
//**************************************************************************************************************************
// Public symbols
//**************************************************************************************************************************
//**************************************************************************************************************************

//unsigned short GetShort(unsigned char *Buffer)
Public GetShort
//unsigned long GetLong(unsigned char *Buffer)
Public GetLong
//void PutShort(unsigned char *Buffer, unsigned short value)
Public PutShort
//void PutLong(unsigned char *Buffer, unsigned long value)
Public PutLong
//void MemCpy(unsigned char *src,unsigned char *des,unsigned short length)
Public MemCpy
//void MemCpyRunsigned char *src,unsigned char *des,unsigned short length)
Public MemCpyR
//void Swap(unsigned char *src,unsigned char *des,unsigned short length)
Public Swap
//void Clear(unsigned char *buf,unsigned short length)
Public Clear
//unsigned short OnesComplementChecksum(unsigned char *buf, unsigned short Length)
Public OnesComplementChecksum



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
// unsigned short GetShort(unsigned char *Buffer)
//**************************************************************************************************************************
GetShort:
	movw	r31:r30,r17:r16
	ld		r17, Z+
	ld		r16, Z
	ret
//**************************************************************************************************************************

	
//**************************************************************************************************************************
// unsigned long GetLong(unsigned char *Buffer)
//**************************************************************************************************************************
GetLong:
	movw	r31:r30,r17:r16
	ld		r19, Z+
	ld		r18, Z+
	ld		r17, Z+
	ld		r16, Z
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void PutShort(unsigned char *Buffer, unsigned short value)
//**************************************************************************************************************************
PutShort:
	movw	r31:r30,r17:r16
	st		Z+, r19
	st		Z, r18
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void PutLong(unsigned char *Buffer, unsigned long value)

//              r17:r16                r23:r22:r21:r20

//**************************************************************************************************************************
PutLong:
	movw	r31:r30,r17:r16
	st		Z+, r23
	st		Z+, r22
	st		Z+, r21
	st		Z, r20
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void MemCpy(unsigned char *src,unsigned char *des,unsigned short length)
//**************************************************************************************************************************
MemCpy:
	movw	r23:r22, r27:r26	; Back up X
	movw	r27:r26, r17:r16	; Copy *src to X
	movw	r31:r30, r19:r18	; Copy *des to Z
	movw	r19:r18, r25:r24	; Back up register pair r25:r24
	movw	r25:r24, r21:r20	; Copy length to register pair r25:r24
MemCpy_loop:
	ld		r16, X+
	st		Z+, r16
	sbiw	r25:r24, 1
	brne	MemCpy_loop
	movw	r27:r26, r23:r22	; Restore X
	movw	r25:r24, r19:r18	; Restore register pair r25:r24
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// Same as MemCpy but copies bytes in reverse order
// void MemCpyr(unsigned char *src,unsigned char *des,unsigned short length)
//**************************************************************************************************************************
MemCpyR:
	movw	r23:r22, r27:r26	; Back up X
	movw	r27:r26, r17:r16	; Copy *src to X
	movw	r31:r30, r19:r18	; Copy *des to Z
	movw	r19:r18, r25:r24	; Back up register pair r25:r24
	movw	r25:r24, r21:r20	; Copy length to register pair r25:r24
	add		r26, r24					; Add length to X
	adc		r27, r25
	add		r30, r24					; Add length to Z
	adc		r31, r25
MemCpyR_loop:
	ld		r16, -X
	st		-Z, r16
	sbiw	r25:r24, 1
	brne	MemCpyR_loop
	movw	r27:r26, r23:r22	; Restore X
	movw	r25:r24, r19:r18	; Restore register pair r25:r24
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void Swap(unsigned char *buf1,unsigned char *buf2,unsigned short length)
//**************************************************************************************************************************
Swap:
	movw	r23:r22, r27:r26	; Back up X
	movw	r27:r26, r17:r16	; Copy *buf1 to X
	movw	r31:r30, r19:r18	; Copy *buf2 to Z
	movw	r19:r18, r25:r24	; Back up register pair r25:r24
	movw	r25:r24, r21:r20	; Copy length to register pair r25:r24
Swap_loop:
	ld		r16, X
	ld		r17, Z
	st		X+, r17
	st		Z+, r16
	sbiw	r25:r24, 1
	brne	Swap_loop
	movw	r27:r26, r23:r22	; Restore X
	movw	r25:r24, r19:r18	; Restore register pair r25:r24
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// void Clear(unsigned char *buf,unsigned short length)
//**************************************************************************************************************************
Clear:
	movw	r31:r30, r17:r16	; Copy *buf to Z
	movw	r23:r22, r25:r24	; Back up register pair r25:r24
	movw	r25:r24, r19:r18	; Copy length to register pair r25:r24
	clr		r16
Clear_loop:
	st		Z+, r16
	sbiw	r25:r24, 1
	brne	Clear_loop
	movw	r25:r24, r23:r22	; Restore register pair r25:r24
	ret
//**************************************************************************************************************************


//**************************************************************************************************************************
// unsigned short OnesComplementChecksum(unsigned char *buf, unsigned short Length)
//**************************************************************************************************************************
OnesComplementChecksum:
	movw	r31:r30, r17:r16																																; Copy *buf to Z
	movw	r23:r22, r25:r24																																; Back up register pair r25:r24
	movw	r25:r24, r19:r18																																; Copy length to register pair r25:r24
	clr		r18																																							; Divide by two and round up if the original length was odd
	lsr		r25
	ror		r24
	adc		r24,r18
	adc		r25,r18
	clr		r16
	clr		r17
	clr		r20
OnesComplementChecksum_loop:
	ld		r19, Z+																																					; r19:r18 = *Z++
	ld		r18, Z+																																					; 
	add		r16,r18																																					; r17:r16 += r19:r18
	adc		r17,r19																																					; 
	adc		r16,r20																																					; Add the carry
	adc		r17,r20																																					; 
	sbiw	r25:r24, 1																																			; R25:r24--
	brne	OnesComplementChecksum_loop
	movw	r25:r24, r23:r22	; Restore register pair r25:r24
	com		r17
	com		r16
	ret
//**************************************************************************************************************************



END
