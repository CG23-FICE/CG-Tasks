using MainProject.Interfaces;
using MainProject.Objects;


namespace MainProject
{
    public class RayTracer
    {
        public Scene Scene { get; set; }

        public RayTracer(Scene scene) { Scene = scene; }

        public float[,] TraceRays(List<IIntersectable>? figures = null)
        {
            if (figures == null)
                figures = Scene.Figures;

            Point[,] projectionPlane = Scene.Camera.GetImaginaryScreen();
            float[,] pixels = new float[projectionPlane.GetLength(0), projectionPlane.GetLength(1)];

            for (int i = 0; i < projectionPlane.GetLength(0); i++)
            {
                for (int j = 0; j < projectionPlane.GetLength(1); j++)
                {
                    foreach (var figure in figures)
                    {
                        var currentRay = new Ray(Scene.Camera.Position, projectionPlane[i, j]);

                        var intersectionPoint = figure.GetIntersectionWith(currentRay);

                        if (intersectionPoint is not null)
                        {
                            var normal = figure.GetNormalAtPoint(intersectionPoint);

                            pixels[i, j] = Vector.Dot(normal, Scene.LightSource);
                        }
                    }
                }
            }
            return pixels;
        }
    }
}
