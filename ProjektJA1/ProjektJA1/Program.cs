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
                12, pixels, BitmapManager.bitmapSource.PixelWidth, BitmapManager.bitmapSource.PixelHeight, Language.C);
            manager.ExecuteEffect();

            BitmapManager.SaveBitmap(pixels, @"D:\dupa\xdd.bmp");

            Console.ReadLine();
        }   
    }
}
