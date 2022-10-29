using System.Drawing;

namespace ProjektJA
{
    internal static class Program
    {
        static void Main()
        {
            Bitmap bitmap = new Bitmap(@"D:\dupa\eo.bmp");

            GrayScaleManager grayScaleManager = new GrayScaleManager(6, ref bitmap, Language.C);
            grayScaleManager.ExecuteEffect();

            bitmap.Save(@"D:\dupa\xd.bmp");
        }   
    }
}
