using System.Collections.Generic;
using Game.FightCreate;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIFightCreateBelligerentData : UIPanelData
    {
    }

    public partial class UIFightCreateLegion : UIBase
    {
        /// <summary>
        /// 参战方id
        /// </summary>
        private int _legionId;

        private UIFightCreate _uiFightCreate;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateBelligerentData ?? new UIFightCreateBelligerentData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateBelligerentData ?? new UIFightCreateBelligerentData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            delete.onClick.AddListener(() =>
            {
                if (_legionId == 0)
                {
                    return; //玩家军团不能删除
                }

                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_legionId];
                if (legionInfo == null)
                {
                    return;
                }

                Dictionary<int, UIFightCreateLegion> belligerents;
                switch (legionInfo.belligerentsId)
                {
                    case 0:
                        belligerents = _uiFightCreate.belligerents1;
                        break;
                    case 1:
                        belligerents = _uiFightCreate.belligerents2;
                        break;
                    default:
                        return;
                }

                if (belligerents.Count <= 1)
                {
                    return; //如果参战方只剩一个军团也不能删除
                }

                _uiFightCreate.DeleteLegion(_legionId);
            });
            chooseButton.onClick.AddListener(() => { _uiFightCreate.ChangeShow(_legionId); });
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(int id, UIFightCreate uiFightCreate)
        {
            _legionId = id;
            _uiFightCreate = uiFightCreate;
            Init();
        }

        public void ChangeShow(bool isShow)
        {
            transform.localScale = isShow ? new Vector3(1.1f, 1.1f, 1.1f) : new Vector3(1, 1, 1);
        }
    }
}