using QFramework;

namespace GameQFramework
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            RegisterSystem<IGameSystem>(new GameSystem());
            RegisterSystem<IResSystem>(new ResSystem());
            RegisterModel<IGameModel>(new GameModel());
            RegisterUtility<IGameUtility>(new GameUtility());
        }
    }
}