using System.Collections.Generic;
using QFramework;

namespace Game.GameBase
{
    public interface IGameSystem : ISystem
    {
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
    }
}