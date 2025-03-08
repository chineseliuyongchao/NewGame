using QFramework;

namespace Game.GameTest
{
    public class GameTestModel : AbstractModel, IGameTestModel
    {
        // 私有字段
        private bool _isTest;
        private bool _fixedHitRateEnabled;
        private bool _aiNoAttack;
        private bool _aiNoMove;
        private bool _canPlaceAI;
        private bool _ignoreMorale;
        private bool _ignoreFatigue;

        protected override void OnInit()
        {
            // 初始化默认值
            _isTest = false; // 默认关闭测试模式
            _fixedHitRateEnabled = false;
            _aiNoAttack = false;
            _aiNoMove = false;
            _canPlaceAI = false;
            _ignoreMorale = false;
            _ignoreFatigue = false;
        }

        // 是否启用测试模式
        public bool IsTest
        {
            get => _isTest;
            set => _isTest = value;
        }

        // 是否启用固定命中率
        public bool FixedHitRateEnabled
        {
            get => _fixedHitRateEnabled && _isTest;
            set => _fixedHitRateEnabled = value;
        }

        // 是否禁用 AI 攻击
        public bool AINoAttack
        {
            get => _aiNoAttack && _isTest;
            set => _aiNoAttack = value;
        }

        // 是否禁用 AI 移动
        public bool AINoMove
        {
            get => _aiNoMove && _isTest;
            set => _aiNoMove = value;
        }

        // 是否允许在战斗开始时摆放 AI
        public bool CanPlaceAI
        {
            get => _canPlaceAI && _isTest;
            set => _canPlaceAI = value;
        }

        // 是否忽略作战意志计算
        public bool IgnoreMorale
        {
            get => _ignoreMorale && _isTest;
            set => _ignoreMorale = value;
        }

        // 是否忽略疲劳值计算
        public bool IgnoreFatigue
        {
            get => _ignoreFatigue && _isTest;
            set => _ignoreFatigue = value;
        }
    }
}