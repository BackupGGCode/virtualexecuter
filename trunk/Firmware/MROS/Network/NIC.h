

#define MTU																			300
#define GLOBAL_BUFFER_SIZE											MTU


extern unsigned char GetVersion();

extern bool NIC_Init(unsigned char *pMAC);

extern signed short NIC_Get(unsigned char *buffer, const unsigned short maxLength);
extern signed char NIC_SendBlocking(const unsigned char *buffer, const unsigned short length);
extern signed char NIC_Send(const unsigned char *buffer, const unsigned short length);

extern unsigned char mac[6];
extern unsigned char globalBuffer[GLOBAL_BUFFER_SIZE];
