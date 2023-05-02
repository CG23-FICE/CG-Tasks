using System;

namespace MainProject.Objects
{
    class ConsoleRenderer
    {
        public static void Render(float[,] pixels)
        {
            //Console.SetWindowSize(pixels.GetLength(0)+1, pixels.GetLength(1)+1);

            for (int i = 0; i < pixels.GetLength(0); i++)
            {
                for (int j = 0; j < pixels.GetLength(1); j++)
                {
                    char c = ' ';
                    if (pixels[i, j] > Single.Epsilon /*&& pixels[i, j] < 0.2*/) c = '#';//'.';
                    //if (pixels[i, j] >= 0.2 && pixels[i, j] < 0.5) c = '*';
                    //if (pixels[i, j] >= 0.5 && pixels[i, j] < 0.8) c = 'O';
                    //if (pixels[i, j] >= 0.8) c = '#';
                    Console.Write(c);
                }
                Console.WriteLine();
            }
        }
    }
}