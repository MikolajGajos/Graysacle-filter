using System.Drawing.Imaging;
using System.Drawing;

namespace ProjektJA.Mechanism
{
    internal class BitmapManager
    {
        private Bitmap bitmap;
        private BitmapData bmpData;

        public BitmapData GetBitmapData()
        {
            return bmpData;
        }

        public void Load(string path)
        {
            bitmap = new Bitmap(path);
            bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
        }

        public void Save(string path)
        {
            bitmap.Save(path);
        }
    }
}
