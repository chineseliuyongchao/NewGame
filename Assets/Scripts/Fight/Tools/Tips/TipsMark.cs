using Fight.Controller;
using Fight.Model;
using Fight.Utils;
using JetBrains.Annotations;
using UnityAttribute;
using UnityEngine;

namespace Fight.Tools.Tips
{
    public class TipsMark : MonoBehaviour
    {
        [Label("气泡类型")] public TipsType tipsType = TipsType.DefaultTips;

        [Label("专属id")] [ReadOnly] public int id;

        [Label("正在展示气泡")] [ReadOnly] public bool showTips;

        [Label("气泡的实例")] [ReadOnly] [CanBeNull]
        public ObjectTips objectTips;

        [Label("气泡的父亲")] [ReadOnly] [CanBeNull]
        public TipsMark parent;

        /// <summary>
        /// 被锁住的气泡不会主动关闭，除非主动调用关闭或者解锁
        /// </summary>
        [Label("气泡被锁住")] [ReadOnly] public bool locking;

        [Label("携带的信息")] public string[] infos;

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