namespace ImageConverter.Sdk.Interfaces
{
    public interface IImageReader : IBaseImageFormater
    {
        byte[] Read(Stream stream);
    }
}
