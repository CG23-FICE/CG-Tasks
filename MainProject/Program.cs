using MainProject.Factories;
using MainProject.Models.ImagePluginsModels;
using MainProject.Models;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using MainProject.Objects;
using MainProject;
using MainProject.Readers;
//using System.Numerics;
//using Aspose.CAD;

internal class Program
{
    private static void Main(string[] args)
    {
        Transformator transform = new Transformator();
        transform.MoveY(-2);
        //transform.MoveY(1);

        Point center = new Point(0, 0, 0);
        Normal direction = new Normal(0, 1, 0);

        Camera camera = new Camera(center, direction, 30, 1, transform);

        //Sphere sphere1 = new Sphere(new Point(5, -0.2f, 0.5f), 0.3f);
        //Sphere sphere2 = new Sphere(new Point(3, 0, 0), 0.3f);
        //Plane plane1 = new Plane(new Normal(1, 1, 1), new Point(44, 44, 44));
        //Plane plane2 = new Plane(new Normal(1, 0.5f, 0), new Point(0, 0, 0));


        Scene scene = new Scene()
        {
            LightSource = new Normal(5.0f, 55.0f, 5.0f),
            Camera = camera
        };

        Transformator transformObj = new Transformator();
        transformObj.RotateAngleY(90);
        transformObj.RotateAngleZ(90);
        transformObj.RotateAngleX(90);

        ObjReader objReader = new ObjReader();
        List<Triangle> Triangles = objReader.Read("D://Studying//6_семестр//CG//MishaIsCringe//ImageConverter.Sdk//Images/cow.obj");
        List<Triangle> TransformedTriangles = Triangles.ToArray().Select(triangle => transformObj.ApplyTransformation(triangle)).ToList();
        foreach (var triangle in TransformedTriangles)
        {
            scene.Figures.Add(triangle);
        }

        //Triangle triangle = new Triangle(new Vector(0, 1, 0), new Vector(0, 0, 1), new Vector(0.5f, 0.5f, 0.5f));
        //scene.Figures.Add(triangle);


        //scene.Figures.Add(sphere1);
        //scene.Figures.Add(sphere2);
        //scene.Figures.Add(plane1);

        RayTracer rayTracer = new RayTracer(scene);
        ConsoleRenderer.Render(rayTracer.TraceRays());

        Console.ReadLine();
        //// load OBJ in an instance of Image via its Load method
        //using (var image = Image.Load("cow.obj"))
        //{
        //    // create an instance of CadRasterizationOptions and set page height & width
        //    var rasterizationOptions = new Aspose.CAD.ImageOptions.CadRasterizationOptions()
        //    {
        //        PageWidth = 1600,
        //        PageHeight = 1600
        //    };

        //    // create an instance of PngOptions
        //    var options = new Aspose.CAD.ImageOptions.PngOptions();

        //    // set the VectorRasterizationOptions property as CadRasterizationOptions
        //    options.VectorRasterizationOptions = rasterizationOptions;

        //    // export OBJ to PNG
        //    image.Save("output.png", options);
        //}
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