using System.Collections.Generic;
using Fight;
using Fight.Utils;
using Game.FightCreate;
using Game.GameBase;
using Game.GameMenu;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
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
        public GameObject unitPrefab;
        public Dictionary<int, UIFightCreateUnit> uiFightCreateUnits;

        /// <summary>
        /// 当前正在展示的参战军队
        /// </summary>
        private int _nowLegionId = -1;

        /// <summary>
        /// 当前选中的派系
        /// </summary>
        private int _chooseFactionId;

        /// <summary>
        /// 当前选中的兵种
        /// </summary>
        private int _chooseArmId;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIFightCreateData ?? new UIFightCreateData();
            // please add init code here
            InitUI();
            base.OnInit(uiData);
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

                //这些应该放到加载界面处理，现在没有加载界面，所以先放这里
                GameApp.Interface.RegisterSystem<IFightComputeSystem>(new FightComputeSystem());
                GameApp.Interface.RegisterSystem<IFightSystem>(new FightSystem());
                this.GetSystem<IFightComputeSystem>().ComputeUnitPos();

                GameApp.Interface.RegisterModel<IAStarModel>(new AStarModel());
                GameApp.Interface.RegisterModel<IFightVisualModel>(new FightVisualModel());
                GameApp.Interface.RegisterModel<IFightCoreModel>(new FightCoreModel());
                this.GetSystem<IFightSystem>().InitFightData();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.FIGHT_SCENE);
            });
            leaveButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE);
            });
            belligerent1Add.onClick.AddListener(() => { AddLegion(Constants.BELLIGERENT1); });
            belligerent2Add.onClick.AddListener(() => { AddLegion(Constants.BELLIGERENT2); });
            chooseFight.onValueChanged.AddListener(type =>
            {
                List<int> factionId = new List<int>(this.GetModel<IGameMenuModel>().FactionDataTypes.Keys);
                if (_chooseFactionId != factionId[type])
                {
                    ClearUnit(true, true);
                }

                _chooseFactionId = factionId[type];
                ChangeShowFight(_chooseFactionId);
            });
            chooseArm.onValueChanged.AddListener(type =>
            {
                List<int> armID = new List<int>(this.GetModel<IGameMenuModel>().ARMDataTypes.Keys);
                int index = 0;
                for (int i = 0; i < armID.Count; i++)
                {
                    ArmDataType armDataType = this.GetModel<IGameMenuModel>().ARMDataTypes[armID[i]];
                    if (armDataType.ID / 100 == _chooseFactionId) //属于派系的兵种
                    {
                        if (index == type)
                        {
                            _chooseArmId = armDataType.ID;
                            break;
                        }

                        index++;
                    }
                }
            });
            belligerent3Add.onClick.AddListener(() => { AddUnit(_chooseArmId); });
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            this.GetModel<IFightCreateModel>().AllLegions.Clear();
            belligerents1 = new Dictionary<int, UIFightCreateLegion>();
            belligerents2 = new Dictionary<int, UIFightCreateLegion>();
            uiFightCreateUnits = new Dictionary<int, UIFightCreateUnit>();
            List<int> factionKeys = new List<int>(this.GetModel<IGameMenuModel>().FactionDataTypes.Keys);
            _chooseFactionId = factionKeys[0];
            List<int> armKeys = new List<int>(this.GetModel<IGameMenuModel>().ARMDataTypes.Keys);
            _chooseArmId = armKeys[0];
            AddLegion(Constants.BELLIGERENT1);
            ChangeShowLegion(Constants.PlayLegionId); //系统默认为玩家添加的军队就会是这个id
            AddLegion(Constants.BELLIGERENT2);

            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            List<int> factionId = new List<int>(this.GetModel<IGameMenuModel>().FactionDataTypes.Keys);
            for (int i = 0; i < factionId.Count; i++)
            {
                options.Add(new Dropdown.OptionData(this.GetModel<IGameMenuModel>().FactionDataTypes[factionId[i]]
                    .FactionName));
            }

            // 将选项列表添加到Dropdown组件
            chooseFight.options = options;
            // 设置默认选项（可选）
            chooseFight.value = 0;
            _chooseFactionId = factionId[0];
            ChangeShowFight(_chooseFactionId);

            createButton.interactable = false;
        }

        /// <summary>
        /// 直接记录上一个军队的id，方便后续军队的id扩充
        /// </summary>
        private int _lastLegionId;

        /// <summary>
        /// 添加一个参战军队
        /// </summary>
        /// <param name="belligerentsId">阵营id</param>
        private void AddLegion(int belligerentsId)
        {
            Dictionary<int, UIFightCreateLegion> belligerents;
            Transform buttonTransform;
            switch (belligerentsId)
            {
                case Constants.BELLIGERENT1:
                    belligerents = belligerents1;
                    buttonTransform = belligerent1Add.transform;
                    break;
                case Constants.BELLIGERENT2:
                    belligerents = belligerents2;
                    buttonTransform = belligerent2Add.transform;
                    break;
                default:
                    return;
            }

            int legionIdMul = 1000;
            //新的军队id的个位根据上一个军队的id个位加1得到，确保每个军队的id都不一样
            int newLegionId = belligerentsId * legionIdMul + _lastLegionId % legionIdMul + 1;
            _lastLegionId = newLegionId;
            LegionInfo legionInfo = new LegionInfo
            {
                legionId = newLegionId,
                belligerentsId = belligerentsId,
                factionsId = 0,
                allUnit = new Dictionary<int, UnitData>()
            };
            this.GetModel<IFightCreateModel>().AllLegions.Add(newLegionId, legionInfo);
            GameObject newLegion = Instantiate(belligerentPrefab, belligerentGroup);
            newLegion.transform.SetSiblingIndex(buttonTransform.GetSiblingIndex());
            UIFightCreateLegion uiFightCreateLegion = newLegion.GetComponent<UIFightCreateLegion>();
            uiFightCreateLegion.InitUI(newLegionId, this);
            belligerents.Add(newLegionId, uiFightCreateLegion);
            CheckCanCreate();
        }

        /// <summary>
        /// 删除一个参战军队
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
                case Constants.BELLIGERENT1:
                    belligerents = belligerents1;
                    break;
                case Constants.BELLIGERENT2:
                    belligerents = belligerents2;
                    break;
                default:
                    return;
            }

            UIFightCreateLegion uiFightCreateLegion = belligerents[legionId];
            belligerents.Remove(legionId);
            uiFightCreateLegion.gameObject.DestroySelf();
            this.GetModel<IFightCreateModel>().AllLegions.Remove(legionId);
            if (_nowLegionId == legionId)
            {
                _nowLegionId = -1;
            }

            CheckCanCreate();
        }

        /// <summary>
        /// 切换展示的军队
        /// </summary>
        /// <param name="legionId"></param>
        public void ChangeShowLegion(int legionId)
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[legionId];
            if (legionInfo == null)
            {
                return;
            }

            Dictionary<int, UIFightCreateLegion> belligerents;
            switch (legionInfo.belligerentsId)
            {
                case Constants.BELLIGERENT1:
                    belligerents = belligerents1;
                    break;
                case Constants.BELLIGERENT2:
                    belligerents = belligerents2;
                    break;
                default:
                    return;
            }

            if (_nowLegionId != -1)
            {
                LegionInfo beforeLegionInfo = this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId];
                Dictionary<int, UIFightCreateLegion> beforeBelligerents;
                switch (beforeLegionInfo.belligerentsId)
                {
                    case Constants.BELLIGERENT1:
                        beforeBelligerents = belligerents1;
                        break;
                    case Constants.BELLIGERENT2:
                        beforeBelligerents = belligerents2;
                        break;
                    default:
                        return;
                }

                UIFightCreateLegion before = beforeBelligerents[_nowLegionId];
                before.ChangeShow(false);
            }

            if (_nowLegionId != legionId)
            {
                ClearUnit(false, true);
            }

            _nowLegionId = legionId;
            ShowUnit();
            UIFightCreateLegion now = belligerents[_nowLegionId];
            now.ChangeShow(true);
        }

        /// <summary>
        /// 切换派系
        /// </summary>
        /// <param name="fightId">派系id</param>
        private void ChangeShowFight(int fightId)
        {
            chooseArm.ClearOptions();
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
            List<int> armID = new List<int>(this.GetModel<IGameMenuModel>().ARMDataTypes.Keys);
            int firstArmId = -1;
            for (int i = 0; i < armID.Count; i++)
            {
                ArmDataType armDataType = this.GetModel<IGameMenuModel>().ARMDataTypes[armID[i]];
                if (armDataType.ID / 100 == fightId) //属于派系的兵种
                {
                    if (firstArmId == -1)
                    {
                        firstArmId = armDataType.ID;
                    }

                    options.Add(new Dropdown.OptionData(armDataType.unitName));
                }
            }

            _chooseArmId = firstArmId;
            chooseArm.options = options;
            this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId].factionsId = _chooseFactionId;
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="armId">单位的兵种id</param>
        private void AddUnit(int armId)
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId];
            int newUnitId = legionInfo.lastUnitId + 1;
            int realUnitId = legionInfo.legionId * 1000 + newUnitId; //确保战场每个单位的id都不一样
            UnitData unitData = new UnitData(this.GetModel<IGameMenuModel>().ARMDataTypes[armId], realUnitId,
                legionInfo.legionId);
            legionInfo.lastUnitId = newUnitId;
            legionInfo.allUnit.Add(realUnitId, unitData);
            GameObject unitShow = Instantiate(unitPrefab, showAllUnit);
            UIFightCreateUnit units = unitShow.GetComponent<UIFightCreateUnit>();
            uiFightCreateUnits.Add(realUnitId, units);
            units.InitUI(realUnitId, armId, this);
            CheckCanCreate();
        }

        /// <summary>
        /// 移除单位
        /// </summary>
        public void DeleteUnit(int unitId)
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId];
            legionInfo.allUnit.Remove(unitId);
            UIFightCreateUnit units = uiFightCreateUnits[unitId];
            uiFightCreateUnits.Remove(unitId);
            units.gameObject.DestroySelf();
            CheckCanCreate();
        }

        /// <summary>
        /// 清空所有展示的军队
        /// </summary>
        private void ClearUnit(bool model, bool view)
        {
            if (model)
            {
                LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId];
                legionInfo.allUnit.Clear();
            }

            if (view)
            {
                List<int> unitId = new List<int>(uiFightCreateUnits.Keys);
                for (int i = 0; i < unitId.Count; i++)
                {
                    UIFightCreateUnit units = uiFightCreateUnits[unitId[i]];
                    uiFightCreateUnits.Remove(unitId[i]);
                    units.gameObject.DestroySelf();
                }
            }

            CheckCanCreate();
        }

        /// <summary>
        /// 展示一个军队
        /// </summary>
        private void ShowUnit()
        {
            LegionInfo legionInfo = this.GetModel<IFightCreateModel>().AllLegions[_nowLegionId];
            List<int> unitId = new List<int>(legionInfo.allUnit.Keys);
            for (int i = 0; i < unitId.Count; i++)
            {
                UnitData data = legionInfo.allUnit[unitId[i]];
                GameObject unitShow = Instantiate(unitPrefab, showAllUnit);
                UIFightCreateUnit units = unitShow.GetComponent<UIFightCreateUnit>();
                uiFightCreateUnits.Add(data.unitId, units);
                units.InitUI(data.unitId, data.armId, this);
            }
        }

        /// <summary>
        /// 检测是否可以开始战斗，防止在一些不合理的情况下进入战斗（比如兵种没有配全）
        /// </summary>
        private void CheckCanCreate()
        {
            List<int> legionId = new List<int>(this.GetModel<IFightCreateModel>().AllLegions.Keys);
            bool allLegionHasArm = true;
            for (int i = 0; i < legionId.Count; i++)
            {
                LegionInfo info = this.GetModel<IFightCreateModel>().AllLegions[legionId[i]];
                if (info.allUnit.Count <= 0)
                {
                    allLegionHasArm = false;
                }
            }

            createButton.interactable = allLegionHasArm;
        }
    }
}