using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Editor
{
    public static class EditorUtils
    {
#if UNITY_EDITOR
        //debug使用
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            SceneManager.LoadScene(0);
        }
#endif
        
        private static readonly string targetTMPFontPath = "Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset";
        private static readonly string targetFontPath = "Assets/Res/Font/simhei.ttf";

        [MenuItem("Tools/TMPUGUI → Text (指定字体)")]
        public static void ReplaceTMPUGUI()
        {
            TMP_FontAsset targetTMPFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(targetTMPFontPath);
            Font targetUIFont = AssetDatabase.LoadAssetAtPath<Font>(targetFontPath);

            if (targetTMPFont == null || targetUIFont == null)
            {
                Debug.LogError("字体资源未找到，请检查路径！");
                return;
            }

            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                bool changed = false;
                TextMeshProUGUI[] tmps = prefab.GetComponentsInChildren<TextMeshProUGUI>(true);

                foreach (var tmp in tmps)
                {
                    if (tmp.font == targetTMPFont)
                    {
                        GameObject go = tmp.gameObject;

                        // 保存属性
                        string text = tmp.text;
                        float fontSize = tmp.fontSize;
                        Color color = tmp.color;
                        TextAlignmentOptions alignment = tmp.alignment;

                        Object.DestroyImmediate(tmp, true);

                        Text uiText = go.AddComponent<Text>();
                        uiText.text = text;
                        uiText.font = targetUIFont;
                        uiText.fontSize = Mathf.RoundToInt(fontSize);
                        uiText.color = color;
                        uiText.alignment = ConvertAlignment(alignment);

                        changed = true;
                    }
                }

                if (changed)
                {
                    EditorUtility.SetDirty(prefab);
                    Debug.Log("替换成功：" + path);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/TMP → Text (指定字体)")]
        public static void ReplaceTMP()
        {
            TMP_FontAsset targetTMPFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(targetTMPFontPath);
            Font targetFont = AssetDatabase.LoadAssetAtPath<Font>(targetFontPath);

            if (targetTMPFont == null || targetFont == null)
            {
                Debug.LogError("字体资源未找到，请检查路径！");
                return;
            }

            string[] guids = AssetDatabase.FindAssets("t:Prefab");

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                bool changed = false;
                TextMeshPro[] tmps = prefab.GetComponentsInChildren<TextMeshPro>(true);

                foreach (var tmp in tmps)
                {
                    if (tmp.font == targetTMPFont)
                    {
                        GameObject go = tmp.gameObject;

                        string text = tmp.text;
                        float fontSize = tmp.fontSize;
                        Color color = tmp.color;
                        TextAlignmentOptions alignment = tmp.alignment;

                        Object.DestroyImmediate(tmp, true);

                        TextMesh textMesh = go.AddComponent<TextMesh>();
                        textMesh.text = text;
                        textMesh.font = targetFont;
                        textMesh.fontSize = Mathf.RoundToInt(fontSize);
                        textMesh.color = color;
                        textMesh.anchor = ConvertAlignment(alignment);

                        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
                        if (renderer)
                        {
                            renderer.materials = Array.Empty<Material>();
                        }

                        changed = true;
                    }
                }

                if (changed)
                {
                    EditorUtility.SetDirty(prefab);
                    Debug.Log("替换成功：" + path);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static TextAnchor ConvertAlignment(TextAlignmentOptions alignment)
        {
            return alignment switch
            {
                TextAlignmentOptions.TopLeft => TextAnchor.UpperLeft,
                TextAlignmentOptions.Top => TextAnchor.UpperCenter,
                TextAlignmentOptions.TopRight => TextAnchor.UpperRight,
                TextAlignmentOptions.Left => TextAnchor.MiddleLeft,
                TextAlignmentOptions.Center => TextAnchor.MiddleCenter,
                TextAlignmentOptions.Right => TextAnchor.MiddleRight,
                TextAlignmentOptions.BottomLeft => TextAnchor.LowerLeft,
                TextAlignmentOptions.Bottom => TextAnchor.LowerCenter,
                TextAlignmentOptions.BottomRight => TextAnchor.LowerRight,
                _ => TextAnchor.MiddleCenter,
            };
        }
    }
}