using Algo.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoTest
{
    [TestClass]
    public class TestQueue
    {
        [TestMethod]
        public void TestCons()
        {
            Queue<int> q = new Queue<int>(new int[] { 1, 2, 3, 4, 5, 6 });
            Assert.AreEqual(6, q.Size);
        }

        [TestMethod]
        public void TestEnqueue()
        {
            Queue<int> q = new Queue<int>();

            for (int i = 0; i < 10; i++)
            {
                q.Enqueue(i);
                Assert.AreEqual(i + 1, q.Size);
            }

            q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            q.Dequeue();

            Assert.AreEqual(6, q.Size);



            for (int i = 0; i < 20; i++)
            {
                q.Enqueue(i);
            }
            Assert.AreEqual(26, q.Size);
        }
    }
}
