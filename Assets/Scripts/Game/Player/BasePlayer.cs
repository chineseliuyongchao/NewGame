using DG.Tweening;
using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;

namespace Game.Player
{
    /// <summary>
    /// 所有游戏角色的基类
    /// </summary>
    public class BasePlayer : BaseGameController
    {
        private Sequence _sequence;

        /// <summary>
        /// 从一个位置移动到下一个位置
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        protected void Move(IntVector2 startPos, IntVector2 endPos)
        {
            if (_sequence == null || !_sequence.active)
            {
                _sequence = DOTween.Sequence();

                PlayerMove(startPos, endPos);
            }
            else if (!_sequence.IsPlaying())
            {
                _sequence = DOTween.Sequence();
                PlayerMove(startPos, endPos);
            }
            else
            {
                _sequence.OnKill(() =>
                {
                    _sequence = DOTween.Sequence();
                    PlayerMove(startPos, endPos);
                });
                _sequence.Kill();
            }
        }

        private void PlayerMove(IntVector2 startPos, IntVector2 endPos)
        {
            PathfindingSingleMessage message =
                PathfindingController.Singleton.Pathfinding(startPos, endPos, this.GetModel<IMapModel>().Map);
            if (message != null)
            {
                for (int i = 0; i < message.PathfindingResult.Count; i++)
                {
                    Vector3 pos = this.GetSystem<IMapSystem>().GetMapToRealPos(transform.parent,
                        this.GetSystem<IMapSystem>().GetGridToMapPos(message.PathfindingResult[i].Pos));
                    _sequence.Append(transform.DOMove(pos, 0.1f * message.Length[i]).SetEase(Ease.Linear));
                }
            }
        }

        /// <summary>
        /// 获取起点网格位置
        /// </summary>
        /// <returns></returns>
        protected IntVector2 GetStartPos()
        {
            return this.GetSystem<IMapSystem>()
                .GetGridMapPos(this.GetSystem<IMapSystem>().GetMapPos(transform));
        }
    }
}