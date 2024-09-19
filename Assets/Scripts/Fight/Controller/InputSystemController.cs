using Fight.Command;
using Fight.Model;
using Game.GameBase;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fight.Controller
{
    /**
     * 管理战斗场景中的所有输入（ui除外）
     */
    [RequireComponent(typeof(PlayerInput))]
    public class InputSystemController : MonoBehaviour, IController
    {
        private IAStarModel _aStarModel;
        private IFightVisualModel _fightVisualModel;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _aStarModel = this.GetModel<IAStarModel>();
            _fightVisualModel = this.GetModel<IFightVisualModel>();
            _playerInput = gameObject.GetComponent<PlayerInput>();
            // _playerInput.actions = FightScene.Ins.inputActionAsset;
            // _playerInput.defaultActionMap = "Fight";
            var clickAction = _playerInput.actions["Click"];
            if (clickAction != null)
            {
                clickAction.performed += OnClickPerformed;
            }

            var mouseDragAction = _playerInput.actions["RightClickDrag"];
            if (mouseDragAction != null)
            {
                mouseDragAction.performed += OnRightClickDragPerformed;
            }
            
            var mouseScrollAction = _playerInput.actions["MouseScroll"];
            if (mouseScrollAction != null)
            {
                mouseScrollAction.performed += MouseScrollPerformed;
            }
        }
        
        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            this.SendCommand<PointerClickCommand>();
        }

        private void OnRightClickDragPerformed(InputAction.CallbackContext context)
        {
            this.SendCommand(new MouseDragCommand(context.ReadValue<Vector2>()));
        }
        
        private void MouseScrollPerformed(InputAction.CallbackContext context)
        {
            this.SendCommand(new MouseScrollCommand(context.ReadValue<Vector2>()));
        }

        private void OnDestroy()
        {
            var clickAction = _playerInput.actions["Click"];
            if (clickAction != null)
            {
                clickAction.performed -= OnClickPerformed;
            }
            var mouseDragAction = _playerInput.actions["RightClickDrag"];
            if (mouseDragAction != null)
            {
                mouseDragAction.performed -= OnRightClickDragPerformed;
            }
            
            var mouseScrollAction = _playerInput.actions["MouseScroll"];
            if (mouseScrollAction != null)
            {
                mouseScrollAction.performed -= MouseScrollPerformed;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}