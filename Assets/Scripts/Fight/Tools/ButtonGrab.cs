using System;
using DG.Tweening;
using UnityAttribute;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fight.Tools
{
    public class ButtonGrab : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler,
        IPointerExitHandler
    {
        private static readonly Color GrayColor = new(200f / 255, 200f / 255, 200f / 255); //按钮的取色

        public bool interactable;

        [SerializeField] private Graphic[] graphics;

        [FormerlySerializedAs("PointerDown")] [SerializeField]
        private ButtonGrabEvent pointerDown;

        [FormerlySerializedAs("PointerClick")] [SerializeField]
        private ButtonGrabEvent pointerClick;

        [FormerlySerializedAs("PointerUp")] [SerializeField]
        private ButtonGrabEvent pointerUp;

        [Label("按下颜色")] public Color pointerDownColor = GrayColor;

        protected virtual void Awake()
        {
            graphics ??= Array.Empty<Graphic>();
            pointerDown ??= new ButtonGrabEvent();
            pointerClick ??= new ButtonGrabEvent();
            pointerUp ??= new ButtonGrabEvent();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            pointerClick?.Invoke(eventData);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable) return;
            foreach (var graphic in graphics) graphic.DOColor(pointerDownColor, 0.1f);

            pointerDown?.Invoke(eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable) return;
            foreach (var graphic in graphics) graphic.DOColor(Color.white, 0.1f);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable) return;
            foreach (var graphic in graphics) graphic.DOColor(Color.white, 0.1f);

            pointerUp?.Invoke(eventData);
        }

        public void AddPointerDownCallBack(UnityAction<PointerEventData> callBack)
        {
            pointerDown.AddListener(callBack);
        }

        public void AddPointerClickCallBack(UnityAction<PointerEventData> callBack)
        {
            pointerClick.AddListener(callBack);
        }

        public void AddPointerUpCallBack(UnityAction<PointerEventData> callBack)
        {
            pointerUp.AddListener(callBack);
        }
    }

    [Serializable]
    public class ButtonGrabEvent : UnityEvent<PointerEventData>
    {
    }
}