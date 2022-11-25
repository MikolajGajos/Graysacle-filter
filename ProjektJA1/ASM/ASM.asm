
.code
GrayScaleASMFunc proc
												
mov		r11, rdx								 ;start index to r11
mov		r10, r8									 ;stop index to r10												   
sub		r10, r11								 ;calculate the counter
mov		rdi, r10								 ;counter to rdi										   
add		rcx, r11								 ;add start offset to rcx

;prepare xmm3 for dividing
mov			eax, 3								 
cvtsi2ss	xmm3, eax							 
punpckldq	xmm3, xmm3							 
punpcklqdq	xmm3, xmm3							 

;xor		ah, ah			
;xor		bh, bh
;xor		eax, eax
												 
grayScaleLoop:									 
	cmp		edi, 0								 ;end condition check
	je		endLoop								 
							
	pxor		xmm0, xmm0

	;first pixel
	pinsrb		xmm0, byte ptr[rcx	  ], 0	
	pinsrb		xmm1, byte ptr[rcx + 1], 0	
	pinsrb		xmm2, byte ptr[rcx + 2], 0	

	;second pixel
	pinsrb		xmm0, byte ptr[rcx + 3], 4	
	pinsrb		xmm1, byte ptr[rcx + 4], 4	
	pinsrb		xmm2, byte ptr[rcx + 5], 4
	
	;third pixel
	pinsrb		xmm0, byte ptr[rcx + 6], 8	
	pinsrb		xmm1, byte ptr[rcx + 7], 8	
	pinsrb		xmm2, byte ptr[rcx + 8], 8

	;fourth pixel
	pinsrb		xmm0, byte ptr[rcx +  9], 12	
	pinsrb		xmm1, byte ptr[rcx + 10], 12	
	pinsrb		xmm2, byte ptr[rcx + 11], 12

	;add values
	paddd		xmm0, xmm1
	paddd		xmm0, xmm2

	;convert integers to floats
	cvtdq2ps	xmm0, xmm0

	;divide
	divps		xmm0, xmm3

	;convert floats to integers
	cvtps2dq	xmm0, xmm0

	;first pixel
	pextrb		eax, xmm0, 0
	mov			byte ptr[rcx	], al					 
	mov			byte ptr[rcx + 1], al				 
	mov			byte ptr[rcx + 2], al

	;second pixel
	pextrb		eax, xmm0, 4
	mov			byte ptr[rcx + 3], al					 
	mov			byte ptr[rcx + 4], al				 
	mov			byte ptr[rcx + 5], al

	;third pixel
	pextrb		eax, xmm0, 8
	mov			byte ptr[rcx + 6], al					 
	mov			byte ptr[rcx + 7], al				 
	mov			byte ptr[rcx + 8], al

	;fourth pixel
	pextrb		eax, xmm0, 12
	mov			byte ptr[rcx + 9], al					 
	mov			byte ptr[rcx + 10], al				 
	mov			byte ptr[rcx + 11], al
	
	add		rcx, 12								 ;table
	sub		rdi, 12								 ;loop counter
	jmp		grayScaleLoop						   
endLoop:										   
    ret											   

GrayScaleASMFunc endp
end



	xor		dx, dx								 
	mov		al, byte ptr[rcx]					 
	mov		bl, byte ptr[rcx + 1]				 
	add		ax, bx								 
	mov		bl, byte ptr[rcx + 2]				 
	add		ax, bx								 
	mov		bx, 3								 
	div		bx									 
	mov		byte ptr[rcx	], al					 
	mov		byte ptr[rcx + 1], al				 
	mov		byte ptr[rcx + 2], al


	mov			al, byte ptr[rcx	]
	cvtsi2ss	xmm0, eax
	mov			al, byte ptr[rcx + 1]
	cvtsi2ss	xmm1, eax
	mov			al, byte ptr[rcx + 2]
	cvtsi2ss	xmm2, eax
	addps		xmm0, xmm1
	addps		xmm0, xmm2
	divps		xmm0, xmm3
	cvtss2si	eax, xmm0
	mov			byte ptr[rcx	], al					 
	mov			byte ptr[rcx + 1], al				 
	mov			byte ptr[rcx + 2], al








