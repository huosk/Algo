using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Collections
{
    public class Stack<T>
    {
        public int Size
        {
            get
            {
                return size;
            }
        }

        private T[] items;
        private int size;
        private int defaultSize = 8;

        public Stack() : this(null) { }

        public Stack(IEnumerable<T> vals)
        {
            size = 0;
            items = new T[defaultSize];
            if (vals != null)
            {
                foreach (var item in vals)
                {
                    Push(item);
                }
            }
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Push(T item)
        {
            if (size == items.Length)
                GrewCapacity();
            items[size++] = item;
        }

        public T Pop()
        {
            if (size == 0)
                throw (new InvalidOperationException("stack under flow"));

            var top = items[--size];
            items[size] = default(T);
            return top;
        }

        public T Peek()
        {
            if (size == 0)
                throw (new InvalidOperationException("peek item from empty stack"));

            return items[size - 1];
        }

        //扩容
        private void GrewCapacity()
        {
            T[] newArray = new T[items.Length == 0 ? defaultSize : items.Length * 2];
            Array.Copy(items, newArray, items.Length);
            items = newArray;
        }
    }
}
