namespace Game.Town
{
    /// <summary>
    /// 聚落相关常量
    /// </summary>
    public class TownConstant
    {
        /// <summary>
        /// 暂定每块农田产量10
        /// </summary>
        public const int FARMLAND_OUTPUT = 10;

        /// <summary>
        /// 暂定每个人每天的粮食消耗是0.05
        /// </summary>
        public const float POPULATION_GRAIN_CONSUME = 0.05f;

        /// <summary>
        /// 暂定每级粮仓粮食存储是1000
        /// </summary>
        public const int GRANARY_RESERVES = 10000;

        /// <summary>
        /// 粮食单价，暂时使用常量
        /// </summary>
        public const int GRAIN_PRICE = 1;

        /// <summary>
        /// 人口繁荣度系数
        /// </summary>
        public const float POPULATION_PROSPERITY_COEFFICIENT = 0.5f;

        /// <summary>
        /// 收入繁荣度系数
        /// </summary>
        public const float INCOME_PROSPERITY_COEFFICIENT = 0.3f;
    }
}