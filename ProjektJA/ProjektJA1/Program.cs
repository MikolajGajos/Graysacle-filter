using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjektJA
{
    internal class Program
    {
        [DllImport(@"C:\Users\Mikołaj\Desktop\ProjektJA1\x64\Debug\ASM.dll")]
        static extern int GreyScaleASM(int a, int b); 

        [DllImport(@"C:\Users\Mikołaj\Desktop\ProjektJA1\x64\Debug\C.dll")]
        static extern int GreyScaleC(int a, int b);

        static void Main()
        {
            Console.WriteLine(GreyScaleASM(1, 2));
            Console.WriteLine(GreyScaleC(22, 2));

            GreyScaleManager test = new GreyScaleManager();
            test.Test(2);

            Console.ReadLine();
        }
    }
}
