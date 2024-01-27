using QFramework;

namespace GameQFramework
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
    }
}