using ImageConverter.Sdk.Models;

namespace ImageConverter.Sdk.Interfaces
{
    public interface IImageReader : IBaseImageFormater
    {
        Bitmap Read(Stream stream);
    }
}
