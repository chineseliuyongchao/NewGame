using GameQFramework;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Achieve
{
    /// <summary>
    /// 所有成就的基类
    /// </summary>
    public abstract class BaseAchieve : ICanGetModel
    {
        /// <summary>
        /// 挑战的名字
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 判断挑战是否完成
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckFinish();

        /// <summary>
        /// 挑战完成
        /// </summary>
        public void OnFinish()
        {
            Debug.Log("成就完成：  " + Name);
            SceneManager.LoadScene("PassScene");
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}