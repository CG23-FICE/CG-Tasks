using FluentAssertions;
using MainProject.Models.Basics;
using MainProject.Models.Shapes;
using NUnit.Framework;

namespace MainProject.Tests.ObjectsTests
{
    internal class SphereTests
    {
        [Test]
        [Category("Negative")]
        public void GetIntersectionWith_NoIntersectionTest()
        {
            var sphere = new Sphere()
            {
                Center = new Point(3, 3, 1),
                Radius = 3
            };

            var ray = new Ray
            {
                Origin = new Point(0, 0, 0),
                Direction = new Normal(2, 1, 4)
            };

            sphere.GetIntersectionWith(ray).Should().BeNull();
        }

        [Test]
        [Category("Positive")]
        public void GetIntersectionWith_IntersectionTest()
        {
            var sphere = new Sphere()
            {
                Center = new Point(3, 3, 1),
                Radius = 3
            };

            var ray = new Ray
            {
                Origin = new Point(0, 0, 0),
                Direction = new Normal(5, 1, 0)
            };

            sphere.GetIntersectionWith(ray).Should().NotBeNull();
        }
    }
}
