#include "NIC_ENC28J60.h"
#include <Kernel/Kernel.h>
#include <Peripherals/SPI.h>


#define NIC_CS																	PORTB_Bit4


static unsigned char selectedBank = 0xff;

unsigned char mac[6]={0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff};


unsigned char GetVersion()
{
	return ReadRegister(EREVID);
}


bool NIC_Init(unsigned char* userMac)
{
	// Wait for OST oscillator start up
	Kernel_Delay(3);
	// Configure Rx buffer
	WriteWideRegister(ERXSTL, 0);
	WriteWideRegister(ERXNDL, 6000);
	WriteWideRegister(ERXRDPTL, 0);
	// Configure Tx buffer
	// none needed
	
	// Set receive filters => ERXFCON
		// Default seems fine - uni and broad cast are accepted
	
// MAC setup
	// MACON1.MARXEN = 1
		// if full duplex also set TXPAUS and RXPAUS
	WriteRegister(MACON1, (1 << _TXPAUS) | (1 << _RXPAUS) | (1 << _MARXEN));
	
	// MACON3.PADCFG
	// MACON3.TXCRCEN
	// MACON3.FULDPX = 1
	// FRMLNEN
	WriteRegister(MACON3, 0xe0 | (1 << _TXCRCEN) | (1 << _FULDPX));
	
	// MACON4
		// DEFER - only if HalfDuplex
	
	// MAMXFL 1518 bytes or less
	WriteWideRegister(MAMXFLL, 1516);
	
	// MABBIPG FullDuplex=0x15 / HalfDuplex=0x12
	WriteRegister(MABBIPG, 0x15);
	
	// MAIPGL = 0x12
	WriteRegister(MAIPGL, 0x12);
	// if HalfDuplex => MAIPGH = 0x0c
	
	// MAC ADR => MAADR1-6
	if(userMac != null)
	{
		mac[0] = userMac[0];
		mac[1] = userMac[1];
		mac[2] = userMac[2];
		mac[3] = userMac[3];
		mac[4] = userMac[4];
		mac[5] = userMac[5];
	}
	
	WriteRegister(MAADR1, mac[0]);
	WriteRegister(MAADR2, mac[1]);
	WriteRegister(MAADR3, mac[2]);
	WriteRegister(MAADR4, mac[3]);
	WriteRegister(MAADR5, mac[4]);
	WriteRegister(MAADR6, mac[5]);
	
	return true;
}




signed short NIC_Get(unsigned char *buffer, const unsigned short maxLength)
{
	return 0;
}

signed char NIC_SendBlocking(const unsigned char *buffer, const unsigned short length)
{
	return 0;
}

signed char NIC_Send(const unsigned char *buffer, const unsigned short length)
{
	return 0;
}

unsigned char mac[6];
unsigned char globalBuffer[GLOBAL_BUFFER_SIZE];




/**

INTERNAL FUNCTIONS

**/






unsigned char ReadRegister(unsigned char address)
{
unsigned char data;

	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_RCR | (address & 0x1f));
	if((MACON1 <= address) && (address <= MISTAT))																				// Dummy read for MAC and MII registers
		SPI_Transfer(0x00);
	data = SPI_Transfer(0x00);
	
	NIC_CS = 1;
	
	return data;
}


void WriteRegister(unsigned char address, unsigned char data)
{
	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_WCR | (address & 0x1f));
	SPI_Transfer(data);
	
	NIC_CS = 1;
}


unsigned short ReadWideRegister(unsigned char address)
{
unsigned short data;

	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_RCR | (address & 0x1f));
	if((MACON1 <= address) && (address <= MISTAT))																				// Dummy read for MAC and MII registers
		SPI_Transfer(0x00);
	data = SPI_Transfer(0x00);
	
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	
	address++;
	
	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_RCR | (address & 0x1f));
	if((MACON1 <= address) && (address <= MISTAT))																				// Dummy read for MAC and MII registers
		SPI_Transfer(0x00);
	data |= (SPI_Transfer(0x00) << 8);
	
	NIC_CS = 1;
	
	return data;
}


void WriteWideRegister(unsigned char address, unsigned short data)
{
	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_WCR | (address & 0x1f));
	SPI_Transfer(data & 0xff);
	
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	NIC_CS = 1;
	
	address++;
	
	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_WCR | (address & 0x1f));
	SPI_Transfer(data >> 8);
	
	NIC_CS = 1;
}


void SetBits(unsigned char address, unsigned char bits)
{
	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_BFS | (address & 0x1f));
	SPI_Transfer(bits);
	
	NIC_CS = 1;
}


void ClearBits(unsigned char address, unsigned char bits)
{
	SelectBank(address);

	NIC_CS = 0;
	
	SPI_Transfer(OPCODE_BFC | (address & 0x1f));
	SPI_Transfer(bits);
	
	NIC_CS = 1;
}


unsigned short ReadPhyRegister(unsigned char address)
{
unsigned short data;

	WriteRegister(MIREGADR, address);
	SetBits(MICMD, (1 << _MIIRD));
	while(IsPhyBusy());
	ClearBits(MICMD, (1 << _MIIRD));
	data = ReadRegister(MIRDH);
	data <<= 8;
	data |= ReadRegister(MIRDL);
	
	return data;
}


void WritePhyRegister(unsigned char address, unsigned short data)
{
	WriteRegister(MIREGADR, address);
	WriteRegister(MIWRL, (data & 0xff));
	WriteRegister(MIWRH, (data >> 8));
	while(IsPhyBusy());
}


void ReadBuffer(unsigned short address, unsigned char* buffer, unsigned short length)
{

}


void WriteBuffer(unsigned short address, unsigned char* buffer, unsigned short length)
{

}




void SelectBank(unsigned char address)
{
	if(address < COMMON_BANK && selectedBank != (address & 0xe0))
	{
		ClearBits(ECON1, (1 << _BSEL1) | (1 << _BSEL0));
		SetBits(ECON1, ((address >> 5) & 0x03));
		selectedBank = address & 0x60;
	}
}

bool IsPhyBusy()
{
	if(ReadRegister(MISTAT) & (1 << _BUSY))
		return true;
	else
		return false;
}
