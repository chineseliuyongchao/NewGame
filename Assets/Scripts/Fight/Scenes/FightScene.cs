using Fight.FsmS;
using Fight.Game.Arms;
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

        public IAStarModel aStarModel;

        private void Awake()
        {
            _ins = this;
            this.GetModel<IAStarModel>().InitStarData();
            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
            inputActionAsset =
                AssetDatabase.LoadAssetAtPath<InputActionAsset>("Assets/Settings/MyControl.inputactions");
            aStarModel = this.GetModel<IAStarModel>();
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
            IFightGameModel fightGameModel = this.GetModel<IFightGameModel>();
            if (fightGameModel.IndexToArmsIdDictionary.TryGetValue(index, out int id))
            {
                return _armsFsm.transform.Find(id.ToString()).GetComponent<ArmsController>();
            }

            return null;
        }

        private void OnDestroy()
        {
            GameApp.Interface.UnRegisterSystem<IFightComputeSystem>();
            GameApp.Interface.UnRegisterSystem<IFightSystem>();

            GameApp.Interface.UnRegisterModel<IAStarModel>();
            GameApp.Interface.UnRegisterModel<IFightGameModel>();
            GameApp.Interface.UnRegisterModel<IFightModel>();
        }
    }
}