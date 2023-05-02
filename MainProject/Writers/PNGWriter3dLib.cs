using StbImageWriteSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Writers
{

    public class PNGWriter3dLib 
    {
        static string _workingDirectory = Environment.CurrentDirectory;
        static string _pathToKorova = Directory.GetParent(_workingDirectory)!
            .Parent!
            .Parent!
            .Parent!
            .GetDirectories()
            .First(x => x.Name == "ImageConverter.Sdk")
            .GetDirectories()
            .First(x => x.Name == "Images")
            .FullName;

        public void Write(string Name, float[,] pixels)
        {
            int width = pixels.GetLength(1);
            int height = pixels.GetLength(0);

            byte[] binary = new byte[width * height * 3];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    byte greyscale = (byte)(pixels[y, x] * 255.0f);
                    const int channels = 3;
                    binary[y * width * channels + x * channels + 0] = greyscale;
                    binary[y * width * channels + x * channels + 1] = greyscale;
                    binary[y * width * channels + x * channels + 2] = greyscale;
                }
            }

            using FileStream stream = new FileStream(Path.Combine(_pathToKorova, Name), FileMode.Create);
            var imageWriter = new ImageWriter();
            //imageWriter.WriteJpg(binary, width, height, ColorComponents.RedGreenBlue, stream, 24);
            imageWriter.WritePng(binary, width, height, ColorComponents.RedGreenBlue, stream);
            stream.Flush();
        }

    }
}
