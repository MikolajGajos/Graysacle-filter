
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
jmp			grayScaleLoop
												 
grayScaleLoop:			
	;end condition check
	cmp			rdi, 0								;compare counter with 0
	je			endLoop								;conditional jump												

	pxor xmm0, xmm0

	movdqu		xmm0, oword ptr[rcx]
	pinsrb		xmm0, r13, 15
	pshufb		xmm0, xmm6

	movdqu		xmm1, oword ptr[rcx]
	pinsrb		xmm1, r13, 15
	pshufb		xmm1, xmm7

	movdqu		xmm2, oword ptr[rcx]
	pinsrb		xmm2, r13, 15
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

	grayScaleLoop1:			
	;end condition check
	cmp			edi, 0								;compare counter with 0
	je			endLoop1							;conditional jump												

	pxor xmm0, xmm0

	;first pixel					  				
	pinsrb		xmm0, byte ptr[rcx	   ], 0			;move b value of first pixel
	pinsrb		xmm1, byte ptr[rcx +  1], 0			;move g value of first pixel
	pinsrb		xmm2, byte ptr[rcx +  2], 0			;move r value of first pixel
									  				
	;second pixel					  				
	pinsrb		xmm0, byte ptr[rcx +  3], 4			
	pinsrb		xmm1, byte ptr[rcx +  4], 4			
	pinsrb		xmm2, byte ptr[rcx +  5], 4			
									  				
	;third pixel					  				
	pinsrb		xmm0, byte ptr[rcx +  6], 8			
	pinsrb		xmm1, byte ptr[rcx +  7], 8			
	pinsrb		xmm2, byte ptr[rcx +  8], 8	
													
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
	jmp		grayScaleLoop1						   

endLoop1:										   
    ret								

GrayScaleASMFunc endp
end



;
movdqu		oword ptr[r12], xmm0 
mov			al, byte ptr[rcx]
mov			byte ptr[r12], al
mov			al, byte ptr[rcx + 3]
mov			byte ptr[r12 + 4], al
mov			al, byte ptr[rcx + 6]
mov			byte ptr[r12 + 8], al
mov			al, byte ptr[rcx + 9]
mov			byte ptr[r12 + 12], al
movdqu		xmm4, oword ptr[r12]

;
mov			al, byte ptr[rcx]
mov			byteArray[0], al
mov			al, byte ptr[rcx + 3]
mov			byteArray[3], al
mov			al, byte ptr[rcx + 6]
mov			byteArray[6], al
mov			al, byte ptr[rcx + 9]
mov			byteArray[9], al
movdqu		xmm0, oword ptr[byteArray]



	;first register
	mov			al, byte ptr[rcx]
	mov			byte ptr[r12], al
	mov			al, byte ptr[rcx + 3]
	mov			byte ptr[r12 + 4], al
	mov			al, byte ptr[rcx + 6]
	mov			byte ptr[r12 + 8], al
	mov			al, byte ptr[rcx + 9]
	mov			byte ptr[r12 + 12], al
	movdqu		xmm0, oword ptr[r12]

	;second register
	mov			al, byte ptr[rcx +  1]
	mov			byte ptr[r12], al
	mov			al, byte ptr[rcx +  4]
	mov			byte ptr[r12 + 4], al
	mov			al, byte ptr[rcx +  7]
	mov			byte ptr[r12 + 8], al
	mov			al, byte ptr[rcx + 10]
	mov			byte ptr[r12 + 12], al
	movdqu		xmm0, oword ptr[r12]

	;third register
	mov			al, byte ptr[rcx +  2]
	mov			byte ptr[r12], al
	mov			al, byte ptr[rcx +  5]
	mov			byte ptr[r12 + 4], al
	mov			al, byte ptr[rcx +  8]
	mov			byte ptr[r12 + 8], al
	mov			al, byte ptr[rcx + 11]
	mov			byte ptr[r12 + 12], al
	movdqu		xmm0, oword ptr[r12]






	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																		
	;cvtdq2ps	xmm0, xmm0							;convert integers to floats in xmm0		
	mulps		xmm0, xmm3							;multiply by 0.333											
	;cvtps2dq	xmm0, xmm0							;convert floats to integers in xmm0


	;first pixel									
	movd		eax, xmm0	    					;move integer value of pixels to eax
	mov			byte ptr[rcx	 ], al				;move b value to table
	mov			byte ptr[rcx +  1], al				;move g value to table 
	mov			byte ptr[rcx +  2], al				;move r value to table
							    					
	;second pixel		
	;pshufd		xmm0, xmm0, 00111001b
	;movd		eax, xmm0    					
	pextrb		eax, xmm0, 8   
	mov			byte ptr[rcx +  3], al					 
	mov			byte ptr[rcx +  4], al				 
	mov			byte ptr[rcx +  5], al				
							    					
	;third pixel			
	pextrb		eax, xmm0, 8    					
	mov			byte ptr[rcx +  6], al					 
	mov			byte ptr[rcx +  7], al				 
	mov			byte ptr[rcx +  8], al				
													
	;fourth pixel	
	pextrb		eax, xmm0, 12						
	mov			byte ptr[rcx +  9], al					 
	mov			byte ptr[rcx + 10], al				 
	mov			byte ptr[rcx + 11], al	



	;first pixel									
	mov			al, byte ptr[r12] 					;move value of first pixel to al
	mov			byte ptr[rcx	 ], al				;move b value to table
	mov			byte ptr[rcx +  1], al				;move g value to table 
	mov			byte ptr[rcx +  2], al				;move r value to table
							    					
	;second pixel		 					
	mov			al, byte ptr[r12 + 4] 	    
	mov			byte ptr[rcx +  3], al					 
	mov			byte ptr[rcx +  4], al				 
	mov			byte ptr[rcx +  5], al				
							    					
	;third pixel			
	mov			al, byte ptr[r12 + 8] 	     					
	mov			byte ptr[rcx +  6], al					 
	mov			byte ptr[rcx +  7], al				 
	mov			byte ptr[rcx +  8], al				
													
	;fourth pixel	
	mov			al, byte ptr[r12 + 12] 							
	mov			byte ptr[rcx +  9], al					 
	mov			byte ptr[rcx + 10], al				 
	mov			byte ptr[rcx + 11], al	



	;first pixel									
	movd		eax, xmm0	    					;move integer value of pixels to eax
	mov			byte ptr[rcx	 ], al				;move b value to table
	mov			byte ptr[rcx +  1], al				;move g value to table 
	mov			byte ptr[rcx +  2], al				;move r value to table
							    					
	;second pixel		
	;pshufd		xmm0, xmm0, 00111001b
	;movd		eax, xmm0    					
	pextrb		eax, xmm0, 8   
	mov			byte ptr[rcx +  3], al					 
	mov			byte ptr[rcx +  4], al				 
	mov			byte ptr[rcx +  5], al				
							    					
	;third pixel			
	pextrb		eax, xmm0, 8    					
	mov			byte ptr[rcx +  6], al					 
	mov			byte ptr[rcx +  7], al				 
	mov			byte ptr[rcx +  8], al				
													
	;fourth pixel	
	pextrb		eax, xmm0, 12						
	mov			byte ptr[rcx +  9], al					 
	mov			byte ptr[rcx + 10], al				 
	mov			byte ptr[rcx + 11], al		