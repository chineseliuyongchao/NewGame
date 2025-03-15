using Fight.Event;
using Fight.Game.Unit;
using Fight.Model;
using Fight.System;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Fight.Command
{
    /// <summary>
    /// 战斗界面的状态命令，标志着战斗进行了哪个状态，处理相关逻辑并且发送相关消息
    /// </summary>
    public class FightCommand : AbstractCommand
    {
        private readonly FightType _type;

        public FightCommand(FightType type)
        {
            _type = type;
        }

        protected override void OnExecute()
        {
            this.GetModel<IFightVisualModel>().FightType = _type;
            GameApp.Interface.UnRegisterSystem<IFightInputSystem>();

            switch (_type)
            {
                case FightType.WAR_PREPARATIONS:
                    GameApp.Interface.RegisterSystem<IFightInputSystem>(new FightInputWarPrepareSystem());
                    this.SendEvent<WarPreparationsEvent>();
                    break;
                case FightType.IN_FIGHT:
                    GameApp.Interface.RegisterSystem<IFightInputSystem>(new FightInputInFightSystem());
                    this.SendEvent<InFightEvent>();
                    break;
                case FightType.SETTLEMENT:
                    GameApp.Interface.RegisterSystem<IFightInputSystem>(new FightInputSettlementSystem());
                    this.SendEvent<SettlementEvent>();
                    break;
                case FightType.FIGHT_OVER:
                    this.SendEvent<FightOverEvent>();
                    break;
            }
        }
    }

    /// <summary>
    /// 单位聚焦
    /// </summary>
    public class SelectUnitFocusCommand : AbstractCommand
    {
        private readonly int _index;
        private UnitController _controller;

        public SelectUnitFocusCommand(int index)
        {
            _index = index;
        }

        public SelectUnitFocusCommand(UnitController controller)
        {
            _controller = controller;
        }

        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!_controller && fightVisualModel.IndexToUnitIdDictionary.TryGetValue(_index, out int id))
            {
                _controller = this.GetModel<IFightVisualModel>().AllUnit[id];
            }

            if (_controller)
            {
                _controller.StartFocusAction();
                if (fightVisualModel.FocusController)
                {
                    fightVisualModel.FocusController.EndFocusAction();
                }

                fightVisualModel.FocusController = _controller;
                this.SendEvent(new SelectUnitFocusEvent(_index, _controller));
            }
            else
            {
                Debug.LogError("找不到兵种，位置是：" + _index);
            }
        }
    }

    /// <summary>
    /// 取消兵种聚焦
    /// </summary>
    public class CancelUnitFocusCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IFightVisualModel fightVisualModel = this.GetModel<IFightVisualModel>();
            if (!fightVisualModel.FocusController)
            {
                return;
            }

            fightVisualModel.FocusController.EndFocusAction();
            this.SendEvent<CancelUnitFocusEvent>();
            fightVisualModel.FocusController = null;
        }
    }

    /// <summary>
    /// 开始回合
    /// </summary>
    public class StartRoundCommand : AbstractCommand
    {
        /// <summary>
        /// 是不是玩家
        /// </summary>
        private readonly bool _isPlayer;

        private readonly int _unitId;

        public StartRoundCommand(bool isPlayer, int unitId)
        {
            _isPlayer = isPlayer;
            _unitId = unitId;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new StartRoundEvent(_isPlayer, _unitId));
        }
    }

    /// <summary>
    /// 结束回合
    /// </summary>
    public class EndRoundCommand : AbstractCommand
    {
        /// <summary>
        /// 是不是玩家
        /// </summary>
        private readonly bool _isPlayer;

        public EndRoundCommand(bool isPlayer)
        {
            _isPlayer = isPlayer;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new EndRoundEvent(_isPlayer));
        }
    }
    
    /// <summary>
    /// 玩家的单位开始行动
    /// </summary>
    public class PlayerUnitActionCommand : AbstractCommand
    {
        private readonly int _unitId;

        public PlayerUnitActionCommand(int unitId)
        {
            _unitId = unitId;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new PlayerUnitActionEvent(_unitId));
        }
    }

    /// <summary>
    /// 玩家的单位等待行动
    /// </summary>
    public class PlayerUnitWaitActionCommand : AbstractCommand
    {
        private readonly int _unitId;

        public PlayerUnitWaitActionCommand(int unitId)
        {
            _unitId = unitId;
        }

        protected override void OnExecute()
        {
            this.SendEvent(new PlayerUnitWaitActionEvent(_unitId));
        }
    }
}