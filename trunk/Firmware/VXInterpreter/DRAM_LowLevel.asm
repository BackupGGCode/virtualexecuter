#include "ioavr.h"
#include "DRAM.h"


NAME DRAM_LowLevel

RSEG CODE :CODE


;***************************************************************************************
; unsigned char DRAM_ReadByte(dram address)
public DRAM_ReadByte
;***************************************************************************************
DRAM_ReadByte:

  cli
	
  out		ADR_PORT, r16
	
	mov		r16, r17
	mov		r17, r18
	mov		r18, r19
	mov		r20, r16
	andi	r20, 0x03
	ori		r20, 0xe0
	out		CTRL_PORT, r20
	
	lsr		r18
	ror		r17
	ror		r16
	
	cbi		CTRL_PORT, RAS_NUMBER
	
	lsr		r18
	ror		r17
	ror		r16
	out		ADR_PORT, r16
	
	mov		r16, r17
	andi	r16, 0x03
	ori		r16, 0xc0
	out		CTRL_PORT, r16
	
	nop
	
	cbi		CTRL_PORT, CAS_NUMBER
	
	nop
	
	in		r16, DATA_IN
	
	ldi		r17, 0xe0
	out		CTRL_PORT, r17
	
	reti
;***************************************************************************************


;***************************************************************************************
; void DRAM_WriteByte(dram address, unsigned char value)
public DRAM_WriteByte
;***************************************************************************************
DRAM_WriteByte:

	cli
	
	ldi		r21, 0xff
	out		DATA_DDR, r21
	
	out		DATA_OUT, r20
	
	out		ADR_PORT, r16
	
	mov		r16, r17
	mov		r17, r18
	mov		r18, r19
	mov		r20, r16
	andi	r20, 0x03
	ori		r20, 0xa0
	out		CTRL_PORT, r20
	
	lsr		r18
	ror		r17
	ror		r16
	
	cbi		CTRL_PORT, RAS_NUMBER

	lsr		r18
	ror		r17
	ror		r16
	
	out		ADR_PORT, r16

	mov		r16, r17
	andi	r16, 0x03
	ori		r16, 0x80
	out		CTRL_PORT, r16

	ldi		r19, 0

	cbi		CTRL_PORT, CAS_NUMBER
	
	ldi		r16, 0xe0
	out		CTRL_PORT, r16

	out		DATA_DDR, r19

	out		DATA_OUT, r19

	reti
;***************************************************************************************


END
