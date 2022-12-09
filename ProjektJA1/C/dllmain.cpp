// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <stdint.h>

void GrayScale(uint8_t* pixels, int beg, int end)
{
    for (int i = beg; i < end; i += 3)
    {
        uint8_t b = pixels[i];
        uint8_t g = pixels[i + 1];
        uint8_t r = pixels[i + 2];

        int avg = b + g + r;
        avg = avg / 3;
        uint8_t byteAvg = (uint8_t)avg;

        pixels[i] = byteAvg;
        pixels[i + 1] = byteAvg;
        pixels[i + 2] = byteAvg;
    }
}


extern "C" __declspec(dllexport) void GrayScaleCFunc(uint8_t * pixels, int beg, int end)
{
    GrayScale(pixels, beg, end);
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