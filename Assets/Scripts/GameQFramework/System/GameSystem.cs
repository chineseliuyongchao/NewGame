using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Constant;

namespace GameQFramework
{
    public class GameSystem : AbstractSystem, IGameSystem
    {
        /// <summary>
        /// 记录是否初始化通用数据（即所有存档共用的数据，比如地图，聚落位置等，只需要在第一次加载存档时加载）
        /// </summary>
        private bool _hasLoadCurrentData;

        protected override void OnInit()
        {
        }

        public void ChangeMenuScene()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void ChangeMainGameScene(string fileName = null)
        {
            this.SendEvent(new ChangeToMainGameSceneEvent());
            LoadCurrentData();
            this.GetSystem<IGameSaveSystem>().LoadGame(fileName);
            SceneManager.LoadScene("MainGameScene");
        }

        /// <summary>
        /// 初始化通用数据
        /// </summary>
        private void LoadCurrentData()
        {
            if (_hasLoadCurrentData)
            {
                return;
            }

            ResLoader resLoader = ResLoader.Allocate();
            var townTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.SETTLEMENT_INFORMATION);
            var familyTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.FAMILY_INFORMATION);
            var roleTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.ROLE_INFORMATION);
            var countryAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.COUNTRY_INFORMATION);
            var mapMeshAsset = resLoader.LoadSync<TextAsset>(MapConfigurationTableConstant.MAP_MESH_INFORMATION);
            this.GetSystem<ITownSystem>().InitTownCommonData(townTextAsset);
            this.GetSystem<IFamilySystem>().InitFamilyCommonData(familyTextAsset);
            this.GetSystem<IFamilySystem>().InitRoleCommonData(roleTextAsset);
            this.GetSystem<ICountrySystem>().InitCountryCommonData(countryAsset);
            this.GetSystem<IMapSystem>().InitMapMeshData(mapMeshAsset);
            _hasLoadCurrentData = true;
        }
    }
}