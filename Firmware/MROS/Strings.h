extern bool Strings_Compare(char* string1, char* string2);
extern bool Strings_Compare_P(string* string1, char* string2);

extern void Strings_Copy(char* source, char* destination);
extern void Strings_Copy_P(string* source, char* destination);

extern char* Strings_GetNextWord(char* string1);

extern unsigned char Strings_Length(char* string1);
extern unsigned char Strings_Length_P(string* string1);

extern unsigned long Strings_ReadValueUnsigned(char* string1);
