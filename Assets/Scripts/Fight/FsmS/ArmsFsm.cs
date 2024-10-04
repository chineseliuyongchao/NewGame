using Fight.Game.Unit;
using Fight.Model;
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
                foreach (var tmp2 in tmp.allUnit)
                {
                    GameObject obj = Instantiate(objArmsGameObject, transform);
                    obj.name = tmp2.Value.unitId.ToString();
                    UnitController controller = obj.AddComponent<UnitController>();
                    controller.unitData = tmp2.Value;
                    controller.Init();
                    obj.transform.position =
                        (Vector3)aStarModel.FightGridNodeInfoList[controller.unitData.currentPosIndex].position;
                    this.GetModel<IFightVisualModel>().AllUnit.Add(tmp2.Value.unitId, controller);
                }
            }
        }
    }
}