using Fight.Command;
using Fight.Game.Unit;
using Fight.Model;
using Fight.Utils;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight.System
{
    /// <summary>
    /// 战斗进行阶段处理输入事件
    /// </summary>
    public class FightInputInFightSystem : AbstractSystem, IFightInputSystem, ICanSendCommand
    {
        protected override void OnInit()
        {
        }

        public void MouseButtonLeft()
        {
            var cam = Camera.main;
            if (!cam)
            {
                return;
            }

            if (!this.GetModel<IFightVisualModel>().InPlayerAction || this.GetModel<IFightVisualModel>().PlayerInAction)
            {
                return;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (hit.collider)
            {
                UnitController controller = hit.collider.gameObject.GetComponent<UnitController>();
                if (controller) //点击的单位
                {
                    if (this.GetSystem<IFightSystem>().IsPlayerUnit(controller.unitData.unitId))
                    {
                        //玩家单位
                        SelectPlayerUnit(controller);
                    }
                    else
                    {
                        //非玩家单位
                        SelectOtherUnit(controller);
                    }
                }
            }
            else
            {
                IAStarModel aStarModel = this.GetModel<IAStarModel>();
                //通过鼠标点击的位置查找这个坐标的index
                int index = aStarModel.GetGridNodeIndexMyRule(worldPosition);
                if (!aStarModel.FightGridNodeInfoList.ContainsKey(index))
                {
                    return;
                }

                if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out var unitId))
                {
                    UnitController controller = this.GetModel<IFightVisualModel>().AllUnit[unitId];
                    if (this.GetSystem<IFightSystem>().IsPlayerUnit(unitId))
                    {
                        //玩家单位
                        SelectPlayerUnit(controller);
                    }
                    else
                    {
                        //非玩家单位
                        SelectOtherUnit(controller);
                    }
                }
                else
                {
                    SelectOtherPos(fightVisualModel, index);
                }
            }
        }

        public void MouseButtonMiddle()
        {
            var cam = Camera.main;
            if (!cam)
            {
                return;
            }

            if (!this.GetModel<IFightVisualModel>().InPlayerAction || this.GetModel<IFightVisualModel>().PlayerInAction)
            {
                return;
            }
        }

        public void MouseButtonRight()
        {
            var cam = Camera.main;
            if (!cam)
            {
                return;
            }

            if (!this.GetModel<IFightVisualModel>().InPlayerAction || this.GetModel<IFightVisualModel>().PlayerInAction)
            {
                return;
            }

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!hit.collider)
            {
                IAStarModel aStarModel = this.GetModel<IAStarModel>();
                //通过鼠标点击的位置查找这个坐标的index
                int index = aStarModel.GetGridNodeIndexMyRule(worldPosition);
                if (!aStarModel.FightGridNodeInfoList.ContainsKey(index))
                {
                    return;
                }

                if (!fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out _))
                {
                    PlayerUnitMove(fightVisualModel, index);
                }
            }
        }

        /// <summary>
        /// 选取了玩家的单位
        /// </summary>
        private void SelectPlayerUnit(UnitController controller)
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!fightVisualModel.FocusController || controller != fightVisualModel.FocusController)
            {
                //当前没有焦点兵种或者点击了其他属于自己的单位
                this.SendCommand(new SelectUnitFocusCommand(controller));
            }
        }

        /// <summary>
        /// 选择到了电脑的单位
        /// </summary>
        private void SelectOtherUnit(UnitController controller)
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            if (this.GetSystem<IFightSystem>().GetBelligerentsIdOfUnit(controller.unitData.unitId) !=
                Constants.BELLIGERENT1) //非玩家阵营，可以攻击
            {
                this.GetModel<IFightCoreModel>().AllLegion[Constants.PLAY_LEGION_ID]
                    .UnitAttack(fightVisualModel.FocusController.unitData.unitId, controller.unitData.unitId);
            }
            else
            {
                //玩家阵营，不可以攻击
            }
        }

        /// <summary>
        /// 点击到了其他位置
        /// </summary>
        private void SelectOtherPos(IFightVisualModel fightVisualModel, int index)
        {
        }

        /// <summary>
        /// 玩家单位移动
        /// </summary>
        private void PlayerUnitMove(IFightVisualModel fightVisualModel, int index)
        {
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            if (this.GetSystem<IFightSystem>().CanWalkableIndex(index))
            {
                //筛选掉障碍物，表示兵种要移动到这个位置
                if (fightVisualModel.FocusController == null)
                {
                    return;
                }

                this.GetModel<IFightCoreModel>().AllLegion[Constants.PLAY_LEGION_ID]
                    .UnitMove(fightVisualModel.FocusController.unitData.unitId, index, null);
            }
        }
    }
}