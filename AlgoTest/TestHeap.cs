using System;
using Algo.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoTest
{
    [TestClass]
    public class TestHeap
    {
        [TestMethod]
        public void TestInsert()
        {
            Heap<int> heap = new Heap<int>();

            for (int i = 0; i < 8; i++)
            {
                heap.Insert(i);
                Assert.AreEqual(i, heap.Max);
            }
        }
    }
}
