using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Ray : IBaseObject
    {
        public Point Origin { get; set; }
        public Vector Direction { get; set; }

        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Ray(Point origin, Point direction)
        {
            Origin = origin;
            Direction = new Vector(origin, direction).Normalize();
        }

        public Ray() { }
    }
}
