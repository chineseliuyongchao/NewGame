using System.Collections;
using System.Threading.Tasks;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameQFramework
{
    public class DelayManager : Singleton<DelayManager>
    {
        private GameObject _goDelayUtil;
        private TaskBehaviour _mTask;
        // ReSharper disable once NotAccessedField.Local
        private Coroutine _mCoroutineWait;

        //内部类
        class TaskBehaviour : MonoBehaviour
        {
            void OnDestroy()
            {
                Instance.Dispose();
            }
        }

        private DelayManager()
        {
        }

        public override void OnSingletonInit()
        {
            GameObject go = new GameObject("#DelayUtil#");
            go.transform.SetParent(UIRoot.Instance.GetTransform(UILevel.Bg));
            _mTask = go.AddComponent<TaskBehaviour>();
            _goDelayUtil = go;
        }

        public Coroutine WaitTime(float time, UnityAction callback)
        {
            return _mTask.StartCoroutine(Coroutine(time, callback));
        }

        public Coroutine WaitNextFrame(UnityAction callback)
        {
            return _mTask.StartCoroutine(Coroutine(callback));
        }

        //取消等待
        public void CancelWait(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                _mTask.StopCoroutine(coroutine);
            }
        }

        IEnumerator Coroutine(float time, UnityAction callback)
        {
            if (time > 0.01)
            {
                yield return new WaitForSeconds(time);
            }

            if (callback != null)
            {
                callback();
            }
        }

        IEnumerator Coroutine(UnityAction callback)
        {
            yield return 0;
            if (callback != null)
            {
                callback();
            }
        }

        public async Task WaitTimeAsync(float time)
        {
            var tcs = new TaskCompletionSource<bool>();
            if (_mTask == null || time <= 0)
            {
                tcs.SetResult(true);
            }
            else
            {
                _mCoroutineWait = _mTask.StartCoroutine(Coroutine(time, () =>
                {
                    tcs.SetResult(true);
                    _mCoroutineWait = null;
                }));
            }

            await tcs.Task;
        }

        public void CancelWaitAsync()
        {
            if (_goDelayUtil != null)
            {
                Object.DestroyImmediate(_goDelayUtil);
                _goDelayUtil = null;
            }
        }
    }
}