using MainProject.Interfaces;

namespace MainProject.Models.Basics
{
    public struct Point
    {
        public float X;
        public float Y;
        public float Z; 
        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point() 
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public static float Distance(Point point1, Point point2)
        {
            var a = Math.Pow(point2.X - point1.X, 2);
            var b = Math.Pow(point2.Y - point1.Y, 2);
            var c = Math.Pow(point2.Z - point1.Z, 2);
            return (float)Math.Sqrt(a + b + c);
        }

        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.X, point.Y - vector.Y, point.Z - vector.Z);
        }

        public Vector ToVector()
        {
            return new Vector(this.X, this.Y, this.Z);
        }
    }
}
