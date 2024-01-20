using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 延时节点
    /// </summary>
    public class WaitNode : BaseActionNode
    {
        public float duration = 1;
        private float _startTime;

        protected override void OnStart()
        {
            _startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            if (Time.time - _startTime > duration)
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.RUNNING;
        }

        public override string GetDescription()
        {
            return "延迟一定时间";
        }
    }
}