using Fight.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fight.Game
{
    public sealed class ArmsButtonGrab : ButtonGrab
    {
        public ObjectArmsView armsView;

        // [FormerlySerializedAs("BeginDrag")] [SerializeField]
        // private ButtonGrabEvent beginDrag;
        //
        // [FormerlySerializedAs("Drag")] [SerializeField]
        // private ButtonGrabEvent drag;
        //
        // [FormerlySerializedAs("EndDrag")] [SerializeField]
        // private ButtonGrabEvent endDrag;
        //
        // private bool _dragging;

        protected override void Awake()
        {
            base.Awake();
            interactable = true;
            // beginDrag ??= new ButtonGrabEvent();
            // drag ??= new ButtonGrabEvent();
            // endDrag ??= new ButtonGrabEvent();
        }

        // public void OnBeginDrag(PointerEventData eventData)
        // {
        //     if (!interactable) return;
        //     _dragging = true;
        //     beginDrag?.Invoke(eventData);
        // }
        //
        // public void OnDrag(PointerEventData eventData)
        // {
        //     if (!interactable || !_dragging) return;
        //     drag?.Invoke(eventData);
        // }
        //
        // public void OnEndDrag(PointerEventData eventData)
        // {
        //     if (!interactable) return;
        //     endDrag?.Invoke(eventData);
        //     _dragging = false;
        // }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (armsView != null) armsView.DoColor(pointerDownColor);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (armsView != null) armsView.DoColor(Color.white);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (armsView != null) armsView.DoColor(Color.white);
        }

        // public void AddBeginDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     beginDrag.AddListener(callBack);
        // }
        //
        // public void AddDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     drag.AddListener(callBack);
        // }
        //
        // public void AddEndDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     endDrag.AddListener(callBack);
        // }
        //
        // public void RemoveBeginDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     beginDrag.RemoveListener(callBack);
        // }
        //
        // public void RemoveDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     drag.RemoveListener(callBack);
        // }
        //
        // public void RemoveEndDragCallBack(UnityAction<PointerEventData> callBack)
        // {
        //     endDrag.RemoveListener(callBack);
        // }
    }
}