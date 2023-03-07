﻿using MainProject.Interfaces;

namespace MainProject.Objects
{
    public class Plane : IIntersectable
    {
        public Vector Normal { get; set; }
        public Point Point { get; set; }
        public bool GetIntersectionWith(Ray ray, out Point PointOfIntersection)
        {
            PointOfIntersection = new Point(0, 0, 0);
            var vectorProduct = Vector.Dot(Normal, ray.Direction);
            if (Math.Abs(vectorProduct) < 1e-6)
            {
                return false;
            }

            var arccos = Math.Acos(vectorProduct / (Normal.Module() * ray.Direction.Module()));
        public Point? GetIntersectionWith(Ray ray)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
