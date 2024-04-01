using System.Collections.Generic;
using GameQFramework;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UITownRoleData : UIPanelData
    {
        public readonly int townId;

        public UITownRoleData(int townId = 0)
        {
            this.townId = townId;
        }
    }

    /// <summary>
    /// 在聚落中查看所有角色的界面
    /// </summary>
    public partial class UITownRole : UIBase
    {
        public UITownShowRole uiTownShowRole;
        private List<UITownShowRole> _uiTownShowRoles;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UITownRoleData ?? new UITownRoleData();
            // please add init code here
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UITownRoleData ?? new UITownRoleData();
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
            leaveButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            _uiTownShowRoles = new List<UITownShowRole>();
            TownDataStorage townDataStorage = this.GetModel<ITownModel>().TownData[mData.townId].storage;
            for (int i = 0; i < townDataStorage.townRoleS.Count; i++)
            {
                UITownShowRole townShowRole = Instantiate(uiTownShowRole, Content);
                townShowRole.InitUI(townDataStorage.townRoleS[i]);
                _uiTownShowRoles.Add(townShowRole);
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            Vector2 size = Content.sizeDelta;
            // ReSharper disable once PossibleLossOfFraction
            size.y = (_uiTownShowRoles.Count + UITownShowRole.UI_TOWN_SHOW_ROLE_ROW - 1) /
                UITownShowRole.UI_TOWN_SHOW_ROLE_ROW * UITownShowRole.UI_TOWN_SHOW_ROLE_HEIGHT;
            Content.GetComponent<RectTransform>().sizeDelta = size;
        }

        protected override Transform AnimTransform()
        {
            return root;
        }
    }
}