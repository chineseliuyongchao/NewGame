using GameQFramework;
using QFramework;
using UnityEngine;

namespace SystemTool.MapProcessing
{
    /// <summary>
    /// 地图图片->网格图
    /// </summary>
    public class MapImageProcessorController : BaseGameController, ISingleton
    {
        public static MapImageProcessorController Singleton =>
            MonoSingletonProperty<MapImageProcessorController>.Instance;

        public void OnSingletonInit()
        {
        }

        /// <summary>
        /// 处理地图图片的主要方法
        /// </summary>
        /// <param name="path">地图路径</param>
        /// <param name="fileName">文件名字</param>
        /// <param name="gridSizeX">x轴网格大小</param>
        /// <param name="gridSizeY">y轴网格大小</param>
        public void ProcessMapImage(string path, string fileName, int gridSizeX, int gridSizeY)
        {
            // 加载原始地图图片
            Texture2D originalTexture = this.GetSystem<IResSystem>().LoadTexture(path + fileName + ".png");

            // 转换为灰度图
            Texture2D grayTexture = ConvertToGrayscale(originalTexture);
            // 保存灰度图
            SaveTexture(grayTexture, path + "gray_" + fileName + ".jpg");

            // 转换为二值化图
            Texture2D binaryTexture = ConvertToBinary(grayTexture);
            // 保存二值化图
            SaveTexture(binaryTexture, path + "binary_" + fileName + ".jpg");

            // 划分网格
            Texture2D gridTexture = DivideIntoGrid(binaryTexture, gridSizeX, gridSizeY);
            // 保存划分后的网格图
            SaveTexture(gridTexture, path + "grid_" + fileName + ".jpg");
        }

        /// <summary>
        /// 转换为灰度图方法
        /// </summary>
        /// <param name="originalTexture"></param>
        /// <returns></returns>
        private Texture2D ConvertToGrayscale(Texture2D originalTexture)
        {
            Texture2D grayTexture = new Texture2D(originalTexture.width, originalTexture.height);
            for (int x = 0; x < grayTexture.width; x++)
            {
                for (int y = 0; y < grayTexture.height; y++)
                {
                    Color pixelColor = originalTexture.GetPixel(x, y);
                    float grayValue = pixelColor.grayscale;
                    grayTexture.SetPixel(x, y, new Color(grayValue, grayValue, grayValue));
                }
            }

            grayTexture.Apply();
            return grayTexture;
        }

        /// <summary>
        /// 转换为二值化图方法
        /// </summary>
        /// <param name="grayTexture"></param>
        /// <returns></returns>
        private Texture2D ConvertToBinary(Texture2D grayTexture)
        {
            Texture2D binaryTexture = new Texture2D(grayTexture.width, grayTexture.height);
            for (int x = 0; x < binaryTexture.width; x++)
            {
                for (int y = 0; y < binaryTexture.height; y++)
                {
                    Color grayPixel = grayTexture.GetPixel(x, y);
                    binaryTexture.SetPixel(x, y, grayPixel.r > 0.5f ? Color.white : Color.black);
                }
            }

            binaryTexture.Apply();
            return binaryTexture;
        }

        /// <summary>
        /// 划分网格方法
        /// </summary>
        /// <param name="binaryTexture"></param>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        private Texture2D DivideIntoGrid(Texture2D binaryTexture, int gridX, int gridY)
        {
            Texture2D gridTexture = new Texture2D(binaryTexture.width / gridX, binaryTexture.height / gridY);
            for (int x = 0; x < gridTexture.width; x++)
            {
                for (int y = 0; y < gridTexture.height; y++)
                {
                    Color averageColor = GetAverageColor(binaryTexture, x * gridX, y * gridY, gridX, gridY);
                    gridTexture.SetPixel(x, y, averageColor);
                }
            }

            gridTexture.Apply();
            return gridTexture;
        }

        /// <summary>
        /// 获取颜色均值方法
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Color GetAverageColor(Texture2D texture, int startX, int startY, int width, int height)
        {
            Color[] pixels = texture.GetPixels(startX, startY, width, height);
            float r = 0f, g = 0f, b = 0f;
            foreach (Color pixel in pixels)
            {
                r += pixel.r;
                g += pixel.g;
                b += pixel.b;
            }

            int count = pixels.Length;
            return new Color(r / count, g / count, b / count);
        }

        /// <summary>
        /// 保存图片方法
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="fileName"></param>
        private void SaveTexture(Texture2D texture, string fileName)
        {
            byte[] imageBytes = texture.EncodeToJPG();
            System.IO.File.WriteAllBytes(fileName, imageBytes);
        }
    }
}