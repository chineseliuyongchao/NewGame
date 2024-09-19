using System.Collections.Generic;
using UnityEngine;

namespace Fight.Tools.Pool
{
    public abstract class UnityObjPool<T> : IObjectPool<T> where T : Object
    {
        protected readonly Queue<T> availableObjects = new Queue<T>();

        public T Get()
        {
            if (availableObjects.Count > 0)
            {
                T result = availableObjects.Dequeue();
                Reset(result);
                return result;
            }

            return CreateNewObject();
        }

        protected abstract T CreateNewObject();
        protected abstract void Reset(T obj);

        public virtual void Release(T obj)
        {
            availableObjects.Enqueue(obj);
        }

        public virtual void Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                availableObjects.Enqueue(CreateNewObject());
            }
        }

        public void Clear()
        {
            availableObjects.Clear();
        }

        public int AvailableCount()
        {
            return availableObjects.Count;
        }
    }
}