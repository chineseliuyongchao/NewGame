using Game.Town;
using GameQFramework;
using QFramework;
using UI;
using UnityEngine;

namespace Game.Player
{
    /// <summary>
    /// 玩家游戏角色
    /// </summary>
    public class MyPlayer : BasePlayer
    {
        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                Vector2Int endPos = this.GetSystem<IMapSystem>().GetGridMapPos(e.selectPos);
                Move(GetStartPos(), endPos, () =>
                {
                    if (e.baseTown != null)
                    {
                        MoveToTown(e.baseTown);
                        this.GetModel<IMyPlayerModel>().AccessTown++;
                    }
                });
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void MoveToTown(BaseTown baseTown)
        {
            UIKit.OpenPanel<UITown>(new UITownData(baseTown.TownId));
        }
    }
}