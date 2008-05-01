#include "SPI.h"


void SPI_Init(unsigned char Prescaler)
{
#if defined(__IOM64_H)
	DDRB |= (1 << 2) | (1 << 1);
	DDRB &= ~(1 << 3);
#elif defined(__IOM32_H) || defined(__IOM162_H)
	DDRB |= (1 << 7) | (1 << 5);
	DDRB &= ~(1 << 6);
#else
#error Module SPI does not support the selected processor!
#endif

	SPCR = (1 << SPE) | (1 << MSTR) | (Prescaler & 0x03);		// Mode 0,0 - sample on leading (rising) edge and setup on trailing (falling) edge

	if(Prescaler & 0x04)
		SPSR = 0x01;
	else
		SPSR = 0x00;
}


unsigned char SPI_Transfer(unsigned char Data)
{
	SPDR = Data;

	while((SPSR & (1 << SPIF)) == false);

	return SPDR;
}
