#include <ioavr.h>
#include <inavr.h>

#pragma once

#if defined(null)
#undef null
#endif
#define null																		(0)
#define NULL																		(0)
#define false																		(0)
#define true																		(1)

typedef unsigned char														bool;

#warning Remember to put '--string_literals_in_flash' in 'Extra Options' in the 'C/C++ compiler' options.
#define string																	const char __flash


typedef void(*EventHandler)(void);

typedef unsigned char														uint8;
typedef signed char															sint8;
typedef unsigned short													uint16;
typedef signed short														sint16;
typedef unsigned long														uint32;
typedef signed long															sint32;
