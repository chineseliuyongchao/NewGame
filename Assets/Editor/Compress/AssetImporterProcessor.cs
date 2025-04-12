using UnityEditor;
using UnityEngine;

namespace Editor.Compress
{
    /// <summary>
    /// 该类用于在资源导入时对纹理和精灵图集进行处理，
    /// 包括检测DDS格式、自动压缩纹理和压缩精灵图集等操作。
    /// 继承自 AssetPostprocessor，可拦截资源导入过程。
    /// </summary>
    public class AssetImporterProcessor : AssetPostprocessor
    {
        /// <summary>
        /// 当纹理资源导入完成后调用该方法。
        /// 该方法主要检测DDS格式，并对特定路径下的纹理进行自动压缩处理。
        /// </summary>
        /// <param name="texture">导入的纹理资源</param>
        void OnPostprocessTexture(Texture2D texture)
        {
            // 检查导入的纹理文件是否以 ".dds" 结尾
            if (assetPath.EndsWith(".dds"))
            {
                // 弹出对话框提示不支持DDS格式的纹理
                EditorUtility.DisplayDialog(
                    "纹理格式异常", // 对话框标题
                    "不支持DDS格式！" + "\n" + assetPath, // 提示信息，显示文件路径
                    "确定" // 按钮文本
                );
            }

            // 如果资源路径符合特定条件（IsStartGame返回true且路径中不包含“效果图”）
            // 则进行自动压缩纹理处理
            if (IsStartGame(assetImporter.assetPath) && !assetImporter.assetPath.Contains("效果图"))
            {
                // 调用 TextureAutoSet.CompressTexture 进行纹理压缩处理
                TextureAutoSet.CompressTexture(assetImporter as TextureImporter);
            }
        }

        /// <summary>
        /// 该静态方法在导入、删除或移动资源后调用。主要用于处理精灵图集资源的自动压缩。
        /// </summary>
        /// <param name="importedAssets">导入的资源路径数组</param>
        /// <param name="deletedAssets">删除的资源路径数组</param>
        /// <param name="movedAssets">移动后的资源路径数组</param>
        /// <param name="movedFromAssetPaths">移动前的资源路径数组</param>
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            // 遍历所有导入的资源
            for (int i = 0; i < importedAssets.Length; i++)
            {
                string assetPath = importedAssets[i];
                // 判断资源是否符合需要处理的条件
                if (!IsStartGameEx(assetPath))
                {
                    return;
                }

                // 如果资源以 ".spriteatlas" 结尾，则进行精灵图集压缩处理
                if (assetPath.EndsWith(".spriteatlas"))
                {
                    // 调用 TextureAutoSet.CompressSpriteAtlas 进行精灵图集压缩
                    TextureAutoSet.CompressSpriteAtlas(assetPath);
                }
            }
        }

        /// <summary>
        /// 判断指定的资源路径是否属于启动游戏相关资源。这里暂时返回 true，实际逻辑可根据需求进行修改。
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>是否符合启动游戏相关资源的条件</returns>
        bool IsStartGame(string path)
        {
            return false;
        }

        /// <summary>
        /// 判断指定的资源路径是否符合启动游戏扩展处理条件。这里暂时返回 true，实际逻辑可根据需求进行修改。
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns>是否符合条件</returns>
        static bool IsStartGameEx(string assetPath)
        {
            return true;
        }
    }
}