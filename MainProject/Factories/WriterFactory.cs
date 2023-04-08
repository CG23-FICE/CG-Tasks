using ImageConverter.Sdk.Interfaces;
using MainProject.Utils;

namespace MainProject.Factories
{
    public class WriterFactory
    {
        public IImageWriter GetWriter(string formatName)
        {
            var writers = PluginsReader.GetWriterPlugins();

            var requestedWriter = writers.FirstOrDefault(writer => writer.FormatName == formatName);

            if (requestedWriter is null)
            {
                throw new Exception("Requested format is not Supported.");
            }

            return requestedWriter;
        }
    }
}
