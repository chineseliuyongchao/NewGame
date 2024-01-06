using System;
using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;

namespace SystemTool.MapProcessing
{
    /// <summary>
    /// 根据网格图片获取地图信息
    /// </summary>
    public class ImageToMapController : BaseGameController, ISingleton
    {
        public static ImageToMapController Singleton => MonoSingletonProperty<ImageToMapController>.Instance;

        public void OnSingletonInit()
        {
        }

        /// <summary>
        /// 获取地图信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public PathfindingMap GetMap(string path)
        {
            Texture2D originalTexture = this.GetSystem<IResSystem>().LoadTexture(path);
            PathfindingMap map = new PathfindingMap(originalTexture.width, originalTexture.height);
            for (int x = 0; x < originalTexture.width; x++)
            {
                for (int y = 0; y < originalTexture.height; y++)
                {
                    Color grayPixel = originalTexture.GetPixel(x, y);
                    bool canPass = grayPixel.r > 0.5f;
                    PathfindingMapNode pathfindingMapNode = new PathfindingMapNode
                    {
                        pos = new Vector2Int(x, y),
                        size = new Vector2Int(1, 1),
                        terrainType = canPass ? TerrainType.CAN_PASS : TerrainType.CANNOT_PASS
                    };
                    map.MapData[x, y] = pathfindingMapNode;
                }
            }

            MergeMapMesh(map);
            map.MapData.ForEach((_, _, node) =>
            {
                if (map.CheckPass(node))
                {
                    if (node.aroundNode == null)
                    {
                        node.aroundNode = map.FindAroundNode(node);
                    }
                }
            });
            return map;
        }

        /// <summary>
        /// 合并网格
        /// </summary>
        private void MergeMapMesh(PathfindingMap map)
        {
            Debug.Log(map.MapSize());
            int[,] meshData = new int[map.MapSize().x, map.MapSize().y];
            map.MapData.ForEach((i, j, node) => { meshData[i, j] = map.CheckPass(node) ? 1 : 0; });

            while (true)
            {
                RectInt rect = FindMaxMapMesh(meshData, map);
                if (rect.width * rect.height <= 1) //如果没有可以合并的网格就停止合并
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 寻找当前最大的可合并网格
        /// </summary>
        /// <param name="meshData"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private RectInt FindMaxMapMesh(int[,] meshData, PathfindingMap map)
        {
            RectInt resultRect = new RectInt();
            int[,] meshHeight = new int[meshData.GetLength(0), meshData.GetLength(1)];
            for (int i = 0; i < meshHeight.GetLength(0); i++)
            {
                for (int j = 0; j < meshHeight.GetLength(1); j++)
                {
                    meshHeight[i, j] = meshData[i, j];
                }
            }

            for (int i = 0; i < meshHeight.GetLength(0); i++)
            {
                for (int j = meshHeight.GetLength(1) - 2; j >= 0; j--)
                {
                    if (meshHeight[i, j] != 0)
                    {
                        meshHeight[i, j] += meshHeight[i, j + 1];
                    }
                }
            }

            for (int i = 0; i < meshHeight.GetLength(1); i++)
            {
                int[] columnValues = new int[meshHeight.GetLength(0)]; // 创建一个数组用于存储该列的数据
                for (int j = 0; j < meshHeight.GetLength(0); j++)
                {
                    columnValues[j] = meshHeight[j, i];
                }

                RectInt rect = LargestRectangleArea(columnValues);
                rect.y = i;
                if (rect.width * rect.height > resultRect.width * resultRect.height)
                {
                    resultRect = rect;
                }
            }

            int c = 0;
            map.MapData.ForEach(resultRect, (i, j, node) =>
            {
                if (i != resultRect.x || j != resultRect.y)
                {
                    map.MapData[i, j] = map.MapData[resultRect.x, resultRect.y];
                }
                else
                {
                    node.size = new Vector2Int(resultRect.width, resultRect.height);
                }

                if (meshData[i, j] == 1)
                {
                    c++;
                }

                meshData[i, j] = 0;
            });
            Debug.LogWarning("本次最大可合成网格信息：" + resultRect + "  减少网格数量：" + c);
            return resultRect;
        }

        /// <summary>
        /// 用单调栈算法判断以地图某一行为底可以形成的最大矩形
        /// </summary>
        /// <param name="heights"></param>
        /// <returns></returns>
        private RectInt LargestRectangleArea(int[] heights)
        {
            RectInt result = new RectInt();
            int n = heights.Length, top = 0, max = 0; // 注意top = 0，表示初始时栈即有一个元素，即具有stack[0]
            int[] stack = new int[n + 1];
            stack[0] = -1; // 初始时栈顶为-1，表示向heights前添加一个0，目的是统一#1行的操作
            for (int i = 0; i <= n; i++)
            {
                while (top != 0 && (i == n || heights[i] < heights[stack[top]]))
                {
                    // i == n是为了在最后弹出所有有效元素
                    int h = heights[stack[top]];
                    top--; //top--以后，stack[top]是当前高度的左边界（不包含）
                    int w = i - stack[top] - 1;
                    max = Math.Max(max, h * w);
                    if (max == h * w)
                    {
                        result.x = stack[top] + 1;
                        result.width = w;
                        result.height = h;
                    }
                }

                stack[++top] = i;
            }

            return result;
        }
    }
}