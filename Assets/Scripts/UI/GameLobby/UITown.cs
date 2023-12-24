using GameQFramework;
using QFramework;

namespace UI
{
    public class UITownData : UIPanelData
    {
        public readonly string townName;

        public UITownData(string townName = "")
        {
            this.townName = townName;
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
            leaveButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            townName.text = mData.townName;
            TownData townData = this.GetModel<ITownModel>().TownData[mData.townName];
            wealthValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.wealth, 5);
            populationValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.population, 5);
            levelValue.text = this.GetUtility<IGameUtility>().NumToKmbt(townData.level, 5);
        }
    }
}