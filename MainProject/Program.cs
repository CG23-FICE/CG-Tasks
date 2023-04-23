using MainProject.Factories;
using MainProject.Models.ImagePluginsModels;
using MainProject.Models;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using MainProject.Objects;
using MainProject;
using Aspose.CAD;

internal class Program
{
    private static void Main(string[] args)
    {
        // load OBJ in an instance of Image via its Load method
        using (var image = Image.Load("cow.obj"))
        {
            // create an instance of CadRasterizationOptions and set page height & width
            var rasterizationOptions = new Aspose.CAD.ImageOptions.CadRasterizationOptions()
            {
                PageWidth = 1600,
                PageHeight = 1600
            };

            // create an instance of PngOptions
            var options = new Aspose.CAD.ImageOptions.PngOptions();

            // set the VectorRasterizationOptions property as CadRasterizationOptions
            options.VectorRasterizationOptions = rasterizationOptions;

            // export OBJ to PNG
            image.Save("output.png", options);
        }
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