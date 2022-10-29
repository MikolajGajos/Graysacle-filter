using System.Drawing;
using Color = System.Drawing.Color;
using System.Drawing.Imaging;
using System.IO;

namespace ProjektJA.Mechanism
{
    internal static class BitmapManager
    {
        //private byte[] ImageToByte(Image img)
        //{
        //    ImageConverter converter = new ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}

        private static byte[] ImageToByte(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        private static float[] ByteArrToFloat(ref byte[] byteArray)
        {
            float[] floatArray = new float[byteArray.Length];
            for (int i = 0; i < floatArray.Length; i++)
            {
                floatArray[i] = (float)byteArray[i];
            }
            return floatArray;
        }        

        public static float[] BitmapToFloat(this Bitmap bitmap)
        {
            byte[] byteArray = ImageToByte(bitmap, ImageFormat.Bmp);
            return ByteArrToFloat(ref byteArray);
        }

        private static void UpdatePixsels(ref float[] pixels, ref Bitmap bitmap)
        {
            int i = 54;
            for (int y = bitmap.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < bitmap.Width; x++, i += 3)
                {
                    Color color = Color.FromArgb((int)pixels[i], (int)pixels[i + 1], (int)pixels[i + 2]);
                    bitmap.SetPixel(x, y, color);
                }
            }
        }

        public static void FloatToBitmap(ref float[] pixels, ref Bitmap bitmap)
        {
            UpdatePixsels(ref pixels, ref bitmap);
        }

    }
}
