using ImageConverter.Sdk.Interfaces;
using MainProject.Utils;

namespace MainProject.Factories
{
    public class WriterFactory
    {
        public static IImageWriter GetWriter(string goalFormat)
        {
            var writers = PluginsReader.GetAllAvailableWriters();

            var requestedWriter = writers.FirstOrDefault(writer => writer.FormatName.ToLower() == goalFormat.ToLower());

            if (requestedWriter is null)
            {
                throw new Exception("Requested format is not Supported.");
            }

            return requestedWriter;
        }
    }
}
