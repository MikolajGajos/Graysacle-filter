using System.Drawing;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;
using System.IO;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Ink;
using System.Windows;
using System.Runtime.CompilerServices;

namespace ProjektJA.Mechanism
{
    internal class BitmapManager
    {
        private static BitmapSource bitmapSource;

        private static float[] ByteToFloat(byte[] bytePixles)
        {
            float[] result = new float[bytePixles.Length];
            for (int i = 0; i < bytePixles.Length; i++)
            {
                result[i] = bytePixles[i];
            }
            return result;
        }

        private static byte[] FloatToByte(float[] floatPixles)
        {
            byte[] result = new byte[floatPixles.Length];
            for (int i = 0; i < floatPixles.Length; i++)
            {
                result[i] = (byte)floatPixles[i];
            }
            return result;
        }

        private static float[] saveToArray()
        {
            int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixlesByte = new byte[bitmapSource.PixelHeight * stride];
            bitmapSource.CopyPixels(pixlesByte, stride, 0);
            return ByteToFloat(pixlesByte);
        }

        public static float[] loadToArray(string path)
        {
            bitmapSource = new BitmapImage(new System.Uri(path));
            return saveToArray();
        }

        private static WriteableBitmap saveToWritablebitap(ref float[] pixels)
        {
            byte[] bytes = FloatToByte(pixels);
            int stride = bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, 96, 96, bitmapSource.Format, null);
            writeableBitmap.WritePixels(new Int32Rect(0, 0, bitmapSource.PixelWidth, bitmapSource.PixelHeight), bytes, stride, 0);
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

        public static void saveBitmap(ref float[] pixels, string pat)
        {
            bitmapSource = saveToWritablebitap(ref pixels);
            Export(pat);
        }
    }
}
