using System.Collections.Generic;
using Fight.Command;
using Fight.Event;
using Fight.Model;
using Fight.System;
using Game.GameBase;
using QFramework;
using UnityEngine;
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

        /// <summary>
        /// 选中的攻击方式对应的按钮
        /// </summary>
        private Button _chooseAttackButton;

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
                this.SendCommand(new EndRoundButtonCommand());
                endRoundButton.gameObject.SetActive(false);
            });
            advanceButton.onClick.AddListener(() => { ChooseAttackType(FightAttackType.ADVANCE, advanceButton); });
            shootButton.onClick.AddListener(() => { ChooseAttackType(FightAttackType.SHOOT, shootButton); });
            sustainedAdvanceButton.onClick.AddListener(() =>
            {
                ChooseAttackType(FightAttackType.SUSTAIN_ADVANCE, sustainedAdvanceButton);
            });
            sustainedShootButton.onClick.AddListener(() =>
            {
                ChooseAttackType(FightAttackType.SUSTAIN_SHOOT, sustainedShootButton);
            });
            chargeButton.onClick.AddListener(() => { ChooseAttackType(FightAttackType.CHARGE, chargeButton); });
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
            this.RegisterEvent<StartRoundEvent>(e =>
            {
                if (e.isPlayer)
                {
                    endRoundButton.gameObject.SetActive(true);
                    fightBehaviorGroup.gameObject.SetActive(true);
                    List<bool> buttonShow = this.GetSystem<IFightSystem>().FightBehaviorButtonShow(e.unitId);
                    int index = 0;
                    fightBehaviorButton.ForEach(button =>
                    {
                        button.gameObject.SetActive(buttonShow[index]);
                        index++;
                    });
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<EndRoundEvent>(e =>
            {
                if (e.isPlayer)
                {
                    fightBehaviorGroup.gameObject.SetActive(false);
                    endRoundButton.gameObject.SetActive(false);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<PlayerUnitActionEvent>(_ =>
            {
                fightBehaviorButton.ForEach(button => button.interactable = false);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<PlayerUnitWaitActionEvent>(_ =>
            {
                fightBehaviorButton.ForEach(button => button.interactable = true);
                ChooseAttackType(FightAttackType.NONE, null);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 开始战斗后的UI变化逻辑
        /// </summary>
        private void StartFight()
        {
            startButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// 选择攻击行为
        /// </summary>
        /// <param name="type"></param>
        /// <param name="button"></param>
        private void ChooseAttackType(FightAttackType type, Button button)
        {
            if (_chooseAttackButton != null)
            {
                _chooseAttackButton.GetComponent<Image>().color = Color.white;
            }

            this.GetModel<IFightVisualModel>().FightAttackType = type;
            _chooseAttackButton = button;
            if (_chooseAttackButton != null)
            {
                _chooseAttackButton.GetComponent<Image>().color = Color.red;
            }
        }
    }
}