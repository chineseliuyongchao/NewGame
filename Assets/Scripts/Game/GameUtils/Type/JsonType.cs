namespace Game.GameUtils
{
    /// <summary>
    /// 所有的json文件对应的类，数组名字对应Sheet1
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    class JsonWrapper<T>
    {
        // ReSharper disable once InconsistentNaming
        public T[] Sheet1;
    }

    /// <summary>
    /// 所有json数据对象的基类
    /// </summary>
    public class BaseJsonData
    {
        /// <summary>
        /// 编号
        /// </summary>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnassignedField.Global
        public int ID;
    }

    /// <summary>
    /// 所有名字json数据对象的基类
    /// </summary>
    public class BaseNameJsonData : BaseJsonData
    {
        /// <summary>
        /// 中文名字
        /// </summary>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnassignedField.Global
        public string Chinese;

        /// <summary>
        /// 英文名字
        /// </summary>
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnassignedField.Global
        public string English;
    }
}