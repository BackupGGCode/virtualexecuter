#include "Network.h"
#include <Kernel/Kernel.h>

/*
	ARP packet format

	
			0				1				2				3				4				5				6				7				8

			*-------*-------*-------*-------*-------*-------*-------*-------*
	0		| Hardware Type | Protocol Type | H Len | P Len |   Operation   |
			*-------*-------*-------*-------*-------*-------*-------*-------*
	8		|            Senders Hardware Address           | Senders Proto-|
			*-------*-------*-------*-------*-------*-------*-------*-------*
	16	|  col Address  |           Targets Hardware Address            |
			*-------*-------*-------*-------*-------*-------*-------*-------*
	24	|  	Targets Protocol Address    |
			*-------*-------*-------*-------*

	Hardware Type:							0x0001 means Ethernet.
	Protocol Type:							0x0800 means IP.
	H Len:											Hardware address length. Length of the MAC adress. Always 6.
	P Len:											Protocol address length. Length of the IP address. Always 4.
	Op Code:										ARP operation:	1 - Unknown
																							2 - ARP Request
																							3 - ARP Response
	Senders Hardware Address:		The MAC address on Ethernet.
	Senders Protocol Address:		The IP address in TCP/IP
	Targets Hardware Address:		The MAC address on Ethernet.
	Targets Protocol Address:		The IP address in TCP/IP

	Some of the fields may appear to have strange names or to be superfluous but keep
	in mind that ARP can be used outside the Ethernet domain.
	
	Operation:

		To obtain the MAC associated with a known IP broadcast an ARP request packet

*/


typedef struct
{
	unsigned long ip;
	unsigned char mac[6];
	unsigned short age;
} ARP_ENTRY;

ARP_ENTRY arpTable[ARP_TABLE_SIZE];


void ARP_Receive(unsigned char *buffer)
{
unsigned char temp;

	if(	(GetShort(buffer + ARP_HARDWARE_TYPE) == 0x0001) &&																// Hardware type is Ethernet
			(GetShort(buffer + ARP_PROTOCOL_TYPE) == 0x0800) &&																// Protocol type is IP
			(buffer[ARP_HARDWARE_ADDRESS_LENGTH] == 6) &&																			// MAC takes up 6 bytes
			(buffer[ARP_PROTOCOL_ADDRESS_LENGTH] == 4)																				// IP address takes up 4 bytes
		)
	{

		switch(GetShort(buffer + ARP_OPERATION))																							// Switch on ARP operation
		{
			case ARP_OPERATION_REQUEST:				if(GetLong(buffer + ARP_TARGETS_PROTOCOL_ADDRESS) == ip)
																				{
																					PutShort(buffer + ARP_OPERATION, ARP_OPERATION_RESPONSE);											// Now it's a reply
																					Swap(buffer + ARP_SENDERS_HARDWARE_ADDRESS, buffer + ARP_TARGETS_HARDWARE_ADDRESS, 10);
																					for(temp = 0 ; temp < 6 ; temp++)
																					{
																						buffer[ARP_SENDERS_HARDWARE_ADDRESS + temp] = mac[temp];
																					}
																					Clear(buffer + ETHERNET_HEADER_SIZE + IP_OPTIONS, 18);
																					ETHERNET_Send(buffer + ETHERNET_SOURCE, ETHERNET_TYPE_ARP, buffer, 46);
																				}
																				break;

			case ARP_OPERATION_RESPONSE:			ARP_Cache(buffer);
																				break;
		}
	}
}


unsigned char* ARP_Request(unsigned long ip)
{
	return 0;
}


/*
	Manages ageing of ARP entries. An entry is newer deleted but as it ages the chance that it will be overwriten increases.
*/
void ARP_Tick()
{
unsigned char temp;

	for(temp = 0 ; temp < ARP_TABLE_SIZE ; temp++)
	{
		if(arpTable[temp].age)
		{
			arpTable[temp].age--;
		}
	}
	
	Kernel_Sleep(SYSTEM_TICKS_PER_SECOND);																								// Sleep for a second
}


/*
	Enters 'IP' into the table. If the IP already exists it's age is reset.
*/
void ARP_Cache(const unsigned char *buffer)
{
unsigned char temp;
unsigned long IP = GetLong(buffer + IP_SOURCE);
unsigned char Oldest = 0;

	for(temp = 0 ; temp < ARP_TABLE_SIZE ; temp++)
	{
		if(arpTable[temp].ip == ip)																															// If IP already exists
		{
			arpTable[temp].age = ~0;																																// Reset it's age
			MemCpy(buffer + ETHERNET_SOURCE, arpTable[temp].mac, 6);																	// Store MAC in case it has changed (is this really a good idea?)
			return;																																							// ...and break out
		}
		
		if(arpTable[temp].age < arpTable[Oldest].age)
		{
			Oldest=temp;
		}
	}

	arpTable[Oldest].ip = ip;																														// Owerwrite the oldest entry
	MemCpy(buffer + ETHERNET_SOURCE, arpTable[Oldest].mac, 6);
	arpTable[Oldest].age = ~0;
}


unsigned char *ARP_LookUp(unsigned long theIP)
{
unsigned char temp;

	for(temp = 0 ; temp < ARP_TABLE_SIZE ; temp++)
	{
		if(arpTable[temp].ip == theIP)
		{
			return arpTable[temp].mac;
		}
	}
	
	return NULL;
}
