using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleASM : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern void GrayScaleASMFunc(IntPtr pixels, int beg, int end, int stride, int width);

        public override void ExecuteEffect(IntPtr pixels, int beg, int end, int stride, int width)
        {
            GrayScaleASMFunc(pixels, beg, end, stride, width);
        }
    }
}
