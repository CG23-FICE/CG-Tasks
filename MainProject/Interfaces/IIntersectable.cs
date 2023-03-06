using MainProject.Objects;

namespace MainProject.Interfaces
{
    public interface IIntersectable
    {
        Point? GetIntersectionWith(Ray ray);
        Vector GetNormalAtPoint(Point point);
    }
}
