using ProjektJA.Mechanism;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ProjektJA
{
    internal class GrayScaleManager
    {
        //private List<Task> tasks = new List<Task>();
        //private int numOfTasks;
        //private byte[] pixels;
        //private int width;
        //private int height;
        //private int bytesPerPixel = 4;
        //private GrayScaleInterface mechanism;

        //private void SelectMechanism(Language language)
        //{
        //    switch (language)
        //    {
        //        case Language.C:
        //            mechanism = new GrayScaleC();
        //            break;
        //        case Language.ASM:
        //            mechanism = new GrayScaleASM();
        //            break;
        //        default:
        //            mechanism = null;
        //            break;
        //    }
        //}

        //public GrayScaleManager(int numOfTasks, byte[] pixels, int width, int height, Language language)
        //{
        //    this.numOfTasks = numOfTasks;
        //    this.pixels = pixels;
        //    this.width = width;
        //    this.height = height;
        //    SelectMechanism(language);
        //}

        //private void AddTasks()
        //{
        //    tasks.Clear();
        //    //int sectionSize = pixels.Length / numOfTasks;
        //    //int end = 0;
        //    //for (int i = 0; i < numOfTasks; i++)
        //    //{
        //    //    int beg = end;
        //    //    end = (i + 1) * sectionSize;
        //    //    while (pixels[end - 1] != 255) end++;
        //    //    int stop = end;
        //    //    tasks.Add(new Task(() => mechanism.ExecuteEffect(pixels, beg, stop)));
        //    //}

        //    int rowsPerTask = height / numOfTasks;
        //    int remainder = height % numOfTasks;
        //    int endPixel = 0;
        //    int currentRow = 0;
        //    for (int i = 0; i < numOfTasks; i++)
        //    {
        //        int start = endPixel;

        //        currentRow += rowsPerTask;
        //        if (remainder > 0)
        //        {
        //            currentRow++;
        //            remainder--;
        //        }

        //        endPixel = bytesPerPixel * currentRow * width;
        //        int stop = endPixel;

        //        tasks.Add(new Task(() => mechanism.ExecuteEffect(pixels, start, stop)));
        //    }

        //}

        //public void ExecuteEffect()
        //{
        //    AddTasks();

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    Parallel.ForEach(tasks, (task) => task.Start());
        //    Task.WaitAll(tasks.ToArray());

        //    sw.Stop();
        //    Console.WriteLine(sw.Elapsed.Ticks);
        //}

        private List<Task> tasks = new List<Task>();
        private GrayScaleInterface mechanism;
        private int numOfTasks;
        private BitmapData bmpData;

        public GrayScaleManager(int numOfTasks, BitmapData bmpData, Language language)
        {
            this.numOfTasks = numOfTasks;
            this.bmpData = bmpData;
            SelectMechanism(language);
        }  

        public double ExecuteEffect()
        {
            AddTasks();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(tasks, (task) => task.Start());
            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            //Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            return sw.Elapsed.TotalMilliseconds;
        }

        private void AddTasks()
        {
            tasks.Clear();

            int rowsPerTask = bmpData.Height / numOfTasks;
            int remainder = bmpData.Height % numOfTasks;
            int endPixel = 0;
            int currentRow = 0;
            for (int i = 0; i < numOfTasks; i++)
            {
                int start = endPixel;
                currentRow += rowsPerTask;
                if (remainder > 0)
                {
                    currentRow++;
                    remainder--;
                }
                endPixel = currentRow * bmpData.Stride;
                int stop = endPixel;

                tasks.Add(new Task(() => mechanism.ExecuteEffect(bmpData.Scan0, start, stop)));
            }
        }

        private void SelectMechanism(Language language)
        {
            switch (language)
            {
                case Language.C:
                    mechanism = new GrayScaleC();
                    break;
                case Language.ASM:
                    mechanism = new GrayScaleASM();
                    break;
                default:
                    mechanism = null;
                    break;
            }
        }
    }
}
