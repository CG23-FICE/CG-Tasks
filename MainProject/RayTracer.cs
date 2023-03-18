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
                    var currentRay = new Ray(Scene.Camera.Position, projectionPlane[i, j]);

                    var nearestIntersection = GetNearestIntersection(currentRay, figures);

                    if (nearestIntersection.point is not null)
                    {
                        var normal = nearestIntersection.figure!.GetNormalAtPoint(nearestIntersection.point);

                        pixels[i, j] = Vector.Dot(normal, Scene.LightSource);
                    }
                }
            }
            return pixels;
        }

        public (IIntersectable? figure, Point? point) GetNearestIntersection(Ray ray, List<IIntersectable> figures)
        {
            IIntersectable? nearestFigure = null;
            Point? nearestPoint = default;
            float nearestDistance = float.MaxValue;
            foreach (var figure in figures)
            {
                var intersectionPoint = figure.GetIntersectionWith(ray);

                if (intersectionPoint is null) { continue; }

                var disatanceToPoint = Point.Distance(Scene.Camera.Position, intersectionPoint);

                if (disatanceToPoint < nearestDistance)
                {
                    nearestDistance = disatanceToPoint;
                    nearestPoint = intersectionPoint;
                    nearestFigure = figure;
                }
            }
            return (nearestFigure, nearestPoint);
        }
    }
}
