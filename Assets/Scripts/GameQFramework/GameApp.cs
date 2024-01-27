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
            RegisterSystem<IGameSaveSystem>(new GameSaveSystem());
            RegisterSystem<IFamilySystem>(new FamilySystem());
            RegisterSystem<ICountrySystem>(new CountrySystem());
            RegisterSystem<IPathfindingSystem>(new PathfindingSystem());
            RegisterSystem<IArmySystem>(new ArmySystem());

            RegisterModel<IGameModel>(new GameModel());
            RegisterModel<IMapModel>(new MapModel());
            RegisterModel<ITownModel>(new TownModel());
            RegisterModel<IMyPlayerModel>(new MyPlayerModel());
            RegisterModel<IFamilyModel>(new FamilyModel());
            RegisterModel<ICountryModel>(new CountryModel());
            RegisterModel<IArmyModel>(new ArmyModel());

            RegisterUtility<IGameUtility>(new GameUtility());
            RegisterUtility<IMathUtility>(new MathUtility());
        }
    }
}