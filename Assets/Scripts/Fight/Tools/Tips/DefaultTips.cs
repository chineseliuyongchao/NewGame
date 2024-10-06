using UnityEngine;
using UnityEngine.UI;

namespace Fight.Tools.Tips
{
    //todo 后面需要重写
    [ExecuteInEditMode]
    public class DefaultTips : ObjectTips
    {
        public Image bg;
        public Text text1;
        public Text text2;
        private static readonly int ImageColor = Shader.PropertyToID("_ImageColor");
        private static readonly int ContourNum = Shader.PropertyToID("_ContourNum");
        
        private static readonly int Speed = Shader.PropertyToID("_Speed");
        private bool _initBgMaterial;

        public override void OnInit<T>(T value)
        {
            text1.text = tipsMark.infos[0];
            text2.text = tipsMark.infos[1];
        }

        public override void Layout(Vector3 localPosition)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                TextHorizontalLayout(text1, text2) + 40f);
            var rect = rectTransform.rect;
            if (!_initBgMaterial)
            {
                _initBgMaterial = true;
                bg.material = Instantiate(bg.material);
                bg.material.SetFloat(ContourNum, 0.015f);
                bg.material.SetFloat(Speed, 0.5f);
            }
            base.Layout(localPosition);
        }
        
        public override void Show()
        {
            base.Show();
            Color color = bg.color;
            color.a = canvasGroup.alpha;
            bg.material.SetColor(ImageColor, color);
        }
        
        private void Update()
        {
            Color color = bg.color;
            color.a = canvasGroup.alpha;
#if UNITY_EDITOR
            if (!_initBgMaterial)
            {
                _initBgMaterial = true;
                bg.material = Instantiate(bg.material);
            }
#endif
            bg.material.SetColor(ImageColor, color);
        }
    }
}