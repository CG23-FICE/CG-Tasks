using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Vector : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector() { }
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


        public static Vector Cross(IVector left, IVector right)
        {
            var i = Math.Abs(left.Y * right.Z - left.Z * right.Y);
            var j = Math.Abs(left.X * right.Z - left.Z * right.X);
            var k = Math.Abs(left.X * right.Y - left.Y * right.X);

            return new Vector(i, -j, k);
        }
        public static float Dot(IVector left, IVector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public IVector Add(IVector vector)
        {
            return new Vector(this.X + vector.X, this.Y + vector.Y, this.Z + vector.Z);
        }

        public IVector Subtract(IVector vector)
        {
            return new Vector(this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z);
        }
        public static IVector operator +(Vector a, Vector b)
        {
            return a.Add(b);
        }

        public static IVector operator -(Vector a, Vector b)
        {
            return a.Subtract(b);
        }
    }
}
