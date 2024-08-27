using Fight.Commands;
using Fight.Enum;
using Fight.FsmS;
using Fight.Game;
using Fight.Game.Arms;
using Fight.Model;
using Fight.System;
using Game.GameBase;
using QFramework;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [HideInInspector] public BattleType currentBattleType;

        //debug
        [HideInInspector] public InputActionAsset inputActionAsset;

        public AStarModel aStarModel;

        private void Awake()
        {
            _ins = this;
            
            GameApp.Interface.RegisterSystem(new GameSystem());
            GameApp.Interface.RegisterSystem<IFightComputeSystem>(new FightComputeSystem());
            
            GameApp.Interface.RegisterModel(new AStarModel());
            GameApp.Interface.RegisterModel(new FightGameModel());
            GameApp.Interface.RegisterModel<IFightModel>(new FightModel());

            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
            inputActionAsset =
                AssetDatabase.LoadAssetAtPath<InputActionAsset>("Assets/Settings/MyControl.inputactions");
            aStarModel = this.GetModel<AStarModel>();

            UIKit.OpenPanel<UIGameFight>(new UIGameFightData());
        }

        private void Start()
        {
            this.SendCommand(new BattleCommand(BattleType.StartWarPreparations));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public ArmsController GetArmsControllerByIndex(int index)
        {
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
            if (fightGameModel.IndexToArmsIdDictionary.TryGetValue(index, out int id))
            {
                return _armsFsm.transform.Find(id.ToString()).GetComponent<ArmsController>();
            }

            return null;
        }

        private void OnDestroy()
        {
            GameApp.Interface.UnRegisterSystem<GameSystem>();
            GameApp.Interface.UnRegisterSystem<IFightComputeSystem>();
            
            GameApp.Interface.UnRegisterModel<AStarModel>();
            GameApp.Interface.UnRegisterModel<FightGameModel>();
            GameApp.Interface.UnRegisterModel<IFightModel>();
        }
    }
}