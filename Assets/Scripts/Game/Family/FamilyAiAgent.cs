using Game.BehaviourTree;
using QFramework;
using UnityEngine;

namespace Game.Family
{
    /// <summary>
    /// 家族的ai代理
    /// </summary>
    public class FamilyAiAgent : AiAgent
    {
        private FamilyBlackBoard _blackBoard;
        private bool _isInit;

        public void Init(FamilyBlackBoard familyBlackBoard)
        {
            _blackBoard = familyBlackBoard;
            _isInit = true;
        }

        public override bool CanBuildTeam()
        {
            if (!_isInit)
            {
                return base.CanBuildTeam();
            }

            if (_blackBoard.familyData.storage.familyWealth > 10000)
            {
                return true;
            }

            return false;
        }

        public override bool SelectTeamGeneral()
        {
            if (!_isInit)
            {
                return base.SelectTeamGeneral();
            }

            for (int i = 0; i < _blackBoard.familyData.storage.familyRoleS.Count; i++)
            {
                int roleId = _blackBoard.familyData.storage.familyRoleS[i];
                if (this.GetModel<IFamilyModel>().RoleData[roleId].roleType == RoleType.INACTIVE)
                {
                    _blackBoard.teamGeneralId = roleId;
                    break;
                }
            }

            return true;
        }

        public override bool BuildTeam()
        {
            if (!_isInit)
            {
                return base.BuildTeam();
            }

            if (_blackBoard.teamGeneralId <= 0)
            {
                return false;
            }

            Debug.Log(_blackBoard.familyData.storage.familyName + "家族组建队伍，将领是" +
                      this.GetModel<IFamilyModel>().RoleData[_blackBoard.teamGeneralId].roleName);
            _blackBoard.buildTeam?.Invoke(_blackBoard.teamGeneralId);

            return true;
        }
    }
}