using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 用于处理json文件
    /// </summary>
    public abstract class JsonParser
    {
        public static T[] ParseJson<T>(string jsonString) where T : BaseJsonData
        {
            // 将JSON数据转换成指定类型的对象数组
            JsonWrapper<T> jsonWrapper = JsonUtility.FromJson<JsonWrapper<T>>(jsonString);
            return jsonWrapper?.Sheet1;
        }
    }

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
        public int ID;

        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
    }
}