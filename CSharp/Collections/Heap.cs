using System;
using System.Collections.Generic;

namespace Algo.Collections
{
    public class Heap<T>
    {
        private T[] items;
        private int count;
        private IComparer<T> comparer;

        public Heap():this(null,Comparer<T>.Default){}

        public Heap(IEnumerable<T> collection,IComparer<T> comparer)
        {

        }

        public void Insert(T item)
        {
            if(count == items.Length-1){
                // 堆满了，需要动态扩容
                return;
            }

            // 插入到数组
            items[++count] = item;

            int check = count;
            int parent = check / 2;

            // 存在父节点，并且比父节点大，则交换位置
            while(parent > 0 && comparer.Compare(items[check],items[parent]) > 0)
            {
                T temp = items[parent];
                items[parent] = items[check];
                items[check] = temp;

                check = parent;
                parent = parent / 2;
            }
        }
    }
}
