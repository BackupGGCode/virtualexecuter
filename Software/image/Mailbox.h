#include "Kernel.h"


extern bool Kernel_PutSmall(unsigned short msg, mailboxSmall* mq);
extern bool Kernel_GetSmall(unsigned short* msg, mailboxSmall* mq);
