using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleASM : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern int GrayScaleASMFunc(float[] pixels, int beg, int end, float[] array3);

        public override void ExecuteEffect(float[] pixels, int beg, int end)
        {
            //GrayScaleASMFunc(pixels, beg, end);

            float[] array255 = { 255.0f, 255.0f, 255.0f, 255.0f };
            float[] array3 = { 3.0f, 3.0f, 3.0f, 3.0f };
            float[] array0 = { 0.0f, 0.0f, 0.0f, 255.0f };

            //Console.WriteLine();
            //Console.WriteLine(pixels[0]);
            //Console.WriteLine(beg);
            //Console.WriteLine(end);
            //Console.WriteLine(end - beg);
            Console.WriteLine(GrayScaleASMFunc(pixels, beg, end, array3));
        }
    }
}
