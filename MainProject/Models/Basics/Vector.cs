namespace MainProject.Models.Basics
{
    public struct Vector
    {
        public float X;
        public float Y;
        public float Z;
        public Vector()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(Point start, Point end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
            Z = end.Z - start.Z;
        }

        public float Module()
        {
            return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public Vector Normalize()
        {
            var module = Module();
            return new Vector(X / module, Y / module, Z / module);
        }

        public Vector Scale(float ratio)
        {
            return new Vector(X * ratio, Y * ratio, Z * ratio);
        }

        public static Vector Cross(Vector left, Vector right)
        {
            var i = left.Y * right.Z - left.Z * right.Y;
            var j = left.X * right.Z - left.Z * right.X;
            var k = left.X * right.Y - left.Y * right.X;

            return new Vector(i, -j, k);
        }
        public static float Dot(Vector left, Vector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public Vector Add(Vector vector)
        {
            return new Vector(X + vector.X, Y + vector.Y, Z + vector.Z);
        }
        public Vector Subtract(Vector vector)
        {
            return new Vector(X - vector.X, Y - vector.Y, Z - vector.Z);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return a.Add(b);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return a.Subtract(b);
        }

        public Point ToPoint()
        {
            return new Point(this.X, this.Y, this.Z);
        }
    }
}
