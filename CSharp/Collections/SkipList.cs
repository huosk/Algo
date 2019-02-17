using System;

namespace Algo.Collections
{
    // 跳表类型
    public class SkipList<T>
        where T: IComparable<T>
    {
        // 头结点
        private SkipListNode head;

        // 尾节点
        private SkipListNode tail;

        // 最大层数
        private int levelCount;

        private System.Random random;

        private const int MAX_LEVEL_COUNT = 16;

        public SkipList()
        {
            levelCount = 1;
            random = new Random();
            head = new SkipListNode
            {
                levels = new SkipLevel[MAX_LEVEL_COUNT]
            };
            for (int i = 0; i < MAX_LEVEL_COUNT; i++)
            {
                head.levels[i] = new SkipLevel();
            }
        }

        public SkipListNode First{
            get{
                return head.levels[0].forward;
            }
        }

        // 查找指定节点
        public SkipListNode FindNode(T target)
        {
            var node = head;

            // 从最顶层索引开始查找
            for (int i = levelCount - 1; i >= 0; i--)
            {
                while (node.levels[i] != null && node.levels[i].forward.item.CompareTo(target)<0)
                {
                    // 跳转到该层的前进节点
                    node = node.levels[i].forward;
                }
            }

            // 循环退出，说明 node 的下一个节点，要么大于查找目标，要么等于查找目标 

            // 进一步判断是否为查找目标
            if (node.levels[0] != null && node.levels[0].forward.item.CompareTo(target) == 0)
            {
                return node.levels[0].forward;
            }
            else
            {
                return null;
            }
        }

        public void Insert(T t)
        {
            // 获取随机索引层[1,MAX_LEVEL_COUNT]
            int level = RandomLevel();

            // 创建新节点
            SkipListNode node = new SkipListNode();
            node.backward = head;
            node.levels = new SkipLevel[level];
            node.item = t;

            // 在每层中，找到要插入的位置（临界结点），
            // 并且暂时借用新节点的levels,将找到的节点存到其中
            SkipListNode temp = head;
            for (int i = level-1; i >= 0; i--)
            {
                node.levels[i] = new SkipLevel();
                while (temp != null && temp.levels!= null &&
                       temp.levels[i] != null && 
                       temp.levels[i].forward != null && 
                       temp.levels[i].forward.item.CompareTo(t) < 0)
                {
                    temp = temp.levels[i].forward;
                }
                node.levels[i].forward = temp;
            }

            // 然后设置临界结点和新结点的前进结点和后退节点
            for (int i = 0; i < level;i++)
            {
                // 要插入的临界结点
                var bound = node.levels[i].forward;
                temp = bound.levels[i].forward;

                // 设置前进结点
                bound.levels[i].forward = node;
                node.levels[i].forward = temp;

                // 设置后退结点
                node.backward = bound;

                if(temp != null)
                {
                    temp.backward = node;
                }
            }

            if(node.levels[0].forward == null)
            {
                tail = node;
            }
        }

        private int RandomLevel()
        {
            return random.Next(1, MAX_LEVEL_COUNT);
        }

        // 跳表节点
        public class SkipListNode
        {
            // 节点包含的层
            public SkipLevel[] levels;

            // 后退节点
            public SkipListNode backward;

            // 节点数据
            public T item;

            public SkipListNode Forward{
                get{
                    return levels[0].forward;
                }
            }
        }

        // 跳转层
        public class SkipLevel
        {
            // 前进节点
            public SkipListNode forward;
        }
    }


}
