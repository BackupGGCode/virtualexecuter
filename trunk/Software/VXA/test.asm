; This is just a demo

; Assembler process:
; 1. Remove all blank lines
; 2. Remove all ; and the rest of that line
; 3. Find all attributes in the assembler file
; 4. Find all labels and add them to the section lists together with their addresses
;    (also add type and count for data and stack section labels)
; 5. Replace label names with the corresponding addresses
; 6. 



; [&/!]name: [<type>] [<count>] [import/export]


.stack
stack: single[100]

.const
MICRO: double 1000 export
KILO: double 1000
TON: import 

.gdata
i: single
a: single[10]
aa: single[10]
sa: double
ga: quad
ea: single[2]

.code
void Start! void
;!Start: void, void
counter: single
Start_Init*
	subd
	adds
	jmp Start_DoTheLoop
	loadls counter 0
Start_DoTheLoop:
	pushls counter
	pushgs i
	subd
	jmpnz Start_DoTheLoop
	loadls counter 100
	pushs 100
	pushs 10
	adds
	popls counter
Bla:
	call Start_Init
