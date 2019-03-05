namespace Algo.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HashMap<TKey, TValue>
    {
        public struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }

        private int[] buckets;
        private Entry[] entries;
        private IEqualityComparer<TKey> comparer;
        private int freeCount;
        private int freeList;
        private int count;

        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new System.ArgumentNullException("key");
            }

            int hashCode = this.comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % this.buckets.Length;

            int entryIndex = this.buckets[targetBucket];

            int lastNode = -1;

            if (this.buckets != null)
            {
                while (entryIndex >= 0)
                {
                    if (this.entries[entryIndex].hashCode == hashCode &&
                        this.comparer.Equals(this.entries[entryIndex].key, key))
                    {
                        // 找到要删除的元素
                        if (lastNode < 0)
                        {
                            // 要删除的结点为头结点
                            this.buckets[targetBucket] = this.entries[entryIndex].next;
                        }
                        else
                        {
                            // 要删除的前置节点，与后置结点链接
                            this.entries[lastNode].next = this.entries[entryIndex].next;
                        }

                        // 释放结点
                        this.entries[entryIndex].hashCode = -1;

                        // 空闲结点也是以链式进行存储，将新的空闲结点指向当先空闲链头
                        this.entries[entryIndex].next = this.freeList;
                        this.entries[entryIndex].key = default(TKey);
                        this.entries[entryIndex].value = default(TValue);

                        // 更新空闲结点的头指针
                        this.freeList = entryIndex;
                        this.freeCount++;

                        return true;
                    }

                    entryIndex = this.entries[entryIndex].next;
                }
            }

            return false;
        }

        private void Insert(TKey key, TValue value)
        {
            if (key == null)
            {
                return;
            }

            // 舍弃符号位
            int hashCode = this.comparer.GetHashCode(key) & 0x7FFFFFFF;

            // 根据散列值，计算槽位
            int targetBucket = hashCode % this.buckets.Length;

            int entryIndex = this.buckets[targetBucket];

            // 检查散列表是否已经存在要插入的键值
            while (entryIndex >= 0)
            {
                if (this.entries[entryIndex].hashCode == hashCode &&
                    this.comparer.Equals(this.entries[entryIndex].key, key))
                {
                    // 相同的键值已经插入到散列表中
                    return;
                }

                entryIndex = this.entries[entryIndex].next;
            }

            // 插入新值
            int index = 0;

            // 动态扩容 - 开始
            if (this.freeCount > 0)
            {
                index = this.freeList;
                this.freeList = this.entries[index].next;
                this.freeCount--;
            }
            else
            {
                if (this.count == this.entries.Length)
                {
                    // 扩容
                    this.Resize();
                    targetBucket = hashCode % this.buckets.Length;
                }

                index = this.count;
                this.count++;
            }

            // 动态扩容 - 结束
            this.entries[index].hashCode = hashCode;     // 保存散列值
            this.entries[index].next = this.buckets[targetBucket];    // 将next 指向当前的链头
            this.entries[index].key = key;
            this.entries[index].value = value;
            this.buckets[targetBucket] = index; // 将链头指向新插入的元素
        }

        private void Resize()
        {
        }
    }
}
