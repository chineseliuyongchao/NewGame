using Fight.Game;
using UnityAttribute;
using UnityEngine;

namespace Fight.Tools
{
    public class MouseManager : MonoBehaviour
    {
        [Label("缩放速度")] [Range(1, 5)] public float zoomSpeed = 1f;
        [Label("拖动速度")] [Range(1, 10)] public float dragSpeed = 1f;

        private float _minZoom; // 最小缩放值
        private float _maxZoom; // 最大缩放值

        private void Awake()
        {
            if (Camera.main)
            {
                _minZoom = 1f;
                _maxZoom = Camera.main.orthographicSize;
            }
        }

        public void HandleMouseScroll(Vector2 offset)
        {
            var main = Camera.main;
            //将offset归一化处理
            offset.Normalize();
            if (main)
            {
                if (offset.y != 0)
                {
                    var orthographicSize = main.orthographicSize;
                    orthographicSize -= offset.y * zoomSpeed;
                    //缩进与拉伸镜头时避免穿帮，处理方式为拉伸的时候镜头同时往中间移动
                    main.orthographicSize = Mathf.Clamp(orthographicSize, _minZoom, _maxZoom);
                    //计算穿帮了多少
                    var camWidthHalf = main.orthographicSize * main.aspect;

                    var localPosition = main.transform.localPosition;

                    var a = Mathf.Abs(localPosition.x) + camWidthHalf - FightController.WorldWidth / 2;
                    var b = Mathf.Abs(localPosition.y) + main.orthographicSize - FightController.WorldHeight / 2;
                    if (a > 0)
                    {
                        localPosition.x += localPosition.x < 0 ? a : -a;
                    }

                    if (b > 0)
                    {
                        localPosition.y += localPosition.y < 0 ? b : -b;
                    }

                    main.transform.localPosition = localPosition;
                }
            }
        }

        public void HandleMouseDrag(Vector2 offset)
        {
            // Debug.Log(offset.x + "    " + offset.y + "         zzzzzzzzzzzzzzzzz");
            if (Camera.main != null)
            {
                var deltaX = -offset.x * dragSpeed * Time.deltaTime;
                var deltaY = -offset.y * dragSpeed * Time.deltaTime;
                var newPosition = Camera.main.transform.localPosition + new Vector3(deltaX, deltaY, 0);
                newPosition = ClampCameraLocalPosition(newPosition);
                Camera.main.transform.localPosition = newPosition;
            }
        }

        private Vector3 ClampCameraLocalPosition(Vector3 targetPosition)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                var camHeightHalf = cam.orthographicSize;
                var camWidthHalf = camHeightHalf * cam.aspect;

                var minX = -FightController.WorldWidth / 2 + camWidthHalf;
                var maxX = FightController.WorldWidth / 2 - camWidthHalf;
                var minY = -FightController.WorldHeight / 2 + camHeightHalf;
                var maxY = FightController.WorldHeight / 2 - camHeightHalf;

                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }

            return targetPosition;
        }
    }
}