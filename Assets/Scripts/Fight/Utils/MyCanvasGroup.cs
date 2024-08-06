using DG.Tweening;
using UnityEngine;

namespace Fight.Utils
{
    public class MyCanvasGroup : MonoBehaviour
    {
        [Range(0f, 1f)] public float alpha = 1f;

        private Renderer[] _renderers;

        void Start()
        {
            _renderers = GetComponentsInChildren<Renderer>();
        }

        void Update()
        {
            SetAlpha(alpha);
        }

        private void SetAlpha(float newAlpha)
        {
            alpha = Mathf.Clamp01(newAlpha);
            foreach (Renderer rend in _renderers)
            {
                var material = rend.material;
                if (material.HasProperty("_Color"))
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                }
            }
        }

        public Tween DoAlpha(float endValue, float duration)
        {
            return DOTween.To(() => alpha, value =>
            {
                alpha = value;
                SetAlpha(alpha);
            }, endValue, duration);
        }
    }
}