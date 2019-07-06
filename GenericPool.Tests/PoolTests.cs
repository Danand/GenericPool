using GenericPool.Core;
using GenericPool.Tests.Dummies;

using NUnit.Framework;

namespace GenericPool.Tests
{
    [TestFixture]
    public class PoolTests
    {
        [Test]
        public void Get_Test()
        {
            var pool = new Pool<ObjectDummy>(
                instance => instance.GetHashCode(),
                instance => new ObjectDummy { Data = instance.Data },
                instance => TestContext.WriteLine($"Got instance of '{instance.GetType()}'"),
                instance => TestContext.WriteLine($"Put instance of '{instance.GetType()}'"));

            var dummyData = "changed";
            var template = new ObjectDummy { Data = dummyData };
            var result = pool.Get(template);

            Assert.AreNotEqual(template, result);
            Assert.AreEqual(template.Data, result.Data);
            Assert.AreEqual(dummyData, result.Data);
        }
    }
}
