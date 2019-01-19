using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algo.Collections
{
    //链表节点
    public class LinkedNode<T>
    {
        public T item;
        public LinkedNode<T> next;

        public LinkedNode(T val)
        {
            item = val;
        }
    }

    public class LinkedList<T>
    {
        public int Length
        {
            get
            {
                return length;
            }
        }

        public LinkedNode<T> First
        {
            get
            {
                return headGuard.next;
            }
        }

        //头结点哨兵
        private LinkedNode<T> headGuard;
        private int length;

        public LinkedList()
        {
            headGuard = new LinkedNode<T>(default(T));
        }

        public LinkedList(IEnumerable<T> list)
        {
            headGuard = new LinkedNode<T>(default(T));
            if (list != null)
            {
                var p = headGuard;
                foreach (T item in list)
                {
                    p.next = new LinkedNode<T>(item);
                    p = p.next;
                    ++length;
                }
            }
        }

        public LinkedNode<T> FindNode(Predicate<LinkedNode<T>> predicate)
        {
            var p = headGuard.next;
            while (p != null && !predicate(p))
                p = p.next;

            return p;
        }

        public LinkedNode<T> FindAt(int position)
        {
            if (position < 0 || position > length)
                throw new IndexOutOfRangeException("position out of index");

            var p = headGuard.next;
            int i = 0;
            while (p != null && i < position)
            {
                p = p.next;
                i++;
            }

            return p;
        }

        public LinkedNode<T> InsertAfter(LinkedNode<T> node, T item)
        {
            if (node == null)
                throw new ArgumentNullException("node is null");

            var newNode = new LinkedNode<T>(item);
            newNode.next = node.next;
            node.next = newNode;

            length++;

            return newNode;
        }

        public LinkedNode<T> InsertAfter(int position, T item)
        {
            if (position < 0 || position > length)
                throw new IndexOutOfRangeException("position out of index");

            var prev = headGuard;
            int counter = 0;
            while (prev != null && counter < position)
            {
                prev = prev.next;
                counter++;
            }

            if (prev != null)
                return InsertAfter(prev, item);
            else
                return null;
        }

        public bool Remove(LinkedNode<T> node)
        {
            //处理Null情况
            if (node == null)
                return false;

            var p = headGuard;
            while (p != null && p.next != node)
                p = p.next;

            if (p == null)
                return false;

            p.next = node.next;
            node.next = null;

            length--;

            return true;
        }

        public LinkedNode<T> RemoveAt(int position)
        {
            var node = FindAt(position);

            if (node != null && Remove(node))
                return node;
            else
                return null;
        }

    }
}
