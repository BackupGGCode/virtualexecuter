; This is just a demo

; Compile process:
; 1. Remove all blank lines
; 2. Remove all ; and the rest of that line
; 3. Find all attributes in the assembler file
; 4. Find all labels and add them to the section lists together with their addresses
;    (also add type and count for data and stack section labels)
; 5. Replace label names with the corresponding addresses
; 6. 

.stack
stack: single 100

.const
KILO: double 1000

.data
i: single
a: single 10
aa: single 10
sa: double 
ga: QUaD 
ea: single 2

.code
start:
	loads 100 aa
	pushcs 100
	pushcs 10
	adds
	callc start