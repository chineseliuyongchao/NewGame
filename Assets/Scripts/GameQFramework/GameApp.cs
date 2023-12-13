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
            RegisterSystem<ITownSystem>(new TownSystem());

            RegisterModel<IGameModel>(new GameModel());
            RegisterModel<IMapModel>(new MapModel());
            RegisterModel<ITownModel>(new TownModel());
            RegisterModel<IMyPlayerModel>(new MyPlayerModel());

            RegisterUtility<IGameUtility>(new GameUtility());
        }
    }
}