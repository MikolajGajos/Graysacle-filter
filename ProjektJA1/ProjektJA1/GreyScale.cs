using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjektJA1
{
    internal class GrayScaleManager
    {
        private List<Task> tasks = new List<Task>();

        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern int GreyScaleASM(int a, int b);

        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\C.dll")]
        static extern int GreyScaleC(int a, int b);

        void Fun(int num)
        {
            Console.WriteLine(GreyScaleASM(num, 0));
            Console.WriteLine(GreyScaleC(num, 0));
        }

        public void TasksTest(int numOfThreads)
        {           
            for (int i = 0; i < numOfThreads; i++)
            {
                int temp = i;
                tasks.Add(new Task(() => Fun(temp)));
            }
            Parallel.ForEach(tasks, (task) => task.Start());
            Task.WaitAll(tasks.ToArray());
        }
    }
}
