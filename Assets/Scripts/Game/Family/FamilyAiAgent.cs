using Game.BehaviourTree;
using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.Game.Family
{
    /// <summary>
    /// 家族的ai代理
    /// </summary>
    public class FamilyAiAgent : AiAgent
    {
        private int _familyId;
        private FamilyBlackBoard _familyBlackBoard;
        private bool _isInit = false;

        public void Init(int familyId, FamilyBlackBoard familyBlackBoard)
        {
            _familyId = familyId;
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
                Debug.Log(this.GetModel<IFamilyModel>().FamilyData[_familyId].familyName + "家族可以组建军队  ");
                return true;
            }

            return false;
        }
    }
}