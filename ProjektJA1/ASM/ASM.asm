
.data
multiplier			dword 0.333
bytesSortingArray	byte  0, 0, 0, 4, 4, 4, 8, 8, 8, 12, 12, 12, 1, 1, 1, 1 
blueComponents		byte  0, 80h, 80h, 80h, 3, 80h, 80h, 80h, 6, 80h, 80h, 80h,  9, 80h, 80h, 80h   
greenComponents		byte  1, 80h, 80h, 80h, 4, 80h, 80h, 80h, 7, 80h, 80h, 80h, 10, 80h, 80h, 80h  
redComponents		byte  2, 80h, 80h, 80h, 5, 80h, 80h, 80h, 8, 80h, 80h, 80h, 11, 80h, 80h, 80h   

.code
GrayScaleASMFunc proc
						
;prepare xmm3 for dividing
mov			eax, dword ptr[multiplier]			;move 3 to eax
movd		xmm3, eax							;move 3 from eax to xmm3 with convertion to float
pshufd		xmm3, xmm3, 0

;calculate width and stride
mov			ebx, dword ptr[rbp + 32]			;width
mov			eax, 3
mul			ebx
mov			r12, rax							;width * 3
mov			rbx, r9								;stride
sub			rbx, rax							;stride - width
mov			rdi, r12							;move counter to rdi

;establish loop counter
mov			r11, rdx							;start index to r11
mov			r10, r8								;stop index to r10												   
sub			r10, r11							;calculate the counter										   
add			rcx, r11							;add start offset to rcx

;prepare registers
movdqu		xmm5, oword ptr[bytesSortingArray]	;
movdqu		xmm6, oword ptr[blueComponents   ]	;
movdqu		xmm7, oword ptr[greenComponents  ]	;
movdqu		xmm8, oword ptr[redComponents    ]	;								
												 
grayScaleLoop:			
	;end condition check
	cmp			rdi, 16								;compare counter with 0
	jl			lastPixels							;conditional jump	

	pxor xmm0, xmm0

	;prepare registers for calculating pixels
	movdqu		xmm0, oword ptr[rcx]
	movdqu		xmm1, xmm0
	movdqu		xmm2, xmm0
	pshufb		xmm0, xmm6
	pshufb		xmm1, xmm7
	pshufb		xmm2, xmm8
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	;extract bytes from register
	pshufb		xmm0, xmm5
	movd		dword ptr[rcx	 ], xmm0
	pshufd		xmm0, xmm0, 00111001b
	movq		qword ptr[rcx + 4], xmm0 

	;counter and pointer
	add			rcx, 12								;add to table
	sub			rdi, 12								;subtract form loop counter
	jmp			grayScaleLoop						   

lastPixels:										   

cmp			rdi, 11							;compare counter with 11
jg			last12							;conditional jump	

cmp			rdi, 9							;compare counter with 9
je			last9							;conditional jump

cmp			rdi, 6							;compare counter with 6
je			last6							;conditional jump

cmp			rdi, 3							;compare counter with 3
je			last3							;conditional jump
	
jmp			endLoop

last12:

	pxor xmm0, xmm0

	movq		xmm0, qword ptr[rcx    ]
	movd		xmm1, dword ptr[rcx + 8]
	movlhps		xmm0, xmm1

	movdqu		xmm1, xmm0
	movdqu		xmm2, xmm0
	pshufb		xmm0, xmm6
	pshufb		xmm1, xmm7
	pshufb		xmm2, xmm8
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	pshufb		xmm0, xmm5
	movd		dword ptr[rcx	 ], xmm0
	pshufd		xmm0, xmm0, 00111001b
	movq		qword ptr[rcx + 4], xmm0 

	add			rcx, 12								;add to table
	sub			rdi, 12								;subtract form loop counter

jmp	lastPixels

last9:

	pxor xmm0, xmm0

	movq		xmm0, qword ptr[rcx    ]
	pinsrb		xmm0, byte  ptr[rcx + 8], 8

	movdqu		xmm1, xmm0
	movdqu		xmm2, xmm0
	pshufb		xmm0, xmm6
	pshufb		xmm1, xmm7
	pshufb		xmm2, xmm8
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	pshufb		xmm0, xmm5
	movq		qword ptr[rcx    ], xmm0 
	pextrb		byte  ptr[rcx + 8], xmm0, 8

jmp	endLoop

last6:

	pxor xmm0, xmm0

	movd		xmm0, dword ptr[rcx    ]
	pinsrb		xmm0, byte  ptr[rcx + 4], 4
	pinsrb		xmm0, byte  ptr[rcx + 5], 5

	movdqu		xmm1, xmm0
	movdqu		xmm2, xmm0
	pshufb		xmm0, xmm6
	pshufb		xmm1, xmm7
	pshufb		xmm2, xmm8
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	pshufb		xmm0, xmm5
	movd		dword ptr[rcx    ], xmm0 
	pextrb		byte  ptr[rcx + 4], xmm0, 4
	pextrb		byte  ptr[rcx + 5], xmm0, 5

jmp	endLoop

last3:

	pxor xmm0, xmm0

	pinsrb		xmm0, byte  ptr[rcx	   ], 0
	pinsrb		xmm0, byte  ptr[rcx + 1], 1
	pinsrb		xmm0, byte  ptr[rcx + 2], 2

	movdqu		xmm1, xmm0
	movdqu		xmm2, xmm0
	pshufb		xmm0, xmm6
	pshufb		xmm1, xmm7
	pshufb		xmm2, xmm8
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	pshufb		xmm0, xmm5
	pextrb		byte  ptr[rcx    ], xmm0, 0
	pextrb		byte  ptr[rcx + 1], xmm0, 1
	pextrb		byte  ptr[rcx + 2], xmm0, 2

endLoop:

add			rcx, rdi
add			rcx, rbx
sub			r10, r9
cmp			r10, 0
je			endProg		
mov			rdi, r12
jmp			grayScaleLoop

endProg:
ret


GrayScaleASMFunc endp
end