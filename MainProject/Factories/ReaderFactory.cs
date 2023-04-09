using ImageConverter.Sdk.Interfaces;
using MainProject.Utils;

namespace MainProject.Factories
{
    public class ReaderFactory
    {
        public static IImageReader GetReader(string goalFormat)
        {
            var readers = PluginsReader.GetAllAvailableReaders();

            var requestedReader = readers.FirstOrDefault(reader => reader.FormatName.ToLower() == goalFormat.ToLower());

            if (requestedReader is null)
            {
                throw new Exception("Requested format is not Supported.");
            }

            return requestedReader;
        }

        public static IImageReader GetReader(FileInfo image)
        {
            return GetReader(image.Extension);
        }
    }
}
