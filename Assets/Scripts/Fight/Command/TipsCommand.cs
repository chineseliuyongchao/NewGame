using Fight.Controller;
using Fight.Game.Unit;
using Fight.Model;
using Fight.Tools;
using Fight.Tools.Tips;
using QFramework;
using UnityEngine;

namespace Fight.Command
{
    public class TipsCommand : AbstractCommand
    {
        private readonly Vector2 _worldPosition;
        private readonly TipsMark _tipsMark;

        public TipsCommand(Vector2 worldPosition, TipsMark tipsMark)
        {
            _worldPosition = worldPosition;
            _tipsMark = tipsMark;
        }

        protected override void OnExecute()
        {
            switch (_tipsMark.tipsType)
            {
                case TipsType.DefaultTips:
                    TipsMark.TipsController.ShowDefaultInfo(_worldPosition, _tipsMark);
                    break;
                case TipsType.ArmsTips:
                    UnitController unitController = _tipsMark.GetComponent<UnitController>();
                    //展示兵种信息
                    if (unitController)
                    {
                        TipsMark.TipsController.ShowUnitInfo(_worldPosition, _tipsMark, unitController.unitData);
                    }

                    break;
            }
        }
    }
}