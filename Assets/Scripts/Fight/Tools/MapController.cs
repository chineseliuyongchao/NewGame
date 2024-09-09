using DG.Tweening;
using Fight.Utils;
using Game.GameBase;
using QFramework;
using UnityAttribute;
using UnityEngine;

namespace Fight.Tools
{
    /**
     * 根据a*的网格生成相应的地图
     */
    public class MapController : BaseGameController
    {
        [Label("原始图块")] [SerializeField] private GameObject piece;
        [Label("提示图块")] [SerializeField] private GameObject tips;

        private Transform _showTransform;
        private Transform _tipsTransform;

        private Tween _alphaAction;

        protected override void OnInit()
        {
            _showTransform = transform.Find("show");
            _tipsTransform = transform.Find("tips");

            var aStarModel = this.GetModel<IAStarModel>();
            var index = 1;
            foreach (var graphNode in aStarModel.FightGridNodeInfoList.Values)
            {
                var obj = Instantiate(piece, _showTransform);
                obj.transform.localPosition = (Vector3)graphNode.position;
                obj.name = index.ToString();

                var obj2 = Instantiate(tips, _tipsTransform);
                obj2.transform.localPosition = (Vector3)graphNode.position;
                obj2.name = (index++).ToString();
            }

            _tipsTransform.gameObject.SetActive(false);
            base.OnInit();
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<SelectArmsFocusEvent>(SelectArmsFocusEvent)
                .UnRegisterWhenGameObjectDestroyed(this);
        }

        private void SelectArmsFocusEvent(SelectArmsFocusEvent focusEvent)
        {
            switch (this.GetModel<IFightCoreModel>().FightType)
            {
                case FightType.WAR_PREPARATIONS:
                    if (_alphaAction is { active: true })
                    {
                        return;
                    }

                    _tipsTransform.gameObject.SetActive(true);
                    MyCanvasGroup tipsMyCanvasGroup = _tipsTransform.GetComponent<MyCanvasGroup>();
                    _alphaAction?.Kill();
                    tipsMyCanvasGroup.alpha = 1f;
                    _alphaAction = tipsMyCanvasGroup.DoAlpha(0.4f, 1f).SetLoops(-1, LoopType.Yoyo);
                    break;
            }
        }
    }
}