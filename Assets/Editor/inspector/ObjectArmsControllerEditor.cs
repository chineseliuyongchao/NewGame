using System.IO;
using Fight.Game.Arms;
using Fight.Game.Arms.Human.Nova;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace Editor.inspector
{
    [CustomEditor(typeof(ArmsController))]
    public class ObjectArmsControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("随机兵种样式", GUILayout.Height(40)))
            {
                HeavyInfantryKnightsControllerBase script =
                    (HeavyInfantryKnightsControllerBase)target;
                随机兵种样式(script.transform);
            }

            if (GUILayout.Button("复制该兵种", GUILayout.Height(40)))
            {
                复制该兵种();
            }
        }

        private void 随机兵种样式(Transform transform)
        {
            SpriteAtlas helmetAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Res/helmet.spriteatlasv2");
            SpriteAtlas shieldAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Res/shield.spriteatlasv2");
            // SpriteAtlas traitAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Res/trait.spriteatlasv2");
            SpriteAtlas weaponAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Res/weapon.spriteatlasv2");
            SpriteAtlas armorAtlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>("Assets/Res/armor.spriteatlasv2");
            SpriteRenderer helmet = transform.Find("helmet").GetComponent<SpriteRenderer>();
            helmet.sprite = helmetAtlas.GetSprite(Random.Range(0, helmetAtlas.spriteCount).ToString());
            
            SpriteRenderer shield = transform.Find("shield").GetComponent<SpriteRenderer>();
            shield.sprite = shieldAtlas.GetSprite(Random.Range(0, shieldAtlas.spriteCount).ToString());
            
            SpriteRenderer weapon = transform.Find("weapon").GetComponent<SpriteRenderer>();
            weapon.sprite = weaponAtlas.GetSprite(Random.Range(0, weaponAtlas.spriteCount).ToString());
            
            SpriteRenderer armor = transform.Find("armor").GetComponent<SpriteRenderer>();
            armor.sprite = armorAtlas.GetSprite(Random.Range(0, armorAtlas.spriteCount).ToString());
            
            AssetDatabase.Refresh();
        }

        private void 复制该兵种()
        {
            GameObject originalPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Resources/HeavyInfantryKnights.prefab");
            for (int i = 2; i < 100; i++)
            {
                string path = $"{Application.dataPath}/Prefabs/Resources/HeavyInfantryKnights{i}.prefab";
                if (!File.Exists(path))
                {
                    PrefabUtility.SaveAsPrefabAsset(originalPrefab, path);
                    break;
                }
            }
        }
    }
}