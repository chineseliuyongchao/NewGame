using QFramework;

namespace GameQFramework
{
    public interface IArmySystem : ISystem
    {
        /// <summary>
        /// 添加军队数据
        /// </summary>
        /// <param name="armyData"></param>
        /// <returns></returns>
        int AddArmy(ArmyData armyData);
    }
}