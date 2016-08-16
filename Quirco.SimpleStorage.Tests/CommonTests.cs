using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Quirco.SimpleStorage.Tests
{
    [TestClass]
    public class CommonTests
    {
        private readonly TestStorage _storage = new TestStorage();

        [TestMethod]
        public void TestPrimitives()
        {
            _storage.Put("int", 1);
            Assert.AreEqual(1, _storage.Get<int>("int"));

            _storage.Put("bool", true);
            Assert.AreEqual(true, _storage.Get<bool>("bool"));

            _storage.Put<string>("str", "Hello");
            Assert.AreEqual("Hello", _storage.Get<string>("str"));
        }

        [TestMethod]
        public void TestLists()
        {
            var list= new List<TestMan>
            {
                new TestMan {Name = "Serg", Age = 34},
                new TestMan {Name = "Julia", Age = 32}
            };

            _storage.Put("list", list);

            var result = _storage.Get<List<TestMan>>("list");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Serg", result.First().Name);
            Assert.AreEqual(34, result.First().Age);
        }

        [TestMethod]
        public void TestCircularRefs()
        {
            var list = new List<TestMan>
            {
                new TestMan {Name = "Serg", Age = 34},
                new TestMan {Name = "Julia", Age = 32}
            };

            list.First().Parent = list.Last();
            list.Last().Parent = list.First();

            _storage.Put("list", list);

            var result = _storage.Get<List<TestMan>>("list");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Serg", result.First().Name);
            Assert.AreEqual(34, result.First().Age);
            Assert.AreEqual(result.First(), result.Last().Parent);
            Assert.AreEqual(result.Last(), result.First().Parent);
        }

        [TestMethod]
        public void TestInheritedObjectsPersistence()
        {
            var list = new List<TestMan>
            {
                new TestMan {Name = "Serg", Age = 34},
                new ParentTestMan {Name = "Julia", Age = 32, Child = "child"}
            };
            
            _storage.Put("list", list);

            var result = _storage.Get<List<TestMan>>("list");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Serg", result.First().Name);
            Assert.AreEqual(34, result.First().Age);
            Assert.AreEqual(typeof(ParentTestMan), result.Last().GetType());
            Assert.AreEqual("child", ((ParentTestMan)result.Last()).Child);
        }
    }

    #region Test classes

    internal class TestMan
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public TestMan Parent { get; set; }
    }

    internal class ParentTestMan : TestMan
    {
        public string Child { get; set; }
    }

    #endregion
}