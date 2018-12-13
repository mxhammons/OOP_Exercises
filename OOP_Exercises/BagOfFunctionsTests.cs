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
            var immutablePush = BagOfFunctionsType.GetMethod("ImmutablePush");
            var result = immutablePush.Invoke(null, new object[] { new object[] { 1, 2 }, 3 });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(3, (result as object[]).Last(), "element was not added to array");
        }
        [Test]
        public void ImmutablePushLeavesOriginalArrayIntact()
        {
            var immutablePush = BagOfFunctionsType.GetMethod("ImmutablePush");
            var input = new object[] { 1, 2 };
            immutablePush.Invoke(null, new object[] { input, 3 });
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
            var immutablePop = BagOfFunctionsType.GetMethod("ImmutablePop");
            var result = immutablePop.Invoke(null, new object[] { new object[] { 1, 2 } });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(1, (result as object[]).Length, "item should have been removed");
            Assert.AreEqual(1, (result as object[]).Last(), "element was not removed");
        }
        [Test]
        public void ImmutablePopLeavesOriginalArrayIntact()
        {
            var immutablePop = BagOfFunctionsType.GetMethod("ImmutablePop");
            var input = new object[] { 1, 2 };
            immutablePop.Invoke(null, new object[] { input });
            Assert.AreEqual(2, input.Length, "length should not change");
            Assert.AreEqual(1, Convert.ToInt32(input.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(input.Skip(1).First()), "second element should not change");
        }
        [Test]
        public void ImmutableShiftExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("ImmutableShift"));
        }
        [Test]
        public void ImmutableShiftReturnsArrayWithElementAdded()
        {
            var immutableShift = BagOfFunctionsType.GetMethod("ImmutableShift");
            var result = immutableShift.Invoke(null, new object[] { new object[] { 1, 2 } });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(1, (result as object[]).Length, "first item should have been removed");
            Assert.AreEqual(2, (result as object[]).Last(), "first element was not removed");
        }
        [Test]
        public void ImmutableShiftLeavesOriginalArrayIntact()
        {
            var immutableShift = BagOfFunctionsType.GetMethod("ImmutableShift");
            var input = new object[] { 1, 2 };
            immutableShift.Invoke(null, new object[] { input });
            Assert.AreEqual(2, input.Length, "length should not change");
            Assert.AreEqual(1, Convert.ToInt32(input.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(input.Skip(1).First()), "second element should not change");
        }
        [Test]
        public void ImmutableUnShiftExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("ImmutableUnShift"));
        }
        [Test]
        public void ImmutableUnShiftReturnsArrayWithElementAdded()
        {
            var immutableUnShift = BagOfFunctionsType.GetMethod("ImmutableUnShift");
            var result = immutableUnShift.Invoke(null, new object[] { new object[] { 1, 2 }, 3 });
            Assert.IsTrue(result is object[]);
            Assert.AreEqual(3, (result as object[]).Length, "item should have been added");
            Assert.AreEqual(3, (result as object[]).First(), "item should have been added as the first item");
        }
        [Test]
        public void ImmutableUnShiftLeavesOriginalArrayIntact()
        {
            var immutableUnShift = BagOfFunctionsType.GetMethod("ImmutableUnShift");
            var input = new object[] { 1, 2 };
            immutableUnShift.Invoke(null, new object[] { input, 3 });
            Assert.AreEqual(2, input.Length, "length should not change");
            Assert.AreEqual(1, Convert.ToInt32(input.First()), "first element should not change");
            Assert.AreEqual(2, Convert.ToInt32(input.Skip(1).First()), "second element should not change");
        }
        [Test]
        public void IntersectionExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("Intersection"));
        }
        [Test]
        public void IntersectionReturnsIntersectionOfArrays()
        {
            var intersection = BagOfFunctionsType.GetMethod("Intersection");
            var result = intersection.Invoke(null, new object[] { new object[] { 1, 2, 3 }, new object[] { 2, 3, 4 } });
            Assert.AreEqual(2, (result as object[]).Length, "Expected two elements");
            Assert.AreEqual(2, (result as object[]).First(), "item should have been added");
            Assert.AreEqual(3, (result as object[]).Last(), "item should have been added as the first item");
        }
        [Test]
        public void AddExists()
        {
            Assert.IsNotNull(BagOfFunctionsType.GetMethod("Add"));
        }
        [Test]
        public void AddHasOneParameter()
        {
            Assert.AreEqual(1, BagOfFunctionsType.GetMethod("Add").GetParameters().Count());
        }
        [Test]
        public void AddReturnsAFunction()
        {
            Assert.AreEqual(typeof(Func<decimal, decimal>), BagOfFunctionsType.GetMethod("Add").ReturnType);
        }
        [Test]
        public void PassingNumberAReturnsFunctionThatTakesNumberBAndAddsAAndB()
        {
            var add = BagOfFunctionsType.GetMethod("Add");
            var result = add.Invoke(null, new object[] {2m });
            Assert.IsTrue(result is Func<decimal, decimal>);
            var fResult = result as Func<decimal, decimal>;
            var final = fResult(2m);
            Assert.AreEqual(4, final);
        }
    }
}
