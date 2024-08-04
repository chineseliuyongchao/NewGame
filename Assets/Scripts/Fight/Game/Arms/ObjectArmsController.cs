using DG.Tweening;
using Fight.Commands;
using Fight.Enum;
using Fight.Events;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fight.Game.Arms
{
    public class ObjectArmsController<T1, T2> : MonoBehaviour, IState, IController, IObjectArmsController
        where T1 : ObjectArmsModel, new() where T2 : ObjectArmsView
    {
        public T1 model;
        [HideInInspector] public T2 view;
        [HideInInspector] public ArmsButtonGrab armsButtonGrab;

        private Tween _colorChangeTween;
        private Vector3 _currentDragPosition;

        private int _currentIndex;

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
            armsButtonGrab = o.AddComponent<ArmsButtonGrab>();
            armsButtonGrab.armsView = view;
            this.RegisterEvent<StartWarPreparationsEvent>(w => StartWarPreparationsEvent())
                .UnRegisterWhenGameObjectDestroyed(o);
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
            armsButtonGrab.AddBeginDragCallBack(StartWarPreparationsBeginDrag);
            armsButtonGrab.AddDragCallBack(StartWarPreparationsDrag);
            armsButtonGrab.AddEndDragCallBack(StartWarPreparationsEndDrag);
        }

        protected void EndWarPreparationsEvent()
        {
            armsButtonGrab.RemoveBeginDragCallBack(StartWarPreparationsBeginDrag);
            armsButtonGrab.RemoveDragCallBack(StartWarPreparationsDrag);
            armsButtonGrab.RemoveEndDragCallBack(StartWarPreparationsEndDrag);
        }

        protected void StartBattleEvent()
        {
        }

        private void StartWarPreparationsBeginDrag(PointerEventData data)
        {
            var cam = Camera.main;
            if (!cam) return;

            _currentDragPosition = cam.ScreenToWorldPoint(data.position);
            _currentDragPosition.z = 0;
        }

        private void StartWarPreparationsDrag(PointerEventData data)
        {
            var cam = Camera.main;
            if (!cam) return;

            var worldPosition = cam.ScreenToWorldPoint(data.position);
            worldPosition.z = 0;
            transform.position += worldPosition - _currentDragPosition;
            _currentDragPosition = worldPosition;
        }

        private void StartWarPreparationsEndDrag(PointerEventData data)
        {
            var aStarModel = this.GetModel<AStarModel>();
            var gamePlayerModel = this.GetModel<GamePlayerModel>();
            var index = aStarModel.GetGridNodeIndexMyRule(data.pointerCurrentRaycast.worldPosition);
            if (!gamePlayerModel.FightScenePositionDictionary.ContainsValue(index))
            {
                gamePlayerModel.FightScenePositionDictionary[name] = index;
                _currentIndex = index;
            }

            //可以在周围寻找一个位置
            transform.position = (Vector3)aStarModel.FightGridNodeInfoList[_currentIndex].position;
        }
    }
}