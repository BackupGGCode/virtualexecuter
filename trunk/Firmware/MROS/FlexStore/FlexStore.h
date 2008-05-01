#include <globals.h>


typedef struct
{
	unsigned long index;
	unsigned long start;
	unsigned long size;
	unsigned long current;
} fsFile;

/*
typedef struct
{
	unsigned long start;
	unsigned long current;
	unsigned long size;
} fsDir;
*/

extern void FileStore_Init(unsigned char (*functionReadByte)(unsigned long address),
										void (*functionReadBytes)(unsigned long address, unsigned char* data, unsigned short length),
										unsigned long (*functionReadLong)(unsigned long address));
extern bool FileStore_OpenFile(char* name, fsFile* file);
extern void FileStore_GetFileName(fsFile* file, char* name, unsigned char maxLength);
extern unsigned short FileStore_ReadBytes(fsFile* file, unsigned char* data, unsigned short length);
extern unsigned long FileStore_ReadLine(fsFile* file, unsigned char* data, unsigned short maxLength);
extern bool FileStore_GetNextFileEntry(fsFile* file, bool getFirst);
