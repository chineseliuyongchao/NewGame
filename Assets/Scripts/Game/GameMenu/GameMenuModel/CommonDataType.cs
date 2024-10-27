using System;
using Game.GameUtils;
using UnityAttribute;

namespace Game.GameMenu
{
    /// <summary>
    /// 兵种数据类型
    /// </summary>
    [Serializable]
    public class ArmDataType : BaseJsonData
    {
        /// <summary>
        /// 兵种名称
        /// </summary>
        [Label("兵种名称")] public string unitName;

        /// <summary>
        /// 总血量
        /// </summary>
        [Label("总血量")] public int totalHp;

        /// <summary>
        /// 总人数
        /// </summary>
        [Label("总人数")] public int totalTroops;

        /// <summary>
        /// 攻击能力
        /// </summary>
        [Label("攻击能力")] public int attack;

        /// <summary>
        /// 冲锋加成
        /// </summary>
        [Label("冲锋加成")] public int charge;

        /// <summary>
        /// 防御能力（近战）
        /// </summary>
        [Label("防御能力（近战）")] public int defenseMelee;

        /// <summary>
        /// 防御能力（远程）
        /// </summary>
        [Label("防御能力（远程）")] public int defenseRange;

        /// <summary>
        /// 近战杀伤（普通）
        /// </summary>
        [Label("近战杀伤（普通）")] public int meleeNormal;

        /// <summary>
        /// 近战杀伤（破甲）
        /// </summary>
        [Label("近战杀伤（破甲）")] public int meleeArmor;

        /// <summary>
        /// 攻击范围
        /// </summary>
        [Label("攻击范围")] public int attackRange;

        /// <summary>
        /// 护甲强度
        /// </summary>
        [Label("护甲强度")] public int armor;

        /// <summary>
        /// 移动能力
        /// </summary>
        [Label("移动能力")] public int mobility;

        /// <summary>
        /// 视野
        /// </summary>
        [Label("视野")] public int sight;

        /// <summary>
        /// 隐蔽
        /// </summary>
        [Label("隐蔽")] public int stealth;

        /// <summary>
        /// 弹药量（0代表没有弹药，-1代表拥有无限弹药）
        /// </summary>
        [Label("弹药量")] public int ammo;

        /// <summary>
        /// 射程
        /// </summary>
        [Label("射程")] public int range;

        /// <summary>
        /// 装填速度
        /// </summary>
        [Label("装填速度")] public int reload;

        /// <summary>
        /// 精度
        /// </summary>
        [Label("精度")] public int accuracy;

        /// <summary>
        /// 远程杀伤（普通）
        /// </summary>
        [Label("远程杀伤（普通）")] public int rangeNormal;

        /// <summary>
        /// 远程杀伤（破甲）
        /// </summary>
        [Label("远程杀伤（破甲）")] public int rangeArmor;

        /// <summary>
        /// 最高作战意志
        /// </summary>
        [Label("最高作战意志")] public int maximumMorale;

        /// <summary>
        /// 疲劳值上限
        /// </summary>
        [Label("疲劳值上限")] public int maximumFatigue;

        /// <summary>
        /// 价格
        /// </summary>
        [Label("价格")] public int cost;
    }

    /// <summary>
    /// 派系数据类型
    /// </summary>
    [Serializable]
    public class FactionDataType : BaseJsonData
    {
        /// <summary>
        /// 派系名称
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string FactionName;
    }
}