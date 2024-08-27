using Fight.Model;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public class ArmsControllerBase<T> : ArmsController, IState
        where T : ObjectArmsView
    {
        [HideInInspector] public T view;

        public ArmsControllerBase(ArmData armData)
        {
            this.armData = armData;
        }

        private void Awake()
        {
            //todo
            fightCurrentIndex = Random.Range(0, this.GetModel<AStarModel>().FightGridNodeInfoList.Count);
            
            view = gameObject.AddComponent<T>();
            view.OnInit(transform);
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
            // this.SendCommand(new TraitCommand(model, TraitActionType.StartRound));
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
            // this.SendCommand(new TraitCommand(model, TraitActionType.EndRound));
        }
    }
}