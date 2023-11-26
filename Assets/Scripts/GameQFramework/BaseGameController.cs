using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IBaseGameController : IController
    {
    }

    public class BaseGameController : MonoBehaviour, IBaseGameController
    {
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        void Awake()
        {
            // please add init code here
            OnInit();
        }

        protected virtual void OnInit()
        {
            OnListenButton();
            OnListenEvent();
        }

        protected virtual void OnListenButton()
        {
        }

        protected virtual void OnListenEvent()
        {
        }
    }
}