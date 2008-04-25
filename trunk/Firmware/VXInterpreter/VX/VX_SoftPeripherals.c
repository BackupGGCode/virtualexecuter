#include "Globals.h"
#include "VX_SoftPeripherals.h"


void VX_SoftPeripherals_Init()
{
unsigned short i;

  for(i=0; i<256; i++)
  {
    if(peripherals[i].init != null)
    {
      peripherals[i].init();
    }
  }
}

unsigned char VX_SoftPeripherals_Read(unsigned char number)
{
  if(peripherals[number].read != null)
  {
    return peripherals[number].read();
  }
  else
  {
    return 0;
  }
}

void VX_SoftPeripherals_Write(unsigned char number, unsigned char value)
{
  if(peripherals[number].write != null)
  {
    peripherals[number].write(value);
  }
}
