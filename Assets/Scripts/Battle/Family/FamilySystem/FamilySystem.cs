﻿using System.Collections.Generic;
using Game.GameUtils;
using QFramework;
using UnityEngine;

namespace Battle.Family
{
    public class FamilySystem : AbstractSystem, IFamilySystem
    {
        protected override void OnInit()
        {
        }

        public void InitFamilyCommonData(TextAsset textAsset, TextAsset nameTextAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IFamilyModel>().FamilyCommonData);
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(nameTextAsset, this.GetModel<IFamilyModel>().FamilyNameData);
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

        public void InitRoleCommonData(TextAsset textAsset, TextAsset nameTextAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(textAsset, this.GetModel<IFamilyModel>().RoleCommonData);
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(nameTextAsset, this.GetModel<IFamilyModel>().RoleNameData);
        }

        public int AddNewRole(RoleData roleData)
        {
            int roleId = this.GetModel<IFamilyModel>().RoleData.Count + 1;
            this.GetModel<IFamilyModel>().RoleData.Add(roleId, roleData);
            return roleId;
        }

        public int AddNewFamily(FamilyData familyData)
        {
            int familyId = this.GetModel<IFamilyModel>().FamilyData.Count + 1;
            FamilyDataNoStorage noStorage = new FamilyDataNoStorage();
            familyData.noStorage = noStorage;
            this.GetModel<IFamilyModel>().FamilyData.Add(familyId, familyData);
            return familyId;
        }
    }
}