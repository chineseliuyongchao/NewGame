using System;
using System.Collections.Generic;
using Fight.Model;
using Game.GameMenu;
using QFramework;
using Random = System.Random;

namespace Fight.System
{
    public class FightComputeSystem : AbstractSystem, IFightComputeSystem
    {
        /// <summary>
        /// 所有兵种数据
        /// </summary>
        private Dictionary<int, ArmDataType> _armDataTypes;

        protected override void OnInit()
        {
            _armDataTypes = this.GetModel<IGameMenuModel>().ARMDataTypes;
        }

        public void AssaultWithRetaliation(int armAId, int armBId)
        {
            ArmData armA = this.GetModel<IFightModel>().ARMDataTypes[armAId];
            ArmData armB = this.GetModel<IFightModel>().ARMDataTypes[armBId];
            //单位1进攻单位2，单位2反击
            ArmData arm2Before = new ArmData(armB);
            AssaultNoRetaliation(armA, armB);
            AssaultNoRetaliation(arm2Before, armA);
        }

        public void AssaultNoRetaliation(int armAId, int armBId)
        {
            ArmData armA = this.GetModel<IFightModel>().ARMDataTypes[armAId];
            ArmData armB = this.GetModel<IFightModel>().ARMDataTypes[armBId];
            AssaultNoRetaliation(armA, armB);
        }

        public void Shoot(int armAId, int armBId)
        {
            ArmData armA = this.GetModel<IFightModel>().ARMDataTypes[armAId];
            ArmData armB = this.GetModel<IFightModel>().ARMDataTypes[armBId];
            OneShoot(armA, armB);
        }

        /// <summary>
        /// 单次攻击计算
        /// </summary>
        /// <param name="armA"></param>
        /// <param name="armB"></param>
        private void AssaultNoRetaliation(ArmData armA, ArmData armB)
        {
            //计算命中次数
            int realAttack = RealAttack(armA);
            int realDefenseMelee = RealDefenseMelee(armB);
            float hitProbability = Math.Max(0.05f, Math.Min(1, realAttack / (realDefenseMelee * 3f))); //命中概率
            int successAttackNum = CompleteSuccessAttackNum(hitProbability, armA.NowTroops); //成功命中次数
            // Debug.Log("命中概率：" + hitProbability + "  成功命中次数：" + successAttackNum);

            //计算单次实际杀伤（普通杀伤和破甲杀伤）
            float realMeleeNormal =
                Math.Max(_armDataTypes[armA.armId].meleeNormal - _armDataTypes[armB.armId].armor / 2, 0); //实际普通杀伤
            int armRealMeleeArmor = this.RealMeleeArmor(armA); //兵种的破甲杀伤修正
            float realMeleeArmorFactor = Math.Max(0.1f, Math.Min(1, //实际破甲杀伤系数
                1 - (_armDataTypes[armB.armId].armor - (float)armRealMeleeArmor) / armRealMeleeArmor * 0.15f));
            float realMeleeArmor = armRealMeleeArmor * realMeleeArmorFactor; //实际破甲杀伤
            // Debug.Log("实际普通杀伤：" + realMeleeNormal + "  实际破甲杀伤：" + realMeleeArmor + "  实际破甲杀伤系数：" + realMeleeArmorFactor);

            //计算实际攻击伤害
            int totalDamage = (int)(successAttackNum * (realMeleeNormal + realMeleeArmor)); //攻击产生的总伤害
            armB.NowHp -= totalDamage; //计算剩余血量
            int theoryMaxNum = _armDataTypes[armB.armId].totalTroops; //理论最大人数
            int theoryMinNum =
                (int)Math.Ceiling(theoryMaxNum * ((float)armB.NowHp / _armDataTypes[armB.armId].totalHp)); //理论最小人数
            float computeTroopsFactor = 0.7f; //剩余人数计算系数
            int theoryNowTroops = theoryMinNum + (int)((theoryMaxNum - theoryMinNum) * Math.Pow(armB.NowHp /
                (float)_armDataTypes[armB.armId].totalHp, computeTroopsFactor)); //剩余理论人数
            armB.NowTroops = Math.Max(theoryMinNum, Math.Min(theoryMaxNum, theoryNowTroops)); //剩余实际人数
            // Debug.Log("攻击产生的总伤害：" + totalDamage + "  理论最大人数：" + theoryMaxNum + "  理论最小人数：" + theoryMinNum + "  剩余理论人数：" + theoryNowTroops);
        }

