#include "Network.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>


unsigned char Network_Init(unsigned char ip1, unsigned char ip2, unsigned char ip3, unsigned char ip4)
{
signed char result;

	ip=Num2IP(ip1, ip2, ip3, ip4);

	result=NIC_Init(0);

	UART_WriteByte(result+'0');

	if(result)
	{
		return 1;
	}

	Kernel_CreateTask(ARP_Tick);
	
	Kernel_CreateTask(ETHERNET_Receive);
	
	return 0;
}


unsigned long Num2IP(unsigned char ip1, unsigned char ip2, unsigned char ip3, unsigned char ip4)
{
unsigned long temp;

	temp = (ip1 << 8) | ip2;
	temp <<= 16;
	temp |= (ip3 << 8) | ip4;
	
	return temp;
}

/*
unsigned long Str2IP(char* ip)
{
	return NULL;
}


unsigned long Str2IP_P(__flash char* ip)
{
	return NULL;
}
*/
