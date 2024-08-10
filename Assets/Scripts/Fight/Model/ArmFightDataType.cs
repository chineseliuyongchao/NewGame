using Game.GameMenu;

namespace Fight.Model
{

    /// <summary>
    /// 在攻击模拟时记录一个单位的实时属性
    /// </summary>
    public class ArmData
    {
        /// <summary>
        /// 兵种id
        /// </summary>
        public int ARMId;

        /// <summary>
        /// 当前血量
        /// </summary>
        private int _nowHp;

        public int NowHp
        {
            get => _nowHp;
            set
            {
                _nowHp = value;
                if (_nowHp < 0)
                {
                    _nowHp = 0;
                }
            }
        }

        /// <summary>
        /// 当前人数
        /// </summary>
        private int _nowTroops;

        public int NowTroops
        {
            get => _nowTroops;
            set
            {
                _nowTroops = value;
                if (_nowTroops < 0)
                {
                    _nowTroops = 0;
                }
            }
        }

        /// <summary>
        /// 当前弹药量
        /// </summary>
        public int NowAmmo;

        /// <summary>
        /// 当前作战意志
        /// </summary>
        public int NowMorale;

        /// <summary>
        /// 当前疲劳值
        /// </summary>
        public int NowFatigue;

        public ArmData(ArmData armData)
        {
            ARMId = armData.ARMId;
            _nowHp = armData._nowHp;
            _nowTroops = armData._nowTroops;
            NowAmmo = armData.NowAmmo;
            NowMorale = armData.NowMorale;
            NowFatigue = armData.NowFatigue;
        }

        public ArmData(ArmDataType armDataType, int id)
        {
            ARMId = id;
            _nowHp = armDataType.totalHp;
            _nowTroops = armDataType.totalTroops;
            NowAmmo = armDataType.ammo;
            NowMorale = armDataType.maximumMorale;
            NowFatigue = armDataType.maximumFatigue;
        }

        public void Reset(ArmData data)
        {
            ARMId = data.ARMId;
            _nowHp = data._nowHp;
            _nowTroops = data._nowTroops;
            NowAmmo = data.NowAmmo;
            NowMorale = data.NowMorale;
            NowFatigue = data.NowFatigue;
        }
    }

    /// <summary>
    /// 攻击形式
    /// </summary>
    public enum AttackFormType
    {
        /// <summary>
        /// 单向攻击：只有队伍a会攻击，b不会反击
        /// </summary>
        ONE_WAY_ATTACK,

        /// <summary>
        /// 互相攻击：a会攻击，b也会反击
        /// </summary>
        MUTUAL_ATTACK,

        /// <summary>
        /// 互相进攻：a会进攻，b也会进攻，同时ab都会反击
        /// </summary>
        MUTUAL_OFFENSE
    }
}