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
            this.comparer = comparer;
            this.items = new T[10];
        }

        public T Max
        {
            get
            {
                if (items.Length > 1 && items[1] != null)
                {
                    return items[1];
                }

                return default(T);
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

            this.Swim(this.count);
        }

        public void RemoveMax()
        {
            if (count <= 0) return;

            // 将堆顶元素交换到最后一个位置
            items[1] = items[count];
            items[count--] = default(T);

            Sink(1);
        }

        /// <summary>
        /// 自下而上进行上浮堆化
        /// </summary>
        /// <param name="i">其实索引</param>
        private void Swim(int i)
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
        private void Sink(int i)
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
    }
}
