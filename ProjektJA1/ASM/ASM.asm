
.code
GrayScaleASMFunc proc
												
mov		r11, rdx								 ;start index to r11
mov		r10, r8									 ;stop index to r10												   
sub		r10, r11								 ;calculate the counter
mov		rdi, r10								 ;counter to rdi
shr		edi, 2									 ;divide counter by 4 											   
add		rcx, r11								 ;add start offset to rcx
												 
grayScaleLoop:									 
	cmp		edi, 0h								 ;end condition check
	je		endLoop								 
												 
	mov		dx, 0								 
	mov		al, byte ptr[rcx]					 
	mov		bl, byte ptr[rcx + 1]				 
	add		ax, bx								 
	mov		bl, byte ptr[rcx + 2]				 
	add		ax, bx								 
	mov		bx, 3								 
	div		bx									 
	mov		byte ptr[rcx], al					 
	mov		byte ptr[rcx+ 1], al				 
	mov		byte ptr[rcx + 2], al				 
												 
	add		rcx, 4								 ;increment table
	dec		rdi									 ;decrement loop counter
	jmp		grayScaleLoop						   
endLoop:										   
    ret											   

GrayScaleASMFunc endp
end