using System.Runtime.InteropServices;

namespace ProjektJA.Mechanism
{
    internal class GrayScaleASM : GrayScaleInterface
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA1\x64\Debug\ASM.dll")]
        static extern void GrayScaleASMFunc(byte[] pixels, int beg, int end, float[] array3);

        public override void ExecuteEffect(byte[] pixels, int beg, int end)
        {
            float[] array3 = { 3, 3, 3, 3 };
            GrayScaleASMFunc(pixels, beg, end, array3);
        }
    }
}
