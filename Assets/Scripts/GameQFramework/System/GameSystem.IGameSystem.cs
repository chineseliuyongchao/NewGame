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
        public void ChangeMainGameScene(string fileName = null);
    }
}