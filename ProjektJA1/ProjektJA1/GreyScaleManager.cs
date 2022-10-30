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
        private float[] pixels;
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

        public GrayScaleManager(int numOfTasks, ref float[] pixels, Language language)
        {
            this.numOfTasks = numOfTasks;
            this.pixels = pixels;
            SelectMechanism(language);
        }

        private void AddTasks()
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
            //
            AddTasks();;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.ForEach(tasks, (task) => task.Start());
            Task.WaitAll(tasks.ToArray());

            sw.Stop();
            Console.WriteLine("Time in miliseconds: " + sw.Elapsed.Milliseconds);
            //

            Console.ReadLine();
        }
    }
}
