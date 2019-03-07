using System;
using System.Collections.Generic;

namespace Algo.Collections
{
    public class Heap<T>
    {
        private T[] items;
        private int count;
        private IComparer<T> comparer;

        public Heap()
            : this(null, Comparer<T>.Default)
        {
        }

        public Heap(IEnumerable<T> collection, IComparer<T> comparer)
        {
        }


        /// <summary>
        /// 自下而上进行上浮堆化
        /// </summary>
        /// <param name="i">其实索引</param>
        public void Swim(int i)
        {
            while (i > 1 && this.comparer.Compare(this.items[i], this.items[i / 2]) > 0)
            {
                T temp = this.items[i / 2];
                this.items[i / 2] = this.items[i];
                this.items[i] = temp;

                i = i / 2;
            }
        }

        /// <summary>
        /// 自上而下进行下沉堆化
        /// </summary>
        /// <param name="i">起始索引</param>
        public void Sink(int i)
        {
            while (i * 2 <= this.count)
            {
                // 找到左右子节点中较大的一个
                int maxChild = i * 2;
                if (this.comparer.Compare(this.items[i * 2], this.items[(i * 2) + 1]) < 0)
                {
                    maxChild++;
                }

                if (this.comparer.Compare(this.items[maxChild], this.items[i]) < 0)
                {
                    break;
                }

                T temp = this.items[maxChild];
                this.items[maxChild] = this.items[i];
                this.items[i] = temp;

                i = maxChild;
            }
        }

        public void Insert(T item)
        {
            if (this.count == this.items.Length - 1)
            {
                // 堆满了，需要动态扩容
                return;
            }

            // 插入到数组
            this.items[++this.count] = item;

            int check = this.count;
            int parent = check / 2;

            // 存在父节点，并且比父节点大，则交换位置
            while (parent > 0 && comparer.Compare(this.items[check], items[parent]) > 0)
            {
                T temp = items[parent];
                items[parent] = items[check];
                items[check] = temp;

                check = parent;
                parent = parent / 2;
            }
        }

        public void RemoveMax()
        {
            if (count <= 0) return;

            // 将堆顶元素交换到最后一个位置
            items[1] = items[count];
            items[count--] = default(T);

            int index = 1;
            while (index * 2 <= count)
            {
                // 找到左右子节点中较大的一个
                int maxChild = index * 2;
                if (maxChild + 1 <= count && comparer.Compare(items[maxChild], items[maxChild + 1]) < 0)
                    maxChild++;

                // 如果比较大的还大，说明已经堆化完毕
                if (comparer.Compare(items[maxChild], items[index]) < 0)
                    break;

                // 如果比较大的小，交换元素
                T temp = items[maxChild];
                items[maxChild] = items[index];
                items[index] = items[maxChild];

                // 继续下沉
                index = maxChild;
            }
        }
    }
}
