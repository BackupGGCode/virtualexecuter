#include "Network.h"


void ETHERNET_Send(const unsigned char *destination, unsigned short type, unsigned char *buffer, unsigned short length)
{
	MemCpy(destination, buffer + ETHERNET_DESTINATION, 6);
	MemCpy(mac, buffer + ETHERNET_SOURCE, 6);
	PutShort(buffer + ETHERNET_TYPE, type);
	NIC_SendBlocking(buffer, ETHERNET_HEADER_SIZE + length);
}


void ETHERNET_Receive()
{
	if(NIC_Get(globalBuffer, GLOBAL_BUFFER_SIZE) >= ETHERNET_HEADER_SIZE)
	{
		switch(GetShort(globalBuffer + ETHERNET_TYPE))																				// Demultiplex
		{
			case ETHERNET_TYPE_IP:			ARP_Cache(globalBuffer);
																	IP_Receive(globalBuffer);
																	break;
			case ETHERNET_TYPE_ARP:			ARP_Receive(globalBuffer);
																	break;
			default:										
																	break;
		}
	}
}
