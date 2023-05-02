//using Aspose.CAD;
using MainProject.Interfaces;
using MainProject.Models.Basics;

namespace MainProject.Models.Shapes
{
    public class Triangle : IIntersectable
    {
        public Vector P1 { get; }
        public Vector P2 { get; }
        public Vector P3 { get; }
        public Triangle() { }
        public Triangle(Vector p1, Vector p2, Vector p3)
        {
            var p1p2Side = Point.Distance(p1.ToPoint(), p2.ToPoint());
            var p2p3Side = Point.Distance(p2.ToPoint(), p3.ToPoint());
            var p3p1Side = Point.Distance(p3.ToPoint(), p1.ToPoint());

            if (p1p2Side + p3p1Side <= p2p3Side
            || p1p2Side + p2p3Side <= p3p1Side
            || p2p3Side + p3p1Side <= p1p2Side)
            {
                throw new ArgumentException("The triangle could not be created witha passed points.");
            }

            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public Point? GetIntersectionWith(Ray ray)
        {
            Vector P1P2 = P2 - P1;
            Vector P1P3 = P3 - P1;
            Vector N = Vector.Cross(P1P2, P1P3);

            float NRayDirection = Vector.Dot(N, ray.Direction.ToVector());
            if (Math.Abs(NRayDirection) < Single.Epsilon) // almost 0
                return null;

            // compute d parameter using equation 2
            float d = -Vector.Dot(N, P1);

            float t = -(Vector.Dot(N, ray.Origin.ToVector()) + d) / NRayDirection;

            // check if the triangle is behind the ray
            if (t < 0)
                return null; // the triangle is behind

            // compute the intersection point using equation 1
            Vector P = ray.GetPointAtDistance(t).ToVector();

            // Step 2: inside-outside test
            Vector C; // vector perpendicular to triangle's plane

            // edge 0
            Vector edge0 = P2 - P1;
            Vector vp0 = P - P1;
            C = Vector.Cross(edge0, vp0);
            if (Vector.Dot(N, C) < 0)
                return null; // P is on the right side

            // edge 1
            Vector edge1 = P3 - P2;
            Vector vp1 = P - P2;
            C = Vector.Cross(edge1, vp1);
            if (Vector.Dot(N, C) < 0)
                return null; // P is on the right side

            // edge 2
            Vector edge2 = P1 - P3;
            Vector vp2 = P - P3;
            C = Vector.Cross(edge2, vp2);
            if (Vector.Dot(N, C) < 0)
                return null; // P is on the right side;
            return new Point(P.X, P.Y, P.Z);
        }


        public Vector GetNormalAtPoint(Point point)
        {
            var p1p2 = new Vector(P1.ToPoint(), P2.ToPoint());
            var p1p3 = new Vector(P1.ToPoint(), P3.ToPoint());
            var normal = Vector.Cross(p1p2, p1p3);
            return normal.Normalize();
        }
    }
}
