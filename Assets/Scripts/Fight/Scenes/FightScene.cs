using Fight.Commands;
using Fight.Enum;
using Fight.Game;
using QFramework;
using UnityEngine;

namespace Fight.Scenes
{
    /**
     * 管理整个场景的状态：战前准备-战斗中-追击中-战斗结束
     */
    public class FightScene : MonoBehaviour, IController
    {
        private void Start()
        {
            this.SendCommand(new BattleCommand(BattleType.StartWarPreparations));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}