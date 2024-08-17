using System;
using Game.GameMenu;
using UnityEngine;

namespace Fight.Model
{
    /// <summary>
    /// 在攻击模拟时记录一个单位的实时属性
    /// </summary>
    [Serializable]
    public class ArmData
    {
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
            set => _nowHp = Mathf.Clamp(value, 0, ArmDataType.totalHp);
        }

        private int _nowTroops;

        /// <summary>
        /// 当前人数
        /// </summary>
        public int NowTroops
        {
            get => _nowTroops;
            set => _nowTroops = Mathf.Clamp(value, 0, ArmDataType.totalTroops);
        }

        private int _nowAmmo;

        /// <summary>
        /// 当前弹药量
        /// </summary>
        public int NowAmmo
        {
            get => _nowAmmo;
            set => _nowAmmo = Mathf.Clamp(value, 0, ArmDataType.ammo);
        }

        private int _nowMorale;

        /// <summary>
        /// 当前作战意志
        /// </summary>
        public int NowMorale
        {
            get => _nowMorale;
            set => _nowMorale = Mathf.Clamp(value, 0, ArmDataType.maximumMorale);
        }

        private int _nowFatigue;

        /// <summary>
        /// 当前疲劳值
        /// </summary>
        public int NowFatigue
        {
            get => _nowFatigue;
            set => _nowFatigue = Mathf.Clamp(value, 0, ArmDataType.maximumFatigue);
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
        public readonly ArmDataType ArmDataType;

        public ArmData(ArmData armData)
        {
            ArmDataType = armData.ArmDataType;
            armId = armData.armId;
            _nowHp = armData._nowHp;
            _nowTroops = armData._nowTroops;
            NowAmmo = armData.NowAmmo;
            NowMorale = armData.NowMorale;
            NowFatigue = armData.NowFatigue;
            IsCharge = armData.IsCharge;
            IsStick = armData.IsStick;
        }

        public ArmData(ArmDataType armDataType, int id)
        {
            ArmDataType = armDataType;
            armId = id;
            _nowHp = armDataType.totalHp;
            _nowTroops = armDataType.totalTroops;
            NowAmmo = armDataType.ammo;
            NowMorale = armDataType.maximumMorale;
            NowFatigue = armDataType.maximumFatigue;
            IsCharge = false;
            IsStick = false;
        }

        public void Reset(ArmData data)
        {
            armId = data.armId;
            _nowHp = data._nowHp;
            _nowTroops = data._nowTroops;
            NowAmmo = data.NowAmmo;
            NowMorale = data.NowMorale;
            NowFatigue = data.NowFatigue;
        }
    }
}