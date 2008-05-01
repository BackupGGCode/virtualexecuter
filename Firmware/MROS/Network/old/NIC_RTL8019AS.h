#include "iom32.h"
//#include <config.h>

/*
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
*/

//#define MTU								1514
//#define MTU																			300

#define NETWORK_MTU															300

#if !defined(NETWORK_MTU)
#error "NETWORK_MTU must be defined to set the maximum size of Ethernet packets!"
#endif

#define GLOBAL_BUFFER_SIZE											NETWORK_MTU


#ifndef __IAR_SYSTEMS_ASM__												// Not valid when assembling - only for compiler


/***************************************************************************************************************************
	Public members
***************************************************************************************************************************/

/***************************************************************************************************************************
	Return 0 on success.
***************************************************************************************************************************/
extern signed char NIC_Init(const unsigned char *pMAC);

extern unsigned char NIC_Read(const unsigned char register);
extern void NIC_Write(const unsigned char register, const unsigned char data);
extern signed short NIC_Get(unsigned char *buffer, const unsigned short maxLength);
extern signed char NIC_SendBlocking(const unsigned char *buffer, const unsigned short length);
extern signed char NIC_Send(const unsigned char *buffer, const unsigned short length);

/*
extern unsigned short GetShort(const unsigned char *buffer);
extern unsigned long GetLong(const unsigned char *buffer);
extern void PutShort(unsigned char *buffer, unsigned short value);
extern void PutLong(unsigned char *buffer, unsigned long value);
extern void MemCpy(const unsigned char *src,unsigned char *des,unsigned short length);
extern void MemCpyR(const unsigned char *src,unsigned char *des,unsigned short length);
extern void Swap(unsigned char *src,unsigned char *des,unsigned short length);
extern void Clear(unsigned char *buf,unsigned short length);
extern unsigned short OnesComplementChecksum(const unsigned char *buf, unsigned short length);
*/

extern unsigned char mac[6];
extern unsigned char globalBuffer[GLOBAL_BUFFER_SIZE];

/***************************************************************************************************************************
	End of public members
***************************************************************************************************************************/

#endif	// __IAR_SYSTEMS_ASM__
