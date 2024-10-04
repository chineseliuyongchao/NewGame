using Fight.Model;
using Fight.Tools.Pool;
using Fight.Tools.Tips;
using Game.GameBase;
using QFramework;
using UnityAttribute;
using UnityEngine;

namespace Fight.Controller
{
    public class TipsController : MonoBehaviour, IController
    {
        [Label("默认气泡预制体")] public GameObject defaultTipsPrefab;
        [Label("兵种气泡预制体")] public GameObject armsTipsPrefab;

        private GameObjectPool _defaultTipsPool;
        private GameObjectPool _armsTipsPool;

        private void Awake()
        {
            TipsMark.TipsController = this;
            var transform1 = transform;
            _defaultTipsPool = new GameObjectPool(defaultTipsPrefab, transform1, 2);
            _armsTipsPool = new GameObjectPool(armsTipsPrefab, transform1, 2);
        }

        public void ShowDefaultInfo(Vector2 worldPosition, TipsMark tipsMark)
        {
            GameObject obj = _defaultTipsPool.Get();
            DefaultTips defaultTips = obj.GetComponent<DefaultTips>();
            defaultTips.tipsMark = tipsMark;
            defaultTips.OnInit(tipsMark.infos);
            defaultTips.hideCallback = () => { _defaultTipsPool.Release(obj); };
            defaultTips.Layout(transform.InverseTransformPoint(worldPosition));
            defaultTips.Show();
        }

        /// <summary>
        /// 展示兵种信息
        /// </summary>
        /// <param name="worldPosition">用户鼠标放置的准确坐标</param>
        /// <param name="tipsMark">相应的气泡标识</param>
        /// <param name="unitData">兵种数据</param>
        public void ShowUnitInfo(Vector2 worldPosition, TipsMark tipsMark, UnitData unitData)
        {
            GameObject obj = _armsTipsPool.Get();
            ArmsTips armsTips = obj.GetComponent<ArmsTips>();
            armsTips.tipsMark = tipsMark;
            armsTips.OnInit(unitData);
            armsTips.hideCallback = () => { _armsTipsPool.Release(obj); };
            armsTips.Layout(transform.InverseTransformPoint(worldPosition));
            armsTips.Show();
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }
}