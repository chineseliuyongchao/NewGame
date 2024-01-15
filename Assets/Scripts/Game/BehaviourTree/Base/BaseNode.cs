using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 行为树节点的基类
    /// </summary>
    public abstract class BaseNode : ScriptableObject
    {
        public BehaviourTreeState behaviourTreeState = BehaviourTreeState.RUNNING;
        public bool started;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        /// <summary>
        /// 更新行为
        /// </summary>
        /// <returns></returns>
        public BehaviourTreeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            behaviourTreeState = OnUpdate();
            if (behaviourTreeState == BehaviourTreeState.FAILURE || behaviourTreeState == BehaviourTreeState.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return behaviourTreeState;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract BehaviourTreeState OnUpdate();

        public virtual BaseNode Clone()
        {
            return Instantiate(this);
        }
    }

    public enum BehaviourTreeState
    {
        /// <summary>
        /// 行为正在执行
        /// </summary>
        RUNNING,

        /// <summary>
        /// 行为失败
        /// </summary>
        FAILURE,

        /// <summary>
        /// 行为成功
        /// </summary>
        SUCCESS
    }
}