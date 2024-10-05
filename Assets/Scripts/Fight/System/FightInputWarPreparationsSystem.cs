using Fight.Command;
using Fight.Game.Unit;
using Fight.Model;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight.System
{
    /// <summary>
    /// 战斗准备阶段处理输入事件
    /// </summary>
    public class FightInputWarPreparationsSystem : AbstractSystem, IFightInputSystem, ICanSendCommand
    {
        protected override void OnInit()
        {
        }

        public void MouseButtonLeft()
        {
            MouseButton();
        }

        public void MouseButtonMiddle()
        {
            var cam = Camera.main;
            if (!cam)
            {
                return;
            }
        }

        public void MouseButtonRight()
        {
            MouseButton();
        }

        private void MouseButton()
        {
            var cam = Camera.main;
            if (!cam)
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
                        SelectPlayerUnit(fightVisualModel, controller);
                    }
                    else
                    {
                        //非玩家单位
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
                    if (this.GetSystem<IFightSystem>().IsPlayerUnit(unitId))
                    {
                        //玩家单位
                        SelectPlayerUnit(fightVisualModel, index);
                    }
                    else
                    {
                        //非玩家单位
                    }
                }
                else
                {
                    SelectOtherPos(fightVisualModel, index);
                }
            }
        }

        /// <summary>
        /// 选取了玩家的单位
        /// </summary>
        private void SelectPlayerUnit(IFightVisualModel fightVisualModel, UnitController controller)
        {
            if (!fightVisualModel.FocusController || controller != fightVisualModel.FocusController)
            {
                //当前没有焦点兵种或者点击了其他属于自己的单位
                this.SendCommand(new SelectUnitFocusCommand(controller));
            }
            else
            {
                //点击的是当前选中的单位
                this.SendCommand(new CancelUnitFocusCommand());
            }
        }

        /// <summary>
        /// 选取了玩家的单位
        /// </summary>
        private void SelectPlayerUnit(IFightVisualModel fightVisualModel, int index)
        {
            if (!fightVisualModel.FocusController || index != fightVisualModel.FocusController.unitData.currentPosIndex)
            {
                //当前没有焦点兵种或者点击了其他属于自己的单位
                this.SendCommand(new SelectUnitFocusCommand(index));
            }
            else
            {
                //点击的是当前选中的单位
                this.SendCommand(new CancelUnitFocusCommand());
            }
        }

        /// <summary>
        /// 点击到了其他位置
        /// </summary>
        private void SelectOtherPos(IFightVisualModel fightVisualModel, int index)
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

                fightVisualModel.FocusController.ChangePos(index);
            }
        }
    }
}