void Memory_Copy_Tiny(void* source, void* destination, unsigned char length)
{
unsigned char* s = (unsigned char*)source;
unsigned char* d = (unsigned char*)destination;

	while(length--)
	{
		*d = *s;
		d++;
		s++;
	}
}

void Memory_Copy_Small(void* source, void* destination, unsigned short length)
{
unsigned char* s = (unsigned char*)source;
unsigned char* d = (unsigned char*)destination;

	while(length--)
	{
		*d = *s;
		d++;
		s++;
	}
}
