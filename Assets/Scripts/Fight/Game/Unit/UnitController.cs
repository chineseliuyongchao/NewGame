using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Fight.Controller;
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
    /// 每个单位，只负责单位的实际行动表现，不参与玩家或者电脑的命令决策
    /// </summary>
    public class UnitController : BaseGameController
    {
        /// <summary>
        /// 兵种的相关数据信息
        /// </summary>
        public UnitData unitData;

        public ObjectUnitView view;

        public UnitProgressBarController unitProgressBar;

        private Tween _focusAction;

        /// <summary>
        /// 单位每次行动结束的回调
        /// </summary>
        private Action _actionEnd;

        public void Init()
        {
            view = this.AddComponent<ObjectUnitView>();
            view.OnInit(this.transform);

            unitProgressBar = transform.Find("unitProgressBar").GetComponent<UnitProgressBarController>();
            unitProgressBar.OnInit(unitData);
            if (unitData.legionId == Constants.PLAY_LEGION_ID)
            {
            }
            else
            {
                var transform1 = transform;
                transform1.rotation = Quaternion.Euler(0, 180, 0);

                unitProgressBar.transform.rotation = Quaternion.Euler(0, -360, 0);
            }

            ChangeOrderLayer(); //初始化的时候也应该排序
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
        /// 改变位置
        /// </summary>
        /// <param name="endIndex"></param>
        public void ChangePos(int endIndex)
        {
            this.GetSystem<IFightSystem>().UnitChangePos(this, endIndex);
            Vector3 endPosition =
                (Vector3)this.GetModel<IAStarModel>().FightGridNodeInfoList[endIndex].position;
            transform.position = endPosition;
            ChangeOrderLayer();
            if (_focusAction != null)
            {
                StartFocusAction();
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="endIndex">终点的位置id</param>
        /// <param name="actionEnd"></param>
        /// <param name="moveOnceEnd">每移动完一步以后调用，传入移动后的位置，返回是否可以继续移动（true->可以继续移动）</param>
        public Task Move(int endIndex, Action actionEnd, Func<int, Task<bool>> moveOnceEnd)
        {
            _actionEnd = actionEnd;
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
                this.GetModel<IFightVisualModel>().PlayerInAction = true;
                for (int i = 1; i < path.vectorPath.Count; i++)
                {
                    sequence.AppendCallback(() =>
                    {
                        if (!this.GetSystem<IFightComputeSystem>().MoveOnce(unitData.unitId))
                        {
                            sequence.Kill();
                            MoveEnd();
                        }
                        else
                        {
                            unitProgressBar.UpdateProgress();
                        }
                    });
                    sequence.Append(transform.DOMove(path.vectorPath[i], 0.5f));
                    var i1 = i;

                    async void callback()
                    {
                        int index = this.GetModel<IAStarModel>().GetGridNodeIndexMyRule(path.vectorPath[i1]);
                        this.GetSystem<IFightSystem>().UnitChangePos(this, index);
                        ChangeOrderLayer();
                        if (moveOnceEnd != null)
                        {
                            if (!await moveOnceEnd(index))
                            {
                                sequence.Kill();
                                MoveEnd();
                            }
                        }
                    }

                    sequence.AppendCallback(callback);
                }

                sequence.AppendCallback(MoveEnd);
            });
            return Task.CompletedTask;
        }

        /// <summary>
        /// 单位攻击（此处的攻击是泛指所有的攻击行为）
        /// </summary>
        public void Attack()
        {
            unitProgressBar.UpdateProgress();
        }

        /// <summary>
        /// 单位被攻击（此处的攻击是泛指所有的攻击行为）
        /// </summary>
        public void Attacked()
        {
            unitProgressBar.UpdateProgress();
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public void UpdateType()
        {
            UnitType oldType = unitData.UnitType;
            this.GetSystem<IFightComputeSystem>().UpdateUnitType(unitData.unitId);
            UnitType newType = unitData.UnitType;
            switch (oldType)
            {
                case UnitType.NORMAL:
                {
                    switch (newType)
                    {
                        case UnitType.COLLAPSE:
                            List<UnitController> nearUnit = this.GetSystem<IFightSystem>().GetUnitsNearUnit(this);
                            for (int i = 0; i < nearUnit.Count; i++)
                            {
                                nearUnit[i].NearUnitCollapse(unitData);
                            }

                            break;
                    }
                }
                    break;
            }
        }

        /// <summary>
        /// 移动结束
        /// </summary>
        private void MoveEnd()
        {
            StartFocusAction();
            this.GetModel<IFightVisualModel>().PlayerInAction = false;
            if (_actionEnd != null)
            {
                _actionEnd();
                _actionEnd = null;
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
            unitProgressBar.ChangeOrderLayer(beginIndex);
        }

        /// <summary>
        /// 周围单位崩溃
        /// </summary>
        /// <param name="data"></param>
        private void NearUnitCollapse(UnitData data)
        {
            this.GetSystem<IFightComputeSystem>().AroundUnitCollapseChangeMorale(unitData,
                this.GetSystem<IFightSystem>().GetCampIdOfUnit(unitData.unitId) ==
                this.GetSystem<IFightSystem>().GetCampIdOfUnit(data.unitId));
            UpdateType();
        }
    }
}