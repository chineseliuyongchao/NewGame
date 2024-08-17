using System;
using Game.GameMenu;
using UnityAttribute;
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
        public int ARMId;

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
            //todo 缺失总弹药量
            set => _nowAmmo = Mathf.Clamp(value, 0, 100);
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

        /// <summary>
        /// 存放一个引用，方便快速获取
        /// </summary>
        public readonly ArmDataType ArmDataType;

        public ArmData(ArmData armData)
        {
            ARMId = armData.ARMId;
            _nowHp = armData._nowHp;
            _nowTroops = armData._nowTroops;
            NowAmmo = armData.NowAmmo;
            NowMorale = armData.NowMorale;
            NowFatigue = armData.NowFatigue;
        }

        public ArmData(ArmDataType armDataType, int id)
        {
            ArmDataType = armDataType;
            ARMId = id;
            _nowHp = armDataType.totalHp;
            _nowTroops = armDataType.totalTroops;
            NowAmmo = armDataType.ammo;
            NowMorale = armDataType.maximumMorale;
            NowFatigue = armDataType.maximumFatigue;
        }

        public void Reset(ArmData data)
        {
            ARMId = data.ARMId;
            _nowHp = data._nowHp;
            _nowTroops = data._nowTroops;
            NowAmmo = data.NowAmmo;
            NowMorale = data.NowMorale;
            NowFatigue = data.NowFatigue;
        }
    }

    /// <summary>
    /// 攻击形式
    /// </summary>
    public enum AttackFormType
    {
        /// <summary>
        /// 单向攻击：只有队伍a会攻击，b不会反击
        /// </summary>
        ONE_WAY_ATTACK,

        /// <summary>
        /// 互相攻击：a会攻击，b也会反击
        /// </summary>
        MUTUAL_ATTACK,

        /// <summary>
        /// 互相进攻：a会进攻，b也会进攻，同时ab都会反击
        /// </summary>
        MUTUAL_OFFENSE
    }
}