extern void Kernel_InitHeap();
extern void* Kernel_Allocate(unsigned short size);
extern void Kernel_Deallocate(void* pointer);
extern unsigned short Kernel_GetFreeHeapSpace();
extern void Kernel_PrintHeap();
