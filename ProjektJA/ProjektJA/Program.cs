using System.Runtime.InteropServices;
using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        [DllImport(@"D:\studia\github\JAproj\ProjektJA\x64\Debug\ASM.dll")]
        static extern int GreyScaleASM(int a, int b);

        [DllImport(@"D:\studia\github\JAproj\ProjektJA\x64\Debug\C.dll")]
        static extern int GreyScaleC(int a, int b);

        Console.WriteLine(GreyScaleASM(1, 3));
        Console.WriteLine(GreyScaleC(8, 5));

        //Image bitmap = (Bitmap)Image.FromFile(@"mypath.bmp");
        
    }
}

//wczytywanie danych i tworzenie watkow na za tydzien