using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 行为树节点的基类
    /// </summary>
    public abstract class BaseNode : ScriptableObject
    {
        public BehaviourTreeState treeState = BehaviourTreeState.INACTIVE;
        public bool started;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public Blackboard blackboard;
        [HideInInspector] public AiAgent aiAgent;
        [TextArea] public string description;

        /// <summary>
        /// 更新行为
        /// </summary>
        /// <returns></returns>
        public BehaviourTreeState Update()
        {
            if (treeState == BehaviourTreeState.FAILURE || treeState == BehaviourTreeState.SUCCESS)
            {
                //如果节点已经执行完，就直接返回执行完的状态
                return treeState;
            }

            if (!started)
            {
                OnStart();
                started = true;
            }

            treeState = OnUpdate();
            if (treeState == BehaviourTreeState.FAILURE || treeState == BehaviourTreeState.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return treeState;
        }

        /// <summary>
        /// 重置状态
        /// </summary>
        public virtual void Resetting()
        {
            treeState = BehaviourTreeState.INACTIVE;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract BehaviourTreeState OnUpdate();

        public virtual BaseNode Clone()
        {
            return Instantiate(this);
        }

        public abstract string GetDescription();
    }

    public enum BehaviourTreeState
    {
        /// <summary>
        /// 闲置状态
        /// </summary>
        INACTIVE,

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