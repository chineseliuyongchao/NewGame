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
            IAStarModel aStarModel = this.GetModel<IAStarModel>();
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            int index = aStarModel.GetGridNodeIndexMyRule(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
            if (!aStarModel.FightGridNodeInfoList.ContainsKey(index))
            {
                return;
            }

            //当前没有焦点兵种或者点击了其他属于自己的单位
            if (fightVisualModel.FocusController == null)
            {
                if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out var unitId))
                {
                    if (this.GetSystem<IFightSystem>().IsPlayerUnit(unitId))
                    {
                        //点击位置是玩家的单位
                        this.SendCommand(new SelectUnitFocusCommand(index));
                    }
                    else
                    {
                        //点击位置不是玩家的单位
                    }
                }
                else
                {
                    //点击位置没有单位
                }
            }
            else
            {
                if (index == fightVisualModel.FocusController.unitData.currentPosition)
                {
                    //点击的是当前选中的单位
                    this.SendCommand(new CancelUnitFocusCommand());
                }
                else if (fightVisualModel.IndexToUnitIdDictionary.TryGetValue(index, out var unitId))
                {
                    //点击到了其他单位
                    if (this.GetSystem<IFightSystem>().IsPlayerUnit(unitId))
                    {
                        //点击位置是玩家的单位
                        this.SendCommand(new SelectUnitFocusCommand(index));
                    }
                    else
                    {
                        //点击位置不是玩家的单位
                    }
                }
                else if (this.GetSystem<IFightSystem>().CanWalkableIndex(index))
                {
                    //筛选掉障碍物，表示兵种要移动到这个位置
                    this.SendCommand(new UnitMoveCommand(index));
                }
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