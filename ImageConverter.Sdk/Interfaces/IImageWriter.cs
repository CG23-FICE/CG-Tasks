namespace ImageConverter.Sdk.Interfaces
{
    public interface IImageWriter : IBaseImageFormater
    {
        void Write(string path);
    }
}
