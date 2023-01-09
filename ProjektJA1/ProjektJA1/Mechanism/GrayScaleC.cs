using System;
using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleC : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Release\C.dll")]
        static extern void GrayScaleCFunc(IntPtr pixels, int start, int end, int stride, int width);

        public override void ExecuteEffect(IntPtr pixels, int start, int end, int stride, int width)
        {
            GrayScaleCFunc(pixels, start, end, stride, width);
        }
    }
}
