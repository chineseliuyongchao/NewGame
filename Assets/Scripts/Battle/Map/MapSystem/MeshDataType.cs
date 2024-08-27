using System;

namespace Battle.Map
{
    /// <summary>
    /// 用于保存寻路节点的对象
    /// </summary>
    [Serializable]
    public class MeshDataType
    {
        public int x;
        public int y;
        public int width;
        public int height;
    }
}