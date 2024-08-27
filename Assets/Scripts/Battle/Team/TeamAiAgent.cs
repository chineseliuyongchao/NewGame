using Game.BehaviourTree;
using UnityEngine;

namespace Battle.Team
{
    /// <summary>
    /// 队伍的ai代理
    /// </summary>
    public class TeamAiAgent : AiAgent
    {
        private TeamBlackBoard _teamBlackBoard;

        public void Init(TeamBlackBoard teamBlackBoard)
        {
            _teamBlackBoard = teamBlackBoard;
        }

        public override bool CanMoveToTown(out int townId)
        {
            townId = Random.Range(1, 4); //暂无具体逻辑，先用随机数代替
            return Random.Range(0, 2) > 0;
        }

        public override void MoveToTown()
        {
            Debug.Log("移动到聚落：  " + _teamBlackBoard.targetTownId);
            _teamBlackBoard?.moveToTown(_teamBlackBoard.targetTownId);
        }

        public override void Patrol()
        {
            Debug.Log("开始巡逻");
            _teamBlackBoard?.patrol(2); //暂时先沿着聚落2巡逻
        }
    }
}