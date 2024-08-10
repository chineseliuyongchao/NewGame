using QFramework;
using UnityEngine;

namespace Game.GameMenu
{
    public interface IGameMenuSystem : ISystem
    {
        /// <summary>
        /// 初始化兵种数据
        /// </summary>
        /// <param name="textAsset"></param>
        void InitArmData(TextAsset textAsset);
    }
}