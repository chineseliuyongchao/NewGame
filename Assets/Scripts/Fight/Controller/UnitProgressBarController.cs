using DG.Tweening;
using Fight.Model;
using Fight.Utils;
using Game.FightCreate;
using Game.GameBase;
using QFramework;
using TMPro;
using UnityEngine;

namespace Fight.Controller
{
    public class UnitProgressBarController : MonoBehaviour, IController
    {
        [SerializeField] private SpriteRenderer unitProgressBg;
        [SerializeField] private TextMeshPro peopleCurrentNum;
        [SerializeField] private TextMeshPro peopleTotalNum;
        [SerializeField] private SpriteRenderer hpProgress;
        [SerializeField] private SpriteRenderer moraleProgress;
        [SerializeField] private SpriteRenderer fatigueProgress;
        [SerializeField] private SpriteRenderer actionProgress;

        private static readonly Color HpBaseColor = new Color32(139, 0, 0, 255);
        private static readonly Color HpCoreColor = new Color32(255, 0, 0, 255);

        private static readonly Color MoraleBaseColor = new Color32(105, 105, 105, 255);
        private static readonly Color MoraleCoreColor = new Color32(0, 255, 0, 255);

        private static readonly Color FatigueBaseColor = new Color32(0, 0, 139, 255);
        private static readonly Color FatigueCoreColor = new Color32(255, 255, 0, 255);

        private static readonly Color ActionBaseColor = new Color32(64, 64, 64, 255);
        private static readonly Color ActionCoreColor = new Color32(30, 144, 255, 255);

        private MaterialPropertyBlock _hpPropertyBlock;
        private MaterialPropertyBlock _moralePropertyBlock;
        private MaterialPropertyBlock _fatiguePropertyBlock;
        private MaterialPropertyBlock _actionPropertyBlock;

        private UnitData _currentUnitData;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int CoreColor = Shader.PropertyToID("_CoreColor");
        private static readonly int Progress = Shader.PropertyToID("_Progress");
        private static readonly int Threshold = Shader.PropertyToID("_Threshold");
        private static readonly int ThresholdColor = Shader.PropertyToID("_ThresholdColor");

        private float HpProgressValue => _currentUnitData == null
            ? 1
            : (float)_currentUnitData.NowHp / _currentUnitData.armDataType.totalHp;

        private float MoraleProgressValue => _currentUnitData == null
            ? 1
            : (float)_currentUnitData.NowMorale / _currentUnitData.armDataType.maximumMorale;

        private float FatigueProgressValue => _currentUnitData == null
            ? 1
            : (float)_currentUnitData.NowFatigue / _currentUnitData.armDataType.maximumFatigue;

        private float ActionProgressValue => _currentUnitData == null
            ? 1
            : (float)_currentUnitData.NowActionPoints / Constants.INIT_ACTION_POINTS;

        private void ProgressInit()
        {
            _hpPropertyBlock = new MaterialPropertyBlock();
            _hpPropertyBlock.SetColor(BaseColor, HpBaseColor);
            _hpPropertyBlock.SetColor(CoreColor, HpCoreColor);

            _moralePropertyBlock = new MaterialPropertyBlock();
            _moralePropertyBlock.SetColor(BaseColor, MoraleBaseColor);
            _moralePropertyBlock.SetColor(CoreColor, MoraleCoreColor);

            _fatiguePropertyBlock = new MaterialPropertyBlock();
            _fatiguePropertyBlock.SetColor(BaseColor, FatigueBaseColor);
            _fatiguePropertyBlock.SetColor(CoreColor, FatigueCoreColor);

            _actionPropertyBlock = new MaterialPropertyBlock();
            _actionPropertyBlock.SetColor(BaseColor, ActionBaseColor);
            _actionPropertyBlock.SetColor(CoreColor, ActionCoreColor);

            _hpPropertyBlock.SetFloat(Progress, HpProgressValue);
            _moralePropertyBlock.SetFloat(Progress, MoraleProgressValue);
            _fatiguePropertyBlock.SetFloat(Progress, FatigueProgressValue);
            _actionPropertyBlock.SetFloat(Progress, ActionProgressValue);

            // _hpPropertyBlock.SetFloat(Threshold, 0.01f);
            // _hpPropertyBlock.SetColor(ThresholdColor, HpCoreColor * 10);
            // _moralePropertyBlock.SetFloat(Threshold, 0.01f);
            // _moralePropertyBlock.SetColor(ThresholdColor, MoraleCoreColor * 10);
            // _fatiguePropertyBlock.SetFloat(Threshold, 0.01f);
            // _fatiguePropertyBlock.SetColor(ThresholdColor, FatigueCoreColor * 10);
            // _actionPropertyBlock.SetFloat(Threshold, 0.01f);
            // _actionPropertyBlock.SetColor(ThresholdColor, ActionCoreColor * 10);

            hpProgress.SetPropertyBlock(_hpPropertyBlock);
            moraleProgress.SetPropertyBlock(_moralePropertyBlock);
            fatigueProgress.SetPropertyBlock(_fatiguePropertyBlock);
            actionProgress.SetPropertyBlock(_actionPropertyBlock);
        }

        public void OnInit(int unitId)
        {
            OnInit(this.GetModel<IFightCreateModel>().AllLegions[Constants.PLAY_LEGION_ID].allUnit[unitId]);
        }

        public void OnInit(UnitData unitData)
        {
            _currentUnitData = unitData;
            peopleCurrentNum.text = _currentUnitData.NowTroops.ToString();
            peopleTotalNum.text = _currentUnitData.armDataType.totalTroops.ToString();
            ProgressInit();
        }

        public void UpdateProgress()
        {
            if (_currentUnitData.NowTroops != int.Parse(peopleCurrentNum.text))
            {
                peopleCurrentNum.text = _currentUnitData.NowTroops.ToString();
            }

            DoValue(HpProgressValue, hpProgress, _hpPropertyBlock);
            DoValue(MoraleProgressValue, moraleProgress, _moralePropertyBlock);
            DoValue(FatigueProgressValue, fatigueProgress, _fatiguePropertyBlock);
            DoValue(ActionProgressValue, actionProgress, _actionPropertyBlock);
        }

        private void DoValue(float endValue, SpriteRenderer sprite, MaterialPropertyBlock block, float duration = 0.3f)
        {
            float beginValue = block.GetFloat(Progress);
            DOTween.To(() => beginValue, value =>
            {
                beginValue = value;
                block.SetFloat(Progress, value);
                sprite.SetPropertyBlock(block);
            }, endValue, duration);
        }

        private void OnValidate()
        {
            _currentUnitData = null;
            ProgressInit();
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void ChangeOrderLayer(int beginIndex)
        {
            unitProgressBg.sortingOrder = beginIndex++;
            peopleCurrentNum.sortingOrder = peopleTotalNum.sortingOrder = hpProgress.sortingOrder =
                moraleProgress.sortingOrder = fatigueProgress.sortingOrder = actionProgress.sortingOrder = beginIndex;
        }
    }
}