using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ProjektJA.Mechanism
{
    internal class BitmapManager
    {
        //private static BitmapSource bitmapSource;
        //public static int Height { set; get; }
        //public static int Width { set; get; }

        //private static byte[] SaveToArray()
        //{
        //    int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
        //    byte[] pixels = new byte[bitmapSource.PixelHeight * stride];
        //    bitmapSource.CopyPixels(pixels, stride, 0);

        //    return pixels;
        //}

        //public static byte[] LoadToArray(string path)
        //{
        //    bitmapSource = new BitmapImage(new System.Uri(path));
        //    Height = bitmapSource.PixelHeight;
        //    Width = bitmapSource.PixelWidth;
        //    return SaveToArray();
        //}

        //private static WriteableBitmap SaveToWriteablebitap(byte[] pixels)
        //{
        //    int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
        //    WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, 96, 96, bitmapSource.Format, null);
        //    writeableBitmap.WritePixels(new Int32Rect(0, 0, bitmapSource.PixelWidth, bitmapSource.PixelHeight), pixels, stride, 0);
        //    return writeableBitmap;
        //}

        //private static void Export(string path)
        //{
        //    using (var fileStream = new FileStream(path, FileMode.Create))
        //    {
        //        BitmapEncoder encoder = new BmpBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        //        encoder.Save(fileStream);
        //    }
        //}

        //public static void SaveBitmap(byte[] pixels, string path)
        //{
        //    bitmapSource = SaveToWriteablebitap(pixels);
        //    Export(path);
        //}

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
