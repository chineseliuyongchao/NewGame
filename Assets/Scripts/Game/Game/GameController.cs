using GameQFramework;
using QFramework;
using UI;
using UnityEngine;

namespace Game.Game
{
    public class GameController : BaseGameController
    {
        public GameObject meshPrefab;

        protected override void OnControllerStart()
        {
            base.OnControllerStart();
            UIKit.OpenPanel<UIGameLobby>();
            //将寻路网格显示出来
            // PathfindingMap map = this.GetModel<IMapModel>().Map;
            // int num = 0;
            // map.MapData.ForEach((i, j, value) =>
            // {
            //     if (map.CheckPass(value))
            //     {
            //         GameObject mseh = Instantiate(meshPrefab, transform);
            //         mseh.transform.localScale = new Vector3(value.size.X, value.size.Y);
            //         mseh.transform.position = new Vector3((i * 10 - 960 + value.size.X * 5) / 100f,
            //             (j * 10 - 540 + value.size.Y * 5) / 100f);
            //         num++;
            //     }
            // });
            // Debug.Log("网格数量：" + num);
        }
    }
}