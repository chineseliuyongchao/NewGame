using System;
using System.Collections.Generic;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 随机选择节点，随机选择所有的节点，直到有一个成功
    /// </summary>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class RandomSelectorNode : BaseCompositeNode
    {
        /// <summary>
        /// 记录每一个子节点是否执行
        /// </summary>
        private List<bool> _executedNodes;

        /// <summary>
        /// 记录执行过的子节点的数量
        /// </summary>
        private int _executeNum;

        private int _current;

        protected override void OnStart()
        {
            if (_executedNodes == null)
            {
                _executedNodes = new List<bool>();
                for (int i = 0; i < children.Count; i++)
                {
                    _executedNodes.Add(false);
                }
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    _executedNodes[i] = false;
                }
            }

            _executeNum = 0;
            ComputeNewCurrent();
        }

        protected override void OnStop()
        {
        }

        protected override BehaviourTreeState OnUpdate()
        {
            var child = children[_current];
            switch (child.Update())
            {
                case BehaviourTreeState.RUNNING:
                    return BehaviourTreeState.RUNNING;
                case BehaviourTreeState.FAILURE:
                    if (!ComputeNewCurrent())
                    {
                        return BehaviourTreeState.FAILURE;
                    }

                    break;
                case BehaviourTreeState.SUCCESS:
                    return BehaviourTreeState.SUCCESS;
            }

            return BehaviourTreeState.RUNNING;
        }

        /// <summary>
        /// 随机寻找一个子节点
        /// </summary>
        /// <returns>是否能够找到一个没有被执行的子节点</returns>
        protected virtual bool ComputeNewCurrent()
        {
            Random random = new Random();
            while (true)
            {
                if (_executeNum == children.Count)
                {
                    return false;
                }

                int num = random.Next(children.Count);
                if (_executedNodes[num])
                {
                    continue;
                }

                _current = num;
                _executedNodes[num] = true;
                _executeNum++;
                return true;
            }
        }

        public override string GetDescription()
        {
            return "随机执行所有子节点，直到有子节点成功";
        }
    }
}