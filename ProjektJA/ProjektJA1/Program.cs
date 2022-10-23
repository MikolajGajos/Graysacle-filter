using System;
using System.Runtime.InteropServices;
using System.Drawing;

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

            Bitmap bmp = new Bitmap(@"D:\dupa\eo.bmp");
            for (int i = 0; i < bmp.Width; i++)
            {
                bmp.SetPixel(i, 0, Color.Blue);
                bmp.SetPixel(i, 1, Color.Red);
            }
            bmp.Save(@"D:\dupa\xd.bmp");


            Console.ReadLine();
        }
    }
}
