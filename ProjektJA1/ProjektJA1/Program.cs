using ProjektJA.Mechanism;
using System;

namespace ProjektJA
{
    internal static class Program
    {
        static void Main()
        {
            BitmapManager bm = new BitmapManager();
            bm.Load(@"D:\dupa\eo.bmp");

            GrayScaleManager manager = new GrayScaleManager(1, bm.GetBitmapData(), Language.ASM);

            for (int i = 0; i < 10; i++)
            {
                manager.ExecuteEffect();
            }

            bm.Save(@"D:\dupa\xdd.bmp");

            Console.ReadLine();
        }
    }
}
