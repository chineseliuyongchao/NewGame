using System.Collections.Generic;
using Fight.Utils;
using Game.FightCreate;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIFightCreateLegionData : UIPanelData
    {
    }

    public partial class UIFightCreateLegion : UIBase
    {
        /// <summary>
        /// 阵营id
        /// </summary>
        private int _legionId;

        private UIFightCreate _uiFightCreate;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateLegionData ?? new UIFightCreateLegionData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateLegionData ?? new UIFightCreateLegionData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            delete.onClick.AddListener(() =>
            {
                if (_legionId == 0)
                {
                    return; //玩家军队不能删除
                }

                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_legionId];
                if (legionInfo == null)
                {
                    return;
                }

                Dictionary<int, UIFightCreateLegion> camps;
                switch (legionInfo.campId)
                {
                    case Constants.BELLIGERENT1:
                        camps = _uiFightCreate.campA;
                        break;
                    case Constants.BELLIGERENT2:
                        camps = _uiFightCreate.campB;
                        break;
                    default:
                        return;
                }

                if (camps.Count <= 1)
                {
                    return; //如果阵营只剩一个军队也不能删除
                }

                _uiFightCreate.DeleteLegion(_legionId);
            });
            chooseButton.onClick.AddListener(() => { _uiFightCreate.ChangeShowLegion(_legionId); });
        }

        protected override void OnListenEvent()
        {
        }

        public void InitUI(int id, UIFightCreate uiFightCreate)
        {
            _legionId = id;
            _uiFightCreate = uiFightCreate;
            Init();
            legionName.text = "军队：" + _legionId;
        }

        public void ChangeShow(bool isShow)
        {
            transform.localScale = isShow ? new Vector3(1.1f, 1.1f, 1.1f) : new Vector3(1, 1, 1);
        }
    }
}