using System.Collections.Generic;
using Game.Country;
using Game.Family;
using Game.GameMenu;
using Game.GameSave;
using Game.Map;
using Game.Player;
using Game.Team;
using Game.Town;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameBase
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
            this.SendEvent(new ChangeToMenuSceneEvent());
            SceneManager.LoadScene("MenuScene");
        }

        public void ChangeMainGameScene(string fileName = null)
        {
            this.SendEvent(new ChangeToMainGameSceneEvent());
            LoadCurrentData();
            this.GetSystem<IGameSaveSystem>().LoadGame(fileName);
            LoadNoStorageData();
            InitNewGameData(fileName);
            SceneManager.LoadScene("MainGameScene");
        }

        public void ChangeGameCreateScene()
        {
            this.SendEvent(new ChangeToGameCreateSceneEvent());
            SceneManager.LoadScene("CreateGameScene");
        }

        public string GetDataName(List<string> nameList)
        {
            if (nameList.Count <= this.GetModel<IGameMenuModel>().Language)
            {
                return nameList[0];
            }

            return nameList[this.GetModel<IGameMenuModel>().Language];
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
            var townTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.TOWN_INFORMATION);
            var townNameTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.TOWN_NAME);
            var familyTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.FAMILY_INFORMATION);
            var familyNameTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.FAMILY_NAME);
            var roleTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.ROLE_INFORMATION);
            var roleNameTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.ROLE_NAME);
            var countryAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.COUNTRY_INFORMATION);
            var countryNameTextAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.COUNTRY_NAME);
            var mapMeshAsset = resLoader.LoadSync<TextAsset>(MapConfigurationTableConstant.MAP_MESH_INFORMATION);
            this.GetSystem<ITownSystem>().InitTownCommonData(townTextAsset, townNameTextAsset);
            this.GetSystem<IFamilySystem>().InitFamilyCommonData(familyTextAsset, familyNameTextAsset);
            this.GetSystem<IFamilySystem>().InitRoleCommonData(roleTextAsset, roleNameTextAsset);
            this.GetSystem<ICountrySystem>().InitCountryCommonData(countryAsset, countryNameTextAsset);
            this.GetSystem<IMapSystem>().InitMapMeshData(mapMeshAsset);
            _hasLoadCurrentData = true;
        }

        /// <summary>
        /// 初始化不要保存的数据，这些数据都是由需要保存的数据算出，所以要在存档读取完成以后调用system计算
        /// </summary>
        private void LoadNoStorageData()
        {
            this.GetSystem<ITownSystem>().InitTownNoStorageData();
            this.GetSystem<IFamilySystem>().InitFamilyNoStorageData();
        }

        /// <summary>
        /// 初始化新游戏的数据
        /// </summary>
        private void InitNewGameData(string fileName)
        {
            if (fileName != null)
            {
                return;
            }

            CreateGameData data = this.GetModel<IMyPlayerModel>().CreateGameData;
            int roleId = this.GetSystem<IFamilySystem>().AddNewRole(new RoleData
            {
                name = new List<string> { data.playerName },
                roleAge = data.playerAge
            });
            this.GetModel<IMyPlayerModel>().RoleId = roleId;

            int familyId = this.GetSystem<IFamilySystem>().AddNewFamily(new FamilyData(new FamilyDataStorage
            {
                name = new List<string> { data.familyName },
                familyWealth = 1000, //家族开局默认1000
                familyLevel = 1,
                countryId = -1, //开局没有效忠的国家
                familyLeaderId = roleId,
                familyRoleS = new List<int>(roleId),
                familyTownS = new List<int>()
            }));
            this.GetModel<IMyPlayerModel>().FamilyId = familyId;

            int teamId = this.GetSystem<ITeamSystem>().AddTeam(new TeamData
            {
                generalRoleId = roleId,
                number = 1,
                teamType = TeamType.HUT_FIELD
            });
            this.GetModel<IMyPlayerModel>().TeamId = teamId;
        }
    }
}