#define ARP_HEADER_SIZE													28
#define ARP_HEADER															ETHERNET_HEADER_SIZE

// ARP header fields
#define ARP_HARDWARE_TYPE												ARP_HEADER+0
#define ARP_PROTOCOL_TYPE												ARP_HEADER+2
#define ARP_HARDWARE_ADDRESS_LENGTH							ARP_HEADER+4
#define ARP_PROTOCOL_ADDRESS_LENGTH							ARP_HEADER+5
#define ARP_OPERATION														ARP_HEADER+6
#define ARP_SENDERS_HARDWARE_ADDRESS						ARP_HEADER+8
#define ARP_SENDERS_PROTOCOL_ADDRESS						ARP_HEADER+14
#define ARP_TARGETS_HARDWARE_ADDRESS						ARP_HEADER+18
#define ARP_TARGETS_PROTOCOL_ADDRESS						ARP_HEADER+24

// ARP operations
#define ARP_OPERATION_REQUEST										0x0001
#define ARP_OPERATION_RESPONSE									0x0002


#define ARP_TABLE_SIZE													3


extern void ARP_Receive(unsigned char *buffer);
extern void ARP_Tick();
extern void ARP_Cache(const unsigned char *buffer);
extern unsigned char *ARP_LookUp(unsigned long theIP);
