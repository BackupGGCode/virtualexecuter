#include <globals.h>


#define SPI_PRESCALER_2						0x04 | 0x00
#define SPI_PRESCALER_4						0x00
#define SPI_PRESCALER_8						0x04 | 0x01
#define SPI_PRESCALER_16					0x01
#define SPI_PRESCALER_32					0x04 | 0x02
#define SPI_PRESCALER_64					0x02	// Alternaltivly 0x07 could be used (SPI2X = 1)
#define SPI_PRESCALER_128					0x03


extern void SPI_Init(unsigned char Prescaler);
extern unsigned char SPI_Transfer(unsigned char Data);
