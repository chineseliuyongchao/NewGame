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

        private void Start()
        {
            OnControllerStart();
        }

        protected virtual void OnControllerStart()
        {
        }

        protected virtual void OnListenButton()
        {
        }

        protected virtual void OnListenEvent()
        {
        }
    }
}