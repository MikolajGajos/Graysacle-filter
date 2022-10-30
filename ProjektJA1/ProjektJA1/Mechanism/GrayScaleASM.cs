using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleASM : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern void GrayScaleASMFunc(float[] pixels, int beg, int end);

        public override void ExecuteEffect(float[] pixels, int beg, int end)
        {
            GrayScaleASMFunc(pixels, beg, end);
        }
    }
}
