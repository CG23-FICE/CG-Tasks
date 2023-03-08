// See https://aka.ms/new-console-template for more information
using MainProject.Objects;
using MainProject;

//------Testing-------

Point center = new Point(0, 0, 0);
Vector direction = new Vector(1, 0, 0);

Camera camera = new Camera(center, direction, 80, 2);

Sphere sphere1 = new Sphere(new Point(5, 0, 0), 0.5f);
Sphere sphere2 = new Sphere(new Point(5, 1, 1), 0.3f);
Plane plane1 = new Plane(new Vector(0, 1, 0).Normalize(), new Point(0, 1, 0));
Plane plane2 = new Plane(new Vector(1, 0.5f, 0).Normalize(), new Point(0, 0, 0));

Scene scene = new Scene()
{
    LightSource = new Vector(5.0f, 55.0f, 5.0f),
    Camera = camera
};
//scene.Figures.Add(sphere1);
//scene.Figures.Add(sphere2);
scene.Figures.Add(plane2);

RayTracer rayTracer = new RayTracer(scene);
ConsoleRenderer.Render(rayTracer.TraceRays());

Console.ReadLine();



//Point[,] Array = camera.GetImaginaryScreen();

//for (int i = 0; i < Array.GetLength(0); i++)
//{
//    for (int j = 0; j < Array.GetLength(1); j++)
//    {
//        Console.Write("{0} {1} {2}", Array[i, j].X, Array[i, j].Y, Array[i, j].Z + "\t");
//    }
//    Console.WriteLine();
//}

//Console.ReadLine();
