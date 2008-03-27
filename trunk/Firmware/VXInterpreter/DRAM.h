#define DRAM_SIZE																1048576


typedef unsigned long dram;


extern void DRAM_Init();
extern void DRAM_Refresh();

extern dram DRAM_Allocate(unsigned long size);
extern void DRAM_Deallocate(dram chunk);

extern unsigned char DRAM_ReadByte(unsigned long address);
extern void DRAM_WriteByte(unsigned long address, unsigned char value);
extern unsigned long DRAM_ReadLong(unsigned long address);
extern void DRAM_WriteLong(unsigned long address, unsigned long value);
extern void DRAM_ReadBytes(unsigned char* data, unsigned long address, unsigned long length);
extern void DRAM_WriteBytes(unsigned char* data, unsigned long address, unsigned long length);
extern unsigned long DRAM_GetFreeHeapSpace();

extern void DRAM_PrintBlockList();
