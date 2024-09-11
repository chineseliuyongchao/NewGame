using System.Collections.Generic;
using Fight.Game;
using JetBrains.Annotations;
using QFramework;

namespace Fight
{
    /**
     * 存放战斗场景中通用属性以及数据
     */
    public class FightVisualModel : AbstractModel, IFightVisualModel
    {
        private Dictionary<int, int> _armsIdToIndexDictionary;
        private Dictionary<int, int> _indexToArmsIdDictionary;
        private Dictionary<int, int> _enemyIdToIndexDictionary;
        private Dictionary<int, int> _indexToEnemyIdDictionary;
        [CanBeNull] private ArmsController _focusController;
        private Dictionary<int, ArmsController> _allArm;

        protected override void OnInit()
        {
            _armsIdToIndexDictionary = new Dictionary<int, int>();
            _indexToArmsIdDictionary = new Dictionary<int, int>();
            _enemyIdToIndexDictionary = new Dictionary<int, int>();
            _indexToEnemyIdDictionary = new Dictionary<int, int>();
            _allArm = new Dictionary<int, ArmsController>();
        }

        public Dictionary<int, int> ArmsIdToIndexDictionary => _armsIdToIndexDictionary;
        public Dictionary<int, int> IndexToArmsIdDictionary => _indexToArmsIdDictionary;
        public Dictionary<int, int> EnemyIdToIndexDictionary => _enemyIdToIndexDictionary;
        public Dictionary<int, int> IndexToEnemyIdDictionary => _indexToEnemyIdDictionary;

        public ArmsController FocusController
        {
            get => _focusController;
            set => _focusController = value;
        }

        public Dictionary<int, ArmsController> AllArm
        {
            get => _allArm;
            set => _allArm = value;
        }
    }
}