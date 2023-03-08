using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Plane : IIntersectable
    {
        public Vector Normal { get; set; }
        public Point Point { get; set; }

        public Plane(Vector normal, Point point)
        {
            Normal = normal; 
            Point = point;
        }

        public Point? GetIntersectionWith(Ray ray)
        {
            //throw new NotImplementedException();
            var vectorProduct = Vector.Dot(Normal.Normalize(), ray.Direction.Normalize());
            if (Math.Abs(vectorProduct) < 0) 
            {
                return null;
            }

            Vector difference = new Vector(ray.Origin, Point);
            var distance = Vector.Dot(difference, Normal.Normalize()) / vectorProduct;

            return ray.Origin + ray.Direction.Normalize().Scale(distance);

            /*var vectorProduct = Vector.Dot(Normal, ray.Direction);
            if (Math.Abs(vectorProduct) >= 0) //90 degrees or less
            {
                return null;
            }
            */


            /*var arccos = Math.Acos(vectorProduct / (Normal.Module() * ray.Direction.Module()));

            return arccos > Math.PI / 2 && arccos < Math.PI; //between 90 and 180 degrees*/
        }

            return arccos > Math.PI / 2 && arccos < Math.PI; //between 90 and 180 degrees
        }
        public Vector GetNormalAtPoint(Point point)
        {
            return Normal;
        }
    }
}
