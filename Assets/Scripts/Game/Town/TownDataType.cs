using Utils;

namespace Game.Town
{
    /// <summary>
    /// 记录聚落数据
    /// </summary>
    [System.Serializable]
    public class Town : BaseJsonData
    {
        public int[] TownPos;
    }
}