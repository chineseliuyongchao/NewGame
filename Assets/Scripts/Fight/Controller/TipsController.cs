using Fight.Model;
using Fight.Tools;
using Fight.Tools.Pool;
using Game.GameBase;
using QFramework;
using UnityAttribute;
using UnityEngine;

namespace Fight.Controller
{
    public class TipsController : MonoBehaviour, IController
    {
        [Label("兵种气泡预制体")] public GameObject armsTipsPrefab;

        private GameObjectPool _armsTipsPool;

        private void Awake()
        {
            _armsTipsPool = new GameObjectPool(armsTipsPrefab, transform, 2);
            TipsMark.TipsController = this;
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