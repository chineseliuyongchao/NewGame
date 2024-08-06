namespace Fight.Game.Arms
{
    public interface IObjectArmsController
    {
        void OnInit();

        ObjectArmsModel GetModel();

        ObjectArmsView GetView();

        void StartFocusAction();

        void EndFocusAction();
    }
}