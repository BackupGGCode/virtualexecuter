#include <ioavr.h>



#define NIC_PORT																PORTC
#define NIC_DDR																	DDRC
#define NIC_PIN																	PINC

#define NIC_ADR																	PORTB

#define NIC_WR_PORT															PORTA
#define NIC_WR_BIT															5
#define NIC_RD_PORT															PORTA
#define NIC_RD_BIT															6
#define NIC_RESET_PORT													PORTA
#define NIC_RESET_BIT														4

#define MTU																			1514
#define GLOBAL_BUFFER_SIZE											500



/***************************************************************************************************************************
	Public members
***************************************************************************************************************************/

extern unsigned char MAC[6];
extern unsigned char GlobalBuffer[GLOBAL_BUFFER_SIZE];

extern signed char NIC_Init(const unsigned char *pMAC);

extern unsigned char NIC_Read(const unsigned char Register);
extern void NIC_Write(const unsigned char Register, const unsigned char Data);
extern signed short NIC_Get(unsigned char *Buffer, const unsigned short MaxLength);
extern signed char NIC_SendBlocking(const unsigned char *Buffer, const unsigned short Length);
extern signed char NIC_Send(const unsigned char *Buffer, const unsigned short Length);

/***************************************************************************************************************************
	End of public members
***************************************************************************************************************************/
