using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Fight.Tools;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight.Command
{
    /// <summary>
    /// 鼠标点击
    /// </summary>
    public class PointerClickCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var cam = Camera.main;
            if (!cam) return;
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (hit.collider)
            {
                //点击的是兵种，在这里只有点击玩家的单位才能生效
                UnitController controller = hit.collider.gameObject.GetComponent<UnitController>();
                if (controller && this.GetSystem<IFightSystem>().IsPlayerUnit(controller.unitData.unitId))
                {
                    SelectPlayerUnit(fightVisualModel, controller);
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

                if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out var unitId) &&
                    this.GetSystem<IFightSystem>().IsPlayerUnit(unitId))
                {
                    SelectPlayerUnit(fightVisualModel, index);
                }
                else
                {
                    SelectOtherUnit(fightVisualModel, index);
                }
            }
        }

        /// <summary>
        /// 选取了玩家的单位
        /// </summary>
        private void SelectPlayerUnit(IFightVisualModel fightVisualModel, int index)
        {
            if (!fightVisualModel.FocusController ||
                index != fightVisualModel.FocusController.unitData.currentPosition)
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
        /// 选取了玩家的单位
        /// </summary>
        private void SelectPlayerUnit(IFightVisualModel fightVisualModel, UnitController controller)
        {
            if (!fightVisualModel.FocusController ||
                controller != fightVisualModel.FocusController)
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
        /// 选取了其他单位
        /// </summary>
        private void SelectOtherUnit(IFightVisualModel fightVisualModel, int index)
        {
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            if (this.GetSystem<IFightSystem>().CanWalkableIndex(index))
            {
                //筛选掉障碍物，表示兵种要移动到这个位置
                this.SendCommand(new UnitMoveCommand(index));
            }
        }
    }

    public class MouseDragCommand : AbstractCommand
    {
        private readonly Vector2 _offset;

        public MouseDragCommand(Vector2 offset)
        {
            _offset = offset;
        }

        protected override void OnExecute()
        {
            var cam = Camera.main;
            if (cam)
            {
                MouseManager mouseManager = cam.GetComponent<MouseManager>();
                if (mouseManager)
                {
                    mouseManager.HandleMouseDrag(_offset);
                }
            }
        }
    }

    public class MouseScrollCommand : AbstractCommand
    {
        private readonly Vector2 _offset;

        public MouseScrollCommand(Vector2 offset)
        {
            _offset = offset;
        }

        protected override void OnExecute()
        {
            var cam = Camera.main;
            if (cam)
            {
                MouseManager mouseManager = cam.GetComponent<MouseManager>();
                if (mouseManager)
                {
                    mouseManager.HandleMouseScroll(_offset);
                }
            }
        }
    }
}