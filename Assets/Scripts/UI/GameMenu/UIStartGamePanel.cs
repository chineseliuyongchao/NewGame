using System.Collections.Generic;
using GameQFramework;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIStartGamePanelData : UIPanelData
    {
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
            newGameButton.onClick.AddListener(() => { this.GetSystem<IGameSystem>().ChangeMainGameScene(); });
            backToMenu.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeToMainGameSceneEvent>(_ => { CloseSelf(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<DeleteFileEvent>(_ => { UpdateUI(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnInitUI()
        {
            List<string> list = this.GetSystem<IGameSaveSystem>().LoadGameFileList();
            for (int i = 0; i < list.Count; i++)
            {
                UIFileData fileData = Instantiate(uiFileData, fileDataContent);
                fileData.InitUI(list[i]);
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            List<string> list = this.GetSystem<IGameSaveSystem>().LoadGameFileList();
            Vector2 size = fileDataContent.GetComponent<RectTransform>().sizeDelta;
            size.y = list.Count * UIFileData.UI_FILE_DATA_HEIGHT;
            fileDataContent.GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}