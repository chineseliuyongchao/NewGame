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
            this.RegisterEvent<ChangeTimeEvent>(e =>
            {
                if (this.GetModel<IGameModel>().NowTime.Equals(GameTime.RefreshPopulationTime))
                {
                    UpdatePopulationGrowth();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        /// <summary>
        /// 更新聚落人口自然增长量
        /// </summary>
        protected virtual void UpdatePopulationGrowth()
        {
            // 获取当前聚落的人口数据
            TownData townData = this.GetModel<ITownModel>().TownData[_townId];
            // 假设每天的自然增长率为 0.1%
            float naturalGrowthRate = 0.001f;
            // 计算今天的自然增长量
            int naturalGrowth = (int)(townData.population * naturalGrowthRate);
            // 更新人口数据
            townData.population += naturalGrowth;
        }
    }
}