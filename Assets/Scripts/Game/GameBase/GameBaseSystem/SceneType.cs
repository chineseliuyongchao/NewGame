namespace Game.GameBase
{
    /// <summary>
    /// 记录所有场景的类型
    /// </summary>
    public enum SceneType
    {
        /// <summary>
        /// 打开游戏时的默认场景
        /// </summary>
        MENU_SCENE,

        /// <summary>
        /// 创建战役的场景
        /// </summary>
        CREATE_GAME_SCENE,

        /// <summary>
        /// 战役场景
        /// </summary>
        MAIN_GAME_SCENE,

        /// <summary>
        /// 创建战斗的场景
        /// </summary>
        CREATE_FIGHT_SCENE,

        /// <summary>
        /// 战斗场景
        /// </summary>
        FIGHT_SCENE
    }
}