using NUnit.Framework;
using Algo.Collections;
using System;
using System.Collections.Generic;

namespace MacTest
{
    [TestFixture()]
    public class Test
    {
        [Test]
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


        [Test]
        public void TestComp()
        {
            Assert.AreEqual(true, Comparer<int>.Default.Compare(2, 1) > 0);
        }

        [Test]
        public void TestBinarySearchTree()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Insert(33);
            tree.Insert(16);
            tree.Insert(50);
            tree.Insert(13);
            tree.Insert(18);
            tree.Insert(34);
            tree.Insert(58);
            tree.Insert(15);
            tree.Insert(17);
            tree.Insert(25);
            tree.Insert(51);
            tree.Insert(66);
            tree.Insert(19);
            tree.Insert(27);
            tree.Insert(55);

            Assert.AreEqual(33, tree.Root);
        }


        [Test]
        public void TestBinarySearchTreeRemove()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(new int[]{
                33,16,50,13,18,34,58,15,17,25,51,66,19,27,55
            });

            tree.Delete(55);

            tree.Delete(13);

            tree.Delete(18);
        }
    }
}
