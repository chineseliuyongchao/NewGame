using System.Collections.Generic;
using QFramework;

namespace Fight.System
{
    public interface IFightComputeSystem : ISystem
    {
        /// <summary>
        /// 进入战斗场景后自动计算所有军队的单位位置
        /// </summary>
        void ComputeUnitPos();

        /// <summary>
        /// 一次有反击的进攻，相当于a对b攻击，b也有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="armAId">单位a的id</param>
        /// <param name="armBId">单位b的id</param>
        void AssaultWithRetaliation(int armAId, int armBId);

        /// <summary>
        /// 一次没有反击的进攻，相当于a对b攻击，b没有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="armAId">单位a的id</param>
        /// <param name="armBId">单位b的id</param>
        void AssaultNoRetaliation(int armAId, int armBId);

        /// <summary>
        /// 一次射击
        /// </summary>
        /// <param name="armAId"></param>
        /// <param name="armBId"></param>
        void Shoot(int armAId, int armBId);

        /// <summary>
        /// 获取各个军队在同一回合的行动顺序
        /// </summary>
        /// <returns></returns>
        List<int> LegionOrder();
    }
}