using System.Collections.Generic;
using Fight.Model;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace UI
{
    public class UIFightCreateData : UIPanelData
    {
    }

    /// <summary>
    /// 创建战斗的界面
    /// </summary>
    public partial class UIFightCreate : UIBase
    {
        public GameObject belligerentPrefab;
        public Dictionary<int, UIFightCreateLegion> belligerents1;
        public Dictionary<int, UIFightCreateLegion> belligerents2;

        /// <summary>
        /// 当前正在展示的参战军团
        /// </summary>
        private int _nowShow = -1;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateData ?? new UIFightCreateData();
            // please add init code here
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateData ?? new UIFightCreateData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            createButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.FIGHT_SCENE);
            });
            leaveButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE);
            });
            belligerent1Add.onClick.AddListener(() => { AddLegion(0); });
            belligerent2Add.onClick.AddListener(() => { AddLegion(1); });
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            this.GetModel<IFightCreateModel>().AllLegions.Clear();
            belligerents1 = new Dictionary<int, UIFightCreateLegion>();
            belligerents2 = new Dictionary<int, UIFightCreateLegion>();
            AddLegion(0);
            ChangeShow(0);
            AddLegion(1);
        }

        /// <summary>
        /// 添加一个参战军团
        /// </summary>
        /// <param name="belligerentsId">参战方id</param>
        private void AddLegion(int belligerentsId)
        {
            Dictionary<int, UIFightCreateLegion> belligerents;
            Transform buttonTransform;
            switch (belligerentsId)
            {
                case 0:
                    belligerents = belligerents1;
                    buttonTransform = belligerent1Add.transform;
                    break;
                case 1:
                    belligerents = belligerents2;
                    buttonTransform = belligerent2Add.transform;
                    break;
                default:
                    return;
            }

            int num = belligerents.Count;
            int newLegionId = belligerentsId * 100 + num;
            LegionInfo legionInfo = new LegionInfo
            {
                legionId = newLegionId,
                belligerentsId = belligerentsId,
                factionsId = 0,
                allArm = new Dictionary<int, ArmData>()
            };
            this.GetModel<IFightCreateModel>().AllLegions.Add(newLegionId, legionInfo);
            GameObject newLegion = Instantiate(belligerentPrefab, belligerentGroup);
            newLegion.transform.SetSiblingIndex(buttonTransform.GetSiblingIndex());
            UIFightCreateLegion uiFightCreateLegion = newLegion.GetComponent<UIFightCreateLegion>();
            uiFightCreateLegion.InitUI(newLegionId, this);
            belligerents.Add(newLegionId, uiFightCreateLegion);
        }

        /// <summary>
        /// 删除一个参战军团
        /// </summary>
        public void DeleteLegion(int legionId)
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[legionId];
            if (legionInfo == null)
            {
                return;
            }

            Dictionary<int, UIFightCreateLegion> belligerents;
            switch (legionInfo.belligerentsId)
            {
                case 0:
                    belligerents = belligerents1;
                    break;
                case 1:
                    belligerents = belligerents2;
                    break;
                default:
                    return;
            }

            UIFightCreateLegion uiFightCreateLegion = belligerents[legionId];
            belligerents.Remove(legionId);
            uiFightCreateLegion.gameObject.DestroySelf();
            this.GetModel<IFightCreateModel>().AllLegions.Remove(legionId);
            if (_nowShow == legionId)
            {
                _nowShow = -1;
            }
        }

        public void ChangeShow(int legionId)
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[legionId];
            if (legionInfo == null)
            {
                return;
            }

            Dictionary<int, UIFightCreateLegion> belligerents;
            switch (legionInfo.belligerentsId)
            {
                case 0:
                    belligerents = belligerents1;
                    break;
                case 1:
                    belligerents = belligerents2;
                    break;
                default:
                    return;
            }

            if (_nowShow != -1)
            {
                UIFightCreateLegion after = belligerents[_nowShow];
                after.ChangeShow(false);
            }

            _nowShow = legionId;
            UIFightCreateLegion now = belligerents[_nowShow];
            now.ChangeShow(true);
        }
    }
}