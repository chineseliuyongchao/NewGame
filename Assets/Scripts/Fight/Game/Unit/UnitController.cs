using DG.Tweening;
using Fight.Model;
using Fight.System;
using Fight.Utils;
using Game.GameBase;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

namespace Fight.Game.Unit
{
    /// <summary>
    /// 单个兵种
    /// </summary>
    public class UnitController : BaseGameController
    {
        /// <summary>
        /// 兵种的相关数据信息
        /// </summary>
        public UnitData unitData;

        public ObjectUnitView view;
        private Tween _focusAction;

        public void Init()
        {
            view = this.AddComponent<ObjectUnitView>();
            view.OnInit(this.transform);
            if (unitData.legionId == Constants.PlayLegionId)
            {
            }
            else
            {
                var transform1 = transform;
                var transformRotation = transform1.rotation;
                transformRotation.eulerAngles = new Vector3(0, 180, 0);
                transform1.rotation = transformRotation;
            }

            ChangeOrderLayer(); //初始化的时候也应该排序

            //todo
            view.Find<TextMesh>(Constants.DebugText).text = unitData.armDataType.unitName;
        }

        /// <summary>
        /// 被选取为焦点兵种时的动作
        /// </summary>
        public virtual void StartFocusAction()
        {
            _focusAction?.Kill();
            _focusAction = transform.DOMoveY(this.GetModel<IAStarModel>().GetUnitRelayPosition(unitData).y + 0.5f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// 被取消焦点兵种后的清理函数
        /// </summary>
        public virtual void EndFocusAction()
        {
            _focusAction?.Kill();
            transform.DOMove(this.GetModel<IAStarModel>().GetUnitRelayPosition(unitData), 0.2f).SetEase(Ease.OutSine);
        }

        /// <summary>
        /// 兵种移动的动作，需要根据当前战斗的状态变更方式
        /// </summary>
        /// <param name="endIndex"></param>
        public virtual void UnitMoveAction(int endIndex)
        {
            switch (this.GetModel<IFightVisualModel>().FightType)
            {
                case FightType.WAR_PREPARATIONS:
                {
                    this.GetSystem<IFightSystem>().UnitChangePos(this, endIndex);
                    Vector3 endPosition =
                        (Vector3)this.GetModel<IAStarModel>().FightGridNodeInfoList[endIndex].position;
                    transform.position = endPosition;
                    ChangeOrderLayer();
                }
                    break;
                case FightType.IN_FIGHT:
                {
                    this.GetModel<IAStarModel>().FindNodePath(unitData.currentPosIndex, endIndex, path =>
                    {
                        if (path.error)
                        {
                            Debug.LogError("Pathfinding error: " + path.errorLog);
                            return;
                        }

                        Sequence sequence = DOTween.Sequence();
                        EndFocusAction();
                        sequence.AppendInterval(0.3f);
                        this.GetModel<IFightVisualModel>().PlayerMoving = true;
                        for (int i = 1; i < path.vectorPath.Count; i++)
                        {
                            sequence.AppendCallback(() =>
                            {
                                if (!this.GetSystem<IFightComputeSystem>().MoveOnce(unitData.unitId))
                                {
                                    sequence.Kill();
                                    this.GetModel<IFightVisualModel>().PlayerMoving = false;
                                }
                            });
                            sequence.Append(transform.DOMove(path.vectorPath[i], 0.5f));
                            var i1 = i;
                            sequence.AppendCallback(() =>
                            {
                                int index = this.GetModel<IAStarModel>().GetGridNodeIndexMyRule(path.vectorPath[i1]);
                                this.GetSystem<IFightSystem>().UnitChangePos(this, index);
                                ChangeOrderLayer();
                            });
                        }

                        sequence.AppendCallback(() =>
                        {
                            StartFocusAction();
                            this.GetModel<IFightVisualModel>().PlayerMoving = false;
                        });
                    });
                }
                    break;
                case FightType.SETTLEMENT:
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
            int beginIndex = Mathf.Max(Constants.FightNodeVisibleHeightNum -
                                       unitData.currentPosIndex / Constants.FightNodeVisibleWidthNum, 1) * 1000;
            view.ChangeOrderLayer(beginIndex);
        }
    }
}