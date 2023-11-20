using System;
using QFramework;
using Utils;

namespace UI
{
    public class MapLatticeData : UIPanelData
    {
    }

    /// <summary>
    /// 用于测试寻路算法的每个按钮
    /// </summary>
    public partial class MapLattice : UIPanel
    {
        public IntVector2 Pos;
        public Action<IntVector2> Operate;

        private void Awake()
        {
            OnInit();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as MapLatticeData ?? new MapLatticeData();
            // please add init code here
            button.onClick.AddListener(() => { Operate(Pos); });
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }

        public void SetCannotPass()
        {
            button.interactable = false;
        }

        public void ShowRoute()
        {
            route.gameObject.SetActive(true);
        }
    }
}