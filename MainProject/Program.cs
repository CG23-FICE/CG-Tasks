using MainProject.Factories;
using MainProject.Models.ImagePluginsModels;
using MainProject.Models;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using MainProject.Objects;
using MainProject;

internal class Program
{
    private static void Main(string[] args)
    {
        Transformator cameraTransformator = new();
        cameraTransformator.RotateAngleX(45);
        cameraTransformator.Move(new Vector(0, -0.5f, 0));
        Point center = new Point(0, 0, 0);
        Normal direction = new Normal(1, 0, 0);

        Camera camera = new Camera(center, direction, 30, 2, cameraTransformator);

        Sphere sphere1 = new Sphere(new Point(5, -0.2f, 0.5f), 0.3f);
        Sphere sphere2 = new Sphere(new Point(3, 0, 0), 0.3f);
        Plane plane1 = new Plane(new Normal(1, 1, 1), new Point(44, 44, 44));
        Plane plane2 = new Plane(new Normal(1, 0.5f, 0), new Point(0, 0, 0));

        Scene scene = new Scene()
        {
            LightSource = new Normal(5.0f, 55.0f, 5.0f),
            Camera = camera
        };
        scene.Figures.Add(sphere2);
        scene.Figures.Add(sphere1);

        RayTracer rayTracer = new RayTracer(scene);
        ConsoleRenderer.Render(rayTracer.TraceRays());

        Console.ReadLine();
        //var imageInfo = ArgumentsReader(args);
        //var imageReader = ReaderFactory.GetReader(imageInfo.Image);
        //var imageWriter = WriterFactory.GetWriter(imageInfo.GoalFormat);
        //imageWriter.Write(imageReader.Read(imageInfo.Image.OpenRead()), imageInfo.OutputDirectoryPath);
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