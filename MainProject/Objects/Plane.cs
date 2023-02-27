using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Plane : IIntersectable
    {
        public Vector Normal { get; set; }
        public Point Point { get; set; }
        public bool GetIntersectionWith(Ray ray)
        {
            var vectorProduct = Vector.Dot(Normal, ray.Direction);
            if (Math.Abs(vectorProduct) < 1e-6)
            {
                return false;
            }
            var intersection = Vector.Dot(Point - ray.Origin, Normal) / vectorProduct;
            return intersection >= 0;
        }
    }
}
