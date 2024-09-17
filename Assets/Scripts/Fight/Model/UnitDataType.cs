using System;
using Game.GameMenu;
using UnityEngine;

namespace Fight
{
    /// <summary>
    /// 记录一个单位的实时属性
    /// </summary>
    [Serializable]
    public class UnitData
    {
        /// <summary>
        /// 所属军队的id
        /// </summary>
        public int legionId;

        /// <summary>
        /// 战场上的单位id
        /// </summary>
        public int unitId;

        /// <summary>
        /// 该兵种在战场上的位置信息
        /// </summary>
        public int currentPosition;

        /// <summary>
        /// 兵种id
        /// </summary>
        public int armId;

        private int _nowHp;

        /// <summary>
        /// 当前血量
        /// </summary>
        public int NowHp
        {
            get => _nowHp;
            set => _nowHp = Mathf.Clamp(value, 0, armDataType.totalHp);
        }

        private int _nowTroops;

        /// <summary>
        /// 当前人数
        /// </summary>
        public int NowTroops
        {
            get => _nowTroops;
            set => _nowTroops = Mathf.Clamp(value, 0, armDataType.totalTroops);
        }

        private int _nowAmmo;

        /// <summary>
        /// 当前弹药量
        /// </summary>
        public int NowAmmo
        {
            get => _nowAmmo;
            set => _nowAmmo = Mathf.Clamp(value, 0, armDataType.ammo);
        }

        private int _nowMorale;

        /// <summary>
        /// 当前作战意志
        /// </summary>
        public int NowMorale
        {
            get => _nowMorale;
            set => _nowMorale = Mathf.Clamp(value, 0, armDataType.maximumMorale);
        }

        private int _nowFatigue;

        /// <summary>
        /// 当前疲劳值
        /// </summary>
        public int NowFatigue
        {
            get => _nowFatigue;
            set => _nowFatigue = Mathf.Clamp(value, 0, armDataType.maximumFatigue);
        }

        private bool _isCharge;

        /// <summary>
        /// 单位是否在冲锋
        /// </summary>
        public bool IsCharge
        {
            get => _isCharge;
            set => _isCharge = value;
        }

        private bool _isStick;

        /// <summary>
        /// 单位是否在坚守
        /// </summary>
        public bool IsStick
        {
            get => _isStick;
            set => _isStick = value;
        }

        /// <summary>
        /// 存放一个引用，方便快速获取
        /// </summary>
        public ArmDataType armDataType;

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
            IsCharge = unitData.IsCharge;
            IsStick = unitData.IsStick;
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
            IsCharge = false;
            IsStick = false;
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
            IsCharge = data._isCharge;
            IsStick = data.IsStick;
        }
    }
}