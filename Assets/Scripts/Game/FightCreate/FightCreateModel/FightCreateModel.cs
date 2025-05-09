﻿using System.Collections.Generic;
using Fight.Model;
using QFramework;

namespace Game.FightCreate
{
    public class FightCreateModel : AbstractModel, IFightCreateModel
    {
        private Dictionary<int, LegionInfo> _allCamp;

        protected override void OnInit()
        {
            _allCamp = new Dictionary<int, LegionInfo>();
        }

        public Dictionary<int, LegionInfo> AllLegions
        {
            get => _allCamp;
            set => _allCamp = value;
        }
    }

    /// <summary>
    /// 记录每一个参战军队的信息
    /// </summary>
    public class LegionInfo
    {
        /// <summary>
        /// 军队id，一个阵营可能有多个军队，每个军队有多个单位（军队编号1001的是玩家，使用Constants.PlayLegionId变量，方便后续可能得修改）
        /// </summary>
        public int legionId;

        /// <summary>
        /// 阵营的id，目前参考全战只有两个阵营
        /// </summary>
        public int campId;

        /// <summary>
        /// 记录军队的派系id
        /// </summary>
        public int factionsId;

        /// <summary>
        /// 记录上一个单位的id，用于自动生成单位id
        /// </summary>
        public int lastUnitId;

        /// <summary>
        /// 记录阵营所有的单位信息
        /// </summary>
        public Dictionary<int, UnitData> allUnit;
    }
}