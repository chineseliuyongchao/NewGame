using System;
using Fight.Utils;
using Game.GameMenu;
using UnityAttribute;
using UnityEngine;

namespace Fight.Model
{
    /// <summary>
    /// 记录一个单位的实时属性
    /// </summary>
    [Serializable]
    public class UnitData
    {
        /// <summary>
        /// 兵种id
        /// </summary>
        [Label("兵种id")] public int armId;

        /// <summary>
        /// 所属军队的id
        /// </summary>
        [Label("所属军队的id")] public int legionId;

        /// <summary>
        /// 战场上的单位id
        /// </summary>
        [Label("单位id")] public int unitId;

        /// <summary>
        /// 该兵种在战场上的位置信息
        /// </summary>
        [Label("位置信息")] public int currentPosIndex;

        /// <summary>
        /// 存放一个引用，方便快速获取
        /// </summary>
        [Label("兵种属性")] public ArmDataType armDataType;

        private bool _isCharge;

        private bool _isStick;

        private int _nowAmmo;

        private int _nowFatigue;

        private int _nowHp;

        private int _nowMorale;

        private int _nowTroops;

        private int _nowActionPoints;

        private UnitType _unitType;

        public UnitData(UnitData unitData)
        {
            armDataType = unitData.armDataType;
            legionId = unitData.legionId;
            unitId = unitData.unitId;
            armId = unitData.armId;
            NowHp = unitData.NowHp;
            NowTroops = unitData.NowTroops;
            NowAmmo = unitData.NowAmmo;
            NowMorale = unitData.NowMorale;
            NowFatigue = unitData.NowFatigue;
            NowActionPoints = unitData.NowActionPoints;
            IsCharge = unitData.IsCharge;
            IsStick = unitData.IsStick;
            UnitType = unitData.UnitType;
        }

        public UnitData(ArmDataType armDataType, int id, int legionId)
        {
            this.armDataType = armDataType;
            this.legionId = legionId;
            unitId = id;
            armId = armDataType.ID;
            NowHp = armDataType.totalHp;
            NowTroops = armDataType.totalTroops;
            NowAmmo = armDataType.ammo;
            NowMorale = armDataType.maximumMorale;
            NowFatigue = armDataType.maximumFatigue;
            NowActionPoints = Constants.InitActionPoints;
            IsCharge = false;
            IsStick = false;
            UnitType = UnitType.NORMAL;
        }

        /// <summary>
        /// 当前血量
        /// </summary>
        public int NowHp
        {
            get => _nowHp;
            set => _nowHp = Mathf.Clamp(value, 0, armDataType.totalHp);
        }

        /// <summary>
        /// 当前人数
        /// </summary>
        public int NowTroops
        {
            get => _nowTroops;
            set => _nowTroops = Mathf.Clamp(value, 0, armDataType.totalTroops);
        }

        /// <summary>
        /// 当前弹药量
        /// </summary>
        public int NowAmmo
        {
            get => _nowAmmo;
            set => _nowAmmo = Mathf.Clamp(value, 0, armDataType.ammo);
        }

        /// <summary>
        /// 当前作战意志
        /// </summary>
        public int NowMorale
        {
            get => _nowMorale;
            set => _nowMorale = Mathf.Clamp(value, 0, armDataType.maximumMorale);
        }

        /// <summary>
        /// 当前疲劳值
        /// </summary>
        public int NowFatigue
        {
            get => _nowFatigue;
            set => _nowFatigue = Mathf.Clamp(value, 0, armDataType.maximumFatigue);
        }

        /// <summary>
        /// 当前的行动点数
        /// </summary>
        public int NowActionPoints
        {
            get => _nowActionPoints;
            set => _nowActionPoints = Mathf.Clamp(value, 0, Constants.InitActionPoints);
        }

        /// <summary>
        /// 单位是否在冲锋
        /// </summary>
        public bool IsCharge
        {
            get => _isCharge;
            set => _isCharge = value;
        }

        /// <summary>
        /// 单位是否在坚守
        /// </summary>
        public bool IsStick
        {
            get => _isStick;
            set => _isStick = value;
        }

        /// <summary>
        /// 单位状态
        /// </summary>
        public UnitType UnitType
        {
            get => _unitType;
            set => _unitType = value;
        }

        public void Reset(UnitData data)
        {
            armDataType = data.armDataType;
            legionId = data.legionId;
            unitId = data.unitId;
            armId = data.armId;
            NowHp = data.NowHp;
            NowTroops = data.NowTroops;
            NowAmmo = data.NowAmmo;
            NowMorale = data.NowMorale;
            NowFatigue = data.NowFatigue;
            NowActionPoints = data.NowActionPoints;
            IsCharge = data._isCharge;
            IsStick = data.IsStick;
            UnitType = data.UnitType;
        }
    }
}