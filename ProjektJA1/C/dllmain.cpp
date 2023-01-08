// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <stdint.h>

void GrayScale(uint8_t* pixels, int beg, int end, int stride, int width)
{
   width *= 3;
   int currentpixel = beg;

   while (currentpixel < end)
    {
        for (int i = currentpixel; i < currentpixel + width; i += 3)
        {
            uint8_t b = pixels[i    ];
            uint8_t g = pixels[i + 1];
            uint8_t r = pixels[i + 2];

            int avg = b + g + r;
            avg /= 3;
            uint8_t byteavg = (uint8_t)avg;

            pixels[i    ] = byteavg;
            pixels[i + 1] = byteavg;
            pixels[i + 2] = byteavg;
        }
        currentpixel += stride;
   } 
}


extern "C" __declspec(dllexport) void GrayScaleCFunc(uint8_t * pixels, int beg, int end, int stride, int width)
{
    GrayScale(pixels, beg, end, stride, width);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}