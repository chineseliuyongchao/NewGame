using Game.GameBase;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChangeBgSprite : BaseGameController
    {
        public Image bg;

        protected override void OnInit()
        {
            base.OnInit();
            int bgIndex = Random.Range(0, UiBg.BG_NUM);
            Sprite bgSprite = resLoader.LoadSync<Sprite>("bg" + bgIndex);
            bg.sprite = bgSprite;
        }
    }
}