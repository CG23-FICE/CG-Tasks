#define Light
#define MT

using MainProject.Interfaces;
using MainProject.Models.Basics;
using System.Data;

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
#if MT
            Parallel.For(0, projectionPlane.GetLength(0), i =>
            {
                for (int j = 0; j < projectionPlane.GetLength(1); j++)
                {
                    var currentRay = new Ray(Scene.Camera.Position, projectionPlane[i, j]);

                    var nearestIntersection = GetNearestIntersection(currentRay, figures);
#if Light
                    if (nearestIntersection.point is not null)
                    {
                        var normal = nearestIntersection.figure!.GetNormalAtPoint((Point)nearestIntersection.point);

                        pixels[i, j] = Vector.Dot(normal, Scene.LightSource.ToVector());
                    }
#else

                        pixels[i, j] = nearestIntersection.point is not null ? 1.0f : 0.0f;
                    
#endif
                }
            }); // Parallel.For
#else

            for (int i = 0; i < projectionPlane.GetLength(0); i++)
            {
                for (int j = 0; j < projectionPlane.GetLength(1); j++)
                {
                    var currentRay = new Ray(Scene.Camera.Position, projectionPlane[i, j]);

                    var nearestIntersection = GetNearestIntersection(currentRay, figures);
#if Light
                    if (nearestIntersection.point is not null)
                    {
                        var normal = nearestIntersection.figure!.GetNormalAtPoint((Point)nearestIntersection.point);

                        pixels[i, j] = Vector.Dot(normal, Scene.LightSource.ToVector());
                    }
#else

                        pixels[i, j] = nearestIntersection.point is not null ? 1.0f : 0.0f;
                    
#endif

                }
            }
#endif
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

                var disatanceToPoint = Point.Distance(Scene.Camera.Position, (Point)intersectionPoint);

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
