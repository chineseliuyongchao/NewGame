using UnityEngine;
using UnityEngine.Events;

namespace Fight.Tools.Pool
{
    public sealed class GameObjectPool : UnityObjPool<GameObject>
    {
        private readonly GameObject _cloned;
        private readonly Transform _parent;

        public UnityAction<GameObject> extraCreat;
        public UnityAction<GameObject> extraReset;
        public UnityAction<GameObject> extraRelease;

        public GameObjectPool(GameObject cloned, Transform parent, int max)
        {
            _cloned = cloned;
            _parent = parent;
            Preload(max);
        }

        protected override GameObject CreateNewObject()
        {
            GameObject result = Object.Instantiate(_cloned, _parent);
            extraCreat?.Invoke(result);
            return result;
        }

        protected override void Reset(GameObject obj)
        {
            obj.SetActive(true);
            extraReset?.Invoke(obj);
        }

        public override void Release(GameObject obj)
        {
            extraRelease?.Invoke(obj);
            obj.SetActive(false);
            base.Release(obj);
        }

        public override void Preload(int count)
        {
            base.Preload(count);
            foreach (GameObject obj in availableObjects)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}