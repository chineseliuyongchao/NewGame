/****************************************************************************
 * Copyright (c) 2017 ~ 2018.5 liangxie
 *
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace QFramework
{
    /// <summary>
    /// ResKitAssetsMenu 类用于在 Unity 编辑器中处理与 AssetBundle 相关的操作，
    /// 包括标记资源为 AssetBundle、显示资源的 AssetBundle 名称、复制 GUID 等功能。
    /// 该类利用 Unity 的菜单项（MenuItem）扩展功能，将相关操作添加到 Unity 编辑器的菜单中。
    /// 同时利用 InitializeOnLoad 特性，在编辑器启动时注册 Selection.selectionChanged 事件，以实时更新菜单勾选状态。
    /// </summary>
    [InitializeOnLoad]
    public class ResKitAssetsMenu
    {
        // AssetBundle 输出路径相关常量
        public const string ASSET_BUNDLES_OUTPUT_PATH = "AssetBundles";
        public const string ASSET_BUNDLES_FIRST_PATH = "AssetBundles_First";
        public const string ASSET_BUNDLES_HOT_PATH = "AssetBundles_Hot";

        // 菜单项名称常量
        private const string MarkAssetBundle = "Assets/@ResKit - AssetBundle Mark";
        private const string MarkMutiAssetBundle = "Assets/@ResKit - AssetBundle Mark Muti";
        private const string ShowAssetBundle = "Assets/@ResKit - Show AssetBundle";
        private const string MarkAllAssetBundle = "Assets/@ResKit - AssetBundle Mark All";
        private const string CopyGuidName = "Assets/@ResKit - Copy_GUID";

        // 静态构造函数，在编辑器加载时执行
        static ResKitAssetsMenu()
        {
            // 注册 Selection.selectionChanged 事件，每当编辑器中选中对象改变时调用 OnSelectionChanged 方法
            Selection.selectionChanged = OnSelectionChanged;
        }

        /// <summary>
        /// 当编辑器中选中的对象发生变化时调用，
        /// 用于更新菜单项的勾选状态，反映当前选中资源是否已标记 AssetBundle。
        /// </summary>
        private static void OnSelectionChanged()
        {
            // 获取当前选中的路径（如果有多个对象，返回第一个有效的路径）
            var path = GetSelectedPathOrFallback();
            if (!string.IsNullOrEmpty(path))
            {
                // 根据是否标记，更新菜单项 "MarkAssetBundle" 的勾选状态
                Menu.SetChecked(MarkAssetBundle, Marked(path));
            }

            // 检查多个选中对象是否全部已标记 AssetBundle
            bool bAllMark = true;
            GameObject[] selectedObjects = Selection.gameObjects;
            foreach (GameObject obj in selectedObjects)
            {
                // 获取每个选中对象的路径
                path = AssetDatabase.GetAssetPath(obj);
                if (!Marked(path))
                {
                    bAllMark = false;
                    break;
                }
            }

            // 更新多选菜单项的勾选状态
            Menu.SetChecked(MarkMutiAssetBundle, bAllMark);

            // 针对通过 GUID 选中的文件夹也进行同样检查
            string[] selectedFolders = Selection.assetGUIDs;
            foreach (string folderGuid in selectedFolders)
            {
                path = AssetDatabase.GUIDToAssetPath(folderGuid);
                AssetImporter.GetAtPath(path);
                if (!Marked(path))
                {
                    bAllMark = false;
                    break;
                }
            }

            Menu.SetChecked(MarkMutiAssetBundle, bAllMark);
        }

        /// <summary>
        /// 判断指定路径的资源是否已经标记了 AssetBundle。
        /// 通过获取资源的 AssetImporter，并与根据路径生成的 Bundle 名称进行比较。
        /// </summary>
        /// <param name="path">资源的路径</param>
        /// <returns>如果资源的 assetBundleName 与预期匹配，则返回 true；否则返回 false</returns>
        public static bool Marked(string path)
        {
            try
            {
                var ai = AssetImporter.GetAtPath(path);
                var bundleName = GetAssetBundleName(ai.assetPath);
                // 如果 assetBundleName 不为空，则重置为预期的 bundleName
                if (!ai.assetBundleName.IsNullOrEmpty())
                {
                    ai.assetBundleName = GetAssetBundleName(ai.assetPath);
                }

                // 比较当前 assetBundleName 与预期 bundleName 是否一致
                return string.Equals(ai.assetBundleName, bundleName);
            }
            // 捕获异常，防止资源读取失败导致错误
#pragma warning disable CS0168
            catch (Exception _)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        /// <summary>
        /// 对指定路径的资源进行 AssetBundle 标记。
        /// 如果已经标记，则取消标记；否则设置 assetBundleName 为预期值。
        /// 同时更新菜单勾选状态并移除未使用的 AssetBundle 名称。
        /// </summary>
        /// <param name="path">资源路径</param>
        public static void MarkAb(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var ai = AssetImporter.GetAtPath(path);
                // 如果已经标记，则取消标记
                if (Marked(path))
                {
                    Menu.SetChecked(MarkAssetBundle, false);
                    ai.assetBundleName = null;
                }
                // 如果未标记，则标记资源
                else
                {
                    Menu.SetChecked(MarkAssetBundle, true);
                    ai.assetBundleName = GetAssetBundleName(ai.assetPath);
                }

                Debug.Log("MarkAB path:" + path + ", assetBundleName:" + "<color=#00FF00FF>" + ai.assetBundleName +
                          "</color>");
                // 移除未使用的 AssetBundle 名称
                AssetDatabase.RemoveUnusedAssetBundleNames();
            }
        }

        /// <summary>
        /// 菜单项：标记当前选中的资源或文件夹为 AssetBundle
        /// </summary>
        [MenuItem(MarkAssetBundle)]
        public static void MarkPtabDir()
        {
            var path = GetSelectedPathOrFallback();
            MarkAb(path);
        }

        /// <summary>
        /// 菜单项：批量标记当前选中的多个资源或文件夹为 AssetBundle
        /// </summary>
        [MenuItem(MarkMutiAssetBundle)]
        public static void MarkMutiPtabDir()
        {
            ClearLog();
            // 注释掉的代码为单个资源处理，此处改为处理 GUID 对应的文件夹
            string[] selectedFolders = Selection.assetGUIDs;
            foreach (string folderGuid in selectedFolders)
            {
                var path = AssetDatabase.GUIDToAssetPath(folderGuid);
                if (!string.IsNullOrEmpty(path))
                {
                    MarkAb(path);
                }
            }
        }

        /// <summary>
        /// 菜单项：显示当前选中的资源或文件夹的 AssetBundle 名称
        /// </summary>
        [MenuItem(ShowAssetBundle)]
        public static void ShowMutiPtabDir()
        {
            ClearLog();
            // 针对通过 GUID 选中的文件夹也显示名称
            string[] selectedFolders = Selection.assetGUIDs;
            foreach (string folderGuid in selectedFolders)
            {
                var path = AssetDatabase.GUIDToAssetPath(folderGuid);
                if (!string.IsNullOrEmpty(path))
                {
                    var ai = AssetImporter.GetAtPath(path);
                    Debug.Log("ShowMarkAB path:" + path + ", assetBundleName:" + "<color=#00FF00FF>" +
                              ai.assetBundleName + "</color>");
                }
            }
        }

        /// <summary>
        /// 菜单项：复制当前选中对象的 GUID 到剪贴板
        /// </summary>
        [MenuItem(CopyGuidName)]
        public static void CopyUuid()
        {
            // 获取当前选中对象的路径及其 GUID
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string guid = AssetDatabase.AssetPathToGUID(path);

            // 将 GUID 复制到系统剪贴板
            EditorGUIUtility.systemCopyBuffer = guid;
        }

        /// <summary>
        /// 获取当前选中的路径，如果没有选中有效的文件则返回空字符串。
        /// 遍历选中的所有对象，并返回第一个存在对应文件的路径。
        /// </summary>
        /// <returns>选中对象对应的资源路径</returns>
        private static string GetSelectedPathOrFallback()
        {
            var path = string.Empty;

            foreach (var obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);

                // 如果路径不为空并且对应文件存在，则返回该路径
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    return path;
                }
            }

            return path;
        }

        /// <summary>
        /// 根据资源路径生成对应的 AssetBundle 名称。
        /// 通过替换路径中的特殊字符和子串，生成统一格式的名称。
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>处理后的 AssetBundle 名称</returns>
        public static string GetAssetBundleName(string path)
        {
            string bundleName = path.Replace(".", "_");
            bundleName = bundleName.Replace("/", "_");
            bundleName = bundleName.Replace("Assets_GameTheme_", "");
            bundleName = bundleName.Replace("Assets_GameSLT_Res_", "");
            bundleName = bundleName.Replace("Assets_GameSLT_Scripts_", "");
            bundleName = bundleName.Replace(" ", "");
            bundleName = bundleName.Replace("_Res_", "_");

            bundleName = bundleName.ToLower();
            return bundleName;
        }

        /// <summary>
        /// 获取 AssetBundle 存放的相对根目录。
        /// 目录格式为：/AssetBundles/平台名_Assetbundles/
        /// </summary>
        public static string RelativeAbRootFolder =>
            "/" + ASSET_BUNDLES_OUTPUT_PATH + "/" + AssetBundlePathHelper.GetPlatformName() + "_Assetbundles/";

        /// <summary>
        /// 清空 Unity 编辑器的 Console 日志。
        /// 利用反射调用 UnityEditor 内部的 LogEntries.Clear 方法。
        /// </summary>
        private static void ClearLog()
        {
            var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
            if (logEntries != null)
            {
                var clearMethod = logEntries.GetMethod("Clear",
                    System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                if (clearMethod != null) clearMethod.Invoke(null, null);
            }
        }

        /// <summary>
        /// 菜单项：批量重新标记所有已存在的 AssetBundle 资源
        /// 根据资源后缀（如 .png、.tga、.mat）对资源进行处理，
        /// 对于部分资源取消标记并以其所在文件夹作为 AssetBundle 标记。
        /// </summary>
        [MenuItem(MarkAllAssetBundle)]
        public static void MarkAllAb()
        {
            // 获取所有 AssetBundle 名称
            var allAb = AssetDatabase.GetAllAssetBundleNames();
            // 定义需要特殊处理的资源扩展名列表
            List<string> arrExtension = new List<string>
            {
                ".png", ".mat", ".jpg", ".tga",
                ".mp3", ".mp4",
            };
            // 遍历所有 AssetBundle
            for (int i = 0; i < allAb.Length; i++)
            {
                // 获取每个 AssetBundle 包含的所有资源路径
                var arrPath = AssetDatabase.GetAssetPathsFromAssetBundle(allAb[i]);
                Debug.Log("MarkAB:" + i + ":" + allAb[i]);
                for (int j = 0; j < arrPath.Length; j++)
                {
                    var r = arrPath[j];
                    // 如果该资源已被标记，则根据资源类型进行处理
                    if (Marked(r))
                    {
                        Debug.Log("SelfMark:" + r);
                        var ai = AssetImporter.GetAtPath(r);
                        // 针对图片、材质等资源取消标记，并对所在文件夹进行标记
                        if (r.EndsWith(".png") || r.EndsWith(".tga") || r.EndsWith(".mat"))
                        {
                            ai.assetBundleName = null;
                            ai.assetBundleVariant = null;
                            string folderPath = GetFolderPath(r); // 获取文件夹路径
                            ai = AssetImporter.GetAtPath(folderPath);
                            ai.assetBundleName = GetAssetBundleName(ai.assetPath);
                            Debug.Log("Unmarked and Folder Marked as AB:" + folderPath);
                        }
                        else
                        {
                            // 如果资源已经有标记，则重新设置为预期的 AssetBundle 名称
                            if (!ai.assetBundleName.IsNullOrEmpty())
                            {
                                Debug.Log("remarked SelfMark as AB:" + r);
                                ai.assetBundleName = GetAssetBundleName(ai.assetPath);
                            }
                        }

                        continue;
                    }

                    // 如果资源所在文件夹已被标记，则对资源进行重新标记
                    if (Marked(r.GetFolderPath()))
                    {
                        Debug.Log("FoldMark:" + r);
                        var ai = AssetImporter.GetAtPath(r);
                        if (!ai.assetBundleName.IsNullOrEmpty())
                        {
                            Debug.Log("remarked FoldMarked as AB:" + r);
                            ai.assetBundleName = GetAssetBundleName(ai.assetPath);
                        }
                    }
                }
            }

            // 移除未使用的 AssetBundle 名称
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        /// <summary>
        /// 根据资源路径返回所在文件夹的路径
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns>所在文件夹的路径</returns>
        private static string GetFolderPath(string assetPath)
        {
            return Path.GetDirectoryName(assetPath);
        }
    }
}