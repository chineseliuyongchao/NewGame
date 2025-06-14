using System.Linq;
using Battle.BattleBase;
using Battle.Town;
using Game.Config;
using Game.GameBase;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Map
{
    /// <summary>
    /// 地图控制器，用于管理所有与地图有关的内容
    /// </summary>
    public class MapController : BaseGameController
    {
        private GameObject _mapNodePrefab;
        private GameObject _townNodePrefab;
        
        private Camera _camera;
        protected override void OnInit()
        {
            _mapNodePrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.MAP_NODE);
            _townNodePrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.TONN_NODE);
            Instantiate(_mapNodePrefab, transform);
            Instantiate(_townNodePrefab, transform);
            InitConfig();
        }
        
        private void Update()
        {
            // 检测鼠标左键点击
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseLeftButtonClick();
            }
            
            // 检测鼠标滚轮
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (wheel != 0)
            {
                OnMouseWheelRolling(wheel);
            }
        }

        #region MyRegion
        protected void InitConfig()
        {
            _camera = Camera.allCameras.FirstOrDefault(c => c.name == "Main Camera");
        }

        protected void OnMouseLeftButtonClick()
        {
            // 检测是否点击在UI上
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 点击在UI上，不执行地图点击逻辑
                return;
            }
            Debug.Log("Click on Map");
            // 获取鼠标点击的屏幕坐标
            Vector3 mousePosition = Input.mousePosition;
            
            if (_camera != null)
            {
                Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
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

                this.SendCommand(baseTown != null
                    ? new SelectMapLocationCommand(worldPosition, baseTown.TownId)
                    : new SelectMapLocationCommand(worldPosition, 0));
            }
        }

        protected void OnMouseWheelRolling(float wheelValue)
        {
            // 调用主摄像机的缩放
            _camera.orthographicSize += wheelValue * InputConfig.CameraSizeSpeed;
        }
        #endregion
    }
}