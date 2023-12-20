using System.Collections.Generic;
using QFramework;

namespace GameQFramework
{
    /// <summary>
    /// 游戏保存相关
    /// </summary>
    public interface IGameSaveSystem : ISystem
    {
        /// <summary>
        /// 保存游戏
        /// </summary>
        void SaveGame(string fileName);

        /// <summary>
        /// 读取游戏
        /// </summary>
        void LoadGame(string fileName);

        /// <summary>
        /// 删除游戏
        /// </summary>
        void DeleteGame(string fileName);

        /// <summary>
        /// 添加需要存取数据的对象
        /// </summary>
        /// <param name="saveModel"></param>
        void AddSaveModel(ISaveModel saveModel);

        /// <summary>
        /// 获取游戏存档的记录
        /// </summary>
        /// <returns></returns>
        List<string> LoadGameFileList();
    }
}