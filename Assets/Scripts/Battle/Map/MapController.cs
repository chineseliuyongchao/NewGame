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
        
        /// <summary>
        /// 描述当前摄像机的视角缩放
        /// </summary>
        public float CurCameraSize
        {
            get
            {
                return _curCameraSize;
            }
            set
            {
                // 在这里处理视角缩放的变化逻辑
                float result = Mathf.Clamp(value, _minCameraSize, 100);
                // 根据旧的数据和新的数据响应是否进入了对应等级的缩放
                OnCameraResize(_curCameraSize, result);
                _curCameraSize = result;
                _camera.orthographicSize = result;
            }
        }

        private readonly float _minCameraSize = 3;
        private float _curCameraSize = 0;
        // 州级视角界限值，超出为州级视角
        private float _inProvinceView = 0;
        // 郡级视角界限值，超出为郡级视角
        private float _inPrefectureView = 0;
        // 县级视角界限值，超出为县级视角
        private float _inCountyView = 0;
        protected override void OnInit()
        {
            _mapNodePrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.MAP_NODE);
            _townNodePrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.TONN_NODE);
            _curCameraSize = _camera.orthographicSize;
            
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
            // 没有滚动数值就不必响应了
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
        /// <summary>
        /// 当鼠标滚轮相应的时候
        /// </summary>
        /// <param name="wheelValue"></param>
        protected void OnMouseWheelRolling(float wheelValue)
        {
            // 调用主摄像机的缩放
            CurCameraSize += wheelValue * InputConfig.CameraSizeSpeed;
        }
        
        /// <summary>
        /// 响应摄像机的视角缩放变化
        /// </summary>
        /// <param name="lastSize">上一帧的缩放</param>
        /// <param name="nowSize">当前帧将要执行的缩放</param>
        protected void OnCameraResize(float lastSize,float nowSize)
        {
            int lastState = GetSizeState(lastSize);
            int nowState = GetSizeState(nowSize);
            // 两者不同说明前后两帧视角等级发生了变化
            if (lastState != nowState)
            {
                // 以现在的视角等级为基准
                switch (nowState)
                {
                    // 县级视角地图显示
                    case 1: EnterCountyView();
                    // 郡级视角地图显示
                        break;
                    case 2: EnterPrefectureView();
                        break;
                    // 省级视角地图显示
                    case 3: EnterProvinceView();
                        break;
                }
            }
        }
        
        /// <summary>
        /// 根据当前缩放值获取视角缩放等级
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        protected int GetSizeState(float size)
        {
            if ( size < _inPrefectureView)
            {
                return 1;
            }

            if (size < _inPrefectureView && size > _inProvinceView)
            {
                return 2;
            }

            if (size > _inProvinceView)
            {
                return 3;
            }

            return 0;
        }
        
        /// <summary>
        /// 进入县级视角地图显示
        /// </summary>
        protected void EnterCountyView()
        {
            // todo 
        }
        
        /// <summary>
        /// 进入郡级视角地图显示
        /// </summary>
        protected void EnterPrefectureView()
        {
            // todo
        }
        /// <summary>
        /// 进入省级视角地图显示
        /// </summary>
        protected void EnterProvinceView()
        {
            // todo
        }
        #endregion
    }
}