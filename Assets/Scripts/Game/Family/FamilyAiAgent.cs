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
        private int _familyId;
        private FamilyData _familyData;
        private FamilyBlackBoard _familyBlackBoard;
        private bool _isInit;

        public void Init(int familyId, FamilyBlackBoard familyBlackBoard)
        {
            _familyId = familyId;
            _familyData = this.GetModel<IFamilyModel>().FamilyData[_familyId];
            _familyBlackBoard = familyBlackBoard;
            _isInit = true;
        }

        public override bool CanBuildTeam()
        {
            if (!_isInit)
            {
                return base.CanBuildTeam();
            }

            if (this.GetModel<IFamilyModel>().FamilyData[_familyId].familyWealth > 10000)
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

            for (int i = 0; i < _familyData.familyRoleS.Count; i++)
            {
                int roleId = _familyData.familyRoleS[i];
                if (this.GetModel<IFamilyModel>().RoleData[roleId].roleType == RoleType.INACTIVE)
                {
                    _familyBlackBoard.teamGeneralId = roleId;
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

            if (_familyBlackBoard.teamGeneralId <= 0)
            {
                return false;
            }

            Debug.Log(this.GetModel<IFamilyModel>().FamilyData[_familyId].familyName + "家族组建队伍，将领是" +
                      this.GetModel<IFamilyModel>().RoleData[_familyBlackBoard.teamGeneralId].roleName);
            _familyBlackBoard.buildTeam?.Invoke(_familyBlackBoard.teamGeneralId);

            return true;
        }
    }
}