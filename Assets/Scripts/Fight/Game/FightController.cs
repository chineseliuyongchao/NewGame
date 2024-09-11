using Fight.FsmS;
using Game.GameBase;
using QFramework;
using UI;
using UnityEngine;

namespace Fight.Game
{
    /**
     * 管理整个场景的状态：战前准备-战斗中-战斗结束
     */
    public class FightController : MonoBehaviour, IController
    {
        private ArmsFsm _armsFsm;

        private void Awake()
        {
            this.GetModel<IAStarModel>().InitStarData();
            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
            UIKit.OpenPanel<UIGameFight>(new UIGameFightData());
        }

        private void Start()
        {
            this.SendCommand(new FightCommand(FightType.WAR_PREPARATIONS));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        private void OnDestroy()
        {
            GameApp.Interface.UnRegisterSystem<IFightComputeSystem>();
            GameApp.Interface.UnRegisterSystem<IFightSystem>();

            GameApp.Interface.UnRegisterModel<IAStarModel>();
            GameApp.Interface.UnRegisterModel<IFightVisualModel>();
            GameApp.Interface.UnRegisterModel<IFightCoreModel>();
        }
    }
}