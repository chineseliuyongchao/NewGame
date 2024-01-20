using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 输出日志的节点
    /// </summary>
    public class DebugNode : BaseActionNode
    {
        public string message;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            Debug.Log("OnUpdate:  "+message);
            return BehaviourTreeState.SUCCESS;
        }

        public override string GetDescription()
        {
            return "输出日志";
        }
    }
}