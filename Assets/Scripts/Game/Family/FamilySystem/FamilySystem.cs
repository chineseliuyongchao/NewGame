using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Game.Family
{
    public class FamilySystem : AbstractSystem, IFamilySystem
    {
        protected override void OnInit()
        {
        }

        public void InitFamilyCommonData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IFamilyModel>().FamilyCommonData);
        }

        public void InitRoleCommonData(TextAsset textAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IFamilyModel>().RoleCommonData);
        }

        public int AddNewRole(RoleData roleData)
        {
            int roleId = this.GetModel<IFamilyModel>().RoleData.Count + 1;
            this.GetModel<IFamilyModel>().RoleData.Add(roleId, roleData);
            return roleId;
        }
    }
}