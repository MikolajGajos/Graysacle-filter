using System.Drawing;

namespace ProjektJA.Mechanism
{
    public abstract class GrayScaleInterface
    {
        public abstract void ExecuteEffect(byte[] pixels, int beg, int end); 
    }
}
