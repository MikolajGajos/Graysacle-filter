using System;
using System.Runtime.InteropServices;
using System.Drawing;
using ProjektJA1;
using System.IO;

namespace ProjektJA
{
    internal class Program
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern int GreyScaleASM(int a, int b); 

        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\C.dll")]
        static extern int GreyScaleC(int a, int b);

        static void Main()
        {
            GreyScale x = new GreyScale();

            Bitmap bmp = new Bitmap(@"D:\dupa\eo.bmp");
            x.BitmapTest(bmp, 0, bmp.Width);
            bmp.Save(@"D:\dupa\xd.bmp");

            x.TasksTest(3);

            Console.ReadLine();
        }   
    }
}
