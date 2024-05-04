using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Game.GameBase
{
    public interface IGameSystem : ISystem
    {
        /// <summary>
        /// 初始化多语种数据
        /// </summary>
        void InitLocalizationData(TextAsset localizationAsset, TextAsset dialogueLocalizationAsset,
            TextAsset dialogueTipLocalizationAsset);

        /// <summary>
        /// 切换到菜单场景
        /// </summary>
        public void ChangeMenuScene();

        /// <summary>
        /// 切换到游戏场景
        /// </summary>
        /// <param name="fileName">存档文件名</param>
        public void ChangeMainGameScene(string fileName = null);

        /// <summary>
        /// 切换到创建游戏的场景
        /// </summary>
        public void ChangeGameCreateScene();

        /// <summary>
        /// 获取数据类型对应语言的名字
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        public string GetDataName(List<string> nameList);

        /// <summary>
        /// 获取展示的文本（用于支持多语种）
        /// </summary>
        /// <param name="textId">文本Id</param>
        /// <param name="replace">替换内容</param>
        /// <param name="type">多语种文本种类</param>
        /// <returns></returns>
        string GetLocalizationText(int textId, List<string> replace = null,
            LocalizationType type = LocalizationType.NORMAL);
    }
}