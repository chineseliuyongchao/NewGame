using System;
using Fight.Commands;
using Fight.Enum;
using Fight.FsmS;
using Fight.Game;
using Fight.Game.Arms;
using Game.GameBase;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using GameApp = Fight.Game.GameApp;

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

        //debug
        [HideInInspector]
        public InputActionAsset inputActionAsset;

        public AStarModel AStarModel;
        
        private void Awake()
        {
            _ins = this;
            _armsFsm = transform.Find("ArmsFsm").GetComponent<ArmsFsm>();
            inputActionAsset =
                AssetDatabase.LoadAssetAtPath<InputActionAsset>("Assets/Settings/MyControl.inputactions");
            AStarModel = this.GetModel<AStarModel>();
        }

        private void Start()
        {
            this.SendCommand(new BattleCommand(BattleType.StartWarPreparations));
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public ObjectArmsController GetArmsControllerByIndex(int index)
        {
            FightGameModel fightGameModel = this.GetModel<FightGameModel>();
            if (fightGameModel.IndexToArmsIdDictionary.TryGetValue(index, out int id))
            {
                return _armsFsm.transform.Find(id.ToString()).GetComponent<ObjectArmsController>();
            }

            return null;
        }

        private void Update()
        {
            //debug
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (this.GetSystem<IGameSystem>() == null)
                {
                    Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                }
                this.GetSystem<IGameSystem>().ChangeScene(SceneType.CREATE_FIGHT_SCENE);
            }
        }
    }
}