using Fight.Command;
using Fight.Model;
using Game.GameBase;
using QFramework;

namespace UI
{
    public class UIFightEndData : UIPanelData
    {
    }

    public partial class UIFightEnd : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightEndData ?? new UIFightEndData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightEndData ?? new UIFightEndData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            exitButton.onClick.AddListener(() =>
            {
                this.SendCommand(new FightCommand(FightType.FIGHT_OVER));
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.CREATE_FIGHT_SCENE);
            });
        }

        protected override void OnListenEvent()
        {
        }
    }
}