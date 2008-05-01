#include <globals.h>


#pragma once


typedef unsigned char event;

typedef void (*action)(event e);

typedef struct
{
	event theEvent;
	unsigned long interval;
	unsigned long current;
} timer;


extern void PostEvent(event e);
