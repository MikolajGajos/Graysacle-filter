using ProjektJA.Mechanism;
using System;
using System.Runtime.Remoting.Services;

namespace ProjektJA
{
    internal static class Program
    {
        

        static void Main()
        {
            //BitmapManager bm = new BitmapManager();


            //bm.Load(@"D:\dupa\cipa.bmp");

            //GrayScaleManager manager = new GrayScaleManager(1, bm.GetBitmapData(), Language.ASM);

            //double[] array = new double[100];

            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = manager.ExecuteEffect();
            //    //Console.WriteLine(array[i]);
            //}
            //Array.Sort(array);
            //Console.WriteLine(array[50]);

            //bm.Save(@"D:\dupa\xdd.bmp");


            //bm.Load(@"D:\dupa\4k.bmp");

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

            UserInterface();

            Console.ReadLine();
        }

        static void UserInterface()
        {
            Console.WriteLine("Enter bitmap file path:");
            String pathToFile = Console.ReadLine();
            Console.WriteLine("Enter bitmap save file path:");
            String pathToSave = Console.ReadLine();
            Console.WriteLine("Choose algorithm");
            Console.WriteLine("1 - C");
            Console.WriteLine("2 - Assembly");
            String algorithm = Console.ReadLine();
            Console.WriteLine("Enter threads number [1 - 64]");
            String threads = Console.ReadLine();

            try
            {
                int numOfThreads = int.Parse(threads);
                Language lan = new Language();
                if (algorithm.Equals("1"))
                {
                    lan = Language.C;
                }
                else
                {
                    lan = Language.ASM;
                }

                BitmapManager bm = new BitmapManager();
                bm.Load(pathToFile);
                GrayScaleManager manager = new GrayScaleManager(numOfThreads, bm.GetBitmapData(), lan);
                double time = manager.ExecuteEffect();
                bm.Save(pathToSave);
                Console.WriteLine("Algorithm took " + time + "ms to complete");

            }
            catch(Exception e)
            {
                Console.WriteLine("Something went wrong...");
                Console.WriteLine("Error message: ");
                Console.WriteLine(e.ToString());
            }

        }

        static void ExecuteAlgorithm(int numOfThreads)
        {
            BitmapManager bm = new BitmapManager();
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
