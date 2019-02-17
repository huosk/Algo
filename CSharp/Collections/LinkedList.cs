// <copyright file="LinkedList.cs" company="SKASK">
// Copyright (c) SKASK. All rights reserved.
// </copyright>

namespace Algo.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LinkedList<T>
    {
        // 头结点哨兵
        private readonly LinkedNode<T> headGuard;
        private int length;

        public LinkedList()
        {
            this.headGuard = new LinkedNode<T>(default(T));
        }

        public LinkedList(IEnumerable<T> list)
        {
            this.headGuard = new LinkedNode<T>(default(T));
            if (list != null)
            {
                var p = this.headGuard;
                foreach (T item in list)
                {
                    p.Next = new LinkedNode<T>(item);
                    p = p.Next;
                    ++this.length;
                }
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
        }

        public LinkedNode<T> First
        {
            get
            {
                return this.headGuard.Next;
            }
        }

        public LinkedNode<T> FindNode(Predicate<LinkedNode<T>> predicate)
        {
            var p = this.headGuard.Next;
            while (p != null && !predicate(p))
            {
                p = p.Next;
            }

            return p;
        }

        public LinkedNode<T> FindAt(int position)
        {
            if (position < 0 || position > this.length)
            {
                throw new IndexOutOfRangeException("position out of index");
            }

            var p = this.headGuard.Next;
            int i = 0;
            while (p != null && i < position)
            {
                p = p.Next;
                i++;
            }

            return p;
        }

        public LinkedNode<T> InsertAfter(LinkedNode<T> node, T item)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node is null");
            }

            var newNode = new LinkedNode<T>(item)
            {
                Next = node.Next
            };
            node.Next = newNode;

            this.length++;

            return newNode;
        }

        public LinkedNode<T> InsertAfter(int position, T item)
        {
            if (position < 0 || position > this.length)
            {
                throw new IndexOutOfRangeException("position out of index");
            }

            var prev = this.headGuard;
            int counter = 0;
            while (prev != null && counter < position)
            {
                prev = prev.Next;
                counter++;
            }

            if (prev != null)
            {
                return this.InsertAfter(prev, item);
            }
            else
            {
                return null;
            }
        }

        public bool Remove(LinkedNode<T> node)
        {
            // 处理Null情况
            if (node == null)
            {
                return false;
            }

            var p = this.headGuard;
            while (p != null && p.Next != node)
            {
                p = p.Next;
            }

            if (p == null)
            {
                return false;
            }

            p.Next = node.Next;
            node.Next = null;

            this.length--;

            return true;
        }

        public LinkedNode<T> RemoveAt(int position)
        {
            var node = this.FindAt(position);

            if (node != null && this.Remove(node))
            {
                return node;
            }
            else
            {
                return null;
            }
        }

        public void Reverse()
        {
            if (this.First != null)
            {
                this.ReverseHandler(this.First);
            }
        }

        private LinkedNode<T> ReverseHandler(LinkedNode<T> head)
        {
            if (head == null)
            {
                return null;
            }

            if (head.Next == null)
            {
                this.headGuard.Next = head;
            }
            else
            {
                var remainHead = this.ReverseHandler(head.Next);
                remainHead.Next = head;
            }

            return head;
        }
    }

    // 链表节点
    public class LinkedNode<T>
    {
        public LinkedNode(T val)
        {
            this.Item = val;
        }

        public T Item { get; set; }

        public LinkedNode<T> Next { get; set; }
    }
}
