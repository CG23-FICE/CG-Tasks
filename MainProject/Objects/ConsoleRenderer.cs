using System;

namespace MainProject.Objects
{
    class ConsoleRenderer
    {
        public static void Main(int[,] pixels)
        {
            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    if (pixels[i, j] < 0)
                    {
                        Console.Write(" ");
                    }
                    else if (pixels[i, j] >= 0 && pixels[i, j] < 0.2)
                    {
                        Console.Write(".");
                    }
                    else if (pixels[i, j] >= 0.2 && pixels[i, j] < 0.5)
                    {
                        Console.Write("*");
                    }
                    else if (pixels[i, j] >= 0.5 && pixels[i, j] < 0.8)
                    {
                        Console.Write("O");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
     
        }
    }
}