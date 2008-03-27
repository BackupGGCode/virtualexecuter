#define DF_CS_LOW																(PORTB&=~(1<<0);)
#define DF_CS_HIGH															(PORTB|=(1<<0);)


extern void DataFlash_Init();
extern unsigned char DataFlash_ReadByte(unsigned long address);
extern void DataFlash_WriteByte(unsigned long address, unsigned char value);
extern void DataFlash_ReadBytes(unsigned long address, unsigned char* data, unsigned short length);
extern unsigned long DataFlash_ReadLong(unsigned long address);
extern void DataFlash_WriteBytes(unsigned long address, unsigned char* data, unsigned short length);
