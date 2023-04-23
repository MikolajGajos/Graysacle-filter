using MainProg.Mechanism;
using System;

namespace MainProg
{
    internal static class Program
    {
        static void Main()
        {
            BitmapManager bm = new BitmapManager();

            try
            {
                //bm.Load(@"Enter file name");
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                GrayScaleManager manager = new GrayScaleManager(6, bm.GetBitmapData(), Language.C);
                manager.ExecuteEffect();
                //bm.Save(@"Enter file name");
                Console.ReadLine();
                return;
            }
        }
    }
}
