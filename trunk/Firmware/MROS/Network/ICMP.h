#define ICMP_HEADER_SIZE												4
#define ICMP_HEADER															ETHERNET_HEADER_SIZE+IP_HEADER_SIZE

// ICMP header fields
#define ICMP_TYPE																ICMP_HEADER+0
#define ICMP_CODE																ICMP_HEADER+1
#define ICMP_CHECKSUM														ICMP_HEADER+2
#define ICMP_DATA																ICMP_HEADER+4

// ICMP Types
#define ICMP_TYPE_REPLY													0
#define ICMP_TYPE_REQUEST												8

// ICMP Codes
#define ICMP_CODE_ZERO													0


extern void ICMP_Receive(unsigned char *buffer, unsigned short packetLength);
