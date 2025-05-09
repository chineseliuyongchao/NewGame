using System.Collections.Generic;
using Game.GameMenu;
using QFramework;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace UI
{
    public class UIGameSettingData : UIPanelData
    {
    }

    /// <summary>
    /// 设置界面
    /// </summary>
    public partial class UIGameSetting : UIBase
    {
        public List<GameObject> allSettingGroup;
        public List<Button> allLanguageButton;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameSettingData ?? new UIGameSettingData();
            // please add init code here
            base.OnInit(uiData);
            showUnitHp.isOn = this.GetModel<IGameSettingModel>().ShowUnitHp;
            showUnitTroops.isOn = this.GetModel<IGameSettingModel>().ShowUnitTroops;
            showUnitMorale.isOn = this.GetModel<IGameSettingModel>().ShowUnitMorale;
            showUnitFatigue.isOn = this.GetModel<IGameSettingModel>().ShowUnitFatigue;
            showMovementPoints.isOn = this.GetModel<IGameSettingModel>().ShowMovementPoints;
            automaticSwitchingUnit.isOn = this.GetModel<IGameSettingModel>().AutomaticSwitchingUnit;
            allSettingGroup.ForEach(group => group.SetActive(false));
            basicSettingGroup.gameObject.SetActive(true);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameSettingData ?? new UIGameSettingData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            for (int i = 0; i < allLanguageButton.Count; i++)
            {
                var i1 = i;
                allLanguageButton[i].onClick.AddListener(() =>
                {
                    this.GetModel<IGameSettingModel>().Language = i1;
                    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i1];
                });
            }
            showUnitHp.onValueChanged.AddListener(value => { this.GetModel<IGameSettingModel>().ShowUnitHp = value; });
            showUnitTroops.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameSettingModel>().ShowUnitTroops = value;
            });
            showUnitMorale.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameSettingModel>().ShowUnitMorale = value;
            });
            showUnitFatigue.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameSettingModel>().ShowUnitFatigue = value;
            });
            showMovementPoints.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameSettingModel>().ShowMovementPoints = value;
            });
            automaticSwitchingUnit.onValueChanged.AddListener(value =>
            {
                this.GetModel<IGameSettingModel>().AutomaticSwitchingUnit = value;
            });
            leaveButton.onClick.AddListener(CloseSelf);
            basicSettingButton.onClick.AddListener(() =>
            {
                allSettingGroup.ForEach(group => group.SetActive(false));
                basicSettingGroup.gameObject.SetActive(true);
            });
            fightSettingButton.onClick.AddListener(() =>
            {
                allSettingGroup.ForEach(group => group.SetActive(false));
                fightSettingGroup.gameObject.SetActive(true);
            });
        }

        protected override void OnListenEvent()
        {
        }
    }
}