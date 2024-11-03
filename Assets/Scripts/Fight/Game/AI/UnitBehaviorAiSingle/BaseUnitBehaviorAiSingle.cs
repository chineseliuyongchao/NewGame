using System;
using Fight.Model;
using Game.GameBase;
using QFramework;

namespace Fight.Game.AI
{
    /// <summary>
    /// 一个单位的一次行为
    /// </summary>
    public abstract class BaseUnitBehaviorAiSingle : IController
    {
        /// <summary>
        /// 单位id
        /// </summary>
        public int unitId;

        /// <summary>
        /// 行为类型
        /// </summary>
        public AiBehaviorType behaviorType;

        /// <summary>
        /// 行为是否必须结束，如果必须结束并且剩余行动力不够就不会执行
        /// </summary>
        public bool mustFinish;

        /// <summary>
        /// 记录行为是否结束
        /// </summary>
        protected bool isEnd = false;

        /// <summary>
        /// 开始行为
        /// </summary>
        /// <param name="behaviorEnd"></param>
        public abstract void StartBehavior(Action<bool> behaviorEnd);

        /// <summary>
        /// 行为结束
        /// </summary>
        public abstract void BehaviorEnd();

        /// <summary>
        /// 获取行为的行动类型
        /// </summary>
        /// <returns></returns>
        public abstract ActionType ActionType();

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}