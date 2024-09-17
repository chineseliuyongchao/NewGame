using System;
using Game.GameBase;
using QFramework;

namespace Fight.Game.Legion
{
    /// <summary>
    /// 军队基类，不需要加载到游戏物体
    /// </summary>
    public abstract class BaseLegion : IController
    {
        public int legionId;
        protected Action<int> actionEnd;

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public virtual void Init(int id)
        {
            legionId = id;
            OnListenEvent();
        }

        protected virtual void OnListenEvent()
        {
        }

        /// <summary>
        /// 开始行动
        /// </summary>
        public virtual void StartAction(Action<int> action)
        {
            actionEnd = action;
            //暂时没有行动相关逻辑，直接调用结束
            EndAction();
        }

        /// <summary>
        /// 结束行动
        /// </summary>
        protected virtual void EndAction()
        {
            if (actionEnd != null)
            {
                actionEnd(legionId);
                actionEnd = null;
            }
        }
    }
}