using Fight.Command;
using Fight.Utils;
using Game.GameBase;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Fight.Tools.Tips
{
    public class TipsMarkUI : TipsMark, IPointerEnterHandler, IPointerExitHandler, ICanSendCommand
    {
        private bool _isHovering;
        private float _hoverTime;

        void Update()
        {
            if (_isHovering)
            {
                if (!showTips)
                {
                    _hoverTime += Time.deltaTime;
                    if (_hoverTime >= Constants.HoverThreshold)
                    {
                        Camera cam = GameObject.Find("UICamera").GetComponent<Camera>();
                        if (cam)
                        {
                            this.SendCommand(new TipsCommand(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                                this));
                        }

                        _hoverTime = 0f;
                    }
                }
                else
                {
                    _hoverTime = 0f;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovering = true;
            _hoverTime = 0f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovering = false;
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}