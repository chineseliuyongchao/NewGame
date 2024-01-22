using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// ai代理类
    /// </summary>
    public class AiAgent : MonoBehaviour, IController
    {
        /// <summary>
        /// 是否可以组建新的军队
        /// </summary>
        /// <returns></returns>
        public virtual bool CanBuildArmy()
        {
            return false;
        }

        /// <summary>
        /// 为将要组建的军队挑选一位将军
        /// </summary>
        /// <returns>是否挑选成功</returns>
        public virtual bool SelectArmyGeneral()
        {
            return false;
        }

        /// <summary>
        /// 组建一支军队
        /// </summary>
        /// <returns>是否组建成功</returns>
        public virtual bool BuildArmy()
        {
            return false;
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}