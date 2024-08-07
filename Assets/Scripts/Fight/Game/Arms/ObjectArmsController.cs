using DG.Tweening;
using Fight.Commands;
using Fight.Enum;
using Fight.Events;
using Fight.Scenes;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public class ObjectArmsController<T1, T2> : MonoBehaviour, IState, IController, IObjectArmsController
        where T1 : ObjectArmsModel, new() where T2 : ObjectArmsView
    {
        public T1 model;
        [HideInInspector] public T2 view;

        private Tween _focusAction;

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void OnInit()
        {
            var info = this.GetModel<GamePlayerModel>().ArmsInfoDictionary[name];
            model = (T1)info.ObjectArmsModel;
            model.CurrentIndex = info.RanksIndex;

            GameObject o;
            view = (o = gameObject).AddComponent<T2>();
            view.OnInit(transform);
        }

        public ObjectArmsModel GetModel()
        {
            return model;
        }

        public ObjectArmsView GetView()
        {
            return view;
        }

        public string GetName()
        {
            return name;
        }

        public void ArmsMoveAction(int endIndex)
        {
            switch (FightScene.Ins.currentBattleType)
            {
                case BattleType.StartWarPreparations:
                    Vector3 endPosition = (Vector3)this.GetModel<AStarModel>().FightGridNodeInfoList[endIndex].position;
                    transform.position = endPosition;
                    break;
                case BattleType.StartBattle:
                case BattleType.StartPursuit:

                    break;
            }
        }

        /**
         * 被选取为焦点时的动画
         */
        public void StartFocusAction()
        {
            EndFocusAction();
            _focusAction = transform.DOBlendableMoveBy(new Vector3(0, 0.5f, 0), 0.5f).SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public void EndFocusAction()
        {
            _focusAction?.Kill();
            AStarModel aStarModel = this.GetModel<AStarModel>();
            var tmp = aStarModel.FightGridNodeInfoList[model.CurrentIndex];
            transform.DOMove((Vector3)tmp.position, 0.2f).SetEase(Ease.OutSine);
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

        protected void StartBattleEvent()
        {
        }
    }
}