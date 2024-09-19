using Fight.Controller;
using Fight.Game.Unit;
using Fight.Tools;
using QFramework;
using UnityEngine;

namespace Fight.Command
{
    public class FightTipsCommand : AbstractCommand
    {
        private readonly Vector2 _worldPosition;
        private readonly TipsMark _tipsMark;

        public FightTipsCommand(Vector2 worldPosition, TipsMark tipsMark)
        {
            _worldPosition = worldPosition;
            _tipsMark = tipsMark;
        }

        protected override void OnExecute()
        {
            UnitController unitController = _tipsMark.GetComponent<UnitController>();
            //展示兵种信息
            if (unitController)
            {
                TipsMark.TipsController.ShowUnitInfo(_worldPosition, _tipsMark, unitController.unitData);
            }
        }
    }
}