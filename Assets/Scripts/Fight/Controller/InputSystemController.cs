using Fight.Command;
using Fight.Model;
using Fight.System;
using Fight.Tools.Tips;
using Fight.Utils;
using Game.GameBase;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

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
            // 检查是哪个按键触发的事件
            if (context.control is ButtonControl button)
            {
                if (button.name == "leftButton")
                {
                    this.GetSystem<IFightInputSystem>().MouseButtonLeft();
                }
                else if (button.name == "middleButton")
                {
                    this.GetSystem<IFightInputSystem>().MouseButtonMiddle();
                }
                else if (button.name == "rightButton")
                {
                    this.GetSystem<IFightInputSystem>().MouseButtonRight();
                }
            }

            _hoverTime = 0f;
        }

        private void OnRightClickDragPerformed(InputAction.CallbackContext context)
        {
            _hoverTime = 0f;
            this.SendCommand(new MouseDragCommand(context.ReadValue<Vector2>()));
        }

        private void MouseScrollPerformed(InputAction.CallbackContext context)
        {
            _hoverTime = 0f;
            this.SendCommand(new MouseScrollCommand(context.ReadValue<Vector2>()));
        }

        private void Update()
        {
            if (Camera.main)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
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
                        if (_hoverTime >= Constants.HOVER_THRESHOLD)
                        {
                            Camera uiCam = GameObject.Find("UICamera").GetComponent<Camera>();
                            this.SendCommand(new TipsCommand(uiCam.ScreenToWorldPoint(mousePosition),
                                _currentHoverObject));
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

    /// <summary>
    /// 点击类型
    /// </summary>
    public enum MouseClickType
    {
        LEFT_BUTTON,
        MIDDLE_BUTTON,
        RIGHT_BUTTON
    }
}