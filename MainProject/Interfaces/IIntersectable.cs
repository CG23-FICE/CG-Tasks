using MainProject.Objects;

namespace MainProject.Interfaces
{
    public interface IIntersectable
    {
        bool GetIntersectionWith(Ray ray, out Point PointOfIntersection);
    }
}
