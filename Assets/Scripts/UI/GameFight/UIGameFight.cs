using System.Collections.Generic;
using Fight.Command;
using Fight.Event;
using Fight.Model;
using Game.GameBase;
using QFramework;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace UI
{
    public class UIGameFightData : UIPanelData
    {
    }

    public partial class UIGameFight : UIBase
    {
        public List<Button> fightBehaviorButton;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameFightData ?? new UIGameFightData();
            // please add init code here
            base.OnInit(uiData);
            endRoundButton.gameObject.SetActive(false);
            fightBehaviorGroup.gameObject.SetActive(false);
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
            endRoundButton.onClick.AddListener(() =>
            {
                this.SendCommand(new EndActionCommand(true));
                endRoundButton.gameObject.SetActive(false);
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
            this.RegisterEvent<StartActionEvent>(e =>
            {
                if (e.isPlayer)
                {
                    endRoundButton.gameObject.SetActive(true);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<StartActionEvent>(e =>
            {
                if (e.isPlayer)
                {
                    fightBehaviorGroup.gameObject.SetActive(true);
                    fightBehaviorButton.ForEach(button => button.interactable = false);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<EndActionEvent>(e =>
            {
                if (e.isPlayer)
                {
                    fightBehaviorGroup.gameObject.SetActive(false);
                    fightBehaviorButton.ForEach(button => button.interactable = true);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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