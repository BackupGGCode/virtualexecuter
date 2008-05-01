#include "Network.h"


// The local IP
unsigned long ip;


/*
	Sends an IP datagram to the specified destination.
*/
void IP_Send(unsigned long destination, unsigned char protocol, unsigned char *buffer, unsigned short packetLength)
{
//const unsigned char *TargetMAC=ARP_LookUp(Destination);
//unsigned char BroadcastMAC[6]={0xff,0xff,0xff,0xff,0xff,0xff};
unsigned char pcMac[6]={0x00,0x1a,0x92,0x9d,0x42,0x64};

/*
	if(TargetMAC==NULL)
	{
		// Perform ARP request on 'destination'
	}
*/

	buffer[IP_VERSION] = 0x40 | 5;
	buffer[IP_TYPE] = 0;
	PutShort(buffer + IP_LENGTH, packetLength + IP_HEADER_SIZE);
	PutShort(buffer + IP_ID, 1);
	PutShort(buffer + IP_FRAGMENT, 0);
	buffer[IP_TTL] = 128;
	buffer[IP_PROTOCOL] = protocol;
	PutShort(buffer + IP_CHECKSUM, 0);
	PutLong(buffer + IP_SOURCE, ip);
	PutLong(buffer + IP_DESTINATION, destination);
	PutShort(buffer + IP_CHECKSUM, OnesComplementChecksum(buffer + IP_HEADER, IP_HEADER_SIZE));
	ETHERNET_Send(pcMac, ETHERNET_TYPE_IP, buffer, packetLength + IP_HEADER_SIZE);

	return;

/*	if(TargetMAC)
		ETHERNET_Send(TargetMAC,ETHERNET_TYPE_IP,Buffer,PacketLength+IP_HEADER_SIZE);
	else
		ETHERNET_Send(BroadcastMAC,ETHERNET_TYPE_IP,Buffer,PacketLength+IP_HEADER_SIZE);
*/
}


void IP_Receive(unsigned char *buffer)
{
	if(	(buffer[IP_VERSION] == 0x40 | 5) &&																									// IP version 4 and header length = 5 * 4 bytes
			(GetLong(buffer + IP_DESTINATION) == ip)																					// Destination is me
		)
	{
		switch(buffer[IP_PROTOCOL])																													// Demultiplex packet
		{
			case IP_PROTOCOL_ICMP:	ICMP_Receive(buffer, GetShort(buffer + IP_LENGTH) - IP_HEADER_SIZE);	// Pass packet to ICMP. The length is the length of the ICMP packet (ie not including the IP header)
															break;
			case IP_PROTOCOL_TCP:		
															break;
			case IP_PROTOCOL_UDP:		UDP_Receive(buffer);																			// Pass packet to UDP.
															break;
		}
	}
}
