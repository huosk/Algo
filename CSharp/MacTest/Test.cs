using NUnit.Framework;
using Algo.Collections;
using System;

namespace MacTest
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            SkipList<int> list = new SkipList<int>();
            list.Insert(1);
            list.Insert(2);
            list.Insert(3);
            list.Insert(4);

            Assert.AreEqual(1, list.First.item);
            Assert.AreEqual(2, list.First.Forward.item);
            Assert.AreEqual(3, list.First.Forward.Forward.item);
            Assert.AreEqual(4, list.First.Forward.Forward.Forward.item);
        }
    }
}
