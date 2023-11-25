using UnityEngine;

namespace SystemTool.MapProcessing
{
    public class MapImageProcessor : MonoBehaviour
    {
        // 地图图片路径，相对于Assets文件夹
        public string mapImagePath = "Assets/Image/image1.jpg";

        // 网格大小
        public int gridSizeX = 50;
        public int gridSizeY = 50;

        // 在脚本启动时调用的方法
        private void Start()
        {
            // 处理地图图片
            ProcessMapImage();
        }

        // 处理地图图片的主要方法
        void ProcessMapImage()
        {
            // 加载原始地图图片
            Texture2D originalTexture = LoadTexture(mapImagePath);

            // 转换为灰度图
            Texture2D grayTexture = ConvertToGrayscale(originalTexture);
            // 保存灰度图
            SaveTexture(grayTexture, "Assets/Image/gray_image.jpg");

            // 转换为二值化图
            Texture2D binaryTexture = ConvertToBinary(grayTexture);
            // 保存二值化图
            SaveTexture(binaryTexture, "Assets/Image/binary_image.jpg");

            // 划分网格
            Texture2D gridTexture = DivideIntoGrid(binaryTexture, gridSizeX, gridSizeY);
            // 保存划分后的网格图
            SaveTexture(gridTexture, "Assets/Image/grid_image.jpg");
        }

        /// <summary>
        /// 加载图片方法
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private Texture2D LoadTexture(string imagePath)
        {
            Texture2D texture = new Texture2D(2, 2);
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
            texture.LoadImage(fileData);
            return texture;
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