using Fight.Enum;

namespace Fight.Game.Arms
{
    public interface IObjectArmsController
    {
        void OnInit();

        ObjectArmsModel GetModel();

        ObjectArmsView GetView();

        string GetName();

        void StartFocusAction();

        void EndFocusAction();

        void ArmsMoveAction(int endIndex);
    }
}