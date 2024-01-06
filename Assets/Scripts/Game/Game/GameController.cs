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
            // 将寻路网格显示出来
            PathfindingMap map = this.GetModel<IMapModel>().Map;
            int num = 0;
            // map.MapData.ForEach((i, j, value) =>
            // {
            //     if (map.CheckPass(value) && value.pos.x == i && value.pos.y == j)
            //     {
            //         GameObject mesh = Instantiate(meshPrefab, transform);
            //         mesh.transform.localScale = new Vector3(value.size.x, value.size.y);
            //         // ReSharper disable once PossibleLossOfFraction
            //         mesh.transform.position = new Vector3(
            //             (i * MapConstant.GRID_SIZE - 960 + value.size.x * MapConstant.GRID_SIZE / 2) /
            //             (float)MapConstant.MAP_PIXELS_PER_UNIT,
            //             // ReSharper disable once PossibleLossOfFraction
            //             (j * MapConstant.GRID_SIZE - 540 + value.size.y * MapConstant.GRID_SIZE / 2) /
            //             (float)MapConstant.MAP_PIXELS_PER_UNIT);
            //         num++;
            //     }
            // });
            Debug.Log("网格数量：" + num);
        }
    }
}