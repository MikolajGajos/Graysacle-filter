using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleASM : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern int GreyScaleASM(int a, int b);

        public override void ExecuteEffect(float[] pixels, int beg, int end)
        {
            throw new NotImplementedException();
        }
    }
}
