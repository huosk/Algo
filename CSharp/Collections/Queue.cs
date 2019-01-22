using System;

namespace Algo.Collections
{
    public class Queue<T>
    {
        private T[] items;
        private int size;
        private int head;
        private int tail;
        private int defaultSize = 8;

        public Queue()
        {
            size = 0;
            head = 0;
            tail = 0;
            items = new T[defaultSize];
        }

        public void Enqueue(T item)
        {
            if (size == items.Length)
                GrewCapacity();

            items[tail] = item;
            tail++;
            size++;
        }

        public T Dequeue()
        {
            if (size == 0)
                throw new InvalidOperationException("Queue under flow");
            var item = items[head];
            items[head] = default(T);
            head++;
            size--;
            return item;
        }

        public T Peek()
        {
            if (size == 0)
                throw new InvalidOperationException("Queue is empty");
            return items[head];
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
