using Game.Town;
using GameQFramework;
using QFramework;

namespace UI
{
    public class UITownConscriptionData : UIPanelData
    {
        public ConscriptionData data;

        public UITownConscriptionData(ConscriptionData data = null)
        {
            this.data = data;
        }
    }

    /// <summary>
    /// 征兵界面
    /// </summary>
    public partial class UITownConscription : UIBase
    {
        private int _chooseNum;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UITownConscriptionData ?? new UITownConscriptionData();
            // please add init code here
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UITownConscriptionData ?? new UITownConscriptionData();
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
            Slider.onValueChanged.AddListener(value =>
            {
                _chooseNum = (int)(value * mData.data.canConscription.num);
                chooseNum.text = _chooseNum.ToString();
            });
            leaveButton.onClick.AddListener(() =>
            {
                SoldierStructure soldierStructure = new SoldierStructure
                {
                    num = _chooseNum
                };
                mData.data.realConscription(soldierStructure);
                this.GetModel<IGameModel>().PlayerTeam.UpdateTeamNum(soldierStructure);
                CloseSelf();
            });
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            if (mData.data.canConscription.num > 0)
            {
                title.text = "这里有" + mData.data.canConscription.num + "人愿意响应你";
            }
            else
            {
                title.text = "没有人愿意响应你";
            }
        }
    }
}