using Algo.Sample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoTest
{
    [TestClass]
    public class TestFindMaxSubarray
    {
        [TestMethod]
        public void TestFindMax()
        {
            int[] array = new int[] {
                13,-3,-25,20,-3,-16,-23,18,20,-7,12,-5,-22,15,-4,7
            };

            var result = MaxSumArray.FindMaxSubarray(array, 0, array.Length - 1);
            Assert.AreEqual(7, result.Low);
            Assert.AreEqual(10, result.High);
            Assert.AreEqual(43, result.Sum);
        }

        [TestMethod]
        public void TestFindMaxRoughly()
        {
            int[] array = new int[] {
                13,-3,-25,20,-3,-16,-23,18,20,-7,12,-5,-22,15,-4,7
            };

            var result = MaxSumArray.FindMaxSubarrayDirectly(array);
            Assert.AreEqual(7, result.Low);
            Assert.AreEqual(10, result.High);
            Assert.AreEqual(43, result.Sum);
        }

        [TestMethod]
        public void TestAllNegArrayMax()
        {
            int[] array = new int[] {
                -76,-81,-1,-2,-3,-4,-5,-9,-10,-22,-8,-10
            };

            var result = MaxSumArray.FindMaxSubarray(array, 0, array.Length - 1);
            Assert.AreEqual(2, result.Low);
            Assert.AreEqual(2, result.High);
            Assert.AreEqual(-1, result.Sum);
        }
    }
}
