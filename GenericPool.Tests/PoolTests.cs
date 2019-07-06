using GenericPool.Core;
using GenericPool.Tests.Dummies;

using NUnit.Framework;

namespace GenericPool.Tests
{
    [TestFixture]
    public class PoolTests
    {
        [Test]
        public void Get_Plain()
        {
            var pool = new Pool<ObjectDummy>(
                idSelector:             instance => instance.GetType().GetHashCode(),
                instanceSelector:       instance => new ObjectDummy { Data = instance.Data },
                getFromPoolCallback:    instance => TestContext.WriteLine($"Got instance of '{instance.GetType()}'"),
                putToPoolCallback:      instance => TestContext.WriteLine($"Put instance of '{instance.GetType()}'"));

            var dummyData = "some data";
            var template = new ObjectDummy { Data = dummyData };
            var result = pool.Get(template);

            Assert.AreNotEqual(template, result);
            Assert.AreEqual(template.Data, result.Data);
            Assert.AreEqual(dummyData, result.Data);
        }

        [Test]
        public void Get_WithBuilder()
        {
            var pool = new Pool<ObjectDummy>(
                idSelector:             instance => instance.GetType().GetHashCode(),
                instanceSelector:       instance => new ObjectDummy { Data = instance.Data },
                getFromPoolCallback:    instance => TestContext.WriteLine($"Got instance of '{instance.GetType()}'"),
                putToPoolCallback:      instance => TestContext.WriteLine($"Put instance of '{instance.GetType()}'"));

            var dummyDataOld = "some data";
            var dummyDataNew = "changed";

            var template = new ObjectDummy { Data = dummyDataOld };

            pool.GetWith(template)
                .Then(instance => instance.Data = dummyDataNew)
                .ThenPut();

            var result = pool.Get(template);

            Assert.AreEqual(dummyDataNew, result.Data);
        }
    }
}
