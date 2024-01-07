using System;
using QFramework;

namespace GameQFramework
{
    /// <summary>
    /// 用于存取数据
    /// </summary>
    public interface ISaveModel : ICanGetSystem, ICanGetModel
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Object SaveModel();

        /// <summary>
        /// 加载数据
        /// </summary>
        void LoadModel(string data);

        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitializeModel();

        /// <summary>
        /// 初始化新存档需要初始化但是不需要保存在存档中的数据
        /// </summary>
        void NewArchiveInitData();

        /// <summary>
        /// 用于存取数据时的名字
        /// </summary>
        /// <returns></returns>
        string ModelName();
    }
}