using Algo.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoTest
{
    [TestClass]
    public class TestSkipList
    {
        private SkipList<int> CreateSkipList()
        {
            SkipList<int> list = new SkipList<int>();

            SkipList<int>.SkipListNode node1 = new SkipList<int>.SkipListNode() { item = 1 };
            SkipList<int>.SkipListNode node2 = new SkipList<int>.SkipListNode() { item = 2 };
            SkipList<int>.SkipListNode node3 = new SkipList<int>.SkipListNode() { item = 3 };
            SkipList<int>.SkipListNode node4 = new SkipList<int>.SkipListNode() { item = 4 };
            SkipList<int>.SkipListNode node5 = new SkipList<int>.SkipListNode() { item = 5 };


            list.Head.levels = new SkipList<int>.SkipLevel[] {
                new SkipList<int>.SkipLevel(){forward = node1},
                new SkipList<int>.SkipLevel(){forward = node1},
                new SkipList<int>.SkipLevel(){ forward = node1}
            };

            node1.levels = new SkipList<int>.SkipLevel[] {
                new SkipList<int>.SkipLevel(){ forward = node2},
                new SkipList<int>.SkipLevel(){ forward = node3},
                new SkipList<int>.SkipLevel(){ forward = node5}
            };

            node2.levels = new SkipList<int>.SkipLevel[] {
                new SkipList<int>.SkipLevel(){ forward=node3},
            };

            node3.levels = new SkipList<int>.SkipLevel[] {
                new SkipList<int>.SkipLevel(){ forward = node4},
                new SkipList<int>.SkipLevel(){ forward = node5}
            };

            node4.levels = new SkipList<int>.SkipLevel[] {
                new SkipList<int>.SkipLevel(){ forward = node5}
            };

            list.CalculateLevelCount();

            return list;
        }
        [TestMethod]
        public void TestFind()
        {
            var list = CreateSkipList();

            Assert.AreEqual(1, list.FindNode(1).item);
            Assert.AreEqual(2, list.FindNode(2).item);
            Assert.AreEqual(3, list.FindNode(3).item);
            Assert.AreEqual(4, list.FindNode(4).item);
            Assert.AreEqual(5, list.FindNode(5).item);
        }

        [TestMethod]
        public void TestRemove() {
            var list = CreateSkipList();

            list.Remove(2);

            Assert.AreEqual(3,list.First.GetForward(0).item);
            Assert.AreEqual(3,list.First.GetForward(1).item);
            Assert.AreEqual(5,list.First.GetForward(2).item);

            list.Remove(3);

            Assert.AreEqual(4,list.First.GetForward(0).item);
            Assert.AreEqual(5,list.First.GetForward(1).item);
            Assert.AreEqual(5,list.First.GetForward(2).item);

            list.Remove(1);

            Assert.AreEqual(4, list.First.item);
            Assert.AreEqual(4, list.Head.GetForward(0).item);
            Assert.AreEqual(5, list.Head.GetForward(1).item);
            Assert.AreEqual(5, list.Head.GetForward(2).item);

            Assert.AreEqual(5,list.First.GetForward(0).item);

            list.Remove(5);

            Assert.AreEqual(4, list.First.item);
            Assert.AreEqual(4, list.Head.GetForward(0).item);
            Assert.AreEqual(4, list.Head.GetForward(1).item);
            Assert.AreEqual(4, list.Head.GetForward(2).item);
        }
    }
}
