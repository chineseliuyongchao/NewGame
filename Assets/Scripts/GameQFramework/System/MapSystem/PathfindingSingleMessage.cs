using System.Collections.Generic;

namespace GameQFramework
{
    /// <summary>
    /// 用于记录寻路的单次结果信息
    /// </summary>
    public class PathfindingSingleMessage
    {
        /// <summary>
        /// 记录每个地图网格点
        /// </summary>
        public List<PathfindingMapNode> pathfindingResult;

        /// <summary>
        /// 记录每个网格点到上一个点的距离
        /// </summary>
        public List<float> length;

        public PathfindingSingleMessage(List<PathfindingMapNode> pathfindingResult, List<float> length)
        {
            this.pathfindingResult = pathfindingResult;
            this.length = length;
        }

        public override string ToString()
        {
            string toString = "";
            for (int i = 0; i < pathfindingResult.Count; i++)
            {
                toString += pathfindingResult[i].nodeRect;
                if (length.Count > i)
                {
                    toString += "  " + length[i] + "  ";
                }
            }

            return toString;
        }
    }
}