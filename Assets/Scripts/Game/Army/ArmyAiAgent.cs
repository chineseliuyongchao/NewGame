using Game.BehaviourTree;
using UnityEngine;

namespace Game.Army
{
    /// <summary>
    /// 军队的ai代理
    /// </summary>
    public class ArmyAiAgent : AiAgent
    {
        private ArmyBlackBoard _armyBlackBoard;

        public void Init(ArmyBlackBoard armyBlackBoard)
        {
            _armyBlackBoard = armyBlackBoard;
        }

        public override bool CanMoveToTown(out int townId)
        {
            townId = Random.Range(1, 4); //暂无具体逻辑，先用随机数代替
            return Random.Range(0, 2) > 0;
        }

        public override void MoveToTown()
        {
            Debug.Log("移动到聚落：  " + _armyBlackBoard.targetTownId);
            _armyBlackBoard?.moveToTown(_armyBlackBoard.targetTownId);
        }

        public override void Patrol()
        {
            Debug.Log("开始巡逻");
        }
    }
}