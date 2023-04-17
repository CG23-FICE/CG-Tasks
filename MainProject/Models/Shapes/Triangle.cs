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
            var p1p2Side = Point.Distance(p1, p2);
            var p2p3Side = Point.Distance(p2, p3);
            var p3p1Side = Point.Distance(p3, p1);

            if (p1p2Side + p3p1Side < p2p3Side
            || p1p2Side + p2p3Side < p3p1Side
            || p2p3Side + p3p1Side < p1p2Side)
            {
                throw new ArgumentException("The triangle could not be created witha passed points.");
            }

            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public Point? GetIntersectionWith(Ray ray)
        {
            var triangleNormal = GetNormalAtPoint(P1);

            var rayDirectionDotNormal = Vector.Dot(ray.Direction, triangleNormal);
            if (rayDirectionDotNormal == 0)
            {
                // Ray and triangle are parallel, no intersection
                return null;
            }

            var d = Vector.Dot(triangleNormal, new Vector(P1, ray.Origin)) / rayDirectionDotNormal;
            if (d < 0)
            {
                // Intersection point is behind the ray origin
                return null;
            }

            var intersectionPoint = ray.GetPointAtDistance(d);

            // Calculate the barycentric coordinates of the intersection point
            var v0 = new Vector(P1, P2);
            var v1 = new Vector(P1, P3);
            var v2 = new Vector(P1, intersectionPoint);

            var dot00 = Vector.Dot(v0, v0);
            var dot01 = Vector.Dot(v0, v1);
            var dot02 = Vector.Dot(v0, v2);
            var dot11 = Vector.Dot(v1, v1);
            var dot12 = Vector.Dot(v1, v2);

            var inverseDenominator = 1.0 / (dot00 * dot11 - dot01 * dot01);
            var u = (dot11 * dot02 - dot01 * dot12) * inverseDenominator;
            var v = (dot00 * dot12 - dot01 * dot02) * inverseDenominator;

            if (u >= 0 && v >= 0 && u + v <= 1)
            {
                // Intersection point is inside the triangle
                return intersectionPoint;
            }

            // Intersection point is outside the triangle
            return null;
        }


        public Vector GetNormalAtPoint(Point point)
        {
            var p1p2 = new Vector(P1, P2);
            var p1p3 = new Vector(P1, P3);
            var normal = Vector.Cross(p1p2, p1p3);
            return normal.Normalize();
        }
    }
}
