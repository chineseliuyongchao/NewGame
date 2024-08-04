using Fight.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fight.Tools
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MouseManager : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler
    {
        private bool _dragging;

        private void Awake()
        {
            var boxCollider2D = GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(Constants.WorldWidth, Constants.WorldHeight);
        }

        private void Update()
        {
            var scrollData = Input.GetAxis("Mouse ScrollWheel");
            var main = Camera.main;
            if (main)
                if (scrollData != 0)
                {
                    var orthographicSize = main.orthographicSize;
                    orthographicSize -= scrollData * Constants.ZoomSpeed;
                    //缩进与拉伸镜头时避免穿帮，处理方式为拉伸的时候镜头同时往中间移动
                    main.orthographicSize = Mathf.Clamp(orthographicSize, Constants.MinZoom, Constants.MaxZoom);
                    //计算穿帮了多少
                    var camWidth = main.orthographicSize * main.aspect;
                    var localPosition = main.transform.localPosition;
                    var a = Mathf.Abs(localPosition.x) + camWidth - Constants.WorldWidth / 2;
                    var b = Mathf.Abs(localPosition.y) + main.orthographicSize - Constants.WorldHeight / 2;
                    if (a > 0) localPosition.x += localPosition.x < 0 ? a : -a;

                    if (b > 0) localPosition.y += localPosition.y < 0 ? b : -b;

                    main.transform.localPosition = localPosition;
                }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Camera.main != null)
            {
                var deltaX = -eventData.delta.x * Constants.DragSpeed * Time.deltaTime;
                var deltaY = -eventData.delta.y * Constants.DragSpeed * Time.deltaTime;
                var newPosition = Camera.main.transform.localPosition + new Vector3(deltaX, deltaY, 0);
                newPosition = ClampCameraLocalPosition(newPosition);
                Camera.main.transform.localPosition = newPosition;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_dragging) _dragging = false;
        }

        private Vector3 ClampCameraLocalPosition(Vector3 targetPosition)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                var camHeight = cam.orthographicSize;
                var camWidth = camHeight * cam.aspect;

                var minX = -Constants.WorldWidth / 2 + camWidth;
                var maxX = Constants.WorldWidth / 2 - camWidth;
                var minY = -Constants.WorldHeight / 2 + camHeight;
                var maxY = Constants.WorldHeight / 2 - camHeight;

                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }

            return targetPosition;
        }
    }
}