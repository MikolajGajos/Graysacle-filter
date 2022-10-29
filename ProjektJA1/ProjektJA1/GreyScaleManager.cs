using ProjektJA.Mechanism;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace ProjektJA
{
    internal class GrayScaleManager
    {
        private List<Task> tasks = new List<Task>();
        private int numOfTasks;
        private Bitmap bitmap;
        private GrayScaleInterface mechanism;

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

        public GrayScaleManager(int numOfTasks, ref Bitmap bitmap, Language language)
        {
            this.numOfTasks = numOfTasks;
            this.bitmap = bitmap;
            SelectMechanism(language);
        }

        private void AddTasks(float[] pixels)
        {
            int sectionSize = pixels.Length / numOfTasks;
            for (int i = 0; i < numOfTasks; i++)
            {
                int beg = i * sectionSize;
                int end = (i + 1) * sectionSize;
                tasks.Add(new Task(() => mechanism.ExecuteEffect(pixels, beg, end)));
            }
        }

        public void ExecuteEffect()
        {           
            float[] pixels = BitmapManager.BitmapToFloat(bitmap);

            //
            AddTasks(pixels);;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(tasks, (task) => task.Start());
            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            Console.ReadLine();
            //

            BitmapManager.FloatToBitmap(ref pixels, ref bitmap);
        }
    }
}
