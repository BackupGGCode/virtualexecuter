#include <FileStore/FileStore.h>

#define FLAG_TYPE																0x03
#define FLAG_FILE																(1<<0)
#define FLAG_DIRECTORYTERMINATOR								(1<<1)

#define ENTRY_SIZE															10
#define OFFSET_FLAGS														0
#define OFFSET_START														1
#define OFFSET_SIZE															5
#define OFFSET_NAMELENGTH												9
#define OFFSET_NAME															10

/*

Each file entry on media complies to the following format:

unsigned char flags;
unsigned long start;
unsigned long size;
unsigned char fileNameLength;
unsigned char fileName[fileNameLength];

The flags:

0 - File
1 - Directory terminator
2 - Directory (not implemented)
3 - Reserved
4 - Reserved
5 - Reserved
6 - Reserved
7 - Reserved

*/

unsigned char (*readByte)(unsigned long address);
void (*readBytes)(unsigned long address, unsigned char* data, unsigned short length);
unsigned long (*readLong)(unsigned long address);


unsigned long FindNextFileEntry(unsigned long current);
bool MatchFileName(char* name, unsigned long address);

void FileStore_Init(unsigned char (*functionReadByte)(unsigned long address),
										void (*functionReadBytes)(unsigned long address, unsigned char* data, unsigned short length),
										unsigned long (*functionReadLong)(unsigned long address))
{
	readByte = functionReadByte;
	readBytes = functionReadBytes;
	readLong = functionReadLong;
}


bool FileStore_OpenFile(char* name, fsFile* file)
{
	unsigned long index=0, newIndex;
	
	while(MatchFileName(name, index)==false)
	{
		newIndex=FindNextFileEntry(index);
		if(index==newIndex)
		{
			return false;
		}
		index=newIndex;
	}
	
	file->index=index;
	file->start=readLong(index+OFFSET_START);
	file->size=readLong(index+OFFSET_SIZE);
	file->current=0;
	
	return true;
}

void FileStore_GetFileName(fsFile* file, char* name, unsigned char maxLength)
{
	unsigned char length;

	length = readByte(file->index + OFFSET_NAMELENGTH);
	if(length >= maxLength)
	{
		length = maxLength - 1;
	}
	
	readBytes(file->index + OFFSET_NAME, (unsigned char*)name, (unsigned short)length);
	name[length] = 0;
}

unsigned long FileStore_ReadBytes(fsFile* file, unsigned char* data, unsigned short length)
{
	if(length > (file->size - file->current))
	{
		length = file->size - file->current;
	}
	
	readBytes(file->start + file->current, data, length);
	file->current += length;
	
	return length;
}

/*
unsigned long FileStore_ReadLine(fsFile* file, unsigned char* data, unsigned short maxLength)
{
unsigned char t;
unsigned long count=0;

while(count < maxLength)
{
if(FileStore_ReadBytes(file, &t, 1)==1)
{
*data++ == t;
		}

	}

return maxLength;
}
*/

bool FileStore_GetNextFileEntry(fsFile* file, bool getFirst)
{
	unsigned long index;
	
	if(getFirst)
	{
		index = 0;
	}
	else
	{
		index = FindNextFileEntry(file->index);
	}
	
	if((readByte(index + OFFSET_FLAGS) & FLAG_TYPE) != FLAG_FILE)
	{
		return false;
	}
	
	file->index = index;
	file->start = readLong(index + OFFSET_START);
	file->size = readLong(index + OFFSET_SIZE);
	file->current = 0;
	
	return true;
}



////////////////////////////////////////////////////////////////////////////////////////


/*
Given a pointer to a valid file entry a pointer to the next file entry is returned.
If the current entry is not valid the same entry is returned.
No check is made to ensure that the entry found is valid!
*/
unsigned long FindNextFileEntry(unsigned long current)
{
	unsigned long next = current;
	
	if((readByte(current + OFFSET_FLAGS) & FLAG_TYPE) == FLAG_FILE)
	{
		next = next + ENTRY_SIZE + readByte(current + OFFSET_NAMELENGTH);
	}
	
	return next;
}


/*
Tests if the file specified by name is located on the index given.
Returns true if the file is found otherwise false;
*/
bool MatchFileName(char* name, unsigned long index)
{
unsigned char length;

	if((readByte(index + OFFSET_FLAGS) & FLAG_TYPE) != FLAG_FILE)
	{
		return false;
	}

	length = readByte(index + OFFSET_NAMELENGTH);
	
	index += OFFSET_NAME;
	
	while(*name)
	{
		if(readByte(index++) != *name++)
		{
			return false;
		}
		length--;
	}
	
	if(length > 0)
	{
		return false;
	}
	
	return true;
}
