using System.Collections.Generic;

namespace SystemTool.Pathfinding
{
    /// <summary>
    /// 用于记录寻路的单次结果信息
    /// </summary>
    public class PathfindingSingleMessage
    {
        public List<PathfindingMapNode> PathfindingResult;

        public PathfindingSingleMessage(List<PathfindingMapNode> pathfindingResult)
        {
            PathfindingResult = pathfindingResult;
        }
    }
}