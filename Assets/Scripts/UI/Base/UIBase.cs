using DG.Tweening;
using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;
using DOTween = DG.Tweening.DOTween;
using Sequence = DG.Tweening.Sequence;

namespace UI
{
    /// <summary>
    /// UI界面的基类
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIBase : UIPanel, IController
    {
        private IArchitecture _mArchitecture;
        private ResLoader _mResLoader = ResLoader.Allocate();
        private GameObject _mMaskObject;

        #region 通用弹窗效果

        [Header("通用弹窗效果")] public bool needCloseAnim;
        public bool needOpenAnim;
        public float performTime = 0.3f; //开启或关闭时间
        public Vector3 animScale = new(1.04f, 1.04f, 1.04f); //弹性尺寸
        protected Sequence ShowSequence;
        protected CanvasGroup CanvasGroup;

        #endregion

        [Header("遮罩")] public bool bShowMask;

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            // return mArchitecture;
            return GameApp.Interface;
        }

        private void ShowUIMask(Transform parent)
        {
            if (!bShowMask)
            {
                return;
            }

            if (_mMaskObject == null)
            {
                var prefab = _mResLoader.LoadSync<GameObject>("UIMask");
                _mMaskObject = Instantiate(prefab, parent);
                _mMaskObject.transform.SetSiblingIndex(0);
            }
        }

        protected override void OnInit(IUIData uiData = null)
        {
            CanvasGroup = GetComponent<CanvasGroup>();
            if (CanvasGroup == null)
            {
                CanvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            OnListenButton();
            OnListenEvent();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            ShowUIMask(transform);
        }

        protected override void OnShow()
        {
            if (needOpenAnim)
            {
                ShowCanvas();
            }
        }

        private void ShowCanvas()
        {
            OpenAnim();
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
            _mResLoader.Recycle2Cache();
            _mResLoader = null;
            DOTween.Kill(gameObject);
        }

        protected override void CloseSelf()
        {
            if (needCloseAnim)
            {
                CloseAnim(() =>
                {
                    base.CloseSelf();
                    CanvasGroup.interactable = true;
                });
            }
            else
            {
                base.CloseSelf();
            }
        }

        protected virtual void OnListenButton()
        {
        }

        protected virtual void OnListenEvent()
        {
        }

        /// <summary>
        /// 用于做效果动作的Transform
        /// </summary>
        /// <returns></returns>
        protected virtual Transform AnimTransform()
        {
            return transform;
        }

        /// <summary>
        /// 打开弹窗时的动画
        /// </summary>
        protected virtual void OpenAnim()
        {
            ShowSequence = DOTween.Sequence();
            AnimTransform().localScale = Vector3.zero;
            CanvasGroup.alpha = 0;
            ShowSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
            ShowSequence.Join(CanvasGroup.DOFade(1, performTime));
            ShowSequence.Append(AnimTransform().DOScale(Vector3.one, performTime));
        }

        /// <summary>
        /// 关闭弹窗时的动画
        /// </summary>
        protected virtual void CloseAnim(UICloseBack callBack)
        {
            ShowSequence = DOTween.Sequence();
            AnimTransform().localScale = Vector3.one;
            CanvasGroup.alpha = 1;
            ShowSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
            ShowSequence.Append(AnimTransform().DOScale(Vector3.zero, performTime));
            ShowSequence.Join(CanvasGroup.DOFade(0, performTime));
            ShowSequence.AppendCallback(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}