using Game.Town;
using GameQFramework;
using QFramework;

namespace UI
{
    public class UITownData : UIPanelData
    {
        public readonly int townId;

        public UITownData(int townId = 0)
        {
            this.townId = townId;
        }
    }

    /// <summary>
    /// 聚落界面
    /// </summary>
    public partial class UITown : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UITownData ?? new UITownData();
            // please add init code here
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UITownData ?? new UITownData();
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
            showRoleButton.onClick.AddListener(() =>
            {
                UIKit.OpenPanel<UITownRole>(new UITownRoleData(mData.townId));
            });
            conscriptionButton.onClick.AddListener(() =>
            {
                ConscriptionData data = this.GetSystem<ITownSystem>().Conscription(mData.townId);
                UIKit.OpenPanel<UITownConscription>(new UITownConscriptionData(data));
            });
            leaveButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            TownData townData = this.GetModel<ITownModel>().TownData[mData.townId];
            townName.text = townData.storage.name;
            prosperityValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.noStorage.prosperity, 5);
            populationValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.storage.GetPopulation(), 5);
            levelValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.storage.level, 5);
            militiaValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.storage.militiaNum, 5);
            introduce.text = "这个聚落属于" + this.GetModel<ICountryModel>().CountryData[townData.storage.countryId].name + "的" +
                             this.GetModel<IFamilyModel>().FamilyData[townData.storage.familyId].familyName + "家族统治";
        }
    }
}