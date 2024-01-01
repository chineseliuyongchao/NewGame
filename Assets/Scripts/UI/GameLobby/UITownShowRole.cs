using GameQFramework;
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
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(int roleId)
        {
            RoleData roleData = this.GetModel<IFamilyModel>().RoleData[roleId];
            roleName.text = roleData.roleName;
            countryName.text = "暂未制作国家系统";
            if (this.GetModel<IFamilyModel>().FamilyData.ContainsKey(roleData.familyId))
            {
                familyName.text = this.GetModel<IFamilyModel>().FamilyData[roleData.familyId].familyName;
            }
            else
            {
                familyName.text = "此人的家族籍籍无名";
            }
        }
    }
}