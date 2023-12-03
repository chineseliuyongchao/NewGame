using UnityEditor;
using UnityEngine;

namespace EditorUtils
{
    public class GenExcel2Json2Cs : EditorWindow
    {
        [MenuItem("工具/Excel导出")]
        public static void Init()
        {
            GetWindow(typeof(GenExcel2Json2Cs));
        }

        void OnGUI()
        {
            string excelPath = Application.dataPath + "/Res/File/Excel2Json2Cs/Output/Excel/";
            string jsonPath = Application.dataPath + "/Res/File/Excel2Json2Cs/Output/Json/";

            if (GUI.Button(new Rect(10, 10, 100, 50), "请选择Excel路径"))
            {
                excelPath = EditorUtility.OpenFolderPanel("Choose Excel Path", "", "");
            }

            GUI.Label(new Rect(120, 30, 600, 30), excelPath);

            if (GUI.Button(new Rect(10, 70, 100, 50), "请选择Json路径"))
            {
                jsonPath = EditorUtility.OpenFolderPanel("Choose Json Path", "", "");
            }

            GUI.Label(new Rect(120, 90, 600, 30), jsonPath);
            if (GUI.Button(new Rect(150, 250, 100, 50), "Excel2Json"))
            {
                if (Generator.Excel2Json(excelPath, jsonPath))
                {
                    EditorUtility.DisplayDialog("", "完成", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("", "出错了", "确定");
                }

                AssetDatabase.Refresh();
            }
        }
    }
}