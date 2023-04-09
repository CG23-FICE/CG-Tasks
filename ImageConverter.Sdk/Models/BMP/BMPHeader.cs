using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter.Sdk.Models
{
    public class BMPHeader
    {
        public ushort Id { get; set; }
        public uint FileSize { get; set; }
        public uint Reserved { get; set; }
        public uint HeaderSize { get; set; }
        public uint InfoSize { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public ushort Planes { get; set; }
        public ushort BitsPerPixel { get; set; }
        public uint Compression { get; set; }
        public uint ImageSize { get; set; }
        public uint XPixelsPerMeter { get; set; }
        public uint YPixelsPerMeter { get; set; }
        public uint ColorsUsed { get; set; }
        public uint ColorsImportant { get; set; }

    }
}
