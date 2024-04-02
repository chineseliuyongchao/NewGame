using System.Collections.Generic;
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

        public void InitFamilyNoStorageData()
        {
            Dictionary<int, FamilyData> familyDataS = this.GetModel<IFamilyModel>().FamilyData;
            List<int> key = new List<int>(familyDataS.Keys);
            for (int i = 0; i < key.Count; i++)
            {
                FamilyData familyData = familyDataS[key[i]];
                FamilyDataNoStorage noStorage = new FamilyDataNoStorage();
                familyData.noStorage = noStorage;
            }
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