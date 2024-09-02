using Game.GameBase;
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
        private IAStarModel _aStarModel;
        private IFightGameModel _fightGameModel;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _aStarModel = this.GetModel<IAStarModel>();
            _fightGameModel = this.GetModel<IFightGameModel>();
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

        private void OnDestroy()
        {
            var clickAction = _playerInput.actions["Click"];
            if (clickAction != null)
            {
                clickAction.performed -= OnClickPerformed;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}