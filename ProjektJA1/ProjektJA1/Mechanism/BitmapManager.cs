using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ProjektJA.Mechanism
{
    internal class BitmapManager
    {
        public static BitmapSource bitmapSource;

        private static byte[] SaveToArray()
        {
            int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[bitmapSource.PixelHeight * stride];
            bitmapSource.CopyPixels(pixels, stride, 0);
            return pixels;
        }

        public static byte[] LoadToArray(string path)
        {
            bitmapSource = new BitmapImage(new System.Uri(path));
            return SaveToArray();
        }

        private static WriteableBitmap SaveToWriteablebitap(byte[] pixels)
        {
            int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, 96, 96, bitmapSource.Format, null);
            writeableBitmap.WritePixels(new Int32Rect(0, 0, bitmapSource.PixelWidth, bitmapSource.PixelHeight), pixels, stride, 0);
            return writeableBitmap;
        }

        private static void Export(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(fileStream);
            }
        }

        public static void SaveBitmap(byte[] pixels, string path)
        {
            bitmapSource = SaveToWriteablebitap(pixels);
            Export(path);
        }
    }
}
