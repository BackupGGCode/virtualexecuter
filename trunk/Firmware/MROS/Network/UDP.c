#include "Network.h"


typedef struct
{
	unsigned short port;
	void (*registeredFunctions)(unsigned char *buffer);
} UDP_RegisteredPorts;
static UDP_RegisteredPorts registeredPorts[UDP_MAX_REGISTERED_PORTS]={NULL};


/*
	Sends 'Buffer' to the specified destination IP and port.
*/
void UDP_Send(unsigned long destinationIP, unsigned short destinationPort, unsigned short sourcePort, unsigned char *buffer, unsigned short length)
{
	PutShort(buffer + UDP_SOURCE, sourcePort);
	PutShort(buffer + UDP_DESTINATION, destinationPort);
	PutShort(buffer + UDP_LENGTH,UDP_HEADER_SIZE + length);
	PutShort(buffer + UDP_CHECKSUM, NULL);
	IP_Send(destinationIP, IP_PROTOCOL_UDP, buffer, UDP_HEADER_SIZE + length);
}


/*
	Manages port demultiplexing.
*/
void UDP_Receive(unsigned char *buffer)
{
unsigned char temp;

	for(temp = 0 ; temp < UDP_MAX_REGISTERED_PORTS ; temp++)
	{
		if(registeredPorts[temp].port == GetShort(buffer + UDP_DESTINATION))
		{
			(*registeredPorts[temp].registeredFunctions)(buffer);
			return;
		}
	}
}


/*
	Registers 'TheFunction' to 'Port'. When a UDP datagram is received 'TheFunction' will be called
	and will be given the entire buffer including Ethernet, IP and UDP headers.
	A port can only be registered once.
*/
unsigned char UDP_RegisterPort(unsigned short port, void (*theFunction)(unsigned char *buffer))
{
unsigned char temp;

	for(temp = 0 ; temp < UDP_MAX_REGISTERED_PORTS ; temp++)
	{
		if(registeredPorts[temp].port == port)
		{
			return UDP_ERROR_PORT_ALREADY_REGISTERED;
		}
	}
	
	for(temp = 0 ; temp < UDP_MAX_REGISTERED_PORTS ; temp++)
	{
		if(registeredPorts[temp].port == NULL)
		{
			registeredPorts[temp].port = port;
			registeredPorts[temp].registeredFunctions=theFunction;
			return UDP_OK;
		}
	}
	
	return UDP_ERROR_NO_FREE_SLOTS;
}


/*
	Releases the priorly registered port.
*/
unsigned char UDP_UnregisterPort(unsigned short port)
{
unsigned char temp;

	for(temp = 0 ; temp < UDP_MAX_REGISTERED_PORTS ; temp++)
	{
		if(registeredPorts[temp].port == port)
		{
			registeredPorts[temp].port = NULL;
			registeredPorts[temp].registeredFunctions = NULL;
			return UDP_OK;
		}
	}
	
	return UDP_ERROR_PORT_NOT_REGISTERED;
}
