using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ProjektJA
{
    internal class GreyScaleManager
    {
        private List<Task> tasks = new List<Task>();

        void BitmapTest(int num)
        {
            Bitmap bmp = new Bitmap(@"D:\dupa\eo.bmp");
            for (int i = 0; i < bmp.Width; i++)
            {
                bmp.SetPixel(i, 0, Color.Blue);
                bmp.SetPixel(i, 1, Color.Red);               
            }
            Console.WriteLine(num);
        }

        public void Test(int numOfThreads)
        {
            for (int i = 0; i < numOfThreads; i++)
            {
                tasks.Add(new Task(() => BitmapTest(i)));
            }
            Parallel.ForEach(tasks, (task) => task.Start());
            Task.WaitAll(tasks.ToArray());
        }
    }
}
