#pragma once


#define DATA_IN																	PIND
#define DATA_OUT																PORTD
#define DATA_DDR																DDRD
#define ADR_PORT																PORTA
#define CTRL_PORT																PORTC
#define RAS_NUMBER															5
#define CAS_NUMBER															7
#define WR_NUMBER																6

#define CAS_HIGH																CTRL_PORT |= (1 << CAS_NUMBER)
#define CAS_LOW																	CTRL_PORT &= ~(1 << CAS_NUMBER)
#define RAS_HIGH																CTRL_PORT |= (1 << RAS_NUMBER)
#define RAS_LOW																	CTRL_PORT &= ~(1 << RAS_NUMBER)


#define DRAM_SIZE																1048576
#define NUMBER_OF_ROWS													1024
#define DRAM_PRESCALER													14
#define CAS_RAS_PER_CYCLE												8
#define REFRESH_CYCLES													(NUMBER_OF_ROWS / CAS_RAS_PER_CYCLE)


#ifndef __IAR_SYSTEMS_ASM__
typedef unsigned long dram;


extern void DRAM_Init();
extern void DRAM_Refresh();

extern dram DRAM_Allocate(unsigned long size);
extern void DRAM_Deallocate(dram chunk);

extern unsigned char DRAM_ReadByte(dram address);
extern void DRAM_WriteByte(dram address, unsigned char value);
extern unsigned long DRAM_ReadLong(dram address);
extern void DRAM_WriteLong(dram address, unsigned long value);
extern void DRAM_ReadBytes(dram address, unsigned char* data, unsigned short length);
extern void DRAM_WriteBytes(dram address, unsigned char* data, unsigned short length);
extern unsigned long DRAM_GetFreeHeapSpace();

extern void DRAM_PrintBlockList();
#endif
