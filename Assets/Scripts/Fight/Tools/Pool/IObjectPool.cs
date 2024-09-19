namespace Fight.Tools.Pool
{
    public interface IObjectPool<T>
    {
        // 获取一个对象
        T Get();

        // 释放一个对象回到对象池
        void Release(T obj);

        // 预加载一定数量的对象到对象池中
        void Preload(int count);

        // 清空对象池
        void Clear();

        // 获取当前池中可用对象的数量
        int AvailableCount();
    }
}