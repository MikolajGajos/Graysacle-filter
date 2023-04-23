using System;

namespace MainProg.Mechanism
{
    public abstract class GrayScaleInterface
    {
        public abstract void ExecuteEffect(IntPtr pixels, int start, int end, int stride, int width); 
    }
}
