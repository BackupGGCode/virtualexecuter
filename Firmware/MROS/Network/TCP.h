#ifndef	NULL
#define NULL																		0
#endif

#define TCP_HEADER_SIZE													8
#define TCP_HEADER															ETHERNET_HEADER_SIZE+IP_HEADER_SIZE

// TCP protocol fields
#define TCP_SOURCE															TCP_HEADER+0
#define TCP_DESTINATION													TCP_HEADER+2
#define TCP_LENGTH															TCP_HEADER+4
#define TCP_CHECKSUM														TCP_HEADER+6
#define TCP_DATA																TCP_HEADER+8

#define TCP_DISABLE_CHECKSUM										0

#define TCP_MAX_REGISTERED_PORTS								3


// TCP Error codes
#define TCP_OK																	0
#define TCP_ERROR_NO_FREE_SLOTS									1
#define TCP_ERROR_PORT_ALREADY_REGISTERED				2
#define TCP_ERROR_PORT_NOT_REGISTERED						3


extern void TCP_Send(unsigned long destinationIP, unsigned short destinationPort, unsigned short sourcePort, unsigned char *buffer, unsigned short length);
extern void TCP_Receive(unsigned char *buffer);
unsigned char TCP_RegisterPort(unsigned short port, void (*theFunction)(unsigned char *buffer));
unsigned char TCP_UnregisterPort(unsigned short port);
