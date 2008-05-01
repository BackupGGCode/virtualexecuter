#define ETHERNET_HEADER_SIZE										14

// ARP header fields
#define ETHERNET_DESTINATION										0
#define ETHERNET_SOURCE													6
#define ETHERNET_TYPE														12
#define ETHERNET_DATA														14


#define ETHERNET_TYPE_IP												0x0800
#define ETHERNET_TYPE_ARP												0x0806


extern void ETHERNET_Send(const unsigned char *destination, unsigned short type, unsigned char *buffer, unsigned short length);
extern void ETHERNET_Receive();
