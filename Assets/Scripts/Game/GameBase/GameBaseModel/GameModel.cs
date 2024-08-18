using System.Collections.Generic;
using QFramework;

namespace Game.GameBase
{
    public class GameModel : AbstractModel, IGameModel
    {
        private Dictionary<int, LocalizationData> _localizationData;
        private Dictionary<int, LocalizationData> _dialogueLocalizationData;
        private Dictionary<int, LocalizationData> _dialogueTipLocalizationData;

        protected override void OnInit()
        {
            _localizationData = new Dictionary<int, LocalizationData>();
            _dialogueLocalizationData = new Dictionary<int, LocalizationData>();
            _dialogueTipLocalizationData = new Dictionary<int, LocalizationData>();
        }

        public Dictionary<int, LocalizationData> LocalizationData
        {
            get => _localizationData;
            set => _localizationData = value;
        }

        public Dictionary<int, LocalizationData> DialogueLocalizationData
        {
            get => _dialogueLocalizationData;
            set => _dialogueLocalizationData = value;
        }

        public Dictionary<int, LocalizationData> DialogueTipLocalizationData
        {
            get => _dialogueTipLocalizationData;
            set => _dialogueTipLocalizationData = value;
        }
    }
}