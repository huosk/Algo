// <copyright file="Queue.cs" company="SKASK">
// Copyright (c) SKASK. All rights reserved.
// </copyright>

namespace Algo.Collections
{
    using System;

    public class Queue<T>
    {
        private T[] items;
        private int size;
        private int head;
        private int tail;
        private int defaultSize = 8;

        public Queue()
        {
            this.size = 0;
            this.head = 0;
            this.tail = 0;
            this.items = new T[this.defaultSize];
        }

        public Queue(System.Collections.Generic.IEnumerable<T> vals)
        {
            this.size = 0;
            this.head = 0;
            this.tail = 0;
            this.items = new T[this.defaultSize];
            foreach (var item in vals)
            {
                this.Enqueue(item);
            }
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public void Enqueue(T item)
        {
            if (this.size == this.items.Length)
            {
                this.GrewCapacity();
            }

            this.items[this.tail] = item;
            this.tail = (this.tail + 1) % this.items.Length;
            this.size++;
        }

        public T Dequeue()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException("Queue under flow");
            }

            var item = this.items[this.head];
            this.items[this.head] = default(T);
            this.head = (this.head + 1) % this.items.Length;
            this.size--;
            return item;
        }

        public T Peek()
        {
            if (this.size == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return this.items[this.head];
        }

        // 扩容
        private void GrewCapacity()
        {
            T[] newArray = new T[this.items.Length == 0 ? this.defaultSize : this.items.Length * 2];
            if (this.head < this.tail)
            {
                Array.Copy(this.items, newArray, this.items.Length);
            }
            else
            {
                for (int i = 0; i < this.items.Length; i++)
                {
                    newArray[i] = this.items[(this.head + i) % this.items.Length];
                }
            }

            this.head = 0;
            this.tail = this.items.Length;

            this.items = newArray;
        }
    }
}