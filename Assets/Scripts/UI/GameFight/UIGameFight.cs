using Game.GameBase;
using QFramework;

namespace UI
{
    public class UIGameFightData : UIPanelData
    {
    }

    public partial class UIGameFight : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameFightData ?? new UIGameFightData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameFightData ?? new UIGameFightData();
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
            exitButton.onClick.AddListener(() => { this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE); });
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeFightSceneEvent>(e =>
            {
                if (e.isChangeIn)
                {
                    CloseSelf();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}