using QFramework;
using UnityEngine;

namespace Game.GameBase
{
    public interface IBaseGameController : IController
    {
    }

    public class BaseGameController : MonoBehaviour, IBaseGameController
    {
        static readonly protected ResLoader resLoader = ResLoader.Allocate();

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
            OnListenEvent();
        }

        private void Start()
        {
            OnControllerStart();
        }

        protected virtual void OnControllerStart()
        {
        }

        protected virtual void OnListenEvent()
        {
        }
    }
}