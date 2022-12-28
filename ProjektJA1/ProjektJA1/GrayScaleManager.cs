using ProjektJA.Mechanism;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ProjektJA
{
    internal class GrayScaleManager
    {
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

                tasks.Add(new Task(() => mechanism.ExecuteEffect(bmpData.Scan0, start, stop, bmpData.Stride, bmpData.Width)));
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
