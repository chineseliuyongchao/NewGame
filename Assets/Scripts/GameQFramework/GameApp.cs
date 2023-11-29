using QFramework;

namespace GameQFramework
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            RegisterSystem<IGameSystem>(new GameSystem());
            RegisterSystem<IResSystem>(new ResSystem());
            RegisterSystem<IMapSystem>(new MapSystem());
            
            RegisterModel<IGameModel>(new GameModel());
            RegisterModel<IMapModel>(new MapModel());
            
            RegisterUtility<IGameUtility>(new GameUtility());
        }
    }
}