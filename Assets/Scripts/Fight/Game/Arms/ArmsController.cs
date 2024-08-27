using DG.Tweening;
using Fight.Enum;
using Fight.Model;
using Fight.Scenes;
using Fight.Utils;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Fight.Game.Arms
{
    public class ArmsController : MonoBehaviour, IController
    {
        /// <summary>
        /// 兵种的相关数据信息
        /// </summary>
        public ArmData armData;

        public ObjectArmsView view;

        private Tween _focusAction;
        
        public void OnInit()
        {
            //todo
            view.Find<TextMesh>(Constants.DebugText).text = armData.armDataType.unitName;
        }

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
        /// 兵种每次移动位置都会更新精灵的排序
        /// 排序规则如下：从上到下layer以此增加，将其乘上一个常数，然后依次递加赋值给sprite render
        /// </summary>
        private void ChangeOrderLayer()
        {
            int beginIndex =
                Mathf.Max(Constants.FightNodeVisibleHeightNum - armData.currentPosition / Constants.FightNodeVisibleWidthNum,
                    1) * 1000;
            view.ChangeOrderLayer(beginIndex);
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}