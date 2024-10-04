using Fight.Tools;
using QFramework;
using UnityEngine;

namespace Fight.Command
{
    public class MouseDragCommand : AbstractCommand
    {
        private readonly Vector2 _offset;

        public MouseDragCommand(Vector2 offset)
        {
            _offset = offset;
        }

        protected override void OnExecute()
        {
            var cam = Camera.main;
            if (cam)
            {
                MouseManager mouseManager = cam.GetComponent<MouseManager>();
                if (mouseManager)
                {
                    mouseManager.HandleMouseDrag(_offset);
                }
            }
        }
    }

    public class MouseScrollCommand : AbstractCommand
    {
        private readonly Vector2 _offset;

        public MouseScrollCommand(Vector2 offset)
        {
            _offset = offset;
        }

        protected override void OnExecute()
        {
            var cam = Camera.main;
            if (cam)
            {
                MouseManager mouseManager = cam.GetComponent<MouseManager>();
                if (mouseManager)
                {
                    mouseManager.HandleMouseScroll(_offset);
                }
            }
        }
    }
}