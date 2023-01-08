using ProjektJA.Mechanism;
using System;

namespace ProjektJA
{
    internal static class Program
    {
        static BitmapManager bm = new BitmapManager();

        static void Main()
        {
            bm.Load(@"D:\dupa\dupsko.bmp");

            GrayScaleManager manager = new GrayScaleManager(1, bm.GetBitmapData(), Language.C);


            double[] array = new double[100];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = manager.ExecuteEffect();
                //Console.WriteLine(array[i]);
            }
            Array.Sort(array);
            Console.WriteLine(array[50]);

            bm.Save(@"D:\dupa\xdd.bmp");


            //bm.Load(@"D:\dupa\test.bmp");

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
            

            GrayScaleManager manager = new GrayScaleManager(numOfThreads, bm.GetBitmapData(), Language.ASM);

            double[] array = new double[1000];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = manager.ExecuteEffect();
                //Console.WriteLine(array[i]);
            }
            Array.Sort(array);
            //Console.WriteLine("Threads: " + numOfThreads);
            Console.WriteLine(array[500]);
            //Console.WriteLine();
        }
    }
}
