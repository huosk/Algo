using Algo.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoTest
{
    /// <summary>
    /// TestStack 的摘要说明
    /// </summary>
    [TestClass]
    public class TestStack
    {
        [TestMethod]
        public void TestPeek()
        {
            Stack<int> stack = new Stack<int>();

            Assert.AreEqual(0, stack.Size);

            try
            {
                stack.Peek();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(InvalidOperationException), e.GetType());
            }

            Stack<int> stack2 = new Stack<int>(new int[] { 1, 3, 6 });
            Assert.AreEqual(6, stack2.Peek());
        }

        [TestMethod]
        public void TestPush()
        {
            Stack<int> stack = new Stack<int>();

            Assert.AreEqual(0, stack.Size);

            try
            {
                stack.Peek();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(InvalidOperationException), e.GetType());
            }

            stack.Push(1);

            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, stack.Peek());

            stack.Push(2);

            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(2, stack.Peek());

            stack.Push(3);

            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(3, stack.Peek());
        }

        [TestMethod]
        public void TestPop()
        {
            Stack<int> stack0 = new Stack<int>();
            try
            {
                stack0.Pop();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(InvalidOperationException), e.GetType());
            }

            Stack<int> stack = new Stack<int>(new int[] { 1, 2, 3, 4, 5 });

            for (int i = 5; i > 0; i--)
            {
                Assert.AreEqual(i, stack.Size);
                Assert.AreEqual(i, stack.Pop());
                Assert.AreEqual(i - 1, stack.Size);
            }

            try
            {
                stack.Pop();
            }
            catch (Exception e)
            {
                Assert.AreEqual(typeof(InvalidOperationException), e.GetType());
            }
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            Stack<int> stack = new Stack<int>();
            Assert.AreEqual(true, stack.IsEmpty());

            stack = new Stack<int>(new int[] { 4 });
            Assert.AreEqual(false, stack.IsEmpty());
        }
    }
}
