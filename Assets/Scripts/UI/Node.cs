using System;
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
        private int _number = 100;
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
            PathfindingMap map = new PathfindingMap(_number, _number);
            _mapLattice = new Array2Utils<MapLattice>(_number, _number);
            Random random = new Random();
            for (int i = 0; i < _number * _number; i++)
            {
                GameObject imageGo = Instantiate(mapLatticePrefab, _parentNode);
                MapLattice mapLattice = imageGo.GetComponent<MapLattice>();
                mapLattice.Pos = new IntVector2(i % _number, i / _number);
                bool canPass = random.Next(10) > 2;
                if (!canPass)
                {
                    mapLattice.SetCannotPass();
                }

                mapLattice.Operate = pos =>
                {
                    if (_startPos == new IntVector2(-1, -1))
                    {
                        _startPos = pos;
                    }
                    else
                    {
                        _endPos = pos;
                        long time1 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        Debug.LogError("asd001  " + time1);
                        PathfindingSingleMessage message =
                            PathfindingControllerOld.Singleton.Pathfinding(_startPos, _endPos, map);
                        for (int j = 0; j < 99; j++)
                        {
                            message = PathfindingControllerOld.Singleton.Pathfinding(_startPos, _endPos, map);
                        }

                        for (int j = 0; j < message.PathfindingResult.Count; j++)
                        {
                            _mapLattice[message.PathfindingResult[j].Pos.X, message.PathfindingResult[j].Pos.Y]
                                .ShowRoute(true);
                        }

                        long time2 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        Debug.LogError("asd002  " + time2 + "  " + (time2 - time1));
                        PathfindingSingleMessage message1 =
                            PathfindingController.Singleton.Pathfinding(_startPos, _endPos, map);
                        for (int j = 0; j < 99; j++)
                        {
                            message1 = PathfindingController.Singleton.Pathfinding(_startPos, _endPos, map);
                        }

                        for (int j = 0; j < message1.PathfindingResult.Count; j++)
                        {
                            _mapLattice[message1.PathfindingResult[j].Pos.X, message1.PathfindingResult[j].Pos.Y]
                                .ShowRoute1(true);
                        }

                        long time3 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        Debug.LogError("asd003  " + time3 + "  " + (time3 - time2));
                    }
                };
                float row = i / _number;
                float col = i % _number;
                RectTransform rectTransform = imageGo.GetComponent<RectTransform>();
                rectTransform.anchoredPosition =
                    new Vector2((col - _number / 2) * _imageSize, (row - _number / 2) * _imageSize);
                Vector2 size = rectTransform.sizeDelta;
                rectTransform.localScale = new Vector3(_imageSize / size.x, _imageSize / size.x, _imageSize / size.x);
                _mapLattice[i % _number, i / _number] = mapLattice;

                PathfindingMapNode pathfindingMapNode = new PathfindingMapNode();
                pathfindingMapNode.Pos = new IntVector2(i % _number, i / _number);
                pathfindingMapNode.TerrainType = canPass ? TerrainType.CAN_PASS : TerrainType.CANNOT_PASS;
                map.MapData[i % _number, i / _number] = pathfindingMapNode;
            }

            map.MapData.ForEach((i, j, node) => { node.AroundNode = map.FindAroundNode(new IntVector2(i, j)); });

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