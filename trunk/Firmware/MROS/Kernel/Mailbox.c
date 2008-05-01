bool Kernel_SendMail(unsigned short msg, mailboxSmall* mb)
{
	if(mb->full)
	{
		return false;
	}
	
	mb->pData[mb->head] = msg;
	
	if(++mb->head >= mb->size)
	{
		mb->head = 0;
	}
	
	return true;
}


bool Kernel_GetMail(unsigned short* msg, mailboxSmall* mb)
{
	if(mb->full == false && mb->head == mb->tail)
	{
		return false;
	}

	*msg = mb->pData[mb->tail];

	if(++mb->tail >= mb->size)
	{
		mb->tail = 0;
	}

	return true;
}
