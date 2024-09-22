using Fight.Model;
using Fight.Utils;
using UnityAttribute;
using UnityEngine;
using UnityEngine.UI;

namespace Fight.Tools.Tips
{
    public class ArmsTips : ObjectTips
    {
        public Image bg;
        public VerticalLayoutGroup layoutGroup;

        public TipsMarkUI meleeMark;
        public TipsMarkUI defenseMark;

        [Label("兵种名")] public Text armsName;
        [Label("类型")] public Text type;
        [Label("血量")] public Text hp;
        [Label("人数")] public Text troops;
        [Label("价格")] public Text cost;
        [Label("作战意志")] public Text morale;
        [Label("疲劳值")] public Text fatigue;
        [Label("攻击能力")] public Text attack;
        [Label("冲锋加成")] public Text charge;
        [Label("近战杀伤")] public Text melee;
        [Label("防御能力")] public Text defense;
        [Label("护甲强度")] public Text armor;
        [Label("移动能力")] public Text mobility;
        [Label("视野")] public Text sight;
        [Label("隐蔽")] public Text stealth;
        [Label("远程杀伤")] public Text range2;
        [Label("射程")] public Text range;
        [Label("弹药量")] public Text ammo;
        [Label("装填速度")] public Text reload;
        [Label("精度")] public Text accuracy;
        private static readonly int Scale = Shader.PropertyToID("_Scale");


        public override void OnInit<T>(T value)
        {
            if (!(value is UnitData data))
            {
                return;
            }

            armsName.text = data.armDataType.unitName;
            //todo
            type.text = data.armDataType.unitName;

            hp.text = $"血量：{data.NowHp}/{data.armDataType.totalHp}";
            troops.text = $"人数：{data.NowTroops}/{data.armDataType.totalTroops}";
            cost.text = $"价格：${data.armDataType.cost}";
            morale.text = $"作战意志：{data.NowMorale}/{data.armDataType.maximumMorale}";
            fatigue.text = $"疲劳值：{data.NowFatigue}/{data.armDataType.maximumFatigue}";
            attack.text = "攻击能力：" + data.armDataType.attack;
            charge.text = "冲锋加成：" + data.armDataType.charge;
            melee.text = "近战杀伤：" + (data.armDataType.meleeNormal + data.armDataType.meleeArmor);
            defense.text = "防御能力：" + (data.armDataType.defenseMelee + data.armDataType.defenseRange);
            armor.text = "护甲强度：" + data.armDataType.armor;
            mobility.text = "移动能力：" + data.armDataType.mobility;
            sight.text = "视野：" + data.armDataType.sight;
            stealth.text = "隐蔽：" + data.armDataType.stealth;
            //没有弹药配置表示不是远程兵种
            if (data.armDataType.ammo == 0)
            {
                range2.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                range2.text = (data.armDataType.rangeNormal + data.armDataType.rangeArmor).ToString();
                range.text = data.armDataType.range.ToString();
                ammo.text = data.armDataType.ammo == -1 ? "无限" : data.armDataType.ammo.ToString();
                reload.text = data.armDataType.reload.ToString();
                accuracy.text = data.armDataType.accuracy.ToString();
            }

            meleeMark.infos = new[]
            {
                "普通杀伤：" + data.armDataType.meleeNormal,
                "破甲杀伤：" + data.armDataType.meleeArmor
            };
            meleeMark.parent = tipsMark;
            defenseMark.infos = new[]
            {
                "近战防御：" + data.armDataType.defenseMelee,
                "远程防御：" + data.armDataType.defenseRange
            };
            defenseMark.parent = tipsMark;
        }

        public override void Layout(Vector3 localPosition)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
            RectTransform rectTransform = GetComponent<RectTransform>();
            float maxHeight = layoutGroup.preferredHeight;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
            float maxWidth = 0;
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(armsName, type));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(hp));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(troops, cost));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(morale, fatigue));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(attack, charge, melee));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(defense, armor));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(mobility));
            maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(sight, stealth));
            if (range2.gameObject.activeSelf)
            {
                maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(range2, range, ammo));
                maxWidth = Mathf.Max(maxWidth, TextHorizontalLayout(reload, accuracy));
            }

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth + 20f);
            bg.material = Instantiate(bg.material);
            bg.material.SetVector(Scale, new Vector4(1f, maxHeight / maxWidth));
            base.Layout(localPosition);
        }
    }
}