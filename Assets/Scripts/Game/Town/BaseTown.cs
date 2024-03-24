using System;
using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game.Town
{
    /// <summary>
    /// 所有聚落的基类
    /// </summary>
    public abstract class BaseTown : BaseGameController
    {
        private int _townId;

        public int TownId
        {
            get => _townId;
            set => _townId = value;
        }

        private TownData _townData;

        public void InitTown(int townId)
        {
            _townId = townId;
            _townData = this.GetModel<ITownModel>().TownData[townId];
            name = _townData.name;
            TownCommonData townCommonData = this.GetModel<ITownModel>().TownCommonData[townId];
            transform.position = new Vector3(townCommonData.TownPos[0], townCommonData.TownPos[1]);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(_ =>
            {
                if (this.GetModel<IGameModel>().NowTime.Equals(GameTime.RefreshPopulationTime))
                {
                    UpdatePopulationGrowth();
                }

                if (this.GetModel<IGameModel>().NowTime.Equals(GameTime.RefreshMilitiaTime))
                {
                    UpdateMilitia();
                }

                if (this.GetModel<IGameModel>().NowTime.Equals(GameTime.RefreshGrainTime))
                {
                    UpdateGrain();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 更新聚落人口自然增长量
        /// </summary>
        protected virtual void UpdatePopulationGrowth()
        {
            // 获取上一天的总人口数量
            int lastPopulation = _townData.GetPopulation();
            // 自然死亡率，假设为 0.01（1%）
            float deathRate = 0.005f;
            // 自然出生率，假设为 0.02（2%）
            float birthRate = 0.01f;
            // 人口结构参数
            float populationParameter =
                Math.Min(_townData.malePopulation, _townData.femalePopulation) * 2f / lastPopulation;
            // 计算男性自然死亡量
            int maleDeaths = (int)(lastPopulation * deathRate * _townData.malePopulation / lastPopulation);
            // 计算女性自然死亡量
            int femaleDeaths = (int)(lastPopulation * deathRate * _townData.femalePopulation / lastPopulation);
            // 计算男性自然出生量
            int maleBirths = (int)(lastPopulation * birthRate * populationParameter * 0.5f);
            // 计算女性自然出生量
            int femaleBirths = (int)(lastPopulation * birthRate * populationParameter * 0.5f);
            // 当天自然增长量 = 男性自然增长量 + 女性自然增长量
            int populationGrowth = -maleDeaths - femaleDeaths + maleBirths + femaleBirths;
            // 更新总人口数量
            _townData.UpdateMalePopulation(maleBirths - maleDeaths);
            _townData.UpdateFemalePopulation(femaleBirths - femaleDeaths);
            // // 输出自然增长量
            Debug.Log(_townData.name + "今天的人口自然增长量为：" + populationGrowth);
        }

        /// <summary>
        /// 更新民兵数量
        /// </summary>
        protected virtual void UpdateMilitia()
        {
            // 计算合理的民兵数量
            int reasonableMilitiaCount = (int)(_townData.malePopulation * 0.05);
            if (_townData.malePopulation < 100)
            {
                //如果男性数量小于100，就不会有民兵
                reasonableMilitiaCount = 0;
            }

            // 计算需要增加或减少的比例
            float ratio = 0.1f; // 假设每次变化的比例为10%
            // 计算需要增加或减少的数量
            int delta = reasonableMilitiaCount - _townData.militiaNum;
            // 计算实际增加或减少的数量
            int changeAmount = (int)(delta * ratio);
            _townData.militiaNum += changeAmount;
            Debug.Log(_townData.name + "民兵数量：" + _townData.militiaNum + "  变化量： " + changeAmount);
        }

        /// <summary>
        /// 更新粮食数量
        /// </summary>
        protected virtual void UpdateGrain()
        {
            int farmlandOutput = 10; //暂定每块农田产量10
            float populationGrainConsume = 0.05f; //暂定每个人每天的粮食消耗是0.05
            int granaryReserves = 10000; //暂定每级粮仓粮食存储是1000
            //粮食增加量
            int grainAdd = _townData.farmlandNum * farmlandOutput;
            //粮食消耗量
            int grainConsume = (int)(_townData.GetPopulation() * populationGrainConsume);
            int grainChange = grainAdd - grainConsume;
            //更新粮食存储
            _townData.grainReserves += grainChange;
            //饥荒判定
            if (_townData.grainReserves < 0)
            {
                //饥荒率（0-1之间，饥荒率越高越严重）
                float famineRate = -_townData.grainReserves / (float)grainConsume;
                if (famineRate > 0.4) //饥荒率大于0.4就会有人饿死
                {
                    float mortality = 0.17f * famineRate - 0.067f; //饿死率
                    _townData.UpdateMalePopulation(-(int)(mortality * _townData.malePopulation));
                    _townData.UpdateFemalePopulation(-(int)(mortality * _townData.femalePopulation));
                    Debug.Log(_townData.name + "饥荒率：" + famineRate + "  死亡率： " + mortality);
                }
            }

            _townData.grainReserves = this.GetUtility<IMathUtility>()
                .CrossTheBorder(_townData.grainReserves, granaryReserves * _townData.granaryLevel, 0);
            Debug.Log(_townData.name + "粮食数量：" + _townData.grainReserves);
        }

        /// <summary>
        /// 征兵
        /// </summary>
        /// <returns></returns>
        public virtual ConscriptionData Conscription()
        {
            ConscriptionData conscriptionData = new ConscriptionData
            {
                canConscription = new SoldierStructure
                {
                    //目标可以提供的兵员数量为聚落男性的百分之一
                    num = _townData.malePopulation / 100
                },
                realConscription = RealConscription
            };
            return conscriptionData;
        }

        /// <summary>
        /// 队伍完成征兵后的回调
        /// </summary>
        /// <param name="structure"></param>
        private void RealConscription(SoldierStructure structure)
        {
            _townData.UpdateMalePopulation(-structure.num);
        }
    }

    /// <summary>
    /// 征兵数据
    /// </summary>
    public class ConscriptionData
    {
        /// <summary>
        /// 可以征集的士兵
        /// </summary>
        public SoldierStructure canConscription;

        /// <summary>
        /// 实际征集的士兵
        /// </summary>
        public Action<SoldierStructure> realConscription;
    }

    /// <summary>
    /// 士兵组成
    /// </summary>
    public class SoldierStructure
    {
        public int num;
    }
}