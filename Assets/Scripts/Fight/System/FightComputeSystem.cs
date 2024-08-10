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
                Math.Max(_armDataTypes[armA.ARMId].meleeNormal - _armDataTypes[armB.ARMId].armor, 0); //实际普通杀伤
            int armRealMeleeArmor = this.RealMeleeArmor(armA); //兵种的破甲杀伤修正
            float realMeleeArmorFactor = Math.Max(0.1f, Math.Min(1, //实际破甲杀伤系数
                1 - (_armDataTypes[armB.ARMId].armor - (float)armRealMeleeArmor) / armRealMeleeArmor * 0.15f));
            float realMeleeArmor = armRealMeleeArmor * realMeleeArmorFactor; //实际破甲杀伤
            // Debug.Log("实际普通杀伤：" + realMeleeNormal + "  实际破甲杀伤：" + realMeleeArmor + "  实际破甲杀伤系数：" + realMeleeArmorFactor);

            //计算实际攻击伤害
            int totalDamage = (int)(successAttackNum * (realMeleeNormal + realMeleeArmor)); //攻击产生的总伤害
            armB.NowHp -= totalDamage; //计算剩余血量
            int theoryMaxNum = _armDataTypes[armB.ARMId].totalTroops; //理论最大人数
            int theoryMinNum =
                (int)Math.Ceiling(theoryMaxNum * ((float)armB.NowHp / _armDataTypes[armB.ARMId].totalHp)); //理论最小人数
            float computeTroopsFactor = 0.7f; //剩余人数计算系数
            int theoryNowTroops = theoryMinNum + (int)((theoryMaxNum - theoryMinNum) * Math.Pow(armB.NowHp /
                (float)_armDataTypes[armB.ARMId].totalHp, computeTroopsFactor)); //剩余理论人数
            armB.NowTroops = Math.Max(theoryMinNum, Math.Min(theoryMaxNum, theoryNowTroops)); //剩余实际人数
            // Debug.Log("攻击产生的总伤害：" + totalDamage + "  理论最大人数：" + theoryMaxNum + "  理论最小人数：" + theoryMinNum + "  剩余理论人数：" + theoryNowTroops);
        }

        /// <summary>
        /// 计算实际攻击能力
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealAttack(ArmData armData)
        {
            int correctAttack = _armDataTypes[armData.ARMId].attack;
            int realAttack = correctAttack;
            return realAttack;
        }

        /// <summary>
        /// 计算实际近战防御能力
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealDefenseMelee(ArmData armData)
        {
            int correctDefenseMelee = _armDataTypes[armData.ARMId].defenseMelee;
            int realDefenseMelee = correctDefenseMelee;
            return realDefenseMelee;
        }

        /// <summary>
        /// 计算实际破甲杀伤
        /// </summary>
        /// <param name="armData"></param>
        /// <returns></returns>
        private int RealMeleeArmor(ArmData armData)
        {
            int correctMeleeArmor = _armDataTypes[armData.ARMId].meleeArmor;
            int realMeleeArmor = correctMeleeArmor;
            return realMeleeArmor;
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