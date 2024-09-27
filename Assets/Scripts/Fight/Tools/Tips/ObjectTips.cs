using System;
using System.Collections.Generic;
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

        protected CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = transform.GetComponent<CanvasGroup>();
        }

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
            if (!canvasGroup)
            {
                showCallback?.Invoke();
                showCallback = null;
                return;
            }

            canvasGroup.DOKill();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            // canvasGroup.interactable = false;
            canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = true;
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
            if (!canvasGroup)
            {
                transform.gameObject.SetActive(false);
                hideCallback?.Invoke();
                hideCallback = null;
                CheckParentHide();
                return;
            }

            canvasGroup.DOKill();
            canvasGroup.blocksRaycasts = false;
            // canvasGroup.interactable = false;
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                canvasGroup.gameObject.SetActive(false);
                hideCallback?.Invoke();
                hideCallback = null;
            });
            CheckParentHide();
        }

        private void CheckParentHide()
        {
            if (tipsMark.parent == null || tipsMark.parent.objectTips == null)
            {
                return;
            }

            //todo 后续应该将ui重要的组件暴露出来
            GraphicRaycaster graphicRaycaster = GameObject.Find("UIRoot").GetComponent<GraphicRaycaster>();
            EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerData, results);
            bool flag = false;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject == tipsMark.parent.objectTips.gameObject)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                tipsMark.parent.objectTips.Hide();
            }
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