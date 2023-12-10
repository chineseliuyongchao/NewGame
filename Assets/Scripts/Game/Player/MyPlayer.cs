using GameQFramework;
using QFramework;
using Utils;

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
                IntVector2 endPos = this.GetSystem<IMapSystem>().GetGridMapPos(e.SelectPos);
                Move(GetStartPos(), endPos, () =>
                {
                    if (e.BaseTown != null)
                    {
                        MoveToTown(e.BaseTown);
                    }
                });
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}