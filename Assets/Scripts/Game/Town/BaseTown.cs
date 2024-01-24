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
    }
}