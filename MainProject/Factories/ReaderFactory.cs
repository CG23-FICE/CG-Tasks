using ImageConverter.Sdk.Interfaces;
using MainProject.Utils;

namespace MainProject.Factories
{
    public class ReaderFactory
    {
        public static IImageWriter GetReader(string formatName)
        {
            var readers = PluginsReader.GetReaderPlugins();

            var requestedReader = readers.FirstOrDefault(reader => reader.FormatName == formatName);

            if (requestedReader is null)
            {
                throw new Exception("Requested format is not Supported.");
            }

            return requestedReader;
        }
    }
}
