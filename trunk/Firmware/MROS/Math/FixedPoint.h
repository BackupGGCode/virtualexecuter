typedef struct
{
	signed ahort i;
	unsigned short f;
} fixed;



fixed FixedPoint_Add(fixed a1, fixed a2)
{
fixed result;
signed long i=a1.i + a2.i;
unsigned long f=a1.f + a2.f;

	if(f>9999)
	{
		f-=10000;
		i+=1;
	}
}
