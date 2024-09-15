using Fight;
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

        protected override void OnListenButton()
        {
            exitButton.onClick.AddListener(() => { this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE); });
            startButton.onClick.AddListener(() =>
            {
                this.SendCommand(new FightCommand(FightType.IN_FIGHT));
                StartFight();
            });
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeFightSceneEvent>(e =>
            {
                if (!e.isChangeIn)
                {
                    CloseSelf();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<SettlementEvent>(_ => { UIKit.OpenPanel<UIFightEnd>(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 开始战斗后的UI变化逻辑
        /// </summary>
        private void StartFight()
        {
            startButton.gameObject.SetActive(false);
        }
    }
}