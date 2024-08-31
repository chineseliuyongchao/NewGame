using QFramework;

namespace Fight
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
    }
}