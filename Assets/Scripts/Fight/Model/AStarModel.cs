using System.Collections.Generic;
using Fight.Utils;
using Pathfinding;
using QFramework;
using UnityEngine;

namespace Fight.Model
{
    /**
     * 存放a*的相关表格信息
     */
    public class AStarModel : AbstractModel, IAStarModel
    {
        /// <summary>
        ///     A*中节点的index对应世界的节点的index
        /// </summary>
        private readonly SortedList<uint, int> _aStarNodeToWorldNode = new();

        private readonly SortedList<int, GridNodeBase> _fightGridNodeInfoList = new();

        public SortedList<int, GridNodeBase> FightGridNodeInfoList => _fightGridNodeInfoList;

        protected override void OnInit()
        {
        }

        public void InitStarData()
        {
            for (var i = Constants.FightNodeHeightNum - 1; i >= 0; i--)
            {
                var index = Constants.FightNodeHeightNum - i - 1;
                for (var j = 0; j < Constants.FightNodeWidthNum; j++)
                {
                    var index2 = j + i * Constants.FightNodeWidthNum;
                    var nodeBase = AstarPath.active.data.gridGraph.nodes[index2];
                    var position = (Vector3)nodeBase.position;
                    if (position.x < -Constants.FightSceneWorldWidthHalf - Constants.FightNodeWidthHalf ||
                        position.x > Constants.FightSceneWorldWidthHalf + Constants.FightNodeWidthHalf ||
                        position.y < -Constants.FightSceneWorldHeightHalf - Constants.FightNodeHeightHalf +
                        Constants.FightNodeHeightOffset ||
                        position.y > Constants.FightSceneWorldHeightHalf + Constants.FightNodeHeightHalf -
                        Constants.FightNodeHeightOffset)
                        continue;

                    _fightGridNodeInfoList[index] = nodeBase;
                    // nodeBase.NodeInGridIndex = index;
                    _aStarNodeToWorldNode[nodeBase.NodeIndex] = index;
                    index += Constants.FightNodeHeightNum;
                }
            }
        }

        public GridNodeBase GetGridNode(Vector3 position)
        {
            var info = AstarPath.active.data.gridGraph.GetNearest(position);
            return _fightGridNodeInfoList[_aStarNodeToWorldNode[info.node.NodeIndex]];
        }

        public Vector3 GetGridNodePosition(Vector3 position)
        {
            var nodeBase = GetGridNode(position);
            return (Vector3)nodeBase.position;
        }

        public Vector3 GetUnitRelayPosition(UnitData unitData)
        {
            return (Vector3)FightGridNodeInfoList[unitData.currentPosition].position;
        }

        public int GetGridNodeIndexMyRule(Vector3 position)
        {
            var info = AstarPath.active.data.gridGraph.GetNearest(position);
            if (_aStarNodeToWorldNode.TryGetValue(info.node.NodeIndex, out int result))
            {
                return result;
            }

            return 0;
            // return _aStarNodeToWorldNode[info.node.NodeIndex];
        }

        public void FindNodePath(int index, Vector3 position, OnPathDelegate callBack)
        {
            if (FightGridNodeInfoList.TryGetValue(index, out var nodeBase))
            {
                AstarPath.StartPath(ABPath.Construct((Vector3)nodeBase.position, position, callBack));
            }
        }
    }
}