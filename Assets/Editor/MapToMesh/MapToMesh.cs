using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class MapToMesh : EditorWindow
    {
        [MenuItem("Tools/MapToMesh")]
        public static void Init()
        {
            GetWindow(typeof(MapToMesh));
        }

        private void OnGUI()
        {
            string path = Application.dataPath + "/Res/Image/Map/";
            string jsonPath = Application.dataPath + "/Res/File/Map/Output/Json/";
            if (GUI.Button(new Rect(150, 250, 100, 50), "开始"))
            {
                List<Texture2D> texture2Ds = MapImageProcessor.ProcessMapImage(path, 10, 10);
                if (texture2Ds.Count <= 0)
                {
                    EditorUtility.DisplayDialog("", "图片有问题，请检查格式或者路径", "确定");
                }
                else
                {
                    ImageToMap.GetMap(texture2Ds[0], jsonPath); //目前只有一条数据
                    EditorUtility.DisplayDialog("", "转换完成", "确定");
                }
            }

            AssetDatabase.Refresh();
        }
    }
}