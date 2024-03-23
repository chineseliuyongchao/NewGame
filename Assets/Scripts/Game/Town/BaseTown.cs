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
            _townData.malePopulation += maleBirths - maleDeaths;
            _townData.femalePopulation += femaleBirths - femaleDeaths;
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
            _townData.malePopulation -= structure.num;
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