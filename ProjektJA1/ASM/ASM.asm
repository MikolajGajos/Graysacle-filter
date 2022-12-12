
.data
multiplier			dword 0.333
destinationArray	byte 0, 0, 0, 4, 4, 4, 8, 8, 8, 12, 12, 12, 1, 1, 1, 1 

shuffleArray0		byte 0, 15, 15, 15, 3, 15, 15, 15, 6, 15, 15, 15,  9, 15, 15, 15   
shuffleArray1		byte 1, 15, 15, 15, 4, 15, 15, 15, 7, 15, 15, 15, 10, 15, 15, 15   
shuffleArray2		byte 2, 15, 15, 15, 5, 15, 15, 15, 8, 15, 15, 15, 11, 15, 15, 15   

.code
GrayScaleASMFunc proc
												
;establish loop counter
mov			r11, rdx							;start index to r11
mov			r10, r8								;stop index to r10												   
sub			r10, r11							;calculate the counter
mov			rdi, r10							;move counter to rdi										   
add			rcx, r11							;add start offset to rcx

;prepare registers
movdqu		xmm5, oword ptr[destinationArray]	;
movdqu		xmm6, oword ptr[shuffleArray0]		;
movdqu		xmm7, oword ptr[shuffleArray1]		;
movdqu		xmm8, oword ptr[shuffleArray2]		;
mov			r13, 0								;

;prepare xmm3 for dividing
mov			eax, dword ptr[multiplier]			;move 3 to eax
movd		xmm3, eax							;move 3 from eax to xmm3 with convertion to float
pshufd		xmm3, xmm3, 0
												 
grayScaleLoop:			
	;end condition check
	cmp			rdi, 12								;compare counter with 0
	je			lastPixels							;conditional jump			

	pxor xmm0, xmm0

	movdqu		xmm0, oword ptr[rcx]
	pinsrb		xmm0, r13, 15
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
	movd		dword ptr[rcx + 4], xmm0 
	pshufd		xmm0, xmm0, 00111001b
	movd		dword ptr[rcx + 8], xmm0 

	add		rcx, 12									;add to table
	sub		rdi, 12									;subtract form loop counter
	jmp		grayScaleLoop						   

lastPixels:										   

cmp			rdi, 9							;compare counter with 9
je			last9							;conditional jump

cmp			rdi, 6							;compare counter with 6
je			last6							;conditional jump

cmp			rdi, 3							;compare counter with 3
je			last3							;conditional jump
	
last12:

	pxor xmm0, xmm0

	;first pixel
	pinsrb		xmm0, byte ptr[rcx     ], 0
	pinsrb		xmm1, byte ptr[rcx +  1], 0
	pinsrb		xmm2, byte ptr[rcx +  2], 0

	;second pixel
	pinsrb		xmm0, byte ptr[rcx +  3], 4
	pinsrb		xmm1, byte ptr[rcx +  4], 4
	pinsrb		xmm2, byte ptr[rcx +  5], 4

	;third pixel
	pinsrb		xmm0, byte ptr[rcx +  6], 8
	pinsrb		xmm1, byte ptr[rcx +  7], 8
	pinsrb		xmm2, byte ptr[rcx +  8], 8

	;fourth pixel
	pinsrb		xmm0, byte ptr[rcx +  9], 12
	pinsrb		xmm1, byte ptr[rcx + 10], 12
	pinsrb		xmm2, byte ptr[rcx + 11], 12
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																			
	mulps		xmm0, xmm3							;multiply by 0.333				
	
	pshufb		xmm0, xmm5
	movd		dword ptr[rcx	 ], xmm0
	pshufd		xmm0, xmm0, 00111001b
	movd		dword ptr[rcx + 4], xmm0 
	pshufd		xmm0, xmm0, 00111001b
	movd		dword ptr[rcx + 8], xmm0 

jmp	endLoop

last9:

jmp	endLoop

last6:

jmp	endLoop

last3:

jmp	endLoop

endLoop:
ret


GrayScaleASMFunc endp
end