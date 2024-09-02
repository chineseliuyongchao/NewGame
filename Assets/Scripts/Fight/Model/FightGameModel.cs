using System.Collections.Generic;
using Fight.Game.Arms;
using JetBrains.Annotations;
using QFramework;

namespace Fight
{
    /**
     * 存放战斗场景中通用属性以及数据
     */
    public class FightGameModel : AbstractModel, IFightGameModel
    {
        private readonly Dictionary<int, int> _armsIdToIndexDictionary = new();
        public Dictionary<int, int> ArmsIdToIndexDictionary => _armsIdToIndexDictionary;
        private readonly Dictionary<int, int> _indexToArmsIdDictionary = new();
        public Dictionary<int, int> IndexToArmsIdDictionary => _indexToArmsIdDictionary;
        private readonly Dictionary<int, int> _enemyIdToIndexDictionary = new();
        public Dictionary<int, int> EnemyIdToIndexDictionary => _enemyIdToIndexDictionary;
        private readonly Dictionary<int, int> _indexToEnemyIdDictionary = new();
        public Dictionary<int, int> IndexToEnemyIdDictionary => _indexToEnemyIdDictionary;

        [CanBeNull] private ArmsController _focusController;

        public ArmsController FocusController
        {
            get => _focusController;
            set => _focusController = value;
        }

        protected override void OnInit()
        {
        }
    }
}