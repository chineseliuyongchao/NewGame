using System.Collections.Generic;
using Fight.Game.Arms;
using Fight.Utils;
using QFramework;

namespace Fight.Game
{
    public class GamePlayerModel : AbstractModel, ICanGetModel
    {
        /// <summary>
        ///     key：兵种占据的位置
        /// </summary>
        private readonly SortedSet<int> _myArmsSet2 = new();

        /// <summary>
        ///     key：兵种名
        /// </summary>
        public HashSet<string> ArmsNameSet { get; } = new();

        /// <summary>
        ///     key：兵种的专属id
        ///     value：兵种的信息
        /// </summary>
        public Dictionary<string, ArmsInfo> ArmsInfoDictionary { get; } = new();

        protected override void OnInit()
        {
        }

        public void AddArmsInfo(ArmsInfo armsInfo)
        {
            if (!CanAddArms()) return;

            armsInfo.Id = armsInfo.ArmsName + ArmsUtils.GetRandomByTime();
            for (var i = 0; i < Constants.ArmsRow * Constants.ArmsCol; i++)
                if (!_myArmsSet2.Contains(i))
                {
                    armsInfo.RanksIndex = i;
                    break;
                }

            ArmsNameSet.Add(armsInfo.ArmsName);
            _myArmsSet2.Add(armsInfo.RanksIndex);
            ArmsInfoDictionary[armsInfo.Id] = armsInfo;
        }

        /// <summary>
        ///     是否能够添加兵种到玩家的队伍中
        /// </summary>
        /// <returns>能否添加兵种</returns>
        public bool CanAddArms()
        {
            return ArmsInfoDictionary.Count < Constants.ArmsRow * Constants.ArmsCol;
        }

        public class ArmsInfo
        {
            /// <summary>
            ///     该兵种的名字
            /// </summary>
            public readonly string ArmsName;

            /// <summary>
            ///     兵种模型
            /// </summary>
            public readonly ObjectArmsModel ObjectArmsModel;

            /// <summary>
            ///     该兵种的专属id
            /// </summary>
            public string Id;

            /// <summary>
            ///     在玩家队列中的位置，当生成战斗时会同步到战斗地图的位置
            /// </summary>
            public int RanksIndex;

            public ArmsInfo(string armsName, ObjectArmsModel objectArmsModel)
            {
                ArmsName = armsName;
                ObjectArmsModel = objectArmsModel;
            }
        }
    }
}