using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exercises
{
    [TestFixture]
    public class BagOfFunctionsTests
    {
        Type BagOfFunctionsType;
        [OneTimeSetUp]
        public void FindBagOfFunctions()
        {
            BagOfFunctionsType = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                .SingleOrDefault(t => t.Name == "BagOfFunctions"
                    && t.IsPublic && t.IsAbstract && t.IsSealed);
        }
        [Test]
        public void BagOfFunctionsClassExists()
        {
            Assert.IsNotNull(BagOfFunctionsType, "Static class BagOfFunctions does not exist");
        }
        [Test]
        public void HasIdentityFunction()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("Identity"));
        }
        [Test]
        public void PassingIdentityAValueReturnsTheValue()
        {
            var idenity = BagOfFunctionsType.GetMethod("Identity");
            var result = idenity.Invoke(null, new object[] { 9 });
            Assert.AreEqual(9, result);
        }
        [Test]
        public void HasFirstFunction()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("First"));
        }
        [Test]
        public void FirstReturnsFirstItemOfArray()
        {
            var first = BagOfFunctionsType.GetMethod("First");
            var result = first.Invoke(null, new object[] { new object[] { 9, 10, 11 } });
            Assert.AreEqual(9, result);
        }
        [Test]
        public void FirstOfAnEmptyArrayReturnsNull()
        {
            var first = BagOfFunctionsType.GetMethod("First");
            var result = first.Invoke(null, new object[] { new object[] { } });
            Assert.AreEqual(null, result);
        }
        [Test]
        public void FirstLeavesInputArrayIntact()
        {
            var first = BagOfFunctionsType.GetMethod("First");
            var param = new object[] { new object[] { 1, 2, 3 } };
            var p = param[0] as object[];
            first.Invoke(null, param);
            Assert.IsTrue(p.Length == 3
                && Convert.ToInt32(p.First()) == 1
                && Convert.ToInt32(p.Skip(1).First()) == 2
                && Convert.ToInt32(p.Last()) == 3
            );
        }
        [Test]
        public void HasLastFunction()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("Last"));
        }
        [Test]
        public void LastReturnsLastItemOfArray()
        {
            var Last = BagOfFunctionsType.GetMethod("Last");
            var result = Last.Invoke(null, new object[] { new object[] { 9, 10, 11 } });
            Assert.AreEqual(11, result);
        }
        [Test]
        public void LastOfAnEmptyArrayReturnsNull()
        {
            var Last = BagOfFunctionsType.GetMethod("Last");
            var result = Last.Invoke(null, new object[] { new object[] { } });
            Assert.AreEqual(null, result);
        }
        [Test]
        public void LastLeavesInputArrayIntact()
        {
            var Last = BagOfFunctionsType.GetMethod("Last");
            var param = new object[] { new object[] { 1, 2, 3 } };
            var p = param[0] as object[];
            Last.Invoke(null, param);
            Assert.AreEqual(3, p.Length, "length of original array should not change");
            Assert.AreEqual(1, Convert.ToInt32(p.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(p.Skip(1).First()), "second element should not change");
            Assert.AreEqual(3, Convert.ToInt32(p.Last()), "third element should not change");
        }
        [Test]
        public void HasToArrayFunction()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("ToArray"));
        }
        [Test]
        public void ToArrayReturnsAnArrayFromASingleValue()
        {
            var toArray = BagOfFunctionsType.GetMethod("ToArray");
            var result = toArray.Invoke(null, new object[] { new object[] { 1 } });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(1, (result as object[])[0]);
        }
        [Test]
        public void ToArrayReturnsAnArrayOfValues()
        {
            var toArray = BagOfFunctionsType.GetMethod("ToArray");
            var result = toArray.Invoke(null, new object[] { new object[] { 1, 2, 3 } });
            Assert.IsTrue(result is object[]);
            var p = result as object[];

            Assert.AreEqual(3, p.Length, "length of original array should not change");
            Assert.AreEqual(1, Convert.ToInt32(p.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(p.Skip(1).First()), "second element should not change");
            Assert.AreEqual(3, Convert.ToInt32(p.Last()), "third element should not change");
        }
        [Test]
        public void ToArrayCanProcess10Elements()
        {
            var toArray = BagOfFunctionsType.GetMethod("ToArray");
            var result = toArray.Invoke(null, new object[] { new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 } });
            Assert.IsTrue(result is object[]);
            var p = result as object[];

            Assert.AreEqual(10, p.Length, "the array should contain all the elements");
        }
        [Test]
        public void ImmutablePushExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("ImmutablePush"));
        }
        [Test]
        public void ImmutablePushReturnsArrayWithElementAdded()
        {
            var toArray = BagOfFunctionsType.GetMethod("ImmutablePush");
            var result = toArray.Invoke(null, new object[] { new object[] { 1, 2 }, 3 });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(3, (result as object[]).Last(), "element was not added to array");
        }
        [Test]
        public void ImmutablePushLeavesOriginalArrayIntact()
        {
            var toArray = BagOfFunctionsType.GetMethod("ImmutablePush");
            var input = new object[] { 1, 2 };
            toArray.Invoke(null, new object[] { input, 3 });
            Assert.AreEqual(2, input.Length, "length should not change");
            Assert.AreEqual(1, Convert.ToInt32(input.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(input.Skip(1).First()), "second element should not change");
        }
        [Test]
        public void ImmutablePopExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("ImmutablePop"));
        }
        [Test]
        public void ImmutablePopReturnsArrayWithElementAdded()
        {
            var toArray = BagOfFunctionsType.GetMethod("ImmutablePop");
            var result = toArray.Invoke(null, new object[] { new object[] { 1, 2 } });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(1, (result as object[]).Length, "item should have been removed");
            Assert.AreEqual(1, (result as object[]).Last(), "element was not removed");
        }
        [Test]
        public void ImmutablePopLeavesOriginalArrayIntact()
        {
            var toArray = BagOfFunctionsType.GetMethod("ImmutablePop");
            var input = new object[] { 1, 2 };
            toArray.Invoke(null, new object[] { input});
            Assert.AreEqual(2, input.Length, "length should not change");
            Assert.AreEqual(1, Convert.ToInt32(input.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(input.Skip(1).First()), "second element should not change");
        }
    }
}
