using System;
using Fight.Controller;
using Fight.Utils;
using UnityAttribute;
using UnityEngine;

namespace Fight.Tools
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TipsMark : MonoBehaviour
    {
        [Label("专属id")] public int id;

        [Label("正在展示气泡")] public bool showTips;

        [Label("气泡的实例")]public ObjectTips objectTips;

        public static TipsController TipsController;

        private void Awake()
        {
            id = ArmsUtils.GetRandomByTime();
        }

        private void OnValidate()
        {
            id = ArmsUtils.GetRandomByTime();
        }
    }
}