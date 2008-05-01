#define UDP_HEADER_SIZE													8
#define UDP_HEADER															ETHERNET_HEADER_SIZE+IP_HEADER_SIZE

// UDP header fields
#define UDP_SOURCE															UDP_HEADER+0
#define UDP_DESTINATION													UDP_HEADER+2
#define UDP_LENGTH															UDP_HEADER+4
#define UDP_CHECKSUM														UDP_HEADER+6
#define UDP_DATA																UDP_HEADER+8


#define UDP_DISABLE_CHECKSUM										0

#define UDP_MAX_REGISTERED_PORTS								3														// Each slot takes up 4 bytes (unsigned short + function pointer)


// UDP Error codes
#define UDP_OK																	0
#define UDP_ERROR_NO_FREE_SLOTS									1
#define UDP_ERROR_PORT_ALREADY_REGISTERED				2
#define UDP_ERROR_PORT_NOT_REGISTERED						3


extern void UDP_Send(unsigned long destinationIP, unsigned short destinationPort, unsigned short sourcePort, unsigned char *buffer, unsigned short length);
extern void UDP_Receive(unsigned char *buffer);
unsigned char UDP_RegisterPort(unsigned short port, void (*theFunction)(unsigned char *buffer));
unsigned char UDP_UnregisterPort(unsigned short port);
