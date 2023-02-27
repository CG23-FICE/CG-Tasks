namespace MainProject.Interfaces
{
    public interface IVector
    {
        float X { get; set; }
        float Y { get; set; }
        float Z { get; set; }
        IVector Add(IVector vector);
        IVector Subtract(IVector vector);
    }
}
