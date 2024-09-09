using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight
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
                if (fightVisualModel.IndexToArmsIdDictionary.ContainsKey(index))
                {
                    this.SendCommand(new SelectArmsFocusCommand(index));
                }
            }
            else
            {
                if (index == fightVisualModel.FocusController.armData.currentPosition)
                {
                    //点击的是自己
                    this.SendCommand(new CancelArmsFocusCommand());
                }
                else if (fightVisualModel.IndexToArmsIdDictionary.ContainsKey(index))
                {
                    //点击了其他的兵种
                    this.SendCommand(new SelectArmsFocusCommand(index));
                }
                else if (this.GetSystem<IFightSystem>().CanWalkableIndex(index))
                {
                    //筛选掉障碍物，表示兵种要移动到这个位置
                    this.SendCommand(new ArmsMoveCommand(index));
                }
            }
        }
    }
}