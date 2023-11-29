using DG.Tweening;
using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;

namespace Game.Player
{
    public class PlayerController : BaseGameController
    {
        public GameObject character;
        private GameObject _people;

        protected override void OnInit()
        {
            base.OnInit();
            _people = Instantiate(character, this.transform);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<SelectMapLocationEvent>(e =>
            {
                IntVector2 startPos = this.GetSystem<IMapSystem>()
                    .GetGridMapPos(this.GetSystem<IMapSystem>().GetMapPos(_people.transform));
                IntVector2 endPos = this.GetSystem<IMapSystem>().GetGridMapPos(e.SelectPos);
                PathfindingSingleMessage message =
                    PathfindingController.Singleton.Pathfinding(startPos, endPos, this.GetModel<IMapModel>().Map);
                Sequence sequence = DOTween.Sequence();
                for (int i = 0; i < message.PathfindingResult.Count; i++)
                {
                    Vector3 pos = this.GetSystem<IMapSystem>().GetMapToRealPos(_people.transform.parent,
                        this.GetSystem<IMapSystem>().GetGridToMapPos(message.PathfindingResult[i].Pos));
                    sequence.Append(_people.transform.DOMove(pos, 0.1f).SetEase(Ease.Linear));
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}