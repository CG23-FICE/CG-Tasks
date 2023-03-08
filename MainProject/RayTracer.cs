using MainProject.Objects;


namespace MainProject
{
    public class RayTracer
    {
        public Scene Scene { get; set; }

        public RayTracer(Scene scene) { Scene = scene; }

        public float[,] TraceRays()
        {
            Point[,] projectionPlane = Scene.Camera.GetImaginaryScreen();
            float[,] pixels = new float[projectionPlane.GetLength(0), projectionPlane.GetLength(1)];

            //var interSectionPoint = FindNearestIntersection(Scene.Camera.Position, out IIntersectable? intersectionFigure);

            for (int i = 0; i < projectionPlane.GetLength(0); i++)
            {
                for (int j = 0; j < projectionPlane.GetLength(1); j++)
                {
                    foreach (var figure in Scene.Figures)
                    {
                        var currentRay = new Ray(Scene.Camera.Position, projectionPlane[i, j]);

                        var intersectionPoint = figure.GetIntersectionWith(currentRay);

                        if (intersectionPoint is not null)
                        {
                            var normal = figure.GetNormalAtPoint(intersectionPoint);

                            pixels[i, j] = Vector.Dot(normal, Scene.LightSource);
                        }
                    }

                    /*if (interSectionPoint is not null)
                    {
                        intersectionFigure.GetNormalAtPoint();
                    }*/
                }
            }
            return pixels;
        }

        /*private Point? FindNearestIntersection(Point ray, out IIntersectable? intersectionFigure)
        {
            intersectionFigure = default;
            double minDistance = double.MaxValue;
            Point? intersection = null;
            foreach (var figure in Scene.Figures)
            {
                var intersectionPoint = figure.GetIntersectionWith(ray);
                if (intersectionPoint is not null)
                {
                    double currDistance = new Vector(ray.Origin, intersectionPoint).Module();
                    if (currDistance < minDistance)
                    {
                        intersection = intersectionPoint;
                        intersectionFigure = figure;
                        minDistance = currDistance;
                    }
                }
            }
            return intersection;
        }*/
    }
}
