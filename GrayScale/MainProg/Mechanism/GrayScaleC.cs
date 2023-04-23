using System;
using System.Runtime.InteropServices;

namespace MainProg.Mechanism
{
    internal class GrayScaleC : GrayScaleInterface
    {
        [DllImport(@"\..\..\..\x64\Debug\C.dll")]
        static extern void GrayScaleCFunc(IntPtr pixels, int start, int end, int stride, int width);

        public override void ExecuteEffect(IntPtr pixels, int start, int end, int stride, int width)
        {
            GrayScaleCFunc(pixels, start, end, stride, width);
        }
    }
}
