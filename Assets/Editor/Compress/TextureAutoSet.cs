using System;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace Editor.Compress
{
    /// <summary>
    /// TextureAutoSet 用于自动设置纹理和精灵图集的压缩参数，
    /// 包括对纹理的压缩设置、精灵图集的压缩设置以及相关资源的打包设置。
    /// </summary>
    public class TextureAutoSet : EditorWindow
    {
        // 默认压缩质量设置（这里对应 TextureCompressionQuality.Normal）
        private static readonly int MCompressionQuality = (int)TextureCompressionQuality.Normal;

        // 默认纹理导入格式设置（ASTC压缩格式）
        private static readonly TextureImporterFormat MImporterFormat = TextureImporterFormat.ASTC_6x6;

        /// <summary>
        /// 对单个纹理资源进行压缩设置。方法会检测纹理是否需要更新，更新通用设置后遍历各个平台的设置并进行更新。
        /// </summary>
        /// <param name="importer">纹理的 TextureImporter 对象</param>
        public static void CompressTexture(TextureImporter importer)
        {
            // 如果传入的 importer 为 null，则直接返回
            if (importer == null)
            {
                return;
            }

            // 检查是否需要更新该纹理
            if (!IsNeedUpdate(importer.assetPath))
            {
                return;
            }

            // 更新通用设置并获取对应的平台纹理格式
            var importerFormat = UpdateCommonSetting(importer);

            int allNum = 0;
            // 获取平台类型枚举名称数组，注意第一个元素一般为 ALL 或占位符，从索引1开始遍历
            string[] values = Enum.GetNames(typeof(PlatformType));
            for (int i = 1; i < values.Length; i++)
            {
                // 获取当前平台的纹理设置
                var setting = importer.GetPlatformTextureSettings(values[i]);
                // 检查当前设置是否需要刷新
                if (CheckIsRefresh(setting, importerFormat))
                {
                    // 如果当前格式为 Automatic，则替换为指定格式
                    if (setting.format == TextureImporterFormat.Automatic)
                    {
                        setting.format = importerFormat;
                    }

                    // 将当前平台的设置标记为已覆盖，并设置压缩质量
                    setting.overridden = true;
                    setting.compressionQuality = MCompressionQuality;
                    importer.SetPlatformTextureSettings(setting);
                }
                else
                {
                    // 记录不需要刷新的平台数量
                    allNum++;
                }
            }

            // 如果所有平台都不需要刷新，则视为需要刷新
            var isRefresh = allNum == values.Length ? true : false;
            // 如果需要刷新，则输出日志并重新导入纹理
            if (isRefresh)
            {
                Debug.Log("Compress Texture " + importer.assetPath);
                importer.SaveAndReimport();
            }
        }

        /// <summary>
        /// 更新纹理的通用设置，主要根据路径判断使用不同的纹理格式，同时禁用 Mipmap 和读取开关。
        /// </summary>
        /// <param name="importer">纹理的 TextureImporter 对象</param>
        /// <returns>更新后的 TextureImporterFormat</returns>
        private static TextureImporterFormat UpdateCommonSetting(TextureImporter importer)
        {
            // 默认格式为 MImporterFormat
            var importerFormat = MImporterFormat;
            // 如果纹理路径包含 "Sprites"，说明可能是精灵纹理，使用 ASTC_4x4 格式
            if (importer.assetPath.Contains("Sprites"))
            {
                importerFormat = TextureImporterFormat.ASTC_4x4;
                // 如果路径中还包含 "paytable"，则使用 ASTC_6x6 格式
                if (importer.assetPath.Contains("paytable"))
                {
                    importerFormat = TextureImporterFormat.ASTC_6x6;
                }
            }
            // 如果路径包含 "Fonts"，则使用 ASTC_5x5 格式
            else if (importer.assetPath.Contains("Fonts"))
            {
                importerFormat = TextureImporterFormat.ASTC_5x5;
            }

            // 禁用 Mipmap 生成和读取权限，优化性能
            importer.mipmapEnabled = false;
            importer.isReadable = false;

            return importerFormat;
        }

        /// <summary>
        /// 对精灵图集资源进行压缩设置。会加载指定路径下的精灵图集，禁用紧凑打包，并对各平台的设置进行更新。
        /// </summary>
        /// <param name="path">精灵图集资源的路径</param>
        /// <param name="importerFormat">指定的纹理格式，默认为 ASTC_4x4</param>
        public static void CompressSpriteAtlas(string path,
            TextureImporterFormat importerFormat = TextureImporterFormat.ASTC_4x4)
        {
            // 加载指定路径的精灵图集资源
            SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(path);
            if (atlas == null)
            {
                return;
            }

            // 禁用精灵图集的紧凑打包，并添加目录下的精灵到图集中
            DisableTightPackingAndAddSprites(path);

            bool isRefresh = false;
            // 获取平台类型枚举名称数组，从索引1开始遍历
            string[] values = Enum.GetNames(typeof(PlatformType));
            for (int i = 1; i < values.Length; i++)
            {
                // 获取当前平台的图集设置
                var importer = atlas.GetPlatformSettings(values[i]);
                // 检查是否需要刷新：如果当前设置的格式或压缩质量与指定的不符，则需要刷新
                if (importerFormat != importer.format || importer.compressionQuality != MCompressionQuality)
                {
                    isRefresh = true;
                }

                // 如果当前格式为 Automatic，则设置为指定格式
                if (importer.format == TextureImporterFormat.Automatic)
                {
                    importer.format = importerFormat;
                }

                // 标记当前平台的设置为覆盖，并设置压缩质量
                importer.overridden = true;
                importer.compressionQuality = MCompressionQuality;
                atlas.SetPlatformSettings(importer);
            }

            // 标记图集数据已更改
            EditorUtility.SetDirty(atlas);
            // 如果需要刷新，则重新导入图集资源
            if (isRefresh)
            {
                AssetDatabase.ImportAsset(path);
            }
        }

        /// <summary>
        /// 禁用精灵图集的紧凑打包，并将精灵目录添加到图集中。
        /// </summary>
        /// <param name="atlasAssetPath">精灵图集资源的路径</param>
        private static void DisableTightPackingAndAddSprites(string atlasAssetPath)
        {
            // 加载精灵图集对象
            SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(atlasAssetPath);

            if (atlas != null)
            {
                // 获取当前图集的打包设置，并禁用紧凑打包
                SpriteAtlasPackingSettings packingSettings = atlas.GetPackingSettings();
                packingSettings.enableTightPacking = false;
                atlas.SetPackingSettings(packingSettings);

                // 获取图集所在的目录路径
                string atlasDirectory = Path.GetDirectoryName(atlasAssetPath);
                // 加载该目录对应的文件夹对象
                var folder = AssetDatabase.LoadAssetAtPath<Object>(atlasDirectory);
                bool isNeedRefresh = true;
                // 获取图集当前包含的所有打包项
                var foldSetting = atlas.GetPackables();
                // 检查文件夹是否已经包含在图集中
                for (int i = 0; i < foldSetting.Length; i++)
                {
                    if (foldSetting[i] == folder)
                    {
                        isNeedRefresh = false;
                        break;
                    }
                }

                // 如果图集中未包含该文件夹，则添加进去
                if (isNeedRefresh)
                {
                    atlas.Add(new[] { folder });
                }

                // 下面注释的代码为另一种将文件夹中所有精灵添加到图集的方式
                /*
                // 获取图集文件所在的目录
                string folderPath = System.IO.Path.GetDirectoryName(atlasAssetPath);
                // 加载该目录下所有的Sprite并添加到图集中
                string[] spriteGUIDs = AssetDatabase.FindAssets("t:Sprite", new[] {folderPath});
                foreach (string guid in spriteGUIDs)
                {
                    string spritePath = AssetDatabase.GUIDToAssetPath(guid);
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                    atlas.Add(new[] {sprite});
                }
                Debug.Log($"Tight Packing has been disabled and sprites added to the atlas: {atlasAssetPath}");
                */
            }
            else
            {
                // 如果加载失败，则输出错误日志
                Debug.LogError($"Failed to load atlas at path: {atlasAssetPath}");
            }
        }

        /// <summary>
        /// 检查当前纹理平台设置是否需要刷新。当前实现总是返回 true，实际逻辑可根据需求进行修改。
        /// </summary>
        /// <param name="import">纹理平台设置</param>
        /// <param name="format">指定的纹理格式</param>
        /// <returns>是否需要刷新</returns>
        private static bool CheckIsRefresh(TextureImporterPlatformSettings import, TextureImporterFormat format = TextureImporterFormat.ASTC_6x6)
        {
            bool bRefresh = true;
            // 如果需要根据其他条件判断是否刷新，可在此处添加逻辑
            // 例如：
            // if (import.overridden && import.format == format && import.compressionQuality == MCompressionQuality)
            // {
            //     bRefresh = false;
            // }

            return bRefresh;
        }

        /// <summary>
        /// 判断指定路径的纹理资源是否需要更新压缩设置。当前实现总是返回 true，实际逻辑可根据需求进行修改。
        /// </summary>
        /// <param name="assetPath">纹理资源的路径</param>
        /// <returns>是否需要更新</returns>
        private static bool IsNeedUpdate(string assetPath)
        {
            return false;
        }
    }

    /// <summary>
    /// 平台类型枚举。
    /// 可选平台字符串包括："Standalone", "Web", "iPhone", "Android", "WebGL", "Windows Store Apps", "PS4", "XboxOne", "Nintendo Switch", "tvOS"等。
    /// 当前示例中定义了 ALL, ANDROID, PHONE 三种类型，实际使用时可扩展更多平台。
    /// </summary>
    public enum PlatformType
    {
        ALL,
        ANDROID,
        PHONE,
    }
}