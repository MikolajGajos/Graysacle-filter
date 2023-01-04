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

            GrayScaleManager manager = new GrayScaleManager(64, bm.GetBitmapData(), Language.C);


            double[] array = new double[10];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = manager.ExecuteEffect();
                Console.WriteLine(array[i]);
            }
            Array.Sort(array);
            Console.WriteLine(array[5]);

            bm.Save(@"D:\dupa\xdd.bmp");

            //ExecuteAlgorithm(1);
            //ExecuteAlgorithm(2);
            //ExecuteAlgorithm(4);
            //ExecuteAlgorithm(6);
            //ExecuteAlgorithm(8);
            //ExecuteAlgorithm(10);
            //ExecuteAlgorithm(12);
            //ExecuteAlgorithm(14);
            //ExecuteAlgorithm(16);
            //ExecuteAlgorithm(24);
            //ExecuteAlgorithm(32);
            //ExecuteAlgorithm(40);
            //ExecuteAlgorithm(48);
            //ExecuteAlgorithm(56);
            //ExecuteAlgorithm(64);

            Console.ReadLine();
        }

        static void ExecuteAlgorithm(int numOfThreads)
        {
            BitmapManager bm = new BitmapManager();
            bm.Load(@"D:\dupa\test.bmp");

            GrayScaleManager manager = new GrayScaleManager(numOfThreads, bm.GetBitmapData(), Language.ASM);

            double[] array = new double[10];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = manager.ExecuteEffect();
            }
            Array.Sort(array);
            Console.WriteLine("Threads: " + numOfThreads);
            Console.WriteLine(array[5]);
            Console.WriteLine();
        }
    }
}
