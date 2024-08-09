using Fight.Commands.EventSystem;
using Fight.Game;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight.Tools
{
    /**
     * 管理战斗场景中的所有输入（ui除外）
     */
    [RequireComponent(typeof(PlayerInput))]
    public class InputSystemController : MonoBehaviour, IController
    {
        private AStarModel _aStarModel;
        private FightGameModel _fightGameModel;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _aStarModel = this.GetModel<AStarModel>();
            _fightGameModel = this.GetModel<FightGameModel>();
            _playerInput = gameObject.GetComponent<PlayerInput>();
            // _playerInput.actions = FightScene.Ins.inputActionAsset;
            // _playerInput.defaultActionMap = "Fight";
            var clickAction = _playerInput.actions["Click"];
            if (clickAction != null)
            {
                clickAction.performed += OnClickPerformed;
            }
            
        }
        
        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            this.SendCommand<PointerClickCommand>();
        }
        
        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}