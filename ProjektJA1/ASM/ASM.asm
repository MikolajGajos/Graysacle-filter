
.data
multiplier			dword 0.333
byteArray			byte 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0  
destinationArray	byte 0, 0, 0, 4, 4, 4, 8, 8, 8, 12, 12, 12, 1, 1, 1, 1 

shuffleArray0		byte 0, 15, 15, 15, 3, 15, 15, 15, 6, 15, 15, 15,  9, 15, 15, 15   
shuffleArray1		byte 1, 15, 15, 15, 4, 15, 15, 15, 7, 15, 15, 15, 10, 15, 15, 15   
shuffleArray2		byte 2, 15, 15, 15, 5, 15, 15, 15, 8, 15, 15, 15, 11, 15, 15, 15   

.code
GrayScaleASMFunc proc
												
;establish loop counter
mov			r11, rdx							 ;start index to r11
mov			r10, r8								 ;stop index to r10												   
sub			r10, r11							 ;calculate the counter
mov			rdi, r10							 ;move counter to rdi										   
add			rcx, r11							 ;add start offset to rcx

movdqu		xmm5, oword ptr[destinationArray]
movdqu		xmm6, oword ptr[shuffleArray0]
movdqu		xmm7, oword ptr[shuffleArray1]
movdqu		xmm8, oword ptr[shuffleArray2]
mov			r13, 0			

;prepare xmm3 for dividing
mov			eax, dword ptr[multiplier]			 ;move 3 to eax
movd		xmm3, eax							 ;move 3 from eax to xmm3 with convertion to float
pshufd		xmm3, xmm3, 0
												 
grayScaleLoop:			
	;end condition check
	cmp			rdi, 12								;compare counter with 0
	je			endLoop								;conditional jump												

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
	movd		dword ptr[rcx], xmm0
	pshufd		xmm0, xmm0, 00111001b
	movd		dword ptr[rcx + 4], xmm0 
	pshufd		xmm0, xmm0, 00111001b
	movd		dword ptr[rcx + 8], xmm0 

	add		rcx, 12									;add to table
	sub		rdi, 12									;subtract form loop counter
	jmp		grayScaleLoop						   

endLoop:										   
    ret											   

GrayScaleASMFunc endp
end