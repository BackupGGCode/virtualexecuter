#include <Globals.h>
#include "Config.h"
#include <Kernel/Kernel.h>
#include <Peripherals/UART.h>
#include <strings.h>
#include <VX/VX.h>
#include <DRAM.h>


void VX_Init()
{
	Kernel_CreateTask(VX_Executer);
}
