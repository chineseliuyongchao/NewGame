using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

namespace UI.Base
{
    /// <summary>
    /// 用来检测此次点击事件是否被ui元素消费处理
    /// </summary>
    public static class UIClickInterceptor
    {
        private static PointerEventData _eventData;
        private static readonly List<RaycastResult> _results = new List<RaycastResult>();
        private static GraphicRaycaster _cachedRaycaster;

        public static bool IsUIBlockingClick(Vector2 screenPosition)
        {
            EnsureRaycaster();

            if (_cachedRaycaster == null || EventSystem.current == null)
                return false;

            _eventData ??= new PointerEventData(EventSystem.current);
            _eventData.position = screenPosition;

            _results.Clear();
            _cachedRaycaster.Raycast(_eventData, _results);

            foreach (var result in _results)
            {
                var go = result.gameObject;

                if (!HasRaycastTarget(go)) continue;

                if (go.TryGetComponent<IPointerClickHandler>(out _)) return true;
                if (go.TryGetComponent<Button>(out _)) return true;
                if (go.TryGetComponent<Toggle>(out _)) return true;
                if (go.TryGetComponent<Slider>(out _)) return true;
                if (go.TryGetComponent<InputField>(out _)) return true;
#if TMP_PRESENT
            if (go.TryGetComponent<TMPro.TMP_InputField>(out _)) return true;
#endif
            }

            return false;
        }

        private static void EnsureRaycaster()
        {
            if (_cachedRaycaster != null) return;
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                _cachedRaycaster = canvas.GetComponent<GraphicRaycaster>();
            }
        }

        private static bool HasRaycastTarget(GameObject go)
        {
            var graphic = go.GetComponent<Graphic>();
            if (graphic != null) return graphic.raycastTarget;

#if TMP_PRESENT
        var tmp = go.GetComponent<TMPro.TMP_Text>();
        if (tmp != null) return tmp.raycastTarget;
#endif

            return false;
        }
    }
}