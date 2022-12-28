using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleC : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\C.dll")]
        static extern void GrayScaleCFunc(IntPtr pixels, int beg, int end);

        public override void ExecuteEffect(IntPtr pixels, int beg, int end)
        {
            GrayScaleCFunc(pixels, beg, end);
        }
    }
}
