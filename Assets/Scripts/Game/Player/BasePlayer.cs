using DG.Tweening;
using Game.Town;
using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;
using Utils.Constant;

namespace Game.Player
{
    /// <summary>
    /// 所有游戏角色的基类
    /// </summary>
    public class BasePlayer : BaseGameController
    {
        private Sequence _sequence;

        /// <summary>
        /// 设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="callBack"></param>
        protected void Move(IntVector2 startPos, IntVector2 endPos, MoveCloseBack callBack)
        {
            if (_sequence == null || !_sequence.active)
            {
                _sequence = DOTween.Sequence();

                PlayerMove(startPos, endPos, callBack);
            }
            else if (!_sequence.IsPlaying())
            {
                _sequence = DOTween.Sequence();
                PlayerMove(startPos, endPos, callBack);
            }
            else
            {
                _sequence.OnKill(() =>
                {
                    _sequence = DOTween.Sequence();
                    PlayerMove(startPos, endPos, callBack);
                });
                _sequence.Kill();
            }
        }

        /// <summary>
        /// 使用doTween设置游戏角色的移动路径
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="callBack"></param>
        private void PlayerMove(IntVector2 startPos, IntVector2 endPos, MoveCloseBack callBack)
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

                if (callBack != null)
                {
                    _sequence.AppendCallback(() => { callBack(); });
                }
            }
        }

        /// <summary>
        /// 游戏角色移动到某个聚落
        /// </summary>
        /// <param name="baseTown"></param>
        protected void MoveToTown(BaseTown baseTown)
        {
            Debug.Log("moveToTown:  " + baseTown.name);
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