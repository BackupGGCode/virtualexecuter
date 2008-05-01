#include <ENC28J60.h>



/***************************************************************************************************************************
	ENC28J60 registers
***************************************************************************************************************************/

#define BANK0								(0<<5)
#define BANK1								(1<<5)
#define BANK2								(2<<5)
#define BANK3								(3<<5)


// All banks - common registers

//#define RESERVED					0x1a
#define EIE									0x1b
#define EIR									0x1c
#define ESTAT								0x1d
#define ECON2								0x1e
#define ECON1								0x1f


// Bank 0

#define ERDPTL							BANK0+0x00
#define ERDPTH							BANK0+0x01
#define EWRPTL							BANK0+0x02
#define EWRPTH							BANK0+0x03
#define ETXSTL							BANK0+0x04
#define ETXSTH							BANK0+0x05
#define ETXNDL							BANK0+0x06
#define ETXNDH							BANK0+0x07
#define ERXSTL							BANK0+0x08
#define ERXSTH							BANK0+0x09
#define ERXNDL							BANK0+0x0a
#define ERXNDH							BANK0+0x0b
#define ERXRDPTL						BANK0+0x0c
#define ERXRDPTH						BANK0+0x0d
#define ERXWRPTL						BANK0+0x0e
#define ERXWRPTH						BANK0+0x0f
#define EDMASTL							BANK0+0x10
#define EDMASTH							BANK0+0x11
#define EDMANDL							BANK0+0x12
#define EDMANDH							BANK0+0x13
#define EDMADSTL						BANK0+0x14
#define EDMADSTH						BANK0+0x15
#define EDMACSL							BANK0+0x16
#define EDMACSH							BANK0+0x17
//#define -									BANK0+0x18
//#define -									BANK0+0x19

// Bank 1

#define EHT0								BANK1+0x00
#define EHT1								BANK1+0x01
#define EHT2								BANK1+0x02
#define EHT3								BANK1+0x03
#define EHT4								BANK1+0x04
#define EHT5								BANK1+0x05
#define EHT6								BANK1+0x06
#define EHT7								BANK1+0x07
#define EPMM0								BANK1+0x08
#define EPMM1								BANK1+0x09
#define EPMM2								BANK1+0x0a
#define EPMM3								BANK1+0x0b
#define EPMM4								BANK1+0x0c
#define EPMM5								BANK1+0x0d
#define EPMM6								BANK1+0x0e
#define EPMM7								BANK1+0x0f
#define EPMCSL							BANK1+0x10
#define EPMCSH							BANK1+0x11
//#define -									BANK1+0x12
//#define -									BANK1+0x13
#define EPMOL								BANK1+0x14
#define EPMOH								BANK1+0x15
//#define RESERVED					BANK1+0x16
//#define RESERVED					BANK1+0x17
#define ERXFCON							BANK1+0x18
#define EPKTCNT							BANK1+0x19

// Bank 2

#define MACON1							BANK2+0x00
//#define RESERVED					BANK2+0x01
#define MACON3							BANK2+0x02
#define MACON4							BANK2+0x03
#define MABBIPG							BANK2+0x04
//#define -									BANK2+0x05
#define MAIPGL							BANK2+0x06
#define MAIPGH							BANK2+0x07
#define MACLCON1						BANK2+0x08
#define MACLCON2						BANK2+0x09
#define MAMXFLL							BANK2+0x0a
#define MAMXFLH							BANK2+0x0b
//#define RESERVED					BANK2+0x0c
//#define RESERVED					BANK2+0x0d
//#define RESERVED					BANK2+0x0e
//#define -									BANK2+0x0f
//#define RESERVED					BANK2+0x10
//#define RESERVED					BANK2+0x11
#define MICMD								BANK2+0x12
//#define -									BANK2+0x13
#define MIREGADR						BANK2+0x14
//#define RESERVED					BANK2+0x15
#define MIWRL								BANK2+0x16
#define MIWRH								BANK2+0x17
#define MIRDL								BANK2+0x18
#define MIRDH								BANK2+0x19

// Bank 3

#define MAADR5							BANK3+0x00
#define MAADR6							BANK3+0x01
#define MAADR3							BANK3+0x02
#define MAADR4							BANK3+0x03
#define MAADR1							BANK3+0x04
#define MAADR2							BANK3+0x05
#define EBSTSD							BANK3+0x06
#define EBSTCON							BANK3+0x07
#define EBSTCSL							BANK3+0x08
#define EBSTCSH							BANK3+0x09
#define MISTAT							BANK3+0x0a
//#define -									BANK3+0x0b
//#define -									BANK3+0x0c
//#define -									BANK3+0x0d
//#define -									BANK3+0x0e
//#define -									BANK3+0x0f
//#define -									BANK3+0x10
//#define -									BANK3+0x11
#define EREVID							BANK3+0x12
//#define -									BANK3+0x13
//#define -									BANK3+0x14
#define ECOCON							BANK3+0x15
//#define RESERVED					BANK3+0x16
#define EFLOCON							BANK3+0x17
#define EPAUSL							BANK3+0x18
#define EPAUSH							BANK3+0x19


/***************************************************************************************************************************
	ENC28J60 SPI commands
***************************************************************************************************************************/

#define CMD_READ_REGISTER		0x00
#define CMD_WRITE_REGISTER	0x40
#define CMD_READ_BUFFER			0x3a
#define CMD_WRITE_BUFFER		0x7a
#define CMD_SET_BITS				0x80
#define CMD_CLEAR_BITS			0xa0
#define CMD_RESET						0xff




unsigned char MAC[6];
unsigned char GlobalBuffer[GLOBAL_BUFFER_SIZE];


/***************************************************************************************************************************
	Shadow registers
***************************************************************************************************************************/

unsigned char sECON1;

/***************************************************************************************************************************

***************************************************************************************************************************/


signed char NIC_Init(const unsigned char *pMAC)
{
// SPI_Init(SPI_PRESCALER_16);
// SPI_Transfer(0x00);
	sECON1=0x00;																	// Needed ?
	ECON1=sECON1;

	ECON2=(1<<7)|(1<<6);													// Auto pointer inc and auto packet counter dec

	ERXSTL=RX_BUFFER_START & 0xff;								// Set start of reception buffer
	ERXSTH=RX_BUFFER_START >> 8;
	ERXNDL=RX_BUFFER_END & 0xff;									// Set end of reception buffer
	ERXNDH=RX_BUFFER_END >> 8;
	ERXRDPTL=RX_BUFFER_START & 0xff;							// Set reception pointer to start of reception buffer
	ERXRDPTH=RX_BUFFER_START >> 8;
	
	
	// ERXRDPT = First free location to signal buffer full to the receiving logic
	
	// Automatic padding - MACON3.PADCFG<2:0>
	
	// Automatic Frame level CRC checking - ERXFCON.CRCEN
	// Automatic Frame level CRC generation - MACON3.PADCFG<2:0>
	
	

	ECOCON=0x00;																	// Disable the clock out pin

	MACON1=

	sECON1=(1<<2)|0x00;														// Enable reception
	ECON1=sECON1;
	
	return 0;
}

unsigned char NIC_Read(const unsigned char Register)
{
	return 0;
}


void NIC_Write(const unsigned char Register, const unsigned char Data)
{

}


signed short NIC_Get(unsigned char *Buffer, const unsigned short MaxLength)
{
	return 0;
}


signed char NIC_SendBlocking(const unsigned char *Buffer, const unsigned short Length)
{
	return 0;
}


signed char NIC_Send(const unsigned char *Buffer, const unsigned short Length)
{
	return 0;
}

static void SendSystemReset(void)
{
	BYTE Dummy;

	ENC_CS_IO = 0;
	ENC_SPI_IF = 0;
	ENC_SSPBUF = SR;
	while(!ENC_SPI_IF);		// Wait until the command is transmitted.
	Dummy = ENC_SSPBUF;
	ENC_SPI_IF = 0;
	ENC_CS_IO = 1;
}//end SendSystemReset