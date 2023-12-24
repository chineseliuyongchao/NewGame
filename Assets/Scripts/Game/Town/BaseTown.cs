using GameQFramework;
using QFramework;
using UnityEngine;
using Utils;

namespace Game.Town
{
    /// <summary>
    /// 所有聚落的基类
    /// </summary>
    public class BaseTown : BaseGameController
    {
        public void InitTown(string townName)
        {
            name = townName;
            TownCommonData townCommonData = this.GetModel<ITownModel>().TownCommonData[gameObject.name];
            IntVector2 gridPos = this.GetSystem<IMapSystem>()
                .GetGridMapPos(new Vector3(townCommonData.TownPos[0], townCommonData.TownPos[1]));
            Vector2 pos = this.GetSystem<IMapSystem>().GetGridToMapPos(gridPos);
            transform.position = pos;
        }
    }
}