#include <Globals.h>


typedef enum
{
	VOID,
	SINT8,
	UINT8,
	SINT16,
	UINT16,
	SINT32,
	UINT32,
	FLOAT,
	DOUBLE
} VariableType;


typedef struct
{
	char __flash * text;
	void* previous;
	void* next;
	void* parent;
	void* child;
	VariableType type;
	void* variable;
	bool (*incrementFunction)();
	bool (*decrementFunction)();
	/*
	onSelect
	onDeselect
	onEnter
	onExit
	onIncrement
	onDecrement
	*/
} MenuItem;


extern void Menu_Init(MenuItem* theMenu, void(*menuWriter)(MenuItem* menuItem));
extern void Menu_Next();
extern void Menu_Previous();
extern void Menu_Enter();
extern void Menu_Exit();
extern void Menu_Increment();
extern void Menu_Decrement();
