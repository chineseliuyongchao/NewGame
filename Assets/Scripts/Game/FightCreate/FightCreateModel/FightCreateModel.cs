using System.Collections.Generic;
using Fight.Model;
using QFramework;

namespace Game.FightCreate
{
    public class FightCreateModel : AbstractModel, IFightCreateModel
    {
        private Dictionary<int, LegionInfo> _allBelligerents;

        protected override void OnInit()
        {
            _allBelligerents = new Dictionary<int, LegionInfo>();
        }

        public Dictionary<int, LegionInfo> AllLegions
        {
            get => _allBelligerents;
            set => _allBelligerents = value;
        }
    }

    /// <summary>
    /// 记录每一个参战军团的信息
    /// </summary>
    public class LegionInfo
    {
        /// <summary>
        /// 军团id，一个参战方可能有多个军团，每个军团有多个单位（军团编号0的是玩家）
        /// </summary>
        public int legionId;

        /// <summary>
        /// 参战方的id，目前参考全战只有两个参战方
        /// </summary>
        public int belligerentsId;

        /// <summary>
        /// 记录参展方的派系id
        /// </summary>
        public int factionsId;

        /// <summary>
        /// 记录参展方所有的单位信息
        /// </summary>
        public Dictionary<int, ArmData> allArm;
    }
}