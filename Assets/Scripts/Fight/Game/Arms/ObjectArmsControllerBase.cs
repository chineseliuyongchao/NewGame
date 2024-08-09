using Fight.Commands;
using Fight.Enum;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public class ObjectArmsControllerBase<T1, T2> : ObjectArmsController, IState
        where T1 : ObjectArmsModel, new() where T2 : ObjectArmsView
    {
        public T1 model;
        [HideInInspector] public T2 view;

        public override void OnInit()
        {
            var info = this.GetModel<GamePlayerModel>().ArmsInfoDictionary[id];
            model = (T1)info.ObjectArmsModel;
            model.CurrentIndex = info.RanksIndex;
            
            view = gameObject.AddComponent<T2>();
            view.OnInit(transform);
        }

        public override ObjectArmsModel GetModel()
        {
            return model;
        }

        public override ObjectArmsView GetView()
        {
            return view;
        }

        public bool Condition()
        {
            return true;
        }

        public void Enter()
        {
            this.SendCommand(new TraitCommand(model, TraitActionType.StartRound));
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void OnGUI()
        {
        }

        public void Exit()
        {
            this.SendCommand(new TraitCommand(model, TraitActionType.EndRound));
        }
    }
}