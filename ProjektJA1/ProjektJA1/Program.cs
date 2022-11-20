using ProjektJA.Mechanism;
using System;

namespace ProjektJA
{
    internal static class Program
    {
        static void Main()
        {
            byte[] pixels = BitmapManager.LoadToArray(@"D:\dupa\eo.bmp");

            GrayScaleManager manager = new GrayScaleManager(
                64, pixels, BitmapManager.bitmapSource.PixelWidth, BitmapManager.bitmapSource.PixelHeight, Language.ASM);
            manager.ExecuteEffect();

            BitmapManager.SaveBitmap(pixels, @"D:\dupa\xdd.bmp");

            Console.ReadLine();
        }   
    }
}
