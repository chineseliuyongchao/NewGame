using DG.Tweening;
using Fight.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fight.Tools.Tips
{
    public abstract class ObjectTips : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        [HideInInspector] public TipsMark tipsMark;

        public UnityAction showCallback;
        public UnityAction hideCallback;

        public abstract void OnInit<T>(T value);

        /// <summary>
        /// 通用的气泡布局方案
        /// </summary>
        /// <param name="localPosition">气泡应该位于的位置</param>
        public virtual void Layout(Vector3 localPosition)
        {
            //位置在左上角，气泡在右下角
            //位置在左下角，气泡在右上角
            //位置在右上角，气泡在左下角
            //位置在右下角，气泡在左上角
            RectTransform rectTransform = GetComponent<RectTransform>();
            var rect = rectTransform.rect;
            Vector3 tmp = -new Vector3(Mathf.Sign(localPosition.x), Mathf.Sign(localPosition.y));
            float widthHalf = rect.width / 2f - 20;
            float heightHalf = rect.height / 2f - 20;

            rectTransform.anchoredPosition =
                localPosition + new Vector3(widthHalf * tmp.x, heightHalf * tmp.y);
        }

        public virtual void Show()
        {
            if (tipsMark.showTips)
            {
                return;
            }

            tipsMark.objectTips = this;
            tipsMark.showTips = true;
            if (tipsMark.parent)
            {
                tipsMark.parent.locking = true;
            }

            transform.gameObject.SetActive(true);
            transform.SetAsLastSibling();
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
            if (!tipsMark.showTips || tipsMark.locking)
            {
                return;
            }

            tipsMark.objectTips = null;
            tipsMark.showTips = false;
            if (tipsMark.parent)
            {
                tipsMark.parent.locking = false;
            }

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

        public void OnPointerClick(PointerEventData eventData)
        {
            Hide();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            Hide();
        }

        protected float TextHorizontalLayout(params Text[] texts)
        {
            float maxWidth = 0f;
            foreach (Text text in texts)
            {
                maxWidth += text.preferredWidth;
            }

            maxWidth += (texts.Length - 1) * 10;
            float beginX = -maxWidth / 2f;
            foreach (Text text in texts)
            {
                float width = text.preferredWidth;
                text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                text.rectTransform.SetAnchorPositionX(beginX + width / 2f);
                beginX += width + 10;
            }

            return maxWidth;
        }
    }
}