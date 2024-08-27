using System.Collections.Generic;
using Fight.Game;
using Fight.Game.Arms;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Fight.FsmS
{
    public class ArmsFsm : MonoBehaviour, IController
    {
        private readonly SortedList<string, GameObject> _armsGameObjectList = new();

        public GameObject objArmsGameObject;

        private void Awake()
        {
            //初始化玩家的兵种
            var fightGameModel = this.GetModel<FightGameModel>();

            fightGameModel.GoFightScene();

            var aStarModel = this.GetModel<AStarModel>();

            //获取所有战场上的军队数据
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();

            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                
            }

            // foreach (var info in gamePlayerModel.ArmsInfoDictionary)
            // {
            //     var obj = Instantiate(_armsGameObjectList[info.Value.ArmsName], transform);
            //     obj.transform.position = (Vector3)aStarModel.FightGridNodeInfoList[info.Value.RanksIndex].position;
            //     obj.name = info.Key.ToString();
            //     ArmsController controller = obj.GetComponent<ArmsController>();
            // }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}