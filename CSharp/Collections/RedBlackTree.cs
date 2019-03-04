using System;
using System.Collections.Generic;
using System.Collections;

namespace Algo.Collections
{
    public class RedBlackTree<T>
    {
        public class Node
        {
            public bool IsRed;
            public Node Left;
            public Node Right;
            public T Item;

            public Node(T t)
            {
                this.Item = t;
            }
        }

        private Node tree;
        private IComparer<T> comparer;

        public RedBlackTree() : this(null, Comparer<T>.Default)
        {
        }

        public RedBlackTree(IEnumerable<T> collection, IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public Node RotateLeft(Node h)
        {
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.IsRed = h.IsRed;
            h.IsRed = true;
            return x;
        }

        public Node RotateRight(Node h)
        {
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.IsRed = h.IsRed;
            h.IsRed = true;
            return x;
        }

        public void FlipColor(Node h)
        {
            h.IsRed = !h.IsRed;
            h.Left.IsRed = !h.Left.IsRed;
            h.Right.IsRed = !h.Right.IsRed;
        }

        public void Insert(T t)
        {
            tree = InsertInternal(tree, t);
            tree.IsRed = false;
        }

        public bool Contains(T t)
        {
            return GetNode(t) != null;
        }

        public void Remove(T t)
        {
            if (t == null || !Contains(t))
                return;

            if (!IsRedNode(tree.Left) && !IsRedNode(tree.Right))
                tree.IsRed = true;

            tree = Remove(tree, t);

            if (tree != null)
                tree.IsRed = false;
        }

        private Node Remove(Node h, T t)
        {
            if (comparer.Compare(h.Item, t) < 0)
            {
                if (!IsRedNode(h.Left) && !IsRedNode(h.Right))
                    h = MoveRedToLeft(h);

                h.Left = Remove(h.Left, t);
            }
            else
            {
                if (IsRedNode(h.Left))
                    h = RotateRight(h);

                if (comparer.Compare(h.Item, t) == 0 && h.Right == null)
                    return null;

                if (!IsRedNode(h.Right) && !IsRedNode(h.Right.Left))
                {
                    h = MoveRedToRight(h);
                }

                if (comparer.Compare(h.Item, t) == 0)
                {
                    // 找到右子树的最小值，替换到要删除的节点
                    Node x = MinNode(h.Right);
                    h.Item = x.Item;

                    // 替换完成之后，只需要将最小值节点删除即可
                    h.Right = RemoveMinInternal(h.Right);
                }
                else
                {
                    h.Right = Remove(h.Right, t);
                }
            }

            return Balance(h);
        }

        private Node GetNode(T key)
        {
            Node x = tree;
            while (x != null)
            {
                int comp = comparer.Compare(key, x.Item);
                if (comp < 0) x = x.Left;
                else if (comp > 0) x = x.Right;
                else return x;
            }
            return null;
        }

        public void RemoveMax()
        {
            if (tree == null)
                return;

            if (!IsRedNode(tree.Left) && !IsRedNode(tree.Right))
                tree.IsRed = true;

            tree = RemoveMaxInternal(tree);
        }

        private Node MinNode(Node h)
        {
            if (h.Left == null)
                return h;

            return MinNode(h.Left);
        }

        private Node RemoveMaxInternal(Node h)
        {
            // 因为左链接可以为红链接，所以可以直接将
            // 左链接的红链接旋转到右链接，使得右子节点变成3-节点或4-节点
            if (IsRedNode(h.Left))
                h = RotateRight(h);

            if (h.Right == null)
                return null;

            // 如果节点为2-节点
            if (!IsRedNode(h.Left) && !IsRedNode(h.Right))
            {
                h = MoveRedToRight(h);
            }

            h.Right = RemoveMaxInternal(h.Right);

            return Balance(h);
        }

        private Node MoveRedToRight(Node h)
        {
            FlipColor(h);

            // 判断左侧子节点是否非2-节点（3-节点、4-节点）
            if (h.Left != null && h.Left.Left != null && IsRedNode(h.Left.Left))
            {
                h = RotateRight(h);
                FlipColor(h);
            }
            return h;
        }

        public void RemoveMin()
        {
            if (tree == null)
                return;

            // 因为自上而下遍历过程中，能够保证左子节点非2-节
            // 因此左子节点与父节点的链接为红链接，为了递归中统一
            // 处理，此处将根节点也设置为红色。
            if (!IsRedNode(tree.Left) && !IsRedNode(tree.Right))
                tree.IsRed = true;

            tree = RemoveMinInternal(tree);
        }


        /// <summary>
        /// 删除最小值的递归函数
        /// </summary>
        /// <returns>删除最小值后的左子树根</returns>
        /// <param name="h">需要删除左子树中最小值的节点</param>
        private Node RemoveMinInternal(Node h)
        {
            if (h.Left == null)
                return null;

            // 左子节点红链接 或 左孙子节点红链接表示3-节点
            // 左子节点、左孙子节点都为红表示4-节点
            // 左子节点、左孙子节点都不为红表示2-节点
            if (!IsRedNode(h.Left) && !IsRedNode(h.Left.Left))
            {
                h = MoveRedToLeft(h);
            }

            // 继续自上而下递归调整
            h.Left = RemoveMinInternal(h.Left);


            // 开始自下而上进行分解4-节点，平衡红黑树
            return Balance(h);
        }

        private Node MoveRedToLeft(Node h)
        {
            FlipColor(h);

            // 判断 h 右子节点是否为 3-节点 或 4-节点
            if (h.Right != null && h.Right.Left != null && IsRedNode(h.Right))
            {
                // 右子节点3-节点 或 4-节点，将右子节点的键移到左子节点中
                h.Right = RotateRight(h.Right);
                h = RotateLeft(h);
                FlipColor(h);
            }
            return h;
        }

        private Node Balance(Node h)
        {
            if (IsRedNode(h.Right)) h = RotateLeft(h);
            if (IsRedNode(h.Left) && IsRedNode(h.Left.Left)) RotateRight(h);
            if (IsRedNode(h.Left) && IsRedNode(h.Right)) FlipColor(h);
            return h;
        }

        private bool IsRedNode(Node x)
        {
            // 规定空链接为黑链接
            if (x == null) return false;

            return x.IsRed;
        }

        private Node InsertInternal(Node h, T t)
        {
            if (h == null)
            {
                return new Node(t)
                {
                    IsRed = true
                };
            }

            int comp = this.comparer.Compare(t, h.Item);
            if (comp < 0) h.Left = InsertInternal(h.Left, t);
            else if (comp > 0) h.Right = InsertInternal(h.Right, t);
            else h.Item = t;

            if (IsRedNode(h.Right) && !IsRedNode(h.Left)) h = RotateLeft(h);
            if (IsRedNode(h.Left) && IsRedNode(h.Left.Left)) h = RotateRight(h);
            if (IsRedNode(h.Left) && IsRedNode(h.Right)) FlipColor(h);

            return h;
        }
    }
}
