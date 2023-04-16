using FluentAssertions;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using NUnit.Framework;


namespace MainProject.Tests.ObjectsTests
{
    internal class PlaneTests
    {
        [Test]
        [Category("Negative")]
        public void GetIntersectionWith_ParallelRayTest()
        {
            var plane = new Plane(new Normal(1, 2, 0), new Point(0, 0, 0));

            var ray = new Ray
            {
                Origin = new Point(0, 0, 0),
                Direction = new Normal(-2, 1, 0)
            };

            plane.GetIntersectionWith(ray).Should().BeNull();
        }

        //[Test]
        //[Category("Negative")]
        //public void GetIntersectionWith_NoIntersectionTest()
        //{
        //    var plane = new Plane(new Vector(-1, -2, -3), new Point(0, 0, 0));


        //    var ray = new Ray
        //    {
        //        Origin = new Point(0, 0, 0),
        //        Direction = new Vector(5, 1, 0)
        //    };

        //    plane.GetIntersectionWith(ray).Should().BeNull();
        //}

        [Test]
        [Category("Positive")]
        public void GetIntersectionWith_IntersectionTest()
        {
            var plane = new Plane(new Normal(3, -1, 4), new Point(0, 0, 0));


            var ray = new Ray
            {
                Origin = new Point(0, 0, 0),
                Direction = new Normal(-2, 3, 1)
            };

            plane.GetIntersectionWith(ray).Should().NotBeNull();
        }


    }
}