        /// <summary>
        /// 一次射击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="armA">单位a</param>
        /// <param name="armB">单位b</param>
        private void OneShoot(ArmData armA, ArmData armB)
        {
            //计算命中次数
            int realAccuracy = RealAccuracy(armA);
            int realDefenseRange = RealDefenseRange(armB);
            float hitProbability = Math.Max(0.05f, Math.Min(1, realAccuracy / (realDefenseRange * 3f))); //命中概率
            int successAttackNum = CompleteSuccessAttackNum(hitProbability, armA.NowTroops); //成功命中次数
            // Debug.Log("命中概率：" + hitProbability + "  成功命中次数：" + successAttackNum);

            //计算单次实际杀伤（普通杀伤和破甲杀伤）
            float realRangeNormal =
                Math.Max(_armDataTypes[armA.armId].rangeNormal - _armDataTypes[armB.armId].armor / 2, 0); //实际普通杀伤
            int armRealRangeArmor = this.RealRangeArmor(armA); //兵种的破甲杀伤修正
            float realRangeArmorFactor = Math.Max(0.1f, Math.Min(1, //实际破甲杀伤系数
                1 - (_armDataTypes[armB.armId].armor - (float)armRealRangeArmor) / armRealRangeArmor * 0.15f));
            float realRangeArmor = armRealRangeArmor * realRangeArmorFactor; //实际破甲杀伤
            // Debug.Log("实际普通杀伤：" + realRangeNormal + "  实际破甲杀伤：" + realRangeArmor + "  实际破甲杀伤系数：" + realRangeArmorFactor);

            //计算实际攻击伤害
            int totalDamage = (int)(successAttackNum * (realRangeNormal + realRangeArmor)); //攻击产生的总伤害
            armB.NowHp -= totalDamage; //计算剩余血量
            int theoryMaxNum = _armDataTypes[armB.armId].totalTroops; //理论最大人数
            int theoryMinNum =
                (int)Math.Ceiling(theoryMaxNum * ((float)armB.NowHp / _armDataTypes[armB.armId].totalHp)); //理论最小人数
            float computeTroopsFactor = 0.7f; //剩余人数计算系数
            int theoryNowTroops = theoryMinNum + (int)((theoryMaxNum - theoryMinNum) * Math.Pow(armB.NowHp /
                (float)_armDataTypes[armB.armId].totalHp, computeTroopsFactor)); //剩余理论人数
            armB.NowTroops = Math.Max(theoryMinNum, Math.Min(theoryMaxNum, theoryNowTroops)); //剩余实际人数
            // Debug.Log("攻击产生的总伤害：" + totalDamage + "  理论最大人数：" + theoryMaxNum + "  理论最小人数：" + theoryMinNum + "  剩余理论人数：" + theoryNowTroops);
        }

        /// <summary>
        /// 计算实际攻击能力，计算规则原则上先加后乘
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealAttack(ArmData armData)
        {
            int correctAttack = _armDataTypes[armData.armId].attack;
            if (armData.IsCharge)
            {
                correctAttack += _armDataTypes[armData.armId].charge;
            }

            if (armData.IsStick)
            {
                correctAttack = (int)(correctAttack * 0.75f);
            }

            int realAttack = correctAttack; //修正后攻击能力
            realAttack = ComputeCorrectMorale(realAttack, armData);
            realAttack = ComputeCorrectFatigue(realAttack, armData);

            return realAttack;
        }

        /// <summary>
        /// 计算实际近战防御能力，计算规则原则上先加后乘
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealDefenseMelee(ArmData armData)
        {
            int correctDefenseMelee = _armDataTypes[armData.armId].defenseMelee;
            if (armData.IsStick)
            {
                correctDefenseMelee = (int)(correctDefenseMelee * 1.4f);
            }

            int realDefenseMelee = correctDefenseMelee; //修正后防御能力
            realDefenseMelee = ComputeCorrectMorale(realDefenseMelee, armData);
            realDefenseMelee = ComputeCorrectFatigue(realDefenseMelee, armData);
            return realDefenseMelee;
        }

        /// <summary>
        /// 计算实际破甲杀伤，计算规则原则上先加后乘
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealMeleeArmor(ArmData armData)
        {
            int correctMeleeArmor = _armDataTypes[armData.armId].meleeArmor;
            if (armData.IsCharge)
            {
                correctMeleeArmor += (int)(_armDataTypes[armData.armId].charge * 0.1f);
            }

            int realMeleeArmor = correctMeleeArmor;
            return realMeleeArmor;
        }

        /// <summary>
        /// 计算实际射击精度，计算规则原则上先加后乘
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealAccuracy(ArmData armData)
        {
            int correctAccuracy = _armDataTypes[armData.armId].accuracy;
            int realAccuracy = correctAccuracy;
            realAccuracy = ComputeCorrectMorale(realAccuracy, armData);
            realAccuracy = ComputeCorrectFatigue(realAccuracy, armData);
            return realAccuracy;
        }

        /// <summary>
        /// 计算实际远程防御能力
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealDefenseRange(ArmData armData)
        {
            int correctDefenseRange = _armDataTypes[armData.armId].defenseRange;
            int realDefenseRange = correctDefenseRange;
            realDefenseRange = ComputeCorrectMorale(realDefenseRange, armData);
            realDefenseRange = ComputeCorrectFatigue(realDefenseRange, armData);
            return realDefenseRange;
        }

        /// <summary>
        /// 计算作战意志修正
        /// </summary>
        private int ComputeCorrectMorale(int value, ArmData armData)
        {
            return (int)(value * (1 - (_armDataTypes[armData.armId].maximumMorale - armData.NowMorale) /
                (float)_armDataTypes[armData.armId].maximumMorale * 0.2f));
        }

        /// <summary>
        /// 计算疲劳值修正
        /// </summary>
        private int ComputeCorrectFatigue(int value, ArmData armData)
        {
            return (int)(value * (1 - armData.NowFatigue /
                (float)_armDataTypes[armData.armId].maximumFatigue * 0.5f));
        }

        /// <summary>
        /// 计算实际远程破甲杀伤
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealRangeArmor(ArmData armData)
        {
            int correctRangeArmor = _armDataTypes[armData.armId].rangeArmor;
            int realRangeArmor = correctRangeArmor;
            return realRangeArmor;
        }

        /// <summary>
        /// 计算命中次数
        /// </summary>
        /// <param name="hitProbability">命中概率</param>
        /// <param name="nowTroops">人数</param>
        /// <returns></returns>
        private int CompleteSuccessAttackNum(float hitProbability, int nowTroops)
        {
            int successAttackNum = 0;
            Random random = new Random();
            for (int i = 0; i < nowTroops; i++)
            {
                // 生成0到1之间的随机数
                float randomValue = (float)random.NextDouble();

                // 如果随机数小于命中概率，则计为命中
                if (randomValue < hitProbability)
                {
                    successAttackNum++;
                }
            }

            return successAttackNum;
        }
    }
}