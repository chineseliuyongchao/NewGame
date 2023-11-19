using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;
using Random = System.Random;

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
        private int _number = 10;
        private RectTransform _parentNode;
        private float _imageSize = 50f;

        private IntVector2 _startPos;
        private IntVector2 _endPos;

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
            PathfindingMap map = new PathfindingMap(_number, _number);
            _mapLattice = new Array2Utils<MapLattice>(_number, _number);
            Random random = new Random();
            for (int i = 0; i < _number * _number; i++)
            {
                GameObject imageGo = Instantiate(mapLatticePrefab, _parentNode);
                RectTransform rectTransform = imageGo.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(_imageSize, _imageSize);
                MapLattice mapLattice = imageGo.GetComponent<MapLattice>();
                mapLattice.Pos = new IntVector2(i % _number, i / _number);
                bool canPass = random.Next(10) > 0;
                if (!canPass)
                {
                    mapLattice.SetCannotPass();
                }

                mapLattice.Operate = pos =>
                {
                    if (_startPos == null)
                    {
                        _startPos = pos;
                    }
                    else
                    {
                        _endPos = pos;
                        PathfindingSingleMessage message =
                            PathfindingController.Singleton.Pathfinding(_startPos, _endPos, map);
                        for (int j = 0; j < message.PathfindingResult.Count; j++)
                        {
                            _mapLattice[message.PathfindingResult[j].Pos.X, message.PathfindingResult[j].Pos.Y]
                                .ShowRoute();
                        }
                    }
                };
                float row = i / _number;
                float col = i % _number;
                rectTransform.anchoredPosition =
                    new Vector2((col - _number / 2) * _imageSize, (row - _number / 2) * _imageSize);
                _mapLattice[i % _number, i / _number] = mapLattice;

                PathfindingMapNode pathfindingMapNode = new PathfindingMapNode();
                pathfindingMapNode.Pos = new IntVector2(i % _number, i / _number);
                pathfindingMapNode.TerrainType = canPass ? TerrainType.CAN_PASS : TerrainType.CANNOT_PASS;
                map.MapData[i % _number, i / _number] = pathfindingMapNode;
            }
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