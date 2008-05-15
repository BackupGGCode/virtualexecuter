;******************************************************************************
; Function to initialize the communications channel.
;******************************************************************************
ComInit:
	ldi		temp, high(BAUDRATE)
;	out		UBRR0H, temp
	sts		UBRR0H, temp
	ldi		temp, low(BAUDRATE)
	out		UBRR0L, temp
	ldi		temp, (1<<U2X0)
	out		UCSR0A, temp
	ldi		temp, (1<<RXEN0)|(1<<TXEN0)
	out		UCSR0B, temp
	ret
;******************************************************************************



;******************************************************************************
; Function to send a byte to the host.
; Data must be placed in register 'temp' prior to the function call.
;******************************************************************************
ComPut:
	sbis	UCSR0A, UDRE0
	rjmp	ComPut
	out		UDR0, temp
	ret
;******************************************************************************



;******************************************************************************
; Function to read a byte sent from the host.
; Data is returned in register 'temp'.
;******************************************************************************
ComGet:
	sbis	UCSR0A, RXC0
	rjmp	ComGet
	in		temp, UDR0
	ret
;******************************************************************************



;******************************************************************************
; Function to test if data has arrived from the host.
; After the function call the register 'temp' is cleared if no data was ready
; otherwise 'temp' is set to 1.
;******************************************************************************
ComDataReady:
	ldi		temp, 0
	sbic	UCSR0A, RXC0
	ldi		temp, 1
	ret
;******************************************************************************
