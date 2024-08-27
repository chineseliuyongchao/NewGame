using System;
using System.Collections.Generic;
using Battle.Country;
using Battle.Family;
using Battle.Map;
using Battle.Player;
using Battle.Team;
using Battle.Town;
using Game.GameMenu;
using Game.GameSave;
using Game.GameUtils;
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

        /// <summary>
        /// 当前场景
        /// </summary>
        private SceneType _sceneType;

        protected override void OnInit()
        {
            _sceneType = SceneType.MENU_SCENE;
        }

        public void ChangeBattleScene(string fileName = null)
        {
            this.GetSystem<IGameSaveSystem>().LoadGame(fileName);
            LoadNoStorageData();
            InitNewGameData(fileName);
            ChangeScene(SceneType.BATTLE_SCENE);
        }

        public void ChangeScene(SceneType type)
        {
            switch (_sceneType)
            {
                case SceneType.MENU_SCENE:
                    this.SendEvent(new ChangeMenuSceneEvent(false));
                    break;
                case SceneType.CREATE_BATTLE_SCENE:
                    this.SendEvent(new ChangeBattleCreateSceneEvent(false));
                    break;
                case SceneType.BATTLE_SCENE:
                    this.SendEvent(new ChangeBattleSceneEvent(false));
                    break;
                case SceneType.CREATE_FIGHT_SCENE:
                    this.SendEvent(new ChangeFightCreateSceneEvent(false));
                    break;
                case SceneType.FIGHT_SCENE:
                    this.SendEvent(new ChangeFightSceneEvent(false));
                    break;
            }

            switch (type)
            {
                case SceneType.MENU_SCENE:
                    SceneManager.LoadScene("MenuScene");
                    this.SendEvent(new ChangeMenuSceneEvent(true));
                    break;
                case SceneType.CREATE_BATTLE_SCENE:
                    SceneManager.LoadScene("CreateBattleScene");
                    this.SendEvent(new ChangeBattleCreateSceneEvent(true));
                    break;
                case SceneType.BATTLE_SCENE:
                    SceneManager.LoadScene("BattleScene");
                    this.SendEvent(new ChangeBattleSceneEvent(true));
                    break;
                case SceneType.CREATE_FIGHT_SCENE:
                    SceneManager.LoadScene("CreateFightScene");
                    this.SendEvent(new ChangeFightCreateSceneEvent(true));
                    break;
                case SceneType.FIGHT_SCENE:
                    SceneManager.LoadScene("FightScene");
                    this.SendEvent(new ChangeFightSceneEvent(true));
                    break;
            }

            _sceneType = type;
        }

        public string GetDataName(List<string> nameList)
        {
            if (nameList.Count <= this.GetModel<IGameMenuModel>().Language)
            {
                return nameList[0];
            }

            return nameList[this.GetModel<IGameMenuModel>().Language];
        }

        public string GetLocalizationText(int textId, List<string> replace = null,
            LocalizationType type = LocalizationType.NORMAL)
        {
            LocalizationData localizationData;
            switch (type)
            {
                case LocalizationType.NORMAL:
                    localizationData = this.GetModel<IGameModel>().LocalizationData[textId];
                    break;
                case LocalizationType.DIALOGUE:
                    localizationData = this.GetModel<IGameModel>().DialogueLocalizationData[textId];
                    break;
                case LocalizationType.DIALOGUE_TIP:
                    localizationData = this.GetModel<IGameModel>().DialogueTipLocalizationData[textId];
                    break;
                default:
                    localizationData = this.GetModel<IGameModel>().LocalizationData[textId];
                    break;
            }

            string text = this.GetSystem<IGameSystem>().GetDataName(new List<string>
                { localizationData.Chinese, localizationData.English });
            object[] value;
            if (replace == null)
            {
                value = Array.Empty<object>();
            }
            else
            {
                value = new object[replace.Count];
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = replace[i];
                }
            }

            return string.Format(text, value);
        }

        public void LoadCurrentData()
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

            var localizationAsset = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.LOCALIZATION_TEXT);
            var dialogueLocalizationText =
                resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.DIALOGUE_LOCALIZATION_TEXT);
            var dialogueTipLocalizationText =
                resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.DIALOGUE_TIP_LOCALIZATION_TEXT);

            var troopsNumber = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.TROOPS_NUMBER);
            var factionInformation = resLoader.LoadSync<TextAsset>(ConfigurationTableConstant.FACTION_INFORMATION);

            this.GetSystem<ITownSystem>().InitTownCommonData(townTextAsset, townNameTextAsset);
            this.GetSystem<IFamilySystem>().InitFamilyCommonData(familyTextAsset, familyNameTextAsset);
            this.GetSystem<IFamilySystem>().InitRoleCommonData(roleTextAsset, roleNameTextAsset);
            this.GetSystem<ICountrySystem>().InitCountryCommonData(countryAsset, countryNameTextAsset);

            this.GetSystem<IMapSystem>().InitMapMeshData(mapMeshAsset);

            this.GetSystem<IGameSystem>().InitLocalizationData(localizationAsset, dialogueLocalizationText,
                dialogueTipLocalizationText);

            this.GetSystem<IGameMenuSystem>().InitArmData(troopsNumber);
            this.GetSystem<IGameMenuSystem>().InitFactionData(factionInformation);
            _hasLoadCurrentData = true;
        }

        public void InitLocalizationData(TextAsset localizationAsset, TextAsset dialogueLocalizationAsset,
            TextAsset dialogueTipLocalizationAsset)
        {
            this.GetUtility<IGameUtility>()
                .AnalysisJsonConfigurationTable(localizationAsset, this.GetModel<IGameModel>().LocalizationData);
            this.GetUtility<IGameUtility>().AnalysisJsonConfigurationTable(dialogueLocalizationAsset,
                this.GetModel<IGameModel>().DialogueLocalizationData);
            this.GetUtility<IGameUtility>().AnalysisJsonConfigurationTable(dialogueTipLocalizationAsset,
                this.GetModel<IGameModel>().DialogueTipLocalizationData);
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