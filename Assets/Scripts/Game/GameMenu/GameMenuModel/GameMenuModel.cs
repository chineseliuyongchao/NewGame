using QFramework;

namespace Game.GameMenu
{
    public class GameMenuModel : AbstractModel, IGameMenuModel
    {
        private int _language;

        protected override void OnInit()
        {
        }

        public int Language
        {
            get => _language;
            set => _language = value;
        }
    }
}