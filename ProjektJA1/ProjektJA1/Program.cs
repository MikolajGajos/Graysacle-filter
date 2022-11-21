using ProjektJA.Mechanism;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.Imaging;

namespace ProjektJA
{
    internal static class Program
    {
        static void GrayScale(byte[] pixels, int beg, int end)
        {
            for (int i = beg; i < end; i += 3)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];

                int avg = (r + g + b) / 3;

                pixels[i] = (byte)avg;
                pixels[i + 1] = (byte)avg;
                pixels[i + 2] = (byte)avg;
            }
        }


        static void Main()
        {
            byte[] pixels = BitmapManager.LoadToArray(@"D:\dupa\eo.bmp");

            GrayScaleManager manager = new GrayScaleManager(12, pixels, BitmapManager.Width, BitmapManager.Height, Language.ASM);

            manager.ExecuteEffect();

            BitmapManager.SaveBitmap(pixels, @"D:\dupa\xdd.bmp");

            Console.ReadLine();


            //Image bitmap = Image.FromFile(@"D:\dupa\eo.bmp");
            //MemoryStream memoryStream = new MemoryStream();
            //bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);

            //byte[] pixels = memoryStream.GetBuffer();



            ////GrayScale(pixels, 54, pixels.Length);    

            //memoryStream.Write(pixels, 0, pixels.Length);

            //bitmap = new Bitmap(memoryStream);
            //bitmap.Save(@"D:\dupa\xdd.bmp");
            //Console.ReadLine();
        }   
    }
}
