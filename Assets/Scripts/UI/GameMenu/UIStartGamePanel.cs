using System.Collections.Generic;
using DG.Tweening;
using Game.GameBase;
using Game.GameMenu;
using Game.GameSave;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIStartGamePanelData : UIPanelData
    {
        /// <summary>
        /// 本次打开存档界面是读取存档还是覆盖存档
        /// </summary>
        public readonly bool isLoad;

        /// <summary>
        /// 本次打开存档界面是在主菜单还是游戏界面
        /// </summary>
        public readonly bool isInMenu;

        public UIStartGamePanelData(bool isLoad = true, bool isInMenu = true)
        {
            this.isLoad = isLoad;
            this.isInMenu = isInMenu;
        }
    }

    /// <summary>
    /// 开始游戏界面
    /// </summary>
    public partial class UIStartGamePanel : UIBase
    {
        public UIFileData uiFileData;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIStartGamePanelData ?? new UIStartGamePanelData();
            // please add init code here
            base.OnInit(uiData);
            OnInitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIStartGamePanelData ?? new UIStartGamePanelData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnClose()
        {
            base.OnClose();
        }

        protected override void OnListenButton()
        {
            newGameButton.onClick.AddListener(() => { this.GetSystem<IGameSystem>().ChangeGameCreateScene(); });
            newFileButton.onClick.AddListener(() => { UIKit.OpenPanel<UINewFile>(new UINewFileData()); });
            backToMenuButton.onClick.AddListener(CloseSelf);
            backToGameButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeToMainGameSceneEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<DeleteFileEvent>(_ => { UpdateUI(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<SaveFileDataEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnInitUI()
        {
            List<string> list = this.GetSystem<IGameSaveSystem>().LoadGameFileList();
            for (int i = 0; i < list.Count; i++)
            {
                UIFileData fileData = Instantiate(uiFileData, fileDataContent);
                fileData.InitUI(list[i], mData.isLoad);
            }

            if (mData.isLoad)
            {
                newFileButton.gameObject.SetActive(false);
            }
            else
            {
                newGameButton.gameObject.SetActive(false);
            }

            if (mData.isInMenu)
            {
                backToGameButton.gameObject.SetActive(false);
            }
            else
            {
                backToMenuButton.gameObject.SetActive(false);
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            List<string> list = this.GetSystem<IGameSaveSystem>().LoadGameFileList();
            Vector2 size = fileDataContent.sizeDelta;
            size.y = list.Count * UIFileData.UI_FILE_DATA_HEIGHT;
            fileDataContent.sizeDelta = size;
        }

        protected override Transform AnimTransform()
        {
            return root;
        }

        protected override void OpenAnim()
        {
            showSequence = DOTween.Sequence();
            RectTransform rectTransform = AnimTransform().GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-600, 0);
            canvasGroup.alpha = 0;
            showSequence.Join(canvasGroup.DOFade(1, performTime));
            showSequence.Append(rectTransform.DOAnchorPos(Vector2.one, performTime).SetEase(Ease.OutQuart));
        }

        protected override void CloseAnim(UICloseBack callBack)
        {
            showSequence = DOTween.Sequence();
            RectTransform rectTransform = AnimTransform().GetComponent<RectTransform>();
            canvasGroup.alpha = 1;
            showSequence.Append(rectTransform.DOAnchorPos(new Vector2(-600, 0), performTime));
            showSequence.Join(canvasGroup.DOFade(0, performTime));
            showSequence.AppendCallback(() => { callBack?.Invoke(); });
        }
    }
}