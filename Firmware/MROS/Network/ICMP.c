#include "Network.h"


void ICMP_Receive(unsigned char *buffer, unsigned short packetLength)
{
	if(	(buffer[ICMP_TYPE] == ICMP_TYPE_REQUEST) &&																				// Echo request (ping)
			(buffer[ICMP_CODE] == ICMP_CODE_ZERO)
		)
	{
		buffer[ICMP_TYPE] = ICMP_TYPE_REPLY;
		PutShort(buffer + ICMP_CHECKSUM, NULL);
		PutShort(buffer + ICMP_CHECKSUM, OnesComplementChecksum(buffer + ICMP_HEADER, packetLength));
		IP_Send(GetLong(buffer + IP_SOURCE), IP_PROTOCOL_ICMP, buffer, packetLength);
	}
}
