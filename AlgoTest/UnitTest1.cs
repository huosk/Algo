using Algo.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoTest
{
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void TestInsertAfter()
        {
            LinkedList<int> list = new LinkedList<int>();
            list.InsertAfter(0, 1);
            list.InsertAfter(1, 2);
            list.InsertAfter(2, 3);
            list.InsertAfter(list.Length, list.Length);

            try
            {
                list.InsertAfter(100, 10);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(IndexOutOfRangeException), e.GetType());
            }

            Assert.AreEqual(4, list.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestRemove()
        {
            LinkedList<int> list = new LinkedList<int>();
            var node0 = list.InsertAfter(0, 1);
            var node1 = list.InsertAfter(1, 2);
            var node2 = list.InsertAfter(2, 3);

            list.Remove(null);
            list.Remove(node0);
            list.Remove(node2);
            list.Remove(node1);

            Assert.AreEqual(0, list.Length);


            //空链表删除
            LinkedList<int> list2 = new LinkedList<int>();
            list2.Remove(new LinkedNode<int>(100));

            //1个结点的删除
            LinkedList<int> list3 = new LinkedList<int>();
            var singleNode = list3.InsertAfter(0, 4);
            list.Remove(singleNode);
        }


        [TestMethod]
        public void TestFindAt()
        {
            LinkedList<int> list = new LinkedList<int>();
            try
            {
                list.FindAt(10);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(IndexOutOfRangeException), e.GetType());
            }

            list = new LinkedList<int>(new int[] { 1, 3, 4, 5, 6, 8 });
            var node0 = list.FindAt(0);
            var node3 = list.FindAt(3);
            var node5 = list.FindAt(5);
            var node6 = list.FindAt(6);
            Assert.AreEqual(1, node0.item);
            Assert.AreEqual(5, node3.item);
            Assert.AreEqual(8, node5.item);
            Assert.AreEqual(null, node6);
        }

        [TestMethod]
        public void TestFind()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4, 5, 6 });
            var node0 = list.FindNode((n) => n.item == 7);
            Assert.AreEqual(null, node0);

            var node1 = list.FindNode((n) => n.item == 3);
            Assert.AreNotEqual(null, node1);
            Assert.AreEqual(3, node1.item);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4, 5 });
            var ndoe = list.RemoveAt(2);
            Assert.AreEqual(3, ndoe.item);
            try
            {
                list.RemoveAt(10);
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(IndexOutOfRangeException), e.GetType());
            }

            list.RemoveAt(0);
            list.RemoveAt(0);
            list.RemoveAt(0);
            list.RemoveAt(0);

            Assert.AreEqual(0, list.Length);
            Assert.AreEqual(null, list.First);
        }
    }
}
