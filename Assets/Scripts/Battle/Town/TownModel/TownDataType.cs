using System;
using System.Collections.Generic;

namespace Battle.Town
{
    /// <summary>
    /// 单个聚落的数据
    /// </summary>
    public class TownData
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public TownDataStorage storage;

        // ReSharper disable once UnassignedField.Global
        public TownDataNoStorage noStorage;

        public TownData(TownDataStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// 获取聚落总人口
        /// </summary>
        /// <returns></returns>
        public int GetPopulation()
        {
            return storage.malePopulation + storage.femalePopulation;
        }

        /// <summary>
        /// 获取聚落单日粮食消耗
        /// </summary>
        /// <returns></returns>
        public int DailyGrainConsumption()
        {
            return (int)(GetPopulation() * TownConstant.POPULATION_GRAIN_CONSUME);
        }

        /// <summary>
        /// 所有修改人口的操作走同一方法，便于debug
        /// </summary>
        /// <param name="addNum"></param>
        public void UpdateMalePopulation(int addNum)
        {
            storage.malePopulation += addNum;
        }

        public void UpdateFemalePopulation(int addNum)
        {
            storage.femalePopulation += addNum;
        }
    }

    /// <summary>
    /// 单个聚落需要保存的数据
    /// </summary>
    [Serializable]
    public class TownDataStorage
    {
        /// <summary>
        /// 男性人口
        /// </summary>
        [Obsolete]
        public int malePopulation;

        /// <summary>
        /// 女性人口
        /// </summary>
        [Obsolete]
        public int femalePopulation;

        /// <summary>
        /// 等级
        /// </summary>
        public int level;

        /// <summary>
        /// 所属国家编号
        /// </summary>
        public int countryId;

        /// <summary>
        /// 所属家族编号（目前默认每个聚落都会被分封给国家中的某个家族）
        /// </summary>
        public int familyId;

        /// <summary>
        /// 民兵数量
        /// </summary>
        public int militiaNum;

        /// <summary>
        /// 农田数量
        /// </summary>
        public int farmlandNum;

        /// <summary>
        /// 农田上限
        /// </summary>
        public int farmlandUpperLimit;

        /// <summary>
        /// 粮仓等级
        /// </summary>
        public int granaryLevel;

        /// <summary>
        /// 粮食储量
        /// </summary>
        public int grainReserves;

        /// <summary>
        /// 名字
        /// </summary>
        public List<string> name;

        /// <summary>
        /// 聚落中所有的角色
        /// </summary>
        public List<int> townRoleS;

        public TownDataStorage(TownCommonData townCommonData, TownNameData townNameData)
        {
            malePopulation = townCommonData.InitMalePopulation;
            femalePopulation = townCommonData.InitFemalePopulation;
            level = townCommonData.InitLevel;
            countryId = townCommonData.CountryId;
            familyId = townCommonData.FamilyId;
            militiaNum = townCommonData.MilitiaNum;
            farmlandNum = townCommonData.FarmlandNum;
            farmlandUpperLimit = townCommonData.FarmlandUpperLimit;
            granaryLevel = townCommonData.GranaryLevel;
            grainReserves = townCommonData.GrainReserves;
            name = new List<string> { townNameData.Chinese, townNameData.English };
            townRoleS = new List<int>();
        }
    }

    /// <summary>
    /// 单个聚落不需要保存的数据，数据都是由TownDataStorage算出来的
    /// </summary>
    public class TownDataNoStorage
    {
        /// <summary>
        /// 繁荣度
        /// </summary>
        public int prosperity;
    }
}