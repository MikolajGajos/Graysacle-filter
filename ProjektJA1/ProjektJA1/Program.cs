using ProjektJA.Mechanism;
using System;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Windows;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace ProjektJA
{
    internal static class Program
    {
        static void Blu(int size, byte[] pixels)
        {
            for (int i = 0; i < size; i += 4)
            {
                pixels[i + 1] = 0;
                pixels[i + 2] = 0;
            }
        }

        static void Green(int size, byte[] pixels)
        {
            for (int i = 0; i < size; i += 4)
            {
                pixels[i] = 0;
                pixels[i + 2] = 0;
            }
        }

        static void Red(int size, byte[] pixels)
        {
            for (int i = 0; i < size; i += 4)
            {
                pixels[i] = 0;
                pixels[i + 1] = 0;
            }
        }

        static void Gray(int size, byte[] pixels)
        {
            for (int i = 0; i < size; i += 4)
            {
                float b = pixels[i];
                float g = pixels[i + 1];
                float r = pixels[i + 2];

                float avg = (r + g + b) / 3;

                pixels[i] = (byte)avg;
                pixels[i + 1] = (byte)avg;
                pixels[i + 2] = (byte)avg;
            }
        }

        static void Nigativ(int size, byte[] pixels)
        {
            for (int i = 0; i < size; i += 4)
            {
                float b = pixels[i];
                float g = pixels[i + 1];
                float r = pixels[i + 2];

                pixels[i] = (byte)(255 - b);
                pixels[i + 1] = (byte)(255 - g);
                pixels[i + 2] = (byte)(255 - r);
            }
        }

        static void Main()
        {
            float[] pixels = BitmapManager.loadToArray(@"D:\dupa\eo.bmp");

            GrayScaleManager manager = new GrayScaleManager(1, ref pixels, Language.ASM);
            manager.ExecuteEffect();

            BitmapManager.saveBitmap(ref pixels, @"D:\dupa\xd.bmp");

            Console.ReadLine();

        }   
    }
}
