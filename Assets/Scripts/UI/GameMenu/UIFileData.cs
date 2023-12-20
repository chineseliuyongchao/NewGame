using GameQFramework;
using QFramework;

namespace UI
{
    public class UIFileDataData : UIPanelData
    {
    }

    /// <summary>
    /// 展示每个存档
    /// </summary>
    public partial class UIFileData : UIBase
    {
        /// <summary>
        /// 每个UIFileData占的高度
        /// </summary>
        public const float UI_FILE_DATA_HEIGHT = 100;

        /// <summary>
        /// 游戏存档名字
        /// </summary>
        private string _fileNameValue;

        private void Awake()
        {
            OnInit();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFileDataData ?? new UIFileDataData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFileDataData ?? new UIFileDataData();
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
            open.onClick.AddListener(() => { this.GetSystem<IGameSystem>().ChangeMainGameScene(_fileNameValue); });
            delete.onClick.AddListener(() =>
            {
                this.GetSystem<IGameSaveSystem>().DeleteGame(_fileNameValue);
                this.SendCommand(new DeleteFileCommand());
                Destroy(gameObject);
            });
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(string fileDataName)
        {
            _fileNameValue = fileDataName;
            fileName.text = fileDataName;
        }
    }
}