using GameQFramework;
using QFramework;
using SystemTool.MapProcessing;
using UnityEngine;

namespace Game.Map
{
    public class MapController : BaseGameController
    {
        public MapNode mapNode;

        protected override void OnInit()
        {
            Instantiate(mapNode, transform);
            this.GetModel<IMapModel>().Map =
                ImageToMapController.Singleton.GetMap(MapConstant.MAP_PATH + MapConstant.GRID_MAP_FILE_NAME);
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
                this.SendCommand(new SelectMapLocationCommand(worldPosition));
            }
        }
    }
}