using ImageConverter.Sdk.Interfaces;
using MainProject.Models.ImagePluginsModels;
using MainProject.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        var imageInfo = ArgumentsReader(args);
        var imageReader = ChooseImageReader(imageInfo.Image);
        var imageWriter = ChooseImageWriter(imageInfo.GoalFormat);
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

    private static IImageWriter ChooseImageReader(FileInfo image)
    {
        var extension = image.Extension;
        var availableReaders = PluginsReader.GetAllAvailableReaders();

        var reader = availableReaders.FirstOrDefault(reader => reader.FormatName.ToLower() == extension.ToLower());

        if (reader == null)
        {
            throw new Exception($"Current format {extension} is not supported.");
        }

        return reader;
    }

    private static IImageWriter ChooseImageWriter(string goalFormat)
    {
        var availableWriters = PluginsReader.GetAllAvailableWriters();

        var writer = availableWriters.FirstOrDefault(reader => reader.FormatName.ToLower() == goalFormat.ToLower());

        if (writer == null)
        {
            throw new Exception($"Current format {goalFormat} is not supported.");
        }

        return writer;
    }
}