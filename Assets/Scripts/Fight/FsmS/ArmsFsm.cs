using Fight.Game;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Fight.FsmS
{
    public class ArmsFsm : BaseGameController
    {
        public GameObject objArmsGameObject;

        protected override void OnInit()
        {
            base.OnInit();
            var aStarModel = this.GetModel<IAStarModel>();

            //获取所有战场上的军队数据
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();
            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                foreach (var tmp2 in tmp.allArm)
                {
                    GameObject obj = Instantiate(objArmsGameObject, transform);
                    obj.name = tmp2.Value.unitId.ToString();
                    ArmsController controller = obj.AddComponent<ArmsController>();
                    controller.armData = tmp2.Value;
                    controller.view = obj.AddComponent<ObjectArmsView>();
                    controller.view.OnInit(obj.transform);
                    controller.OnInit();
                    obj.transform.position =
                        (Vector3)aStarModel.FightGridNodeInfoList[controller.armData.currentPosition].position;
                    this.GetModel<IFightVisualModel>().AllArm.Add(tmp2.Value.unitId, controller);
                }
            }
        }
    }
}