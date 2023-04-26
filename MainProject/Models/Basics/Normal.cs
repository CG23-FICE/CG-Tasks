namespace MainProject.Models.Basics
{
    public struct Normal
    {
        public float X;
        public float Y;
        public float Z;

        public Normal(Vector vector)
        {
            var norm = vector.Normalize();
            X = norm.X;
            Y = norm.Y;
            Z = norm.Z;
        }

        public Normal(float x, float y, float z)
        {
            var vector = new Vector(x, y, z).Normalize();
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }
        public Normal(Point start, Point end)
        {
            var vector = new Vector(start, end).Normalize();
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }

        public Vector ToVector()
        {
            return new Vector(this.X, this.Y, this.Z);
        }
    }
}