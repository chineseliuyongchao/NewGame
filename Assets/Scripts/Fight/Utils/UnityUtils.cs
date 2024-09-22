using UnityEngine;
using UnityEngine.UI;

namespace Fight.Utils
{
    /// <summary>
    /// unity的通用工具类
    /// </summary>
    public static class UnityUtils
    {
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
        }

        public static void SetAnchorPositionX(this RectTransform rectTransform, float x)
        {
            Vector3 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.x = x;
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public static void SetAnchorPositionY(this RectTransform rectTransform, float y)
        {
            Vector3 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.y = y;
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public static void LocalPositionMoveBy(this Transform transform, float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x += x;
            localPosition.y += x;
            localPosition.z += x;
            transform.localPosition = localPosition;
        }

        public static void AnchorPositionMoveBy(this RectTransform rectTransform, float x = 0f, float y = 0f,
            float z = 0f)
        {
            Vector3 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.x += x;
            anchoredPosition.y += x;
            anchoredPosition.z += x;
            rectTransform.anchoredPosition = anchoredPosition;
        }

        public static void PositionMoveBy(this Transform transform, float x = 0f, float y = 0f, float z = 0f)
        {
            Vector3 position = transform.position;
            position.x += x;
            position.y += x;
            position.z += x;
            transform.position = position;
        }
    }
}