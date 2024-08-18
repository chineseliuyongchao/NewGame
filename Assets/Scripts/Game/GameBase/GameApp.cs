using Battle.BattleBase;
using Battle.Country;
using Battle.Family;
using Battle.Map;
using Battle.Player;
using Battle.Team;
using Battle.Town;
using Game.GameMenu;
using Game.GameSave;
using Game.GameUtils;
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
            RegisterSystem<IBattleBaseSystem>(new BattleBaseSystem());

            RegisterModel<IGameModel>(new GameModel());
            RegisterModel<IMapModel>(new MapModel());
            RegisterModel<ITownModel>(new TownModel());
            RegisterModel<IMyPlayerModel>(new MyPlayerModel());
            RegisterModel<IFamilyModel>(new FamilyModel());
            RegisterModel<ICountryModel>(new CountryModel());
            RegisterModel<ITeamModel>(new TeamModel());
            RegisterModel<IGameMenuModel>(new GameMenuModel());
            RegisterModel<IBattleBaseModel>(new BattleBaseModel());

            RegisterUtility<IGameUtility>(new GameUtility());
            RegisterUtility<IMathUtility>(new MathUtility());
        }
    }
}