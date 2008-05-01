#ifndef	NULL
#define NULL																		0
#endif

#define APP_UDP_OVERHEAD												ETHERNET_HEADER_SIZE+IP_HEADER_SIZE+UDP_HEADER_SIZE
#define APP_TCP_OVERHEAD												ETHERNET_HEADER_SIZE+IP_HEADER_SIZE+TCP_HEADER_SIZE


extern unsigned long Num2IP(unsigned char ip4, unsigned char ip3, unsigned char ip2, unsigned char ip1);
//extern unsigned long Str2IP(char* ip);
//extern unsigned long Str2IPF(__flash char* ip);
