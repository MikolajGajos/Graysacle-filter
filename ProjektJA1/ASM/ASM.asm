.code
GrayScaleASMFunc proc
mov			ebx, dword ptr[rbp + 48]	; take start index from stack 
mov			r11, rbx					; and give it to r11
mov			ebx, dword ptr[rbp + 56]	; take end index from stack 
mov			r10, rbx					; and give it to r10	

sub			r10, r11					; establish counter 
mov			rdi, r10					; and give it to rdi


sepiaLoop:
	cmp			edi, 0h						; if counter reaches zero, end loop
	je			endLoop						



	sub			rdi, 3						; decrease counter
	jmp			sepiaLoop					; go through loop
endLoop:
    ret
GrayScaleASMFunc endp
end