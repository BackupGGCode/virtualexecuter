extern unsigned char InternalEEPROM_ReadByte(unsigned long address);
extern void InternalEEPROM_WriteByte(unsigned long address, unsigned char value);
extern void InternalEEPROM_ReadBytes(unsigned long address, unsigned char* data, unsigned short length);
extern unsigned long InternalEEPROM_ReadLong(unsigned long address);
extern void InternalEEPROM_WriteBytes(unsigned long address, unsigned char* data, unsigned short length);
