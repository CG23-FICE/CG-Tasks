using MainProject.Factories;
using MainProject.Models.ImagePluginsModels;
using MainProject.Models;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using MainProject.Objects;
using MainProject;
using MainProject.Readers;
using StbImageWriteSharp;
using System.IO;
//using System.Numerics;
//using Aspose.CAD;

internal class Program
{
    static string _workingDirectory = Environment.CurrentDirectory;
    static string _pathToKorova = Directory.GetParent(_workingDirectory)!
        .Parent!
        .Parent!
        .Parent!
        .GetDirectories()
        .First(x => x.Name == "ImageConverter.Sdk")
        .GetDirectories()
        .First(x => x.Name == "Images")
        .FullName;
    private static void Main(string[] args)
    {
        Transformator transform = new Transformator();
        transform.MoveX(-2);
        //transform.MoveY(1);

        Point center = new Point(0, 0, 0);
        Normal direction = new Normal(1, 0, 0);

        Camera camera = new Camera(center, direction, 30, 1, transform);

        Sphere sphere1 = new Sphere(new Point(5, -0.2f, 0.5f), 0.3f);
        Plane plane = new Plane(new Normal(0, 0, 1), new Point(0, 0, -0.5f));
        //Sphere sphere2 = new Sphere(new Point(3, 0, 0), 0.3f);
        //Plane plane1 = new Plane(new Normal(1, 1, 1), new Point(44, 44, 44));
        //Plane plane2 = new Plane(new Normal(1, 0.5f, 0), new Point(0, 0, 0));


        Scene scene = new Scene()
        {
            LightSource = new Point(-2.0f, 1.0f, 1.5f),
            Camera = camera
        };

        scene.Figures.Add(plane);

        Transformator transformObj = new Transformator();
        //transformObj.RotateAngleY(180);
        transformObj.RotateAngleZ(90);
        transformObj.RotateAngleX(90);

        ObjReader objReader = new ObjReader();
        List<Triangle> Triangles = objReader.Read(Path.Combine(_pathToKorova, "cow.obj"));
        List<Triangle> TransformedTriangles = Triangles.ToArray().Select(triangle => transformObj.ApplyTransformation(triangle)).ToList();
        foreach (var triangle in TransformedTriangles)
        {
            scene.Figures.Add(triangle);
        }

        //Triangle triangle = new Triangle(new Vector(0, 1, 0), new Vector(0, 0, 1), new Vector(0.5f, 0.5f, 0.5f));
        //scene.Figures.Add(triangle);

        RayTracer rayTracer = new RayTracer(scene);

        var pixels = rayTracer.TraceRays();

        int width = pixels.GetLength(1);
        int height = pixels.GetLength(0);

        byte[] binary = new byte[width * height * 3];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                byte greyscale = (byte)(pixels[y, x] * 255.0f);
                const int channels = 3;
                binary[y * width * channels + x * channels + 0] = greyscale;
                binary[y * width * channels + x * channels + 1] = greyscale;
                binary[y * width * channels + x * channels + 2] = greyscale;
            }
        }

        using FileStream stream = new FileStream(Path.Combine(_pathToKorova, "NewCowShadow.png"), FileMode.Create);
        var imageWriter = new ImageWriter();
        //imageWriter.WriteJpg(binary, width, height, ColorComponents.RedGreenBlue, stream, 24);
        imageWriter.WritePng(binary, width, height, ColorComponents.RedGreenBlue, stream);
        stream.Flush();
        Console.WriteLine("Finish");
        //ConsoleRenderer.Render(pixels);


        Console.ReadLine();
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