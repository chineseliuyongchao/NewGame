using Game.GameMenu;
using QFramework;

namespace UI
{
    public class UIFightCreateUnitData : UIPanelData
    {
    }

    public partial class UIFightCreateUnit : UIBase
    {
        /// <summary>
        /// 阵营id
        /// </summary>
        private int _unitId;

        /// <summary>
        /// 兵种id
        /// </summary>
        private int _armId;

        private UIFightCreate _uiFightCreate;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateUnitData ?? new UIFightCreateUnitData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateUnitData ?? new UIFightCreateUnitData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            delete.onClick.AddListener(() => { _uiFightCreate.DeleteUnit(_unitId); });
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(int unitId, int armId, UIFightCreate uiFightCreate)
        {
            _unitId = unitId;
            _armId = armId;
            _uiFightCreate = uiFightCreate;
            armName.text = this.GetModel<IGameMenuModel>().ARMDataTypes[_armId].unitName;
            Init();
        }
    }
}