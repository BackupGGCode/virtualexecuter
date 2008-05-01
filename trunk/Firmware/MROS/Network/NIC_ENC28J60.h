#include <Globals.h>
#include "NIC.h"


unsigned char ReadRegister(unsigned char address);
void WriteRegister(unsigned char address, unsigned char data);
unsigned short ReadWideRegister(unsigned char address);
void WriteWideRegister(unsigned char address, unsigned short data);
void SetBits(unsigned char address, unsigned char bits);
void ClearBits(unsigned char address, unsigned char bits);
unsigned short ReadPhyRegister(unsigned char address);
void WritePhyRegister(unsigned char address, unsigned short data);
void ReadBuffer(unsigned short address, unsigned char* buffer, unsigned short length);
void WriteBuffer(unsigned short address, unsigned char* buffer, unsigned short length);
void SelectBank(unsigned char address);
bool IsPhyBusy();


// Instructions

#define OPCODE_RCR															0x00
#define OPCODE_RBM															0x3a
#define OPCODE_WCR															0x40
#define OPCODE_WBM															0x7a
#define OPCODE_BFS															0x80
#define OPCODE_BFC															0xa0
#define OPCODE_SRC															0xff


// Registers

#define COMMON_BANK															(0x07 << 5)
#define BANK0																		(0 << 5)
#define BANK1																		(1 << 5)
#define BANK2																		(2 << 5)
#define BANK3																		(3 << 5)


// Common registers

#define EIE																			COMMON_BANK | 0x1b
#define EIR																			COMMON_BANK | 0x1c
#define ESTAT																		COMMON_BANK | 0x1d
#define _CLKRDY																	0
#define _TXABRT																	1
#define _RXBUSY																	2
#define _LATECOL																4
#define _BUFER																	6
#define _INT																		7
#define ECON2																		COMMON_BANK | 0x1e
#define _VRPS																		3
#define _PWRSV																	5
#define _PKTDEC																	6
#define _AUTOINC																7
#define ECON1																		COMMON_BANK | 0x1f
#define _BSEL0																	0
#define _BSEL1																	1
#define _RXEN																		2
#define _TXRTS																	3
#define _CSUMEN																	4
#define _DMAST																	5
#define _RXRST																	6
#define _TXRST																	7


// Bank 0 registers

#define ERDPTL																	BANK0 | 0x00
#define ERDPTH																	BANK0 | 0x01
#define EWRPTL																	BANK0 | 0x02
#define EWRPTH																	BANK0 | 0x03
#define ETXSTL																	BANK0 | 0x04
#define ETXSTH																	BANK0 | 0x05
#define ETXNDL																	BANK0 | 0x06
#define ETXNDH																	BANK0 | 0x07
#define ERXSTL																	BANK0 | 0x08
#define ERXSTH																	BANK0 | 0x09
#define ERXNDL																	BANK0 | 0x0a
#define ERXNDH																	BANK0 | 0x0b
#define ERXRDPTL																BANK0 | 0x0c
#define ERXRDPTH																BANK0 | 0x0d
#define ERXWRPTL																BANK0 | 0x0e
#define ERXWRPTH																BANK0 | 0x0f
#define EDMASTL																	BANK0 | 0x10
#define EDMASTH																	BANK0 | 0x11
#define EDMANDL																	BANK0 | 0x12
#define EDMANDH																	BANK0 | 0x13
#define EDMADSTL																BANK0 | 0x14
#define EDMADSTH																BANK0 | 0x15
#define EDMACSL																	BANK0 | 0x16
#define EDMACSH																	BANK0 | 0x17

// Bank 1 registers

#define EHT0																		BANK1 | 0x00
#define EHT1																		BANK1 | 0x01
#define EHT2																		BANK1 | 0x02
#define EHT3																		BANK1 | 0x03
#define EHT4																		BANK1 | 0x04
#define EHT5																		BANK1 | 0x05
#define EHT6																		BANK1 | 0x06
#define EHT7																		BANK1 | 0x07
#define EPMM0																		BANK1 | 0x08
#define EPMM1																		BANK1 | 0x09
#define EPMM2																		BANK1 | 0x0a
#define EPMM3																		BANK1 | 0x0b
#define EPMM4																		BANK1 | 0x0c
#define EPMM5																		BANK1 | 0x0d
#define EPMM6																		BANK1 | 0x0e
#define EPMM7																		BANK1 | 0x0f
#define EPMCSL																	BANK1 | 0x10
#define EPMCSH																	BANK1 | 0x11
#define EPMOL																		BANK1 | 0x14
#define EPMOH																		BANK1 | 0x15
#define ERXFCON																	BANK1 | 0x18
#define EPKTCNT																	BANK1 | 0x19

// Bank 2 registers

#define MACON1																	BANK2 | 0x00
#define _MARXEN																	0
#define _PASSALL																1
#define _RXPAUS																	2
#define _TXPAUS																	3
#define MACON3																	BANK2 | 0x02
#define _FULDPX																	0
#define _FMRLNEN																1
#define _HFRMEN																	2
#define _PHDREN																	3
#define _TXCRCEN																4
#define _PADCFG0																5
#define _PADCFG1																6
#define _PADCFG2																7
#define MACON4																	BANK2 | 0x03
#define MABBIPG																	BANK2 | 0x04
#define MAIPGL																	BANK2 | 0x06
#define MAIPGH																	BANK2 | 0x07
#define MACLCON1																BANK2 | 0x08
#define MACLCON2																BANK2 | 0x09
#define MAMXFLL																	BANK2 | 0x0a
#define MAMXFLH																	BANK2 | 0x0b
#define MICMD																		BANK2 | 0x12
#define _MIIRD																	0
#define _MIISCAN																1
#define MIREGADR																BANK2 | 0x14
#define MIWRL																		BANK2 | 0x16
#define MIWRH																		BANK2 | 0x17
#define MIRDL																		BANK2 | 0x18
#define MIRDH																		BANK2 | 0x19

// Bank 3 registers

#define MAADR5																	BANK3 | 0x00
#define MAADR6																	BANK3 | 0x01
#define MAADR3																	BANK3 | 0x02
#define MAADR4																	BANK3 | 0x03
#define MAADR1																	BANK3 | 0x04
#define MAADR2																	BANK3 | 0x05
#define EBSTSD																	BANK3 | 0x06
#define EBSTCON																	BANK3 | 0x07
#define EBSTCSL																	BANK3 | 0x08
#define EBSTCSH																	BANK3 | 0x09
#define MISTAT																	BANK3 | 0x0a
#define _BUSY																		0
#define _SCAN																		1
#define _NVALID																	2
#define EREVID																	BANK3 | 0x12
#define ECOCON																	BANK3 | 0x15
#define EFLOCON																	BANK3 | 0x17
#define EPAUSL																	BANK3 | 0x18
#define EPAUSH																	BANK3 | 0x19


// Phy register

#define PHCON1																	0x00
#define PHSTAT1																	0x01
#define PHID1																		0x02
#define PHID2																		0x03
#define PHCON2																	0x10
#define PHSTAT2																	0x11
#define PHIE																		0x12
#define PHIR																		0x13
#define PHLCON																	0x14
