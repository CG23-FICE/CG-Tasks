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
            //var p1p2Side = Point.Distance(p1, p2);
            //var p2p3Side = Point.Distance(p2, p3);
            //var p3p1Side = Point.Distance(p3, p1);

            //if (p1p2Side + p3p1Side < p2p3Side
            //|| p1p2Side + p2p3Side < p3p1Side
            //|| p2p3Side + p3p1Side < p1p2Side)
            //{
            //    throw new ArgumentException("The triangle could not be created witha passed points.");
            //}

            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        //public Point? GetIntersectionWith(Ray ray)
        //{
        //    var triangleNormal = GetNormalAtPoint(P1);

        //    var rayDirectionDotNormal = Vector.Dot(ray.Direction, triangleNormal);
        //    if (rayDirectionDotNormal == 0)
        //    {
        //        // Ray and triangle are parallel, no intersection
        //        return null;
        //    }

        //    var d = Vector.Dot(triangleNormal, new Vector(P1, ray.Origin)) / rayDirectionDotNormal;
        //    if (d < 0)
        //    {
        //        // Intersection point is behind the ray origin
        //        return null;
        //    }

        //    var intersectionPoint = ray.GetPointAtDistance(d);

        //    // Calculate the barycentric coordinates of the intersection point
        //    var v0 = new Vector(P1, P2);
        //    var v1 = new Vector(P1, P3);
        //    var v2 = new Vector(P1, intersectionPoint);

        //    var dot00 = Vector.Dot(v0, v0);
        //    var dot01 = Vector.Dot(v0, v1);
        //    var dot02 = Vector.Dot(v0, v2);
        //    var dot11 = Vector.Dot(v1, v1);
        //    var dot12 = Vector.Dot(v1, v2);

        //    var inverseDenominator = 1.0 / (dot00 * dot11 - dot01 * dot01);
        //    var u = (dot11 * dot02 - dot01 * dot12) * inverseDenominator;
        //    var v = (dot00 * dot12 - dot01 * dot02) * inverseDenominator;

        //    if (u >= 0 && v >= 0 && u + v <= 1)
        //    {
        //        // Intersection point is inside the triangle
        //        return intersectionPoint;
        //    }

        //    // Intersection point is outside the triangle
        //    return null;
        //}
        //
        //const Vec3f &orig, const Vec3f &dir, const Vec3f &v0, const Vec3f &v1, const Vec3f &v2, float &t

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
            Vector P =  ray.GetPointAtDistance(t).ToVector();

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
            //var p1p2 = new Vector(P1, P2);
            //var p1p3 = new Vector(P1, P3);
            //var normal = Vector.Cross(p1p2, p1p3);
            return new Vector(0, 0, 1);//normal.Normalize();
        }
    }
}
