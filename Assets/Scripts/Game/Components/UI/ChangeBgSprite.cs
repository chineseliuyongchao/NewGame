using Game.GameBase;
using QFramework;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class ChangeBgSprite : BaseGameController
    {
        public Image bg;
        public bool isPure;

        protected override void OnInit()
        {
            base.OnInit();
            if (isPure)
            {
                int bgIndex = Random.Range(0, UiBg.PURE_BG_NUM);
                Sprite bgSprite = resLoader.LoadSync<Sprite>("pureBg" + bgIndex);
                bg.sprite = bgSprite;
                bg.SetNativeSize();
            }
            else
            {
                int bgIndex = Random.Range(0, UiBg.BG_NUM);
                Sprite bgSprite = resLoader.LoadSync<Sprite>("bg" + bgIndex);
                bg.sprite = bgSprite;
                bg.SetNativeSize();
            }
        }
    }
}