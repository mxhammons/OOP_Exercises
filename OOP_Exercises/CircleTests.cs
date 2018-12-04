using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exersizes
{
    [TestFixture]
    public class CircleTests
    {
        Type CircleClass;
        dynamic GetCircle()
        {
            return Activator.CreateInstance(CircleClass);
        }
        [OneTimeSetUp]
        public void FindCircle()
        {
            CircleClass = System.Reflection.Assembly.GetExecutingAssembly().GetModule("OOP_Exercises.dll").GetTypes().SingleOrDefault(t => t.Name == "Circle");

        }
        [Test]
        public void CircleClassExists()
        {
            Assert.IsNotNull(CircleClass);
        }
        [Test]
        public void HasDefaultConstructor()
        {
            Assert.IsNotNull(CircleClass.GetConstructors().SingleOrDefault(c => !c.GetParameters().Any()));
        }
        [Test]
        public void ColorPropertyExistsWithADefaultOfRed()
        {
            Assert.AreEqual("Red", GetCircle().Color, "Color Default value was incorrect.");
        }
        [Test]
        public void RadiusPropertyExistsWithDefaultof1()
        {
            Assert.AreEqual(1, GetCircle().Radius, "Radius Default value was incorrect.");
        }
        [Test]
        public void RadiusCanBeChanged()
        {
            var circ = GetCircle();
            circ.Radius = 10;
            Assert.AreEqual(10, circ.Radius);
        }
        [Test]
        public void SettingNegativeRadiusThrowsException()
        {
            Assert.Throws<Exception>(() => GetCircle().Radius = -1);
        }
        [Test]
        public void AreaPropertyExistsAndIsCorrect()
        {
            var circ = GetCircle();
            circ.Radius = 5;
            Assert.AreEqual(Math.Pow((Math.PI * 5), 2), circ.Area, "Expected an area of (PI*Radius)^2");
            circ.Radius = 50;
            Assert.AreEqual(Math.Pow((Math.PI * 50), 2), circ.Area, "Expected an area of (PI*Radius)^2");
        }
        [Test]
        public void AreaPropertyIsReadOnly()
        {
            Assert.IsFalse(CircleClass.GetProperty("Area").CanWrite);
        }
        [Test]
        public void HasConstructorToSetRadius()
        {
            dynamic circ = Activator.CreateInstance(CircleClass, 5);
            Assert.AreEqual(5, circ.Radius, "Circle's Radius should be set by the constructor");
        }
        [Test]
        public void ConstructorWithRadiusHasDefaultColor()
        {
            dynamic circ = Activator.CreateInstance(CircleClass, 5);
            Assert.AreEqual("Red", circ.Color, "Circle's Color was not the default");
        }
        [Test]
        public void ConstructorDoesNotAcceptNegativeRadius()
        {
            Assert.Throws<System.Reflection.TargetInvocationException>(() => Activator.CreateInstance(CircleClass, -5));
        }
        [Test]
        public void HasConstructorToSetRadiusAndColor_RadiusIsSet()
        {
            dynamic circ = Activator.CreateInstance(CircleClass, 5, "Red");
            Assert.AreEqual(5, circ.Radius, "Circle's Radius should be set by the constructor");
        }
        [Test]
        public void HasConstructorToSetRadiusAndColor_ColorIsSet()
        {
            dynamic circ = Activator.CreateInstance(CircleClass, 5, "Blue");
            Assert.AreEqual("Blue", circ.Color, "Circle's Color should be set by the constructor");
        }


    }
}
