using ImageConverter.Sdk.Interfaces;
using ImageConverter.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Writers
{
    public class BMPWriter : IImageWriter
    {
        public string Title => "Kek";

        public string Description => "kok";

        public string FormatName => ".bmp";

        public void Write(Bitmap bitmap, string path)
        {
            uint rowPadding = 3 - (bitmap.Width * 3 - 1) % 4;
            uint imageSize = bitmap.Width * bitmap.Height * 3 + rowPadding * bitmap.Height;
            uint fileSize = imageSize + 54;

            var header = new BMPHeader()
            {
                Id = 0x4D42, //Convert.ToByte("BM"),
                Reserved = 0,
                FileSize = fileSize,
                HeaderSize = 54,
                InfoSize = 40,
                Width = bitmap.Width,
                Height = bitmap.Height,
                Planes = 1,
                BitsPerPixel = 24,
                Compression = 0,
                ImageSize = imageSize,
                XPixelsPerMeter = 0,
                YPixelsPerMeter = 0,
                ColorsUsed = 0,
                ColorsImportant = 0
            };


            using FileStream stream = new FileStream(path, FileMode.Create);
            using BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(header.Id);
            writer.Write(header.FileSize);
            writer.Write(header.Reserved);
            writer.Write(header.HeaderSize);
            writer.Write(header.HeaderSize);
            writer.Write(header.Width);
            writer.Write(header.Height);
            writer.Write(header.Planes);
            writer.Write(header.BitsPerPixel);
            writer.Write(header.Compression);
            writer.Write(header.ImageSize);
            writer.Write(header.XPixelsPerMeter);
            writer.Write(header.YPixelsPerMeter);
            writer.Write(header.ColorsUsed);
            writer.Write(header.ColorsImportant);
            for (uint y = 0; y < header.Height; y++)
            {
                for (uint x = 0; x < header.Width; x++)
                {
                    writer.Write((byte)bitmap[y, x].R);
                    writer.Write((byte)bitmap[y, x].G);
                    writer.Write((byte)bitmap[y, x].B);
                }
                for (int j = 0; j < rowPadding; j++)
                {
                    writer.Write(Convert.ToByte(0));
                }
            }
        }
    }
}
