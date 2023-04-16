using MainProject.Models.Basics;

namespace MainProject.Interfaces
{
    public interface IIntersectable
    {
        Point? GetIntersectionWith(Ray ray);
        Vector GetNormalAtPoint(Point point);
    }
}
