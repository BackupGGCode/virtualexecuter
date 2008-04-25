#include "VX/VX_SoftPeripherals.h"


void SP_SystemLEDs_Init()
{
  PORTB&=~(1<<7);
  PORTG&=~(1<<3);
  PORTG|=(1<<3);
  DDRB|=(1<<7);
  DDRG|=(1<<3);
}

unsigned char SP_SystemLEDs_Read()
{
unsigned char t = 0;

  if(PORTG & (1<<3))
  {
    t |= (1<<0);
  }
  if(PORTB & (1<<7))
  {
    t |= (1<<1);
  }
  
  return t;
}

void SP_SystemLEDs_Write(unsigned char value)
{
  if(value & (1<<0))
  {
    PORTG|=(1<<3);
  }
  else
  {
    PORTG&=~(1<<3);
  }

  if(value & (1<<1))
  {
    PORTB|=(1<<7);
  }
  else
  {
    PORTB&=~(1<<7);
  }
}
