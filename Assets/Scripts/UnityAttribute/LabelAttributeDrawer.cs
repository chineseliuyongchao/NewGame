using UnityEditor;
using UnityEngine;

namespace UnityAttribute
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is LabelAttribute attr && !string.IsNullOrEmpty(attr.Name)) label.text = attr.Name;

            // 显示属性字段，包含子属性
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // 返回属性的高度，包括所有子属性的高度
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}