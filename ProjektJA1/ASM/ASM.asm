
.code
GrayScaleASMFunc proc
												
;establish loop counter
mov			r11, rdx							 ;start index to r11
mov			r10, r8								 ;stop index to r10												   
sub			r10, r11							 ;calculate the counter
mov			rdi, r10							 ;move counter to rdi										   
add			rcx, r11							 ;add start offset to rcx

;prepare xmm3 for dividing
mov			eax, 3								 ;move 3 to eax
cvtsi2ss	xmm3, eax							 ;move 3 from eax to xmm3 with convertion to float
punpckldq	xmm3, xmm3							 ;broadcast value to lower part 
punpcklqdq	xmm3, xmm3							 ;broadcast value to higher part
												 
grayScaleLoop:			
	;end condition check
	cmp			edi, 0								;compare counter with 0
	je			endLoop								;conditional jump
													
	pxor		xmm0, xmm0							;clear xmm0
													
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
													
	;fourth pixel									
	pinsrb		xmm0, byte ptr[rcx +  9], 12		
	pinsrb		xmm1, byte ptr[rcx + 10], 12		
	pinsrb		xmm2, byte ptr[rcx + 11], 12		
													
	;calculate avarage value of pixels									
	paddd		xmm0, xmm1							;add b with g
	paddd		xmm0, xmm2							;add b+g with r																		
	cvtdq2ps	xmm0, xmm0							;convert integers to floats in xmm0												
	divps		xmm0, xmm3							;divide all 4 pixels by 3																		
	cvtps2dq	xmm0, xmm0							;convert floats to integers in xmm0
													
	;first pixel									
	pextrb		eax, xmm0, 0    					;move integer value of pixels to eax
	mov			byte ptr[rcx	 ], al				;move b value to table
	mov			byte ptr[rcx +  1], al				;move g value to table 
	mov			byte ptr[rcx +  2], al				;move r value to table
							    					
	;second pixel			    					
	pextrb		eax, xmm0, 4    					
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
	
	add		rcx, 12									;add to table
	sub		rdi, 12									;subtract form loop counter
	jmp		grayScaleLoop						   
endLoop:										   
    ret											   

GrayScaleASMFunc endp
end
