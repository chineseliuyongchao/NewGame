using GameQFramework.FamilyModel;
using QFramework;
using UnityEngine;

namespace GameQFramework
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
    }
}