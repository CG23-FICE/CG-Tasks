using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MainProject.Objects;
using NUnit.Framework;



namespace MainProject.Tests.ObjectsTests
{
    internal class RayTracerTests
    {
        [Test]
        [Category("Positive")]
        public void FindClosestIntersection_Intersection_ReturnsTrue()
        {
            Point center = new Point(0, 0, 0);
            Vector direction = new Vector(1, 0, 0);
            Camera camera = new Camera(center, direction, 30, 2);
            Scene scene = new Scene()
            {
                LightSource = new Vector(5.0f, 55.0f, 5.0f),
                Camera = camera
            };
            Sphere sphere1 = new Sphere(new Point(5, 0, 0), 4f);
            scene.Figures.Add(sphere1);



            RayTracer rayTracer = new RayTracer(scene);
            rayTracer.TraceRaysNearestFigure().Should().BeOfType<float[,]>(); 
        }

        [Test]
        [Category("Positive")]
        public void FindClosestIntersection_Intersection_TraceCorrectFigure()
        {
            Point center = new Point(0, 0, 0);
            Vector direction = new Vector(1, 0, 0);
            Camera camera = new Camera(center, direction, 30, 2);
            Scene scene1 = new Scene()
            {
                LightSource = new Vector(5.0f, 55.0f, 5.0f),
                Camera = camera
            };
            Scene scene2 = new Scene()
            {
                LightSource = new Vector(5.0f, 55.0f, 5.0f),
                Camera = camera
            };
            Sphere sphere1 = new Sphere(new Point(5, 0, 0), 4f);
            Sphere sphere2 = new Sphere(new Point(5, 0, 0), 3f);
            scene1.Figures.Add(sphere1);
            scene1.Figures.Add(sphere2);
            scene2.Figures.Add(sphere1);



            RayTracer rayTracer1 = new RayTracer(scene1);
            RayTracer rayTracer2 = new RayTracer(scene2);
            rayTracer1.TraceRaysNearestFigure().Should().BeEquivalentTo(rayTracer2.TraceRays());
        }

        [Test]
        [Category("Negative")]
        public void FindClosestIntersection_Intersection_TraceNotCorrectFigure()
        {
            Point center = new Point(0, 0, 0);
            Vector direction = new Vector(1, 0, 0);
            Camera camera = new Camera(center, direction, 30, 2);
            Scene scene1 = new Scene()
            {
                LightSource = new Vector(5.0f, 55.0f, 5.0f),
                Camera = camera
            };
            Scene scene2 = new Scene()
            {
                LightSource = new Vector(5.0f, 55.0f, 5.0f),
                Camera = camera
            };
            Sphere sphere1 = new Sphere(new Point(5, 0, 0), 4f);
            Sphere sphere2 = new Sphere(new Point(5, 0, 0), 3f);
            scene1.Figures.Add(sphere1);
            scene1.Figures.Add(sphere2);
            scene2.Figures.Add(sphere2);



            RayTracer rayTracer1 = new RayTracer(scene1);
            RayTracer rayTracer2 = new RayTracer(scene2);
            rayTracer1.TraceRaysNearestFigure().Should().NotBeEquivalentTo(rayTracer2.TraceRays());
        }
    }
}
