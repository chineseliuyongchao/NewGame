using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 延时节点
    /// </summary>
    public class WaitNode : BaseActionNode
    {
        public float durtion = 1;
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
            if (Time.time - _startTime > durtion)
            {
                return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.RUNNING;
        }
    }
}