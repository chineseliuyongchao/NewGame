using QFramework;
using SystemTool.MapProcessing;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;

namespace UI
{
    public class NodeData : UIPanelData
    {
    }

    /// <summary>
    /// 用于测试寻路算法的控制器
    /// </summary>
    public partial class Node : UIPanel
    {
        public GameObject mapLatticePrefab;
        private RectTransform _parentNode;
        private float _imageSize = 25f;

        private IntVector2 _startPos = new(-1, -1);
        private IntVector2 _endPos = new(-1, -1);

        private Array2Utils<MapLattice> _mapLattice;

        private void Awake()
        {
            OnInit();
        }

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as NodeData ?? new NodeData();
            // please add init code here
            _parentNode = this.GetComponent<RectTransform>();
            PathfindingMap map = ImageToMapController.Singleton.GetMap("Assets/Res/Image/map/grid_image.jpg");
            int width = map.MapSize().X;
            int height = map.MapSize().Y;
            _mapLattice = new Array2Utils<MapLattice>(map.MapSize().X, map.MapSize().Y);
            for (int i = 0; i < width * height; i++)
            {
                GameObject imageGo = Instantiate(mapLatticePrefab, _parentNode);
                MapLattice mapLattice = imageGo.GetComponent<MapLattice>();
                mapLattice.pos = new IntVector2(i % width, i / width);
                bool canPass = map.MapData[i % width, i / width].terrainType == TerrainType.CAN_PASS;
                if (!canPass)
                {
                    mapLattice.SetCannotPass();
                }

                mapLattice.operate = pos =>
                {
                    if (_startPos == new IntVector2(-1, -1))
                    {
                        _startPos = pos;
                    }
                    else
                    {
                        _endPos = pos;

                        PathfindingSingleMessage message =
                            PathfindingController.Singleton.Pathfinding(_startPos, _endPos, map);
                        for (int j = 0; j < message.pathfindingResult.Count; j++)
                        {
                            _mapLattice[message.pathfindingResult[j].pos.X, message.pathfindingResult[j].pos.Y]
                                .ShowRoute1(true);
                        }
                    }
                };
                int row = i / width;
                int col = i % width;
                RectTransform rectTransform = imageGo.GetComponent<RectTransform>();
                rectTransform.anchoredPosition =
                    new Vector2((col - width / 2) * _imageSize, (row - height / 2) * _imageSize);
                Vector2 size = rectTransform.sizeDelta;
                rectTransform.localScale = new Vector3(_imageSize / size.x, _imageSize / size.x, _imageSize / size.x);
                _mapLattice[col, row] = mapLattice;
            }

            Button.onClick.AddListener(() =>
            {
                _startPos = new(-1, -1);
                _endPos = new(-1, -1);
                _mapLattice.ForEach((_, _, value) =>
                {
                    value.ShowRoute(false);
                    value.ShowRoute1(false);
                });
            });
        }

        protected override void OnOpen(IUIData uiData = null)
        {
        }

        protected override void OnShow()
        {
        }

        protected override void OnHide()
        {
        }

        protected override void OnClose()
        {
        }
    }
}