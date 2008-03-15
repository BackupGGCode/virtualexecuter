extern void DRAM_Init(unsigned long size);
extern void DRAM_Refresh();
extern unsigned long DRAM_Allocate(unsigned long size);
extern void DRAM_Deallocate(unsigned long pointer);

extern unsigned char DRAM_ReadByte(unsigned long address);
extern void DRAM_WriteByte(unsigned long address, unsigned char data);
extern void DRAM_ReadBytes(unsigned char* data, unsigned long address, unsigned long length);
extern void DRAM_WriteBytes(unsigned char* data, unsigned long address, unsigned long length);
