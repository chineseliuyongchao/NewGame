using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 行为树节点的基类
    /// </summary>
    public abstract class BaseNode : ScriptableObject
    {
        public BehaviourTreeState behaviourTreeState = BehaviourTreeState.INACTIVE;
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
                behaviourTreeState = BehaviourTreeState.INACTIVE;
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

        public virtual string GetDescription()
        {
            return "这是一个节点";
        }
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