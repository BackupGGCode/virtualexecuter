#include "VX/VX_SoftPeripherals.h"


void SP_IO_Init()
{
  DDRF = 0x00;
  PORTF = 0xff;
}

unsigned char SP_IO_ReadDirection()
{
  return DDRF;
}

void SP_IO_WriteDirection(unsigned char value)
{
  DDRF = value;
}

unsigned char SP_IO_ReadPort()
{
  return PORTF;
}

void SP_IO_WritePort(unsigned char value)
{
  PORTF = value;
}

unsigned char SP_IO_ReadPin()
{
  return PINF;
}

void SP_IO_WritePin(unsigned char value)
{
  PINF = value;
}
