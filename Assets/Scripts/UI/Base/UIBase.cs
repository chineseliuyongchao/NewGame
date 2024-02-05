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
        protected ResLoader mResLoader = ResLoader.Allocate();
        private GameObject _mMaskObject;

        #region 通用弹窗效果

        [Header("通用弹窗效果")] public bool needCloseAnim;
        public bool needOpenAnim;
        public float performTime = 0.3f; //开启或关闭时间
        public Vector3 animScale = new(1.04f, 1.04f, 1.04f); //弹性尺寸
        protected Sequence showSequence;
        protected CanvasGroup canvasGroup;

        #endregion

        [Header("遮罩")] public bool bShowMask;
        [Header("暂停时间")] public bool stopTime;

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
                var prefab = mResLoader.LoadSync<GameObject>("UIMask");
                _mMaskObject = Instantiate(prefab, parent);
                _mMaskObject.transform.SetSiblingIndex(0);
            }
        }

        protected override void OnInit(IUIData uiData = null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            OnListenButton();
            OnListenEvent();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            ShowUIMask(transform);
            if (stopTime)
            {
                this.SendCommand(new ShowDialogCommand(true));
            }
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
            if (stopTime)
            {
                this.SendCommand(new ShowDialogCommand(false));
            }
        }

        protected override void OnClose()
        {
            mResLoader.Recycle2Cache();
            mResLoader = null;
            DOTween.Kill(gameObject);
        }

        protected override void CloseSelf()
        {
            if (needCloseAnim)
            {
                CloseAnim(() =>
                {
                    base.CloseSelf();
                    canvasGroup.interactable = true;
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
            showSequence = DOTween.Sequence();
            AnimTransform().localScale = Vector3.zero;
            canvasGroup.alpha = 0;
            showSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
            showSequence.Join(canvasGroup.DOFade(1, performTime));
            showSequence.Append(AnimTransform().DOScale(Vector3.one, performTime));
        }

        /// <summary>
        /// 关闭弹窗时的动画
        /// </summary>
        protected virtual void CloseAnim(UICloseBack callBack)
        {
            showSequence = DOTween.Sequence();
            AnimTransform().localScale = Vector3.one;
            canvasGroup.alpha = 1;
            showSequence.Append(AnimTransform().DOScale(animScale, performTime / 2));
            showSequence.Append(AnimTransform().DOScale(Vector3.zero, performTime));
            showSequence.Join(canvasGroup.DOFade(0, performTime));
            showSequence.AppendCallback(() =>
            {
                callBack?.Invoke();
            });
        }
    }
}