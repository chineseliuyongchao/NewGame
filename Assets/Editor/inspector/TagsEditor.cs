using Fight.Tools;
using Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Editor.inspector
{
    [CustomEditor(typeof(TagsManager))]
    public class TagsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("添加标签规划区域", GUILayout.Height(40)))
            {
                TagsManager script = (TagsManager)target;
                添加标签规划区域(script);
            }
        }

        private void 添加标签规划区域(TagsManager seript)
        {
            var transform = seript.transform;
            GameObject obj = new GameObject("Tag" + transform.childCount);
            obj.transform.SetParent(transform);
            obj.AddComponent<GraphUpdateScene>();
            obj.transform.rotation = Quaternion.Euler(-90, 0, 0);
            Selection.activeGameObject = obj;
        }
    }
}