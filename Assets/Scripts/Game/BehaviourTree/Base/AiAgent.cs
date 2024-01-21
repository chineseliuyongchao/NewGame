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

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}