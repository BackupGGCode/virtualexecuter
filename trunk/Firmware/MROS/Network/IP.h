#define IP_HEADER_SIZE													20
#define IP_HEADER																ETHERNET_HEADER_SIZE

// IP protocol fields
#define IP_VERSION															IP_HEADER+0
#define IP_TYPE																	IP_HEADER+1
#define IP_LENGTH																IP_HEADER+2
#define IP_ID																		IP_HEADER+4
#define IP_FRAGMENT															IP_HEADER+6
#define IP_TTL																	IP_HEADER+8
#define IP_PROTOCOL															IP_HEADER+9
#define IP_CHECKSUM															IP_HEADER+10
#define IP_SOURCE																IP_HEADER+12
#define IP_DESTINATION													IP_HEADER+16
#define IP_OPTIONS															IP_HEADER+20


#define IP_PROTOCOL_ICMP												1
#define IP_PROTOCOL_TCP													6
#define IP_PROTOCOL_UDP													17


extern unsigned long ip;


extern void IP_SetIP(unsigned char ip1, unsigned char ip2, unsigned char ip3, unsigned char ip4);
extern void IP_Send(unsigned long destination, unsigned char protocol, unsigned char *buffer, unsigned short packetLength);
extern void IP_Receive(unsigned char *buffer);
