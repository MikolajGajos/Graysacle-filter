// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"

void GrayScale(float* pixels, int beg, int end)
{
    for (int i = beg; i < end; i += 4)
    {
        float b = pixels[i];
        float g = pixels[i + 1];
        float r = pixels[i + 2];

        float avg = (r + g + b) / 3;

        pixels[i] = avg;
        pixels[i + 1] = avg;
        pixels[i + 2] = avg;
    }
}


extern "C" __declspec(dllexport) void GreyScaleCFunc(float* pixels, int beg, int end)
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