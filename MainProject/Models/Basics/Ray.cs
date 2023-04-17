using MainProject.Interfaces;

namespace MainProject.Models.Basics
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
        public Point GetPointAtDistance(float distance)
        {
            return new Point(
                Origin.X + Direction.X * distance,
                Origin.Y + Direction.Y * distance,
                Origin.Z + Direction.Z * distance);
        }
    }
}
