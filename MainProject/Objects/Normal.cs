using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Normal : IBaseObject
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Normal(Vector vector)
        {
            var norm = vector.Normalize();
            X = norm.X;
            Y = norm.Y;
            Z = norm.Z;
        }
    }
}