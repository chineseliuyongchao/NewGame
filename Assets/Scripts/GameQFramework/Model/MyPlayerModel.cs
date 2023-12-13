using QFramework;

namespace GameQFramework
{
    public class MyPlayerModel : AbstractModel, IMyPlayerModel
    {
        private int _accessTown;

        protected override void OnInit()
        {
        }

        public int AccessTown
        {
            get => _accessTown;
            set => _accessTown = value;
        }
    }
}