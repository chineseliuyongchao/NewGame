using DG.Tweening;
using Fight.Enum;
using Fight.Scenes;
using Fight.Utils;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public abstract class ObjectArmsController : MonoBehaviour, IController
    {
        /// <summary>
        /// 兵种的专属id
        /// </summary>
        public int id;

        /// <summary>
        /// 战斗界面中该兵种所处位置
        /// </summary>
        public int fightCurrentIndex;

        /// <summary>
        /// 初始化方法，代替awake
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// 返回当前兵种的具体模型，可以强转
        /// </summary>
        /// <returns>自定义的兵种模型</returns>
        public abstract ObjectArmsModel GetModel();

        /// <summary>
        /// 返回当前兵种的具体视图，可以强转
        /// </summary>
        /// <returns>自定义的兵种视图</returns>
        public abstract ObjectArmsView GetView();

        private Tween _focusAction;

        /// <summary>
        /// 被选取为焦点兵种时的动作
        /// </summary>
        public virtual void StartFocusAction()
        {
            _focusAction?.Kill();
            _focusAction = transform.DOMoveY(this.GetArmsRelayPosition().y + 0.5f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// 被取消焦点兵种后的清理函数
        /// </summary>
        public virtual void EndFocusAction()
        {
            _focusAction?.Kill();
            transform.DOMove(this.GetArmsRelayPosition(), 0.2f).SetEase(Ease.OutSine);
        }

        /// <summary>
        /// 兵种移动的动作，需要根据当前战斗的状态变更方式
        /// </summary>
        /// <param name="endIndex"></param>
        public virtual void ArmsMoveAction(int endIndex)
        {
            switch (FightScene.Ins.currentBattleType)
            {
                case BattleType.StartWarPreparations:
                    Vector3 endPosition = (Vector3)this.GetModel<AStarModel>().FightGridNodeInfoList[endIndex].position;
                    transform.position = endPosition;
                    ChangeOrderLayer();
                    break;
                case BattleType.StartBattle:
                case BattleType.StartPursuit:

                    break;
            }

            if (_focusAction != null)
            {
                StartFocusAction();
            }
        }

        /// <summary>
        /// 兵种每次移动位置都会更新器精灵的排序
        /// 排序规则如下：从上到下layer以此增加，将其乘上一个常数，然后依次递加赋值给sprite render
        /// </summary>
        private void ChangeOrderLayer()
        {
            int beginIndex =
                Mathf.Max(Constants.FightNodeVisibleHeightNum - fightCurrentIndex / Constants.FightNodeVisibleWidthNum,
                    1) * 1000;
            GetView().ChangeOrderLayer(beginIndex);
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}