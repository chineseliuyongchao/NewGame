using Game.Town;
using GameQFramework;
using QFramework;
using UI;

namespace Game.Army
{
    /// <summary>
    /// 玩家自身的军队
    /// </summary>
    public class PlayerArmy : BaseArmy
    {
        protected override void OnInit()
        {
            base.OnInit();
            this.GetModel<IGameModel>().PlayerArmy = this;
        }

        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                Move(GetStartMapPos(), e.selectPos, () =>
                {
                    if (e.townId != 0)
                    {
                        ArriveInTown(e.townId);
                        this.GetModel<IMyPlayerModel>().AccessTown++;
                    }
                });
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void ArriveInTown(int townId)
        {
            UIKit.OpenPanel<UITown>(new UITownData(townId));
        }
    }
}