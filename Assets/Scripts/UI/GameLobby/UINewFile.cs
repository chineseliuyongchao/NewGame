using GameQFramework;
using QFramework;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UINewFileData : UIPanelData
    {
    }

    /// <summary>
    /// 新建存档时的提示弹窗
    /// </summary>
    public partial class UINewFile : UIBase
    {
        /// <summary>
        /// 默认存档名字
        /// </summary>
        private string _fileDataName;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UINewFileData ?? new UINewFileData();
            // please add init code here
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UINewFileData ?? new UINewFileData();
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
            determineButton.onClick.AddListener(() =>
            {
                string dataName = inputFileData.text;
                if (dataName.Equals(""))
                {
                    this.GetSystem<IGameSaveSystem>().SaveGame(this.GetUtility<IGameUtility>().TimeYToS());
                }
                else
                {
                    this.GetSystem<IGameSaveSystem>().SaveGame(dataName);
                }

                CloseSelf();
            });
            cancellationButton.onClick.AddListener(CloseSelf);
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            _fileDataName = this.GetUtility<IGameUtility>().TimeYToS();
            inputFileData.placeholder.GetComponent<TextMeshProUGUI>().text = _fileDataName;
        }

        protected override Transform AnimTransform()
        {
            return root;
        }
    }
}