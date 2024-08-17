using Game.Country;
using Game.Family;
using Game.GameMenu;
using Game.GameSave;
using Game.GameUtils;
using Game.Map;
using Game.Player;
using Game.Team;
using Game.Town;
using QFramework;

namespace Game.GameBase
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
            RegisterSystem<ITeamSystem>(new TeamSystem());
            RegisterSystem<IGameMenuSystem>(new GameMenuSystem());

            RegisterModel<IGameModel>(new GameModel());
            RegisterModel<IMapModel>(new MapModel());
            RegisterModel<ITownModel>(new TownModel());
            RegisterModel<IMyPlayerModel>(new MyPlayerModel());
            RegisterModel<IFamilyModel>(new FamilyModel());
            RegisterModel<ICountryModel>(new CountryModel());
            RegisterModel<ITeamModel>(new TeamModel());
            RegisterModel<IGameMenuModel>(new GameMenuModel());

            RegisterUtility<IGameUtility>(new GameUtility());
            RegisterUtility<IMathUtility>(new MathUtility());
        }
    }
}