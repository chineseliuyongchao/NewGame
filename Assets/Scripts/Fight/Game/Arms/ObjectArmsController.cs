using DG.Tweening;
using Fight.Commands;
using Fight.Enum;
using Fight.Events;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public class ObjectArmsController<T1, T2> : MonoBehaviour, IState, IController, IObjectArmsController
        where T1 : ObjectArmsModel, new() where T2 : ObjectArmsView
    {
        public T1 model;
        [HideInInspector] public T2 view;

        public int CurrentIndex => _currentIndex;
        private int _currentIndex;

        private Tween _focusAction;

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void OnInit()
        {
            var info = this.GetModel<GamePlayerModel>().ArmsInfoDictionary[name];
            model = (T1)info.ObjectArmsModel;
            _currentIndex = info.RanksIndex;

            GameObject o;
            view = (o = gameObject).AddComponent<T2>();
            view.OnInit(transform);
            this.RegisterEvent<StartWarPreparationsEvent>(w => StartWarPreparationsEvent())
                .UnRegisterWhenGameObjectDestroyed(o);
        }

        public ObjectArmsModel GetModel()
        {
            return model;
        }

        public ObjectArmsView GetView()
        {
            return view;
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
            var tmp = aStarModel.FightGridNodeInfoList[_currentIndex];
            transform.DOMove((Vector3)tmp.position, 0.2f).SetEase(Ease.OutSine);
            
            // transform.position = (Vector3)tmp.position;
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

        protected void StartWarPreparationsEvent()
        {
            // _colorChangeTween?.Kill();
            // _colorChangeTween = DOTween.Sequence().AppendCallback(() => view.DoColor(Color.red, 1f))
            //     .AppendInterval(1f).AppendCallback(() => view.DoColor(Color.green, 1f)).AppendInterval(1f)
            //     .SetLoops(-1);
            // armsButtonGrab.AddBeginDragCallBack(StartWarPreparationsBeginDrag);
            // armsButtonGrab.AddDragCallBack(StartWarPreparationsDrag);
            // armsButtonGrab.AddEndDragCallBack(StartWarPreparationsEndDrag);
        }

        protected void EndWarPreparationsEvent()
        {
            // armsButtonGrab.RemoveBeginDragCallBack(StartWarPreparationsBeginDrag);
            // armsButtonGrab.RemoveDragCallBack(StartWarPreparationsDrag);
            // armsButtonGrab.RemoveEndDragCallBack(StartWarPreparationsEndDrag);
        }

        protected void StartBattleEvent()
        {
        }

        // private void StartWarPreparationsBeginDrag(PointerEventData data)
        // {
        //     var cam = Camera.main;
        //     if (!cam) return;
        //
        //     _currentDragPosition = cam.ScreenToWorldPoint(data.position);
        //     _currentDragPosition.z = 0;
        // }
        //
        // private void StartWarPreparationsDrag(PointerEventData data)
        // {
        //     var cam = Camera.main;
        //     if (!cam) return;
        //
        //     var worldPosition = cam.ScreenToWorldPoint(data.position);
        //     worldPosition.z = 0;
        //     transform.position += worldPosition - _currentDragPosition;
        //     _currentDragPosition = worldPosition;
        // }
        //
        // private void StartWarPreparationsEndDrag(PointerEventData data)
        // {
        //     var aStarModel = this.GetModel<AStarModel>();
        //     var fightGameModel = this.GetModel<FightGameModel>();
        //     var index = aStarModel.GetGridNodeIndexMyRule(data.pointerCurrentRaycast.worldPosition);
        //     if (!fightGameModel.FightScenePositionDictionary.ContainsValue(index))
        //     {
        //         fightGameModel.FightScenePositionDictionary[name] = index;
        //         _currentIndex = index;
        //     }
        //
        //     //可以在周围寻找一个位置
        //     transform.position = (Vector3)aStarModel.FightGridNodeInfoList[_currentIndex].position;
        // }
    }
}