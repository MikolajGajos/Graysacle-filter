
.code
GrayScaleASMFunc proc

movdqu	xmm1, oword ptr[r9]						   ;give array3 to xmm1												  
mov		r11, rdx								   ;beg index to r11
mov		r10, r8									   ;end index to r10
												   
sub		r10, r11								   ;establish counter
mov		rdi, r10								   ;give counter to rdi
shr		edi, 2									   ;divide counter by 4 
												   
mov		rax, 4h									   ;prepare multiplying by 4
mul		r11										   ;multiply beg index by 4 (pixels size in bytes) 
add		rcx, rax								   ;add to pixels offset table

grayScaleLoop:
	cmp			edi, 0h						
	je			endLoop						
	
	movdqu		xmm0, oword ptr[rcx]		
	movdqu		xmm2, oword ptr[rcx + 1]	
	movdqu		xmm3, oword ptr[rcx + 2]	
	addps		xmm0, xmm2
	addps		xmm0, xmm3
	punpckldq	xmm0, xmm0					
	punpcklqdq	xmm0, xmm0		
	divps		xmm0, xmm1
	


	movdqu		oword ptr[rcx], xmm0
	
	add			rcx, 4					
	sub			rdi, 1						
	jmp			grayScaleLoop				
endLoop:
    ret

GrayScaleASMFunc endp
end