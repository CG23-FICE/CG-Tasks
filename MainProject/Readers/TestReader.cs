using ImageConverter.Sdk.Interfaces;
using ImageConverter.Sdk.Models;

namespace MainProject.Readers
{
    internal class TestReader : IImageReader
    {
        public string Title => "Kek";

        public string Description => "kok";

        public string FormatName => ".png";

        public Bitmap Read(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
