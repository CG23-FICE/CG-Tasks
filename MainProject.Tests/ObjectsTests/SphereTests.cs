using FluentAssertions;
using MainProject.Objects;
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
                Direction = new Vector(2, 1, 4)
            };

            sphere.GetIntersectionWith(ray).Should().BeFalse();
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
                Direction = new Vector(5, 1, 0)
            };

            sphere.GetIntersectionWith(ray).Should().BeTrue();
        }
    }
}
