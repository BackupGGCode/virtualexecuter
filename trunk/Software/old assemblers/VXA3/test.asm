; 
; 
; 
; 
; 
; 
; [&/!]name: [<type>] [<count>] [import/export]

0x0000 stack:

.stack
stack: single[100]

0x0000 i:
0x0001 a:
0x000b aa:
0x0015 sa:

.data
i: single
a: single[10]
aa: single[10]
sa: double
ga: quad
ea: single[2]

0x0000 Start:
0x0000 0x06
0x0001 0x000f
0x0003 subd
0x0004 adds
0x0005 jmp Start_DoTheLoop
0x000a Start_DoTheLoop:
0x000a pushls counter
0x000d

.code
!Start:
&v: single
&a: single
&m: quad
index: single
count: quad
counter: single
bla: single[10]

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
