#include "VX/VX_SoftPeripherals.h"


void SP_IO_Init()
{
  DDRF = 0x00;
  PORTF = 0x00;
}

unsigned char SP_IO_ReadDirection()
{
  return DDRF;
//  return 0;
}

void SP_IO_WriteDirection(unsigned char value)
{
  DDRF = value;
}

unsigned char SP_IO_ReadPort()
{
  return PORTF;
//  return 0;
}

void SP_IO_WritePort(unsigned char value)
{
  PORTF = value;
}

unsigned char SP_IO_ReadPin()
{
  return PINF;
//  return 0;
}

void SP_IO_WritePin(unsigned char value)
{
  PINF = value;
}
