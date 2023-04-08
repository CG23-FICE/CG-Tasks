namespace ImageConverter.Sdk.Models
{
    public class Bitmap
    {
        private readonly Pixel[,] _bitmap;
        public uint Height { get; set; }
        public uint Width { get; set; }

        public Bitmap(uint height, uint width)
        {
            _bitmap = new Pixel[height, width];
            Height = height;
            Width = width;
        }

        public Pixel this[uint y, uint x]
        {
            get => _bitmap[y, x];
            set => _bitmap[y, x] = value;
        }
    }
}


