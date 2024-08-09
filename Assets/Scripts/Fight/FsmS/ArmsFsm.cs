using System.Collections.Generic;
using Fight.Game;
using Fight.Game.Arms;
using Fight.Game.Arms.Human.Nova;
using Fight.Utils;
using QFramework;
using UnityEngine;

namespace Fight.FsmS
{
    public class ArmsFsm : MonoBehaviour, IController
    {
        private readonly SortedList<string, GameObject> _armsGameObjectList = new();

        private void Awake()
        {
            //初始化玩家的兵种
            var gamePlayerModel = this.GetModel<GamePlayerModel>();
            var fightGameModel = this.GetModel<FightGameModel>();

            //todo 配置测试兵种
            var heavyInfantryKnightsModel = this.GetModel<HeavyInfantryKnightsModel>();
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));
            gamePlayerModel.AddArmsInfo(new GamePlayerModel.ArmsInfo(Constants.HeavyInfantryKnights,
                heavyInfantryKnightsModel.Clone()));

            fightGameModel.GoFightScene();

            var aStarModel = this.GetModel<AStarModel>();
            foreach (var armsName in gamePlayerModel.ArmsNameSet)
                _armsGameObjectList[armsName] = Resources.Load<GameObject>($"Arms/{armsName}");

            foreach (var info in gamePlayerModel.ArmsInfoDictionary)
            {
                var obj = Instantiate(_armsGameObjectList[info.Value.ArmsName], transform);
                obj.transform.position = (Vector3)aStarModel.FightGridNodeInfoList[info.Value.RanksIndex].position;
                obj.name = info.Key.ToString();
                ObjectArmsController controller = obj.GetComponent<ObjectArmsController>();
                controller.id = info.Key;
                controller.OnInit();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}