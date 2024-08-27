using QFramework;

namespace Fight.System
{
    public interface IFightComputeSystem : ISystem
    {
        /// <summary>
        /// 一次有反击的进攻，相当于a对b攻击，b也有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="armAId">单位a的id</param>
        /// <param name="armBId">单位b的id</param>
        public void AssaultWithRetaliation(int armAId, int armBId);

        /// <summary>
        /// 一次没有反击的进攻，相当于a对b攻击，b没有反击，默认a是攻击方，b是被攻击方
        /// </summary>
        /// <param name="armAId">单位a的id</param>
        /// <param name="armBId">单位b的id</param>
        public void AssaultNoRetaliation(int armAId, int armBId);

        /// <summary>
        /// 一次射击
        /// </summary>
        /// <param name="armAId"></param>
        /// <param name="armBId"></param>
        public void Shoot(int armAId, int armBId);
    }
}