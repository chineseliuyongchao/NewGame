using Game.Town;
using GameQFramework;
using QFramework;
using SystemTool.MapProcessing;
using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// 地图控制器，用于管理所有与地图有关的内容
    /// </summary>
    public class MapController : BaseGameController
    {
        public MapNode mapNode;
        public TownNode townNode;

        protected override void OnInit()
        {
            Instantiate(mapNode, transform);
            this.GetModel<IMapModel>().Map =
                ImageToMapController.Singleton.GetMap(MapConstant.MAP_PATH + MapConstant.GRID_MAP_FILE_NAME);
            Instantiate(townNode, transform);
        }

        private void Update()
        {
            // 检测鼠标左键点击
            if (Input.GetMouseButtonDown(0))
            {
                // 获取鼠标点击的屏幕坐标
                Vector3 mousePosition = Input.mousePosition;
                // 使用Camera.main.ScreenToWorldPoint将屏幕坐标转换为世界坐标
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
                BaseTown baseTown = null;
                // 判断是否点击到了游戏物体
                if (hit.collider != null)
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if (clickedObject.CompareTag("Town"))
                    {
                        baseTown = clickedObject.GetComponent<BaseTown>();
                    }
                }

                this.SendCommand(new SelectMapLocationCommand(worldPosition, baseTown));
            }
        }
    }
}