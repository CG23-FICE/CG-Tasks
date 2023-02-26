using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Vector : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        static IVector Cross(IVector left, IVector right)
        {
            var i = Math.Abs(left.Y * right.Z - left.Z * right.Y);
            var j = Math.Abs(left.X * right.Z - left.Z * right.X);
            var k = Math.Abs(left.X * right.Y - left.Y * right.X);

            return new Vector(i, -j, k);
        }

        public IVector Add(IVector vector)
        {
            return new Vector(this.X + vector.X, this.Y + vector.Y, this.Z + vector.Z);
        }

        public IVector Subtract(IVector vector)
        {
            return new Vector(this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z);
        }
    }
}
