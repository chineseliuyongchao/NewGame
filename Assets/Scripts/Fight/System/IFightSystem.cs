using Fight.Game.Unit;
using Fight.Model;
using QFramework;

namespace Fight.System
{
    public interface IFightSystem : ISystem
    {
        /// <summary>
        /// 初始化战场数据
        /// </summary>
        void InitFightData();

        /// <summary>
        /// 判断是否可以移动到某个位置
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool CanWalkableIndex(int index);

        /// <summary>
        /// 判断单位是不是玩家的
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        bool IsPlayerUnit(int unitId);

        /// <summary>
        /// 根据单位id查找单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        UnitData FindUnit(int unitId);

        /// <summary>
        /// 修改一个单位的位置记录
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="endIndex"></param>
        void UnitChangePos(UnitController controller, int endIndex);
    }
}