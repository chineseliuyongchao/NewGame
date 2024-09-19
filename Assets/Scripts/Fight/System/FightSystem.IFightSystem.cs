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
        public bool CanWalkableIndex(int index);

        /// <summary>
        /// 判断单位是不是玩家的
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public bool IsPlayerUnit(int unitId);

        /// <summary>
        /// 感觉单位id查找单位
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public UnitData FindUnit(int unitId);
    }
}