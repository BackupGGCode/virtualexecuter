#pragma once

#define DRAM_SIZE																1048576


typedef unsigned long dram;


extern void DRAM_Init();
extern void DRAM_Refresh();

extern dram DRAM_Allocate(unsigned long size);
extern void DRAM_Deallocate(dram chunk);

extern unsigned char DRAM_ReadByte(dram address);
extern void DRAM_WriteByte(dram address, unsigned char value);
extern unsigned long DRAM_ReadLong(dram address);
extern void DRAM_WriteLong(dram address, unsigned long value);
extern void DRAM_ReadBytes(dram address, unsigned char* data, unsigned long length);
extern void DRAM_WriteBytes(dram address, unsigned char* data, unsigned long length);
extern unsigned long DRAM_GetFreeHeapSpace();

extern void DRAM_PrintBlockList();
