using ImageConverter.Sdk.Interfaces;
using ImageConverter.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Readers
{
    public class BMPReader : IImageReader
    {
        public string Title => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string FormatName => throw new NotImplementedException();

        public Bitmap Read(Stream stream)
        {
            using BinaryReader reader = new BinaryReader(stream);

            BMPHeader Header = new BMPHeader
            {
                Id = reader.ReadUInt16(),
                FileSize = reader.ReadUInt32(),
                Reserved = reader.ReadUInt32(),
                HeaderSize = reader.ReadUInt32(),
                InfoSize = reader.ReadUInt32(),
                Width = reader.ReadUInt32(),
                Height = reader.ReadUInt32(),
                Planes = reader.ReadUInt16(),
                BitsPerPixel = reader.ReadUInt16(),
                Compression = reader.ReadUInt32(),
                ImageSize = reader.ReadUInt32(),
                XPixelsPerMeter = reader.ReadUInt32(),
                YPixelsPerMeter = reader.ReadUInt32(),
                ColorsUsed = reader.ReadUInt32(),
                ColorsImportant = reader.ReadUInt32()
            };
            int rowPadding = 3 - ((int)Header.Width * 3 - 1) % 4;
            int colorTableSize = (int)(Header.HeaderSize + (Header.BitsPerPixel <= 8 ? (1 << Header.BitsPerPixel) * 4 : 0) - 54);
            reader.ReadBytes(colorTableSize);
            var bitmap = new Bitmap(Header.Height, Header.Width);

            for (uint y = 0; y < Header.Height; y++)
            {
                for (uint x = 0; x < Header.Width; x++)
                {
                    bitmap[y, x] = new Pixel(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                }
                reader.ReadBytes(rowPadding);
            }

            return bitmap;
        }
    }
}
