#ifndef __IAR_SYSTEMS_ASM__												// Not valid when assembling - only for compiler


/***************************************************************************************************************************
	Public members
***************************************************************************************************************************/

extern unsigned short GetShort(const unsigned char *buffer);
extern unsigned long GetLong(const unsigned char *buffer);
extern void PutShort(unsigned char *buffer, unsigned short value);
extern void PutLong(unsigned char *buffer, unsigned long value);
extern void MemCpy(const unsigned char *src,unsigned char *des,unsigned short length);
extern void MemCpyr(const unsigned char *src,unsigned char *des,unsigned short length);
extern void Swap(unsigned char *src,unsigned char *des,unsigned short length);
extern void Clear(unsigned char *buf,unsigned short length);
extern unsigned short OnesComplementChecksum(const unsigned char *buf, unsigned short Length);

/***************************************************************************************************************************
	End of public members
***************************************************************************************************************************/

#endif	// __IAR_SYSTEMS_ASM__
