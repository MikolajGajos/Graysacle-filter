using System;
using System.Runtime.InteropServices;
using System.Drawing;
using ProjektJA1;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;
using System.IO;

namespace ProjektJA
{
    internal static class Program
    {
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static float[] ToFloatArray(this byte[] byteArray)
        {
            float[] floatArray = new float[byteArray.Length];
            for (int i = 0; i < floatArray.Length; i++)
            {
                floatArray[i] = (float)byteArray[i];
            }
            return floatArray;
        }

        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static float[] ImageToFloat(Image img)
        {
            byte[] data = img.ToByteArray(ImageFormat.Bmp);
            return data.ToFloatArray();
        }

        public static void CalcualtePixels(float[] pixels)
        {
            for (int i = 0; i < pixels.Length; i += 3) 
            {
                float r = pixels[i];
                float g = pixels[i + 1];
                float b = pixels[i + 2];

                float avg = (r + g + b) / 3;

                pixels[i] = avg;
                pixels[i + 1] = avg;
                pixels[i + 2] = avg;
            }
        }

        public static void FloatToBitmap(float[] pixels, Bitmap bmp)
        {
            int i = 54;
            for (int y = bmp.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < bmp.Width; x++, i += 3)
                {
                    Color color = Color.FromArgb((int)pixels[i], (int)pixels[i + 1], (int)pixels[i + 2]);
                    bmp.SetPixel(x, y, color);
                }
            }            
        }

        public static void GrayScale(float[] pixels, Bitmap bmp)
        {
            CalcualtePixels(pixels);
            FloatToBitmap(pixels, bmp);
        }

        static void Main()
        {
            Bitmap bmp = new Bitmap(@"D:\dupa\eo.bmp");

            float[] pixels = ImageToFloat(bmp);

            GrayScale(pixels, bmp);

            bmp.Save(@"D:\dupa\xd.bmp");

            GrayScaleManager grayScaleManager = new GrayScaleManager();
            grayScaleManager.TasksTest(3);

            Console.ReadLine();
        }   
    }
}
