using DG.Tweening;
using GameQFramework;
using QFramework;
using UnityEngine;
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
        private Sequence _showSequence;
        private CanvasGroup _canvasGroup;

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

        protected override void CloseSelf()
        {
            if (needCloseAnim)
            {
                _showSequence = DOTween.Sequence();
                AnimTransform().localScale = Vector3.one;
                _canvasGroup.alpha = 1;
                _showSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
                _showSequence.Append(AnimTransform().DOScale(Vector3.zero, performTime));
                _showSequence.Join(_canvasGroup.DOFade(0, performTime));
                _showSequence.AppendCallback(() =>
                {
                    base.CloseSelf();
                    _canvasGroup.interactable = true;
                });
            }
            else
            {
                base.CloseSelf();
            }
        }

        private void ShowCanvas()
        {
            _showSequence = DOTween.Sequence();
            AnimTransform().localScale = Vector3.zero;
            _canvasGroup.alpha = 0;
            _showSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
            _showSequence.Join(_canvasGroup.DOFade(1, performTime));
            _showSequence.Append(AnimTransform().DOScale(Vector3.one, performTime));
        }

        protected override void OnInit(IUIData uiData = null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
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

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
            _mResLoader.Recycle2Cache();
            _mResLoader = null;
            DOTween.Kill(gameObject);
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
    }
}