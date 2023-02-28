using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Point : IBaseObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }
    }
}
