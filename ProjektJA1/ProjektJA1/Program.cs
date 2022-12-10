using ProjektJA.Mechanism;
using System;
using System.Security.Authentication;

namespace ProjektJA
{
    internal static class Program
    {
        static void Main()
        {
            BitmapManager bm = new BitmapManager();
            bm.Load(@"D:\dupa\huj.bmp");

            GrayScaleManager manager = new GrayScaleManager(6, bm.GetBitmapData(), Language.ASM);

            double[] array = new double[100];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = manager.ExecuteEffect();
            }
            Array.Sort(array);
            Console.WriteLine(array[50]);

            bm.Save(@"D:\dupa\xdd.bmp");

            Console.ReadLine();
        }
    }
}
