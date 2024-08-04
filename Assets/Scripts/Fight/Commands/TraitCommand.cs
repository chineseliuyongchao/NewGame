using System;
using Fight.Enum;
using Fight.Game.Arms;
using Fight.Systems;
using QFramework;

namespace Fight.Commands
{
    public class TraitCommand : AbstractCommand
    {
        private readonly TraitActionType _actionType;
        private readonly ObjectArmsModel _armsModel;
        private readonly int _traitId;

        public TraitCommand(ObjectArmsModel armsModel, TraitActionType actionType, int traitId = -1)
        {
            _armsModel = armsModel;
            _actionType = actionType;
            _traitId = traitId;
        }

        protected override void OnExecute()
        {
            var traitSystem = this.GetSystem<TraitSystem>();
            var trait = traitSystem.GetTraitById(_traitId);

            switch (_actionType)
            {
                case TraitActionType.Add:
                    if (_armsModel.TraitSet.Add(_traitId)) trait.Apply(_armsModel);

                    break;
                case TraitActionType.Remove:
                    if (_armsModel.TraitSet.Remove(_traitId)) trait.Remove(_armsModel);

                    break;
                case TraitActionType.StartAffecting:
                    trait.StartAffecting(_armsModel);
                    break;
                case TraitActionType.StopAffecting:
                    trait.StopAffecting(_armsModel);
                    break;
                case TraitActionType.StartRound:
                    trait.StartRound(_armsModel);
                    break;
                case TraitActionType.EndRound:
                    trait.EndRound(_armsModel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}