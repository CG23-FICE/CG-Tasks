using MainProject.Interfaces;
using MainProject.Models.Basics;

namespace MainProject.Models.Shapes
{
    public class Triangle : IIntersectable
    {
        public Point P1 { get; }
        public Point P2 { get; }
        public Point P3 { get; }
        public Triangle() { }
        public Triangle(Point p1, Point p2, Point p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public Point? GetIntersectionWith(Ray ray)
        {
            throw new NotImplementedException();
        }

        public Vector GetNormalAtPoint(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
