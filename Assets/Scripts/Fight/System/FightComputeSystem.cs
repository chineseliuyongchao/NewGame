using System;
using System.Collections.Generic;
using Fight.Model;
using Fight.Utils;
using Game.FightCreate;
using Game.GameMenu;
using QFramework;
using Random = UnityEngine.Random;

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

        public void ComputeUnitPos()
        {
            Dictionary<int, LegionInfo> allLegions = this.GetModel<IFightCreateModel>().AllLegions;
            List<int> legionId = new List<int>(allLegions.Keys);
            List<int> pos1 = new List<int>(Constants.MyUnitPositionArray1);
            List<int> pos2 = new List<int>(Constants.MyUnitPositionArray2);
            for (int i = 0; i < legionId.Count; i++)
            {
                LegionInfo legionInfo = allLegions[legionId[i]];
                List<int> armId = new List<int>(legionInfo.allUnit.Keys);
                var pos = legionInfo.belligerentsId == Constants.BELLIGERENT1 ? pos1 : pos2;
                for (int j = 0; j < armId.Count; j++)
                {
                    UnitData unitData = legionInfo.allUnit[armId[j]];
                    int randomIndex = Random.Range(0, pos.Count);
                    unitData.currentPosIndex = pos[randomIndex];
                    pos.RemoveAt(randomIndex); //防止多个单位随机到一个位置
                }
            }
        }

        public void AssaultWithRetaliation(int unitAId, int unitBId)
        {
            UnitData unitA = this.GetSystem<IFightSystem>().FindUnit(unitAId);
            UnitData unitB = this.GetSystem<IFightSystem>().FindUnit(unitBId);
            //单位1进攻单位2，单位2反击
            UnitData arm2Before = new UnitData(unitB);
            AssaultNoRetaliation(unitA, unitB);
            AssaultNoRetaliation(arm2Before, unitA);
        }

        public void AssaultNoRetaliation(int unitAId, int unitBId)
        {
            UnitData unitA = this.GetSystem<IFightSystem>().FindUnit(unitAId);
            UnitData unitB = this.GetSystem<IFightSystem>().FindUnit(unitBId);
            AssaultNoRetaliation(unitA, unitB);
        }

        public void Shoot(int unitAId, int unitBId)
        {
            UnitData unitA = this.GetSystem<IFightSystem>().FindUnit(unitAId);
            UnitData unitB = this.GetSystem<IFightSystem>().FindUnit(unitBId);
            OneShoot(unitA, unitB);
        }

        public bool MoveOnce(int unitId)
        {
            UnitData unit = this.GetSystem<IFightSystem>().FindUnit(unitId);
            int decreaseMovePoint = Constants.MovementParameter - unit.armDataType.mobility;
            if (unit.NowMovementPoints >= decreaseMovePoint)
            {
                unit.NowMovementPoints -= decreaseMovePoint;
                return true;
            }

            return false;
        }

        public bool EnoughMovePoint(int unitId)
        {
            //暂定检测就不够
            return false;
        }

        public List<int> LegionOrder()
        {
            List<int> legionKeys = new List<int>(this.GetModel<IFightCreateModel>().AllLegions.Keys);
            //目前没有顺序逻辑，先根据id顺序处理
            var order = legionKeys;
            return order;
        }

        /// <summary>
        /// 单次攻击计算
        /// </summary>
        /// <param name="unitA"></param>
        /// <param name="unitB"></param>
        private void AssaultNoRetaliation(UnitData unitA, UnitData unitB)
        {
            //计算命中次数
            int realAttack = RealAttack(unitA);
            int realDefenseMelee = RealDefenseMelee(unitB);
            float hitProbability = Math.Max(0.05f, Math.Min(1, realAttack / (realDefenseMelee * 3f))); //命中概率
            int successAttackNum = CompleteSuccessAttackNum(hitProbability, unitA.NowTroops); //成功命中次数
            // Debug.Log("命中概率：" + hitProbability + "  成功命中次数：" + successAttackNum);

            //计算单次实际杀伤（普通杀伤和破甲杀伤）
            float realMeleeNormal =
                Math.Max(_armDataTypes[unitA.armId].meleeNormal - _armDataTypes[unitB.armId].armor / 2, 0); //实际普通杀伤
            int armRealMeleeArmor = this.RealMeleeArmor(unitA); //兵种的破甲杀伤修正
            float realMeleeArmorFactor = Math.Max(0.1f, Math.Min(1, //实际破甲杀伤系数
                1 - (_armDataTypes[unitB.armId].armor - (float)armRealMeleeArmor) / armRealMeleeArmor * 0.15f));
            float realMeleeArmor = armRealMeleeArmor * realMeleeArmorFactor; //实际破甲杀伤
            // Debug.Log("实际普通杀伤：" + realMeleeNormal + "  实际破甲杀伤：" + realMeleeArmor + "  实际破甲杀伤系数：" + realMeleeArmorFactor);

            //计算实际攻击伤害
            int totalDamage = (int)(successAttackNum * (realMeleeNormal + realMeleeArmor)); //攻击产生的总伤害
            unitB.NowHp -= totalDamage; //计算剩余血量
            int theoryMaxNum = _armDataTypes[unitB.armId].totalTroops; //理论最大人数
            int theoryMinNum =
                (int)Math.Ceiling(theoryMaxNum * ((float)unitB.NowHp / _armDataTypes[unitB.armId].totalHp)); //理论最小人数
            float computeTroopsFactor = 0.7f; //剩余人数计算系数
            int theoryNowTroops = theoryMinNum + (int)((theoryMaxNum - theoryMinNum) * Math.Pow(unitB.NowHp /
                (float)_armDataTypes[unitB.armId].totalHp, computeTroopsFactor)); //剩余理论人数
            unitB.NowTroops = Math.Max(theoryMinNum, Math.Min(theoryMaxNum, theoryNowTroops)); //剩余实际人数
            // Debug.Log("攻击产生的总伤害：" + totalDamage + "  理论最大人数：" + theoryMaxNum + "  理论最小人数：" + theoryMinNum + "  剩余理论人数：" + theoryNowTroops);
        }

        /// <summary>
        /// 一次射击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="unitA">单位a</param>
        /// <param name="unitB">单位b</param>
        private void OneShoot(UnitData unitA, UnitData unitB)
        {
            //计算命中次数
            int realAccuracy = RealAccuracy(unitA);
            int realDefenseRange = RealDefenseRange(unitB);
            float hitProbability = Math.Max(0.05f, Math.Min(1, realAccuracy / (realDefenseRange * 3f))); //命中概率
            int successAttackNum = CompleteSuccessAttackNum(hitProbability, unitA.NowTroops); //成功命中次数
            // Debug.Log("命中概率：" + hitProbability + "  成功命中次数：" + successAttackNum);

            //计算单次实际杀伤（普通杀伤和破甲杀伤）
            float realRangeNormal =
                Math.Max(_armDataTypes[unitA.armId].rangeNormal - _armDataTypes[unitB.armId].armor / 2, 0); //实际普通杀伤
            int armRealRangeArmor = this.RealRangeArmor(unitA); //兵种的破甲杀伤修正
            float realRangeArmorFactor = Math.Max(0.1f, Math.Min(1, //实际破甲杀伤系数
                1 - (_armDataTypes[unitB.armId].armor - (float)armRealRangeArmor) / armRealRangeArmor * 0.15f));
            float realRangeArmor = armRealRangeArmor * realRangeArmorFactor; //实际破甲杀伤
            // Debug.Log("实际普通杀伤：" + realRangeNormal + "  实际破甲杀伤：" + realRangeArmor + "  实际破甲杀伤系数：" + realRangeArmorFactor);

            //计算实际攻击伤害
            int totalDamage = (int)(successAttackNum * (realRangeNormal + realRangeArmor)); //攻击产生的总伤害
            unitB.NowHp -= totalDamage; //计算剩余血量
            int theoryMaxNum = _armDataTypes[unitB.armId].totalTroops; //理论最大人数
            int theoryMinNum =
                (int)Math.Ceiling(theoryMaxNum * ((float)unitB.NowHp / _armDataTypes[unitB.armId].totalHp)); //理论最小人数
            float computeTroopsFactor = 0.7f; //剩余人数计算系数
            int theoryNowTroops = theoryMinNum + (int)((theoryMaxNum - theoryMinNum) * Math.Pow(unitB.NowHp /
                (float)_armDataTypes[unitB.armId].totalHp, computeTroopsFactor)); //剩余理论人数
            unitB.NowTroops = Math.Max(theoryMinNum, Math.Min(theoryMaxNum, theoryNowTroops)); //剩余实际人数
            // Debug.Log("攻击产生的总伤害：" + totalDamage + "  理论最大人数：" + theoryMaxNum + "  理论最小人数：" + theoryMinNum + "  剩余理论人数：" + theoryNowTroops);
        }

        /// <summary>
        /// 计算实际攻击能力，计算规则原则上先加后乘
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealAttack(UnitData unitData)
        {
            int correctAttack = _armDataTypes[unitData.armId].attack;
            if (unitData.IsCharge)
            {
                correctAttack += _armDataTypes[unitData.armId].charge;
            }

            if (unitData.IsStick)
            {
                correctAttack = (int)(correctAttack * 0.75f);
            }

            int realAttack = correctAttack; //修正后攻击能力
            realAttack = ComputeCorrectMorale(realAttack, unitData);
            realAttack = ComputeCorrectFatigue(realAttack, unitData);

            return realAttack;
        }

        /// <summary>
        /// 计算实际近战防御能力，计算规则原则上先加后乘
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealDefenseMelee(UnitData unitData)
        {
            int correctDefenseMelee = _armDataTypes[unitData.armId].defenseMelee;
            if (unitData.IsStick)
            {
                correctDefenseMelee = (int)(correctDefenseMelee * 1.4f);
            }

            int realDefenseMelee = correctDefenseMelee; //修正后防御能力
            realDefenseMelee = ComputeCorrectMorale(realDefenseMelee, unitData);
            realDefenseMelee = ComputeCorrectFatigue(realDefenseMelee, unitData);
            return realDefenseMelee;
        }

        /// <summary>
        /// 计算实际破甲杀伤，计算规则原则上先加后乘
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealMeleeArmor(UnitData unitData)
        {
            int correctMeleeArmor = _armDataTypes[unitData.armId].meleeArmor;
            if (unitData.IsCharge)
            {
                correctMeleeArmor += (int)(_armDataTypes[unitData.armId].charge * 0.1f);
            }

            int realMeleeArmor = correctMeleeArmor;
            return realMeleeArmor;
        }

        /// <summary>
        /// 计算实际射击精度，计算规则原则上先加后乘
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealAccuracy(UnitData unitData)
        {
            int correctAccuracy = _armDataTypes[unitData.armId].accuracy;
            int realAccuracy = correctAccuracy;
            realAccuracy = ComputeCorrectMorale(realAccuracy, unitData);
            realAccuracy = ComputeCorrectFatigue(realAccuracy, unitData);
            return realAccuracy;
        }

        /// <summary>
        /// 计算实际远程防御能力
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealDefenseRange(UnitData unitData)
        {
            int correctDefenseRange = _armDataTypes[unitData.armId].defenseRange;
            int realDefenseRange = correctDefenseRange;
            realDefenseRange = ComputeCorrectMorale(realDefenseRange, unitData);
            realDefenseRange = ComputeCorrectFatigue(realDefenseRange, unitData);
            return realDefenseRange;
        }

        /// <summary>
        /// 计算作战意志修正
        /// </summary>
        private int ComputeCorrectMorale(int value, UnitData unitData)
        {
            return (int)(value * (1 - (_armDataTypes[unitData.armId].maximumMorale - unitData.NowMorale) /
                (float)_armDataTypes[unitData.armId].maximumMorale * 0.2f));
        }

        /// <summary>
        /// 计算疲劳值修正
        /// </summary>
        private int ComputeCorrectFatigue(int value, UnitData unitData)
        {
            return (int)(value * (1 - unitData.NowFatigue /
                (float)_armDataTypes[unitData.armId].maximumFatigue * 0.5f));
        }

        /// <summary>
        /// 计算实际远程破甲杀伤
        /// </summary>
        /// <param name="unitData"></param>
        /// <returns></returns>
        private int RealRangeArmor(UnitData unitData)
        {
            int correctRangeArmor = _armDataTypes[unitData.armId].rangeArmor;
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
            for (int i = 0; i < nowTroops; i++)
            {
                // 生成0到1之间的随机数
                float randomValue = Random.Range(0f, 1f);

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