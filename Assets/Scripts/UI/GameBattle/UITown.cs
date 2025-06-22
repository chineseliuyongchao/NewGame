using System.Collections.Generic;
using Battle.Country;
using Battle.Family;
using Battle.Town;
using Game.GameBase;
using Game.GameUtils;
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

        protected override void OnListenButton()
        {
            importantPeople.onClick.AddListener(OnButtonRoleClick);
            government.onClick.AddListener(OnButtonGovernmentClick);
            governmentWorkshop.onClick.AddListener(OnButtonBuildingClick);
            barracks.onClick.AddListener(OnButtonScriptionClick);
            shop.onClick.AddListener(OnButtonShopClick);
            leaveButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            TownData townData = this.GetModel<ITownModel>().TownData[mData.townId];
            countyName.text = this.GetSystem<IGameSystem>().GetDataName(townData.storage.name); //县名字，后面改这个
            countyName.text = "高柳县";
            countyIntroduce.text = this.GetSystem<IGameSystem>().GetLocalizationText(1, new List<string>
            {
                this.GetSystem<IGameSystem>()
                    .GetDataName(this.GetModel<ICountryModel>().CountryData[townData.storage.countryId].name),
                this.GetSystem<IGameSystem>()
                    .GetDataName(this.GetModel<IFamilyModel>().FamilyData[townData.storage.familyId].storage.name)
            }); //可以在这个基础上改
            countyIntroduce.text = "高柳县属于幽州的代郡，（并且是代郡的郡治）。这里属于汉朝廷统治。（加一些简介，不超过两行）。"; //县城简介
            populationValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.GetPopulation(), 5); //人口
            foodSituationValue.text = "非常充足"; //食物情况
            garrisonValue.text = "1.2万"; //驻军
            prosperityValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.noStorage.prosperity, 5); //繁荣度
            publicOrderValue.text = 80.ToString(); //治安度
            loyaltyValue.text = 70.ToString(); //忠诚度
        }

        #region 按钮点击

        private void OnButtonRoleClick()
        {
            UIKit.OpenPanel<UITownRole>(new UITownRoleData(mData.townId));
        }
        
        private void OnButtonGovernmentClick()
        {
            // UIKit.OpenPanel<UITownRole>(new UITownRoleData(mData.townId));
        }

        private void OnButtonBuildingClick()
        {
            UIKit.OpenPanel<UITownBuilding>();
        }

        private void OnButtonScriptionClick()
        {
            ConscriptionData data = this.GetSystem<ITownSystem>().Conscription(mData.townId);
            UIKit.OpenPanel<UITownConscription>(new UITownConscriptionData(data));
        }

        private void OnButtonShopClick()
        {
            UIKit.OpenPanel<UITownShop>();
        }

        #endregion
    }
}