/*


*------------------*
| UDP | TCP | ICMP |
*------------------*
|    IP   |   ARP  |
*------------------*
|     Ethernet     |
*------------------*
|  NIC interface   |
*------------------*
|       NIC        |
*------------------*


*/

#include <Globals.h>
#include <Network/Assist.h>
#include <Network/NIC.h>
#include <Network/Ethernet.h>
#include <Network/ARP.h>
#include <Network/IP.h>
#include <Network/ICMP.h>
#include <Network/UDP.h>


extern unsigned char Network_Init(unsigned char ip1, unsigned char ip2, unsigned char ip3, unsigned char ip4);
extern unsigned long Num2IP(unsigned char ip1, unsigned char ip2, unsigned char ip3, unsigned char ip4);
//extern unsigned long Str2IP(char* ip);
//extern unsigned long Str2IP_P(__flash char* ip);
