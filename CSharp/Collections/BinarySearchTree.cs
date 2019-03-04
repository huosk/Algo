namespace Algo.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BinarySearchTree<T>
    {
        public class BinaryTreeNode
        {
            public BinaryTreeNode leftChild;
            public BinaryTreeNode rightChild;
            public T data;
        }

        private BinaryTreeNode tree;
        private IComparer<T> comparer;


        public BinarySearchTree()
            : this(null, Comparer<T>.Default)
        {
        }

        public BinarySearchTree(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {

        }

        public BinarySearchTree(IEnumerable<T> collection, IComparer<T> comparer)
        {
            this.comparer = comparer;
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    Insert(item);
                }
            }
        }

        public T Root
        {
            get
            {
                if (tree == null)
                    return default(T);

                return tree.data;
            }
        }

        public void Insert(T t)
        {
            if (tree == null)
            {
                tree = new BinaryTreeNode()
                {
                    data = t
                };
                return;
            }

            BinaryTreeNode node = tree;
            while (node != null)
            {
                if (comparer.Compare(t, node.data) < 0)
                {// t < node.data
                    if (node.leftChild == null)
                    {
                        node.leftChild = new BinaryTreeNode()
                        {
                            data = t
                        };
                        break;
                    }
                    node = node.leftChild;
                }
                else
                {
                    if (node.rightChild == null)
                    {
                        node.rightChild = new BinaryTreeNode()
                        {
                            data = t
                        };
                        break;
                    }
                    node = node.rightChild;
                }
            }
        }


        public void Delete(T t)
        {
            if (tree == null)
                return;

            BinaryTreeNode node = tree;         // 要删除的节点
            BinaryTreeNode nodeParent = null;   // 要删除结点的父节点
            while (node != null)
            {
                int comp = comparer.Compare(t, node.data);
                if (comp < 0)
                {// t < node.data
                    nodeParent = node;
                    node = node.leftChild;
                }
                else if (comp > 0)
                {// t > node.data
                    nodeParent = node;
                    node = node.rightChild;
                }
                else
                {// t == node.data
                    break;
                }
            }

            if (node == null)
            {// 没要找到要删除的节点
                return;
            }

            if (node.leftChild != null && node.rightChild != null)
            {// 左右子结点都存在
                BinaryTreeNode minNode = node.rightChild;
                BinaryTreeNode minNodeParent = node;
                while (minNode.leftChild != null &&
                      comparer.Compare(minNode.leftChild.data, minNode.data) < 0)
                {
                    minNodeParent = minNode;
                    minNode = minNode.leftChild;
                }
                node.data = minNode.data;

                // 因为此时 minNode 的卫星数据已经替换了要删除的节点，相当于
                // 要删除的几点与 minNode 互换了位置，因此只需要删除 minNode 即可
                if (minNodeParent.leftChild == minNode) minNodeParent.leftChild = null;
                else minNodeParent.rightChild = null;
            }
            else
            {// 只有一个子结点，或者没有子结点
                BinaryTreeNode child = node.leftChild ?? node.rightChild;
                if (nodeParent == null)
                {// 要删除的为根节点
                    tree = child;
                }
                else
                {
                    if (nodeParent.leftChild == node)
                    {
                        nodeParent.leftChild = child;
                    }
                    else
                    {
                        nodeParent.rightChild = child;
                    }
                }
            }
        }

        public BinaryTreeNode FindNode(T t)
        {
            BinaryTreeNode node = tree;
            while (node != null)
            {
                int comp = comparer.Compare(t, node.data);
                if (comp < 0)
                {
                    node = node.leftChild;
                }
                else if (comp > 0)
                {
                    node = node.rightChild;
                }
                else
                {
                    return node;
                }
            }
            return null;
        }
    }
}
