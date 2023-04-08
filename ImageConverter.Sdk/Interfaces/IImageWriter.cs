using ImageConverter.Sdk.Models;

namespace ImageConverter.Sdk.Interfaces
{
    public interface IImageWriter : IBaseImageFormater
    {
        void Write(Bitmap bitmap, string path);
    }
}
