﻿using MainProject.Interfaces;
using MainProject.Models.Basics;

namespace MainProject.Models.Shapes
{
    public class Plane : IIntersectable
    {
        public Normal Normal { get; set; }
        public Point Point { get; set; }

        public Plane(Normal normal, Point point)
        {
            Normal = normal;
            Point = point;
        }

        public Point? GetIntersectionWith(Ray ray)
        {
            //throw new NotImplementedException();
            var vectorProduct = Vector.Dot(Normal.ToVector(), ray.Direction.ToVector());
            if (Math.Abs(vectorProduct) < 1e-6)
            {
                return null;
            }

            Normal difference = new Normal(ray.Origin, Point);
            var distance = Vector.Dot(difference.ToVector(), Normal.ToVector()) / vectorProduct;
            if (distance < 0)
                return null;

            return ray.Origin + ray.Direction.ToVector().Scale(distance);

            /*var vectorProduct = Vector.Dot(Normal, ray.Direction);
            if (Math.Abs(vectorProduct) >= 0) //90 degrees or less
            {
                return null;
            }
            */


            /*var arccos = Math.Acos(vectorProduct / (Normal.Module() * ray.Direction.Module()));

            return arccos > Math.PI / 2 && arccos < Math.PI; //between 90 and 180 degrees*/
        }

        public Vector GetNormalAtPoint(Point point)
        {
            return Normal.ToVector();
        }
    }
}
