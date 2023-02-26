namespace MainProject.Interfaces
{
    public interface IVector
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
        IVector Add(IVector vector);
        IVector Subtract(IVector vector);
        static float Dot(IVector left, IVector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }
    }
}
