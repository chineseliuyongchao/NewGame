using Fight.Command;
using Fight.Model;
using Fight.Tools;
using Fight.Utils;
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

        private TipsMark _currentHoverObject;
        private float _hoverTime;

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

        private void Update()
        {
            if (Camera.main)
            {
                Vector2 mousePosition = _playerInput.actions["MousePosition"].ReadValue<Vector2>();
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // 发射2D射线检测是否击中物体
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                if (hit.collider)
                {
                    GameObject hoveredObject = hit.collider.gameObject;
                    //检测是否需要展示提示气泡
                    TipsMark tipsMark = hoveredObject.GetComponent<TipsMark>();
                    if (tipsMark && !tipsMark.showTips)
                    {
                        if (!_currentHoverObject)
                        {
                            _currentHoverObject = tipsMark;
                        }
                        else if (_currentHoverObject.id != tipsMark.id)
                        {
                            _currentHoverObject = tipsMark;
                            _hoverTime = 0f;
                        }

                        _hoverTime += Time.deltaTime;
                        if (_hoverTime >= Constants.HoverThreshold)
                        {
                            this.SendCommand(new TipsCommand(worldPosition, _currentHoverObject));
                            _currentHoverObject = null;
                            _hoverTime = 0f;
                        }
                    }
                    else
                    {
                        _currentHoverObject = null;
                        _hoverTime = 0f;
                    }
                }
            }
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