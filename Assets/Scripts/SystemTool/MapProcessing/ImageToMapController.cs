using GameQFramework;
using QFramework;
using SystemTool.Pathfinding;
using UnityEngine;
using Utils;

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
                        Pos = new IntVector2(x, y),
                        TerrainType = canPass ? TerrainType.CAN_PASS : TerrainType.CANNOT_PASS
                    };
                    map.MapData[x, y] = pathfindingMapNode;
                }
            }

            map.MapData.ForEach((i, j, node) => { node.AroundNode = map.FindAroundNode(new IntVector2(i, j)); });
            return map;
        }
    }
}