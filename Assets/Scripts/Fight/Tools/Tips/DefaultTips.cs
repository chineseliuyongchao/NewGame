using UnityEngine;
using UnityEngine.UI;

namespace Fight.Tools.Tips
{
    //todo 后面需要重写
    public class DefaultTips : ObjectTips
    {
        public Image bg;
        public Text text1;
        public Text text2;
        private static readonly int Scale = Shader.PropertyToID("_Scale");
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
                TextHorizontalLayout(text1, text2) + 20f);
            var rect = rectTransform.rect;
            if (!_initBgMaterial)
            {
                _initBgMaterial = true;
                bg.material = Instantiate(bg.material);
            }
            bg.material.SetVector(Scale, new Vector4(1f, rect.height / rect.width));
            base.Layout(localPosition);
        }
    }
}