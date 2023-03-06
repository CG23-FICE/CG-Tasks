using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Sphere : IBaseObject, IIntersectable
    {
        public Point Center { get; set; }
        public float Radius { get; set; }

        public Sphere(Point center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public Sphere() { }

        public Point? GetIntersectionWith(Ray ray)
        {
            var RayDirect2 = Vector.Dot(ray.Direction, ray.Direction); //d2
            var Radius2 = Radius * Radius; //r2
            var K2 = Vector.Dot(new Vector(ray.Origin, Center), new Vector(ray.Origin, Center)); //(o-c)2

            var a = RayDirect2;
            var b = 2 * Vector.Dot(ray.Direction, new Vector(ray.Origin, Center)); // 2*d*(o-c) 
            var c = K2 - Radius2; //k2-r2

            var D = b * b - 4 * a * c;

            if (D < 0)
            {
                return null;
            }

            var distance1 = (-b - Math.Sqrt(D)) / 2;
            var distance2 = (-b + Math.Sqrt(D)) / 2;
            var distance = (float)Math.Min(distance1, distance2);

            if (distance < 0)
            {
                return null;
            }

            return ray.Origin + ray.Direction.Normalize().Scale(distance);
        }

        public Vector GetNormalAtPoint(Point point)
        {
            return new Vector(Center, point).Normalize();
        }
    }
}
