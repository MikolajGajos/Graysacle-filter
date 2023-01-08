using System;

namespace ProjektJA.Mechanism
{
    public abstract class GrayScaleInterface
    {
        public abstract void ExecuteEffect(IntPtr pixels, int beg, int end, int stride, int width); 
    }
}
