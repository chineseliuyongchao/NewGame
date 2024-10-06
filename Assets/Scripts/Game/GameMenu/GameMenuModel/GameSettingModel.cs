using QFramework;
using UnityEngine;

namespace Game.GameMenu
{
    public class GameSettingModel : AbstractModel, IGameSettingModel
    {
        protected override void OnInit()
        {
        }

        public int Language
        {
            get => PlayerPrefs.GetInt("HOP_Setting_Language", 0);
            set => PlayerPrefs.SetInt("HOP_Setting_Language", value);
        }

        public bool ShowUnitHp
        {
            get => PlayerPrefs.GetInt("HOP_Setting_ShowUnitHp", 1) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_ShowUnitHp", value ? 1 : 0);
        }

        public bool ShowUnitTroops
        {
            get => PlayerPrefs.GetInt("HOP_Setting_ShowUnitTroops", 1) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_ShowUnitTroops", value ? 1 : 0);
        }

        public bool ShowUnitMorale
        {
            get => PlayerPrefs.GetInt("HOP_Setting_ShowUnitMorale", 1) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_ShowUnitMorale", value ? 1 : 0);
        }

        public bool ShowUnitFatigue
        {
            get => PlayerPrefs.GetInt("HOP_Setting_ShowUnitFatigue", 0) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_ShowUnitFatigue", value ? 1 : 0);
        }

        public bool ShowMovementPoints
        {
            get => PlayerPrefs.GetInt("HOP_Setting_ShowMovementPoints", 0) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_ShowMovementPoints", value ? 1 : 0);
        }

        public bool AutomaticSwitchingUnit
        {
            get => PlayerPrefs.GetInt("HOP_Setting_AutomaticSwitchingUnit", 0) == 1;
            set => PlayerPrefs.SetInt("HOP_Setting_AutomaticSwitchingUnit", value ? 1 : 0);
        }
    }
}