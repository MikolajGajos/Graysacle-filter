using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleC : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\C.dll")]
        static extern void GreyScaleCFunc(float[] pixels, int beg, int end);

        public override void ExecuteEffect(float[] pixels, int beg, int end)
        {
            GreyScaleCFunc(pixels, beg, end);
        }
    }
}
