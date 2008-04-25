#include <Globals.h>


typedef struct
{
  void (*init)();
  unsigned char (*read)();
  void (*write)(unsigned char value);
} softPeripheral;


extern __flash softPeripheral peripherals[];


extern unsigned char VX_SoftPeripherals_Read(unsigned char number);
extern void VX_SoftPeripherals_Write(unsigned char number, unsigned char value);
