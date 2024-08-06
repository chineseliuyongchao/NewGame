using System;
using Fight.Commands;
using Fight.Enum;
using Fight.FsmS;
using Fight.Game;
using Fight.Game.Arms;
using QFramework;
using UnityEngine;

namespace Fight.Scenes
{
    /**
     * 管理整个场景的状态：战前准备-战斗中-追击中-战斗结束
     */
    public class FightScene : MonoBehaviour, IController
    {
        public static FightScene Ins => _ins;
        private static FightScene _ins;

        private ArmsFsm _armsFsm;

        [HideInInspector]
        public BattleType currentBattleType;

        private void Awake()
        {
            _ins = this;
            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
        }

        private void Start()
        {
            this.SendCommand(new BattleCommand(BattleType.StartWarPreparations));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public IObjectArmsController GetArmsControllerByIndex(int index)
        {
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
            if (fightGameModel.FightSceneArmsNameDictionary.TryGetValue(index, out string armsName))
            {
                return _armsFsm.transform.Find(armsName).GetComponent<IObjectArmsController>();
            }

            return null;
        }
        
    }
}