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
        protected override void OnListenEvent()
        {
            base.OnListenEvent();
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                Move(GetStartMapPos(), e.selectPos, () =>
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