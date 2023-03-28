using System.Collections.Generic;

namespace Frame
{
    /// <summary>
    /// 普通类对象池数据
    /// </summary>
    public class ObjectPoolData
    {
        //对象容器
        public Queue<object> poolQueue = new Queue<object>();

        public ObjectPoolData(object obj)
        {
            PushObj(obj);
        }

        /// <summary>
        /// 将对象放进对象池
        /// </summary>
        /// <param name="obj"></param>
        public void PushObj(object obj)
        {
            poolQueue.Enqueue(obj);
        }

        /// <summary>
        /// 从对象池中获取对象
        /// </summary>
        /// <returns></returns>
        public object GetObj()
        {
            return poolQueue.Dequeue();
        }
    }
}

