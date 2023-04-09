using MainProject.Factories;
using MainProject.Models.ImagePluginsModels;

internal class Program
{
    private static void Main(string[] args)
    {
        var imageInfo = ArgumentsReader(args);
        var imageReader = ReaderFactory.GetReader(imageInfo.Image);
        var imageWriter = WriterFactory.GetWriter(imageInfo.GoalFormat);
        imageWriter.Write(imageReader.Read(imageInfo.Image.OpenRead()), imageInfo.OutputDirectoryPath);
    }

    private static ArgumentReaderResponse ArgumentsReader(string[] args)
    {
        ArgumentReaderResponse response = new ArgumentReaderResponse();
        if (args.Length == 0)
        {
            throw new ArgumentException("The program did not recieve any arguments.");
        }

        try
        {
            var imageSource = args.Single(arg => arg.StartsWith("--source"));
            var imageSourcePath = imageSource.Split("=")[1];
            response.Image = new FileInfo(imageSourcePath);
        }
        catch (InvalidOperationException)
        {
            throw new Exception("The \"--source\" parameter was not only one.");
        }

        try
        {
            var goalFormat = args.Single(arg => arg.StartsWith("--goal-format"));
            var goalFormatValue = goalFormat.Split("=")[1];
            response.GoalFormat = goalFormatValue.StartsWith(".") ? goalFormatValue : $".{goalFormatValue}";
        }
        catch (InvalidOperationException)
        {
            throw new Exception("The \"--goal-format\" parameter was not only one.");
        }

        try
        {
            var outputDirectory = args.Single(arg => arg.StartsWith("--source"));
            response.OutputDirectoryPath = outputDirectory.Split("=")[1];
        }
        catch (InvalidOperationException)
        {
            throw new Exception("The \"--output\" parameter was not only one.");
        }

        return response;
    }
}