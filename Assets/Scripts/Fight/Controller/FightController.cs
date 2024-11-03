using System.Collections.Generic;
using DG.Tweening;
using Fight.Command;
using Fight.Event;
using Fight.FsmS;
using Fight.Game.Legion;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Fight.Utils;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using UI;
using UnityEngine;

namespace Fight.Controller
{
    /**
     * 管理整个场景的状态：战前准备-战斗中-战斗结束
     */
    public class FightController : BaseGameController
    {
        public static float worldWidth; //世界宽
        public static float worldHeight; //世界高

        private ArmsFsm _armsFsm;

        /// <summary>
        /// 军队行动顺序
        /// </summary>
        private List<int> _legionOrder;

        /// <summary>
        /// 正在行动的军队在行动顺序中的位置
        /// </summary>
        private int _actionLegionIndex;

        public Transform legionNode;

        public int ActionLegionIndex
        {
            get => _actionLegionIndex;
            set => _actionLegionIndex = value;
        }

        protected override void OnInit()
        {
            base.OnInit();

            if (Camera.main != null)
            {
                worldHeight = Camera.main.orthographicSize * 2f;
                //需要将uiCamera的大小与战斗场景相机大小统一
                Camera cam = GameObject.Find("UICamera").GetComponent<Camera>();
                cam.orthographicSize = Camera.main.orthographicSize;
            }
            else
            {
                worldHeight = 20f;
            }

            worldWidth = worldHeight / 0.5625f;
            this.GetModel<IAStarModel>().InitStarData();
            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
            UIKit.OpenPanel<UIGameFight>(new UIGameFightData());

            List<int> legionKeys = new List<int>(this.GetModel<IFightCreateModel>().AllLegions.Keys);
            for (int i = 0; i < legionKeys.Count; i++)
            {
                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[legionKeys[i]];
                GameObject legionObject = new GameObject("legion" + legionInfo.legionId);
                legionObject.Parent(legionNode);
                BaseLegion legion;
                if (legionInfo.legionId == Constants.PlayLegionId)
                {
                    legion = legionObject.AddComponent<PlayerLegion>();
                }
                else
                {
                    legion = legionObject.AddComponent<ComputerLegion>();
                }

                legion.Init(legionInfo.legionId);
                this.GetModel<IFightCoreModel>().AllLegion.Add(legionInfo.legionId, legion);
            }

            _legionOrder = this.GetSystem<IFightComputeSystem>().LegionOrder();
            _actionLegionIndex = 0;
        }

        protected override void OnControllerStart()
        {
            this.SendCommand(new FightCommand(FightType.WAR_PREPARATIONS));
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<InFightEvent>(_ => { StartFight(); }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 战斗开始
        /// </summary>
        private void StartFight()
        {
            LegionStartAction(_legionOrder[ActionLegionIndex]);
        }

        /// <summary>
        /// 某个军队开始行动
        /// </summary>
        private void LegionStartAction(int legionId)
        {
            this.GetModel<IFightCoreModel>().AllLegion[legionId].StartRound(LegionEndAction);
        }

        /// <summary>
        /// 某个军队行动结束
        /// </summary>
        private void LegionEndAction(int legionId)
        {
            int index = _legionOrder.IndexOf(legionId);
            if (this.GetSystem<IFightComputeSystem>().CheckFightFinish())
            {
                this.SendCommand(new FightCommand(FightType.SETTLEMENT));
            }
            else
            {
                if (index + 1 >= _legionOrder.Count)
                {
                    ActionLegionIndex = 0;
                    List<int> allUnitKey = new List<int>(this.GetModel<IFightVisualModel>().AllUnit.Keys);
                    for (int i = 0; i < allUnitKey.Count; i++)
                    {
                        UnitController unitController = this.GetModel<IFightVisualModel>().AllUnit[allUnitKey[i]];
                        unitController.unitData.NowActionPoints = Constants.InitActionPoints;
                        unitController.unitProgressBar.UpdateProgress();
                    }
                }
                else
                {
                    ActionLegionIndex = index + 1;
                }

                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(1);
                sequence.AppendCallback(() => { LegionStartAction(_legionOrder[ActionLegionIndex]); });
            }
        }

        private void OnDestroy()
        {
            GameApp.Interface.UnRegisterSystem<IFightComputeSystem>();
            GameApp.Interface.UnRegisterSystem<IFightSystem>();

            GameApp.Interface.UnRegisterModel<IAStarModel>();
            GameApp.Interface.UnRegisterModel<IFightVisualModel>();
            GameApp.Interface.UnRegisterModel<IFightCoreModel>();
        }
    }
}