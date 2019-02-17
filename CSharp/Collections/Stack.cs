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
                return this.size;
            }
        }

        private T[] items;
        private int size;
        private int defaultSize = 8;

        public Stack() : this(null) { }

        public Stack(IEnumerable<T> vals)
        {
            this.size = 0;
            this.items = new T[this.defaultSize];
            if (vals != null)
            {
                foreach (var item in vals)
                {
                    this.Push(item);
                }
            }
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public void Push(T item)
        {
            if (this.size == this.items.Length)
                this.GrewCapacity();
            this.items[this.size++] = item;
        }

        public T Pop()
        {
            if (this.size == 0)
                throw (new InvalidOperationException("stack under flow"));

            var top = this.items[--this.size];
            this.items[this.size] = default(T);
            return top;
        }

        public T Peek()
        {
            if (this.size == 0)
                throw (new InvalidOperationException("peek item from empty stack"));

            return this.items[this.size - 1];
        }

        //扩容
        private void GrewCapacity()
        {
            T[] newArray = new T[this.items.Length == 0 ? this.defaultSize : this.items.Length * 2];
            Array.Copy(this.items, newArray, this.items.Length);
            this.items = newArray;
        }
    }
}
