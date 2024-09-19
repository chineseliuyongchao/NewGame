using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Fight.Tools
{
    public abstract class ObjectTips : MonoBehaviour, IPointerExitHandler
    {
        [FormerlySerializedAs("tipsManager")] [HideInInspector]
        public TipsMark tipsMark;

        public UnityAction showCallback;
        public UnityAction hideCallback;

        public abstract void OnInit<T>(T value);

        public virtual void Show()
        {
            if (tipsMark.showTips)
            {
                return;
            }

            tipsMark.showTips = true;
            transform.gameObject.SetActive(true);
            CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
            if (!canvasGroup)
            {
                showCallback?.Invoke();
                showCallback = null;
                return;
            }

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                showCallback?.Invoke();
                showCallback = null;
            });
        }

        public virtual void Hide()
        {
            if (!tipsMark.showTips)
            {
                return;
            }

            tipsMark.showTips = false;
            CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
            if (!canvasGroup)
            {
                transform.gameObject.SetActive(false);
                hideCallback?.Invoke();
                hideCallback = null;
                return;
            }

            canvasGroup.interactable = false;
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                canvasGroup.gameObject.SetActive(false);
                hideCallback?.Invoke();
                hideCallback = null;
            });
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Hide();
        }
    }
}