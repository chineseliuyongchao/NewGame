using GameQFramework;
using QFramework;

namespace UI
{
    public class TimeNodeData : UIPanelData
    {
    }

    public partial class TimeNode : UIBase
    {
        private void Awake()
        {
            OnInit();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as TimeNodeData ?? new TimeNodeData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as TimeNodeData ?? new TimeNodeData();
            // please add init code here
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
            this.RegisterEvent<QuarterChangeEvent>(e =>
            {
                timeView.text = this.GetModel<IGameModel>().Year + "年_" + this.GetModel<IGameModel>().Month + "月_" +
                                this.GetModel<IGameModel>().Day + "日_" + this.GetModel<IGameModel>().Time + "时_" +
                                this.GetModel<IGameModel>().Quarter + "刻_";
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}