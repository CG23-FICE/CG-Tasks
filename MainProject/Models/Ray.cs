using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Ray : IBaseObject
    {
        public Point Origin { get; set; }
        public Normal Direction { get; set; }

        public Ray(Point origin, Normal direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Ray(Point origin, Point direction)
        {
            Origin = origin;
            Direction = new Normal(origin, direction);
        }

        public Ray() { }
    }
}
