using Game.BehaviourTree;
using GameQFramework;
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

        public override bool CanBuildArmy()
        {
            if (!_isInit)
            {
                return base.CanBuildArmy();
            }

            if (this.GetModel<IFamilyModel>().FamilyData[_familyId].familyWealth > 10000)
            {
                return true;
            }

            return false;
        }

        public override bool SelectArmyGeneral()
        {
            if (!_isInit)
            {
                return base.SelectArmyGeneral();
            }

            for (int i = 0; i < _familyData.familyRoleS.Count; i++)
            {
                int roleId = _familyData.familyRoleS[i];
                if (this.GetModel<IFamilyModel>().RoleData[roleId].roleType == RoleType.INACTIVE)
                {
                    _familyBlackBoard.armyGeneralId = roleId;
                    break;
                }
            }

            return true;
        }

        public override bool BuildArmy()
        {
            if (!_isInit)
            {
                return base.BuildArmy();
            }

            if (_familyBlackBoard.armyGeneralId <= 0)
            {
                return false;
            }

            Debug.Log(this.GetModel<IFamilyModel>().FamilyData[_familyId].familyName + "家族组建军队，将领是" +
                      this.GetModel<IFamilyModel>().RoleData[_familyBlackBoard.armyGeneralId].roleName);
            _familyBlackBoard.buildArmy?.Invoke(_familyBlackBoard.armyGeneralId);

            return true;
        }
    }
}