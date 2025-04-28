using Battle.Player;
using Game.GameBase;
using QFramework;

namespace UI
{
    public class UIBattleCreateData : UIPanelData
    {
    }

    /// <summary>
    /// 创建游戏界面
    /// </summary>
    public partial class UIBattleCreate : UIBase
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIBattleCreateData ?? new UIBattleCreateData();
            // please add init code here
            base.OnInit(uiData);
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIBattleCreateData ?? new UIBattleCreateData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            createButton.onClick.AddListener(() =>
            {
                this.GetModel<IMyPlayerModel>().CreateGameData = new CreateGameData
                {
                    playerName = inputName.text.Equals("") ? "玩家" : inputName.text,
                    playerAge = inputAge.text.ToInt(20),
                    familyName = inputFamilyName.text.Equals("") ? "玩家家族" : inputFamilyName.text
                };
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeBattleScene();
            });
            leaveButton.onClick.AddListener(() =>
            {
                CloseSelf();
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.MENU_SCENE);
            });
        }

        protected override void OnListenEvent()
        {
        }
    }
}