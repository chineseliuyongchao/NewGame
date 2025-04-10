using System;
using System.Collections.Generic;
using Battle.Country;
using Battle.Family;
using Game.Dialogue;
using Game.GameBase;
using QFramework;

namespace UI
{
    public class UITownShowRoleData : UIPanelData
    {
    }

    /// <summary>
    /// 在聚落中查看角色时展示每个角色
    /// </summary>
    public partial class UITownShowRole : UIBase
    {
        /// <summary>
        /// UITownRole界面一行展示几个UITownShowRole
        /// </summary>
        public const int UI_TOWN_SHOW_ROLE_ROW = 6;

        /// <summary>
        /// 一个UITownShowRole占的高度
        /// </summary>
        public const float UI_TOWN_SHOW_ROLE_HEIGHT = 350;

        private void Awake()
        {
            OnInit();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UITownShowRoleData ?? new UITownShowRoleData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UITownShowRoleData ?? new UITownShowRoleData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        protected override void OnListenButton()
        {
            dialogue.onClick.AddListener(() =>
            {
                List<string> dialogueValue = new List<string>();
                List<Action> dialogueAction = new List<Action>();
                UIKit.OpenPanel<UIDialogue>(new UIDialogueData(DialogueConstant.NEW_DIALOGUE_TREE, dialogueValue,
                    dialogueAction));
            });
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(int roleId)
        {
            RoleData roleData = this.GetModel<IFamilyModel>().RoleData[roleId];
            roleName.text = this.GetSystem<IGameSystem>().GetDataName(roleData.name);
            if (this.GetModel<IFamilyModel>().FamilyData.ContainsKey(roleData.familyId))
            {
                FamilyData familyData = this.GetModel<IFamilyModel>().FamilyData[roleData.familyId];
                familyName.text = this.GetSystem<IGameSystem>().GetDataName(familyData.storage.name);
                countryName.text = this.GetSystem<IGameSystem>()
                    .GetDataName(this.GetModel<ICountryModel>().CountryData[familyData.storage.countryId].name);
            }
            else
            {
                familyName.text = this.GetSystem<IGameSystem>().GetLocalizationText(4);
                countryName.text = this.GetSystem<IGameSystem>().GetLocalizationText(5);
            }
        }
    }
}