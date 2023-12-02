using System.Collections.Generic;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 用于记录寻路的单次结果信息
    /// </summary>
    public class PathfindingSingleMessage
    {
        /// <summary>
        /// 记录每个地图网格点
        /// </summary>
        public List<PathfindingMapNode> PathfindingResult;

        /// <summary>
        /// 记录每个网格点到上一个点的距离
        /// </summary>
        public List<float> Length;

        public PathfindingSingleMessage(List<PathfindingMapNode> pathfindingResult, List<float> length)
        {
            PathfindingResult = pathfindingResult;
            Length = length;
        }

        public override string ToString()
        {
            string toString = "";
            for (int i = 0; i < PathfindingResult.Count; i++)
            {
                toString += PathfindingResult[i].Pos;
                if (Length.Count > i)
                {
                    toString += "  " + Length[i] + "  ";
                }
            }

            return toString;
        }
    }
}