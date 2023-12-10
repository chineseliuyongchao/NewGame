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
            Town town = this.GetModel<ITownModel>().TownData[gameObject.name];
            IntVector2 gridPos = this.GetSystem<IMapSystem>()
                .GetGridMapPos(new Vector3(town.TownPos[0], town.TownPos[1]));
            Vector2 pos = this.GetSystem<IMapSystem>().GetGridToMapPos(gridPos);
            transform.position = pos;
        }
    }
}