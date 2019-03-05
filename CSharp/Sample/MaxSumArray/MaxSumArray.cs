namespace Algo.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 最大子数组求值结果
    /// </summary>
    public class Result
    {
        private int low;
        private int high;
        private int sum;

        public int Low { get => this.low; set => this.low = value; }

        public int High { get => this.high; set => this.high = value; }

        public int Sum { get => this.sum; set => this.sum = value; }
    }

    public class MaxSumArray
    {
        /// <summary>
        /// 暴力法求解最大子数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>最大子数组</returns>
        public static Result FindMaxSubarrayDirectly(int[] array)
        {
            var result = new Result()
            {
                Sum = int.MinValue
            };
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {
                sum = 0;

                for (int j = i; j < array.Length; j++)
                {
                    sum += array[j];
                    if (sum > result.Sum)
                    {
                        result.Sum = sum;
                        result.Low = i;
                        result.High = j;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 查找最大子数组
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="low">左侧边界</param>
        /// <param name="high">右侧边界</param>
        /// <returns>查找结果</returns>
        public static Result FindMaxSubarray(int[] array, int low, int high)
        {
            if (low == high)
            {
                return new Result()
                {
                    Low = low,
                    High = high,
                    Sum = array[low]
                };
            }
            else
            {
                int mid = (int)Math.Floor((low + high) / 2.0f);
                var leftResult = FindMaxSubarray(array, low, mid);
                var rightResult = FindMaxSubarray(array, mid + 1, high);
                var midResult = FindCrossMaxSubarray(array, low, high, mid);

                if (leftResult.Sum >= rightResult.Sum && leftResult.Sum >= midResult.Sum)
                {
                    return leftResult;
                }
                else if (rightResult.Sum >= leftResult.Sum && rightResult.Sum >= midResult.Sum)
                {
                    return rightResult;
                }
                else
                {
                    return midResult;
                }
            }
        }

        private static Result FindCrossMaxSubarray(int[] array, int low, int high, int mid)
        {
            Result left = new Result()
            {
                Sum = mid > low ? int.MinValue : 0,
            };
            int sum = 0;
            for (int i = mid; i >= low; i--)
            {
                sum += array[i];
                if (sum > left.Sum)
                {
                    left.Sum = sum;
                    left.Low = i;
                    left.High = mid;
                }
            }

            sum = 0;
            Result right = new Result()
            {
                Sum = mid < high ? int.MinValue : 0
            };
            for (int i = mid + 1; i <= high; i++)
            {
                sum += array[i];
                if (sum > right.Sum)
                {
                    right.Sum = sum;
                    right.Low = mid;
                    right.High = i;
                }
            }

            return new Result()
            {
                Sum = left.Sum + right.Sum,
                Low = left.Low,
                High = right.High
            };
        }
    }
}
