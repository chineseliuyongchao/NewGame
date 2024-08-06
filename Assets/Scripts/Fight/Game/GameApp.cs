using Fight.Game.Arms.Human.Nova;
using Fight.Systems;
using QFramework;

namespace Fight.Game
{
    public class GameApp : Architecture<GameApp>
    {
        protected override void Init()
        {
            RegisterSystem(new TraitSystem());
            RegisterModel(new GamePlayerModel());
            RegisterModel(new AStarModel());
            
            RegisterModel(new FightGameModel());

            RegisterModel(new HeavyInfantryKnightsModel());
        }
    }
}