using System.Collections.Generic;
using Fight.Game.Unit;
using Fight.Model;
using Fight.Utils;
using Game.FightCreate;
using Game.GameBase;
using Game.GameMenu;
using QFramework;
using UnityEngine;

namespace Fight.FsmS
{
    public class ArmsFsm : BaseGameController
    {
        /// <summary>
        /// 存储已经从ab包拿出来的兵种预制体
        /// </summary>
        /// <returns></returns>
        private readonly Dictionary<int, GameObject> _unitPrefabCache = new();

        protected override void OnInit()
        {
            base.OnInit();
            var aStarModel = this.GetModel<IAStarModel>();

            //获取所有战场上的军队数据
            //兵种的预制体名称一定与数据表中兵种名称保持一致
            IFightCreateModel fightCreateModel = this.GetModel<IFightCreateModel>();
            IGameMenuModel gameMenuModel = this.GetModel<IGameMenuModel>();
            var unitDictionary = gameMenuModel.ARMDataTypes;
            foreach (var tmp in fightCreateModel.AllLegions.Values)
            {
                foreach (var tmp2 in tmp.allUnit)
                {
                    int armId = tmp2.Value.armId;
                    GameObject obj = null;
                    if (_unitPrefabCache.TryGetValue(armId, out var prefab))
                    {
                        obj = Instantiate(prefab, transform);
                    }
                    else if (unitDictionary.TryGetValue(tmp2.Value.armId, out var dataType))
                    {
                        string name1 = dataType.unitName;
                        prefab = resLoader.LoadSync<GameObject>(Constants.AssetsPrefabUnitAb, name1);
                        if (prefab)
                        {
                            _unitPrefabCache.TryAdd(armId, prefab);
                            obj = Instantiate(prefab, transform);
                        }
                    }

                    if (obj)
                    {
                        obj.name = tmp2.Value.unitId.ToString();
                        UnitController controller = obj.AddComponent<UnitController>();
                        controller.unitData = tmp2.Value;
                        controller.Init();
                        obj.transform.position =
                            (Vector3)aStarModel.FightGridNodeInfoList[controller.unitData.currentPosIndex].position;
                        this.GetModel<IFightVisualModel>().AllUnit.Add(tmp2.Value.unitId, controller);
                    }
                    else
                    {
                        Debug.LogError("这个兵种没有被找到.");
                    }
                }
            }
        }
    }
}