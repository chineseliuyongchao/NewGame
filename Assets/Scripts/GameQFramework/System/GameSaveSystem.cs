using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace GameQFramework
{
    public class GameSaveSystem : AbstractSystem, IGameSaveSystem
    {
        private readonly List<ISaveModel> _saveModels = new();

        protected override void OnInit()
        {
        }

        public void SaveGame(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME))
            {
                Directory.CreateDirectory(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME);
            }

            AddGameFileData(fileName);
            StreamWriter file = File.CreateText(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME + "/" +
                                                fileName + ".txt");
            GameData gameData = new GameData();
            for (int i = 0; i < _saveModels.Count; i++)
            {
                string key = _saveModels[i].ModelName();
                string json = JsonUtility.ToJson(_saveModels[i].SaveModel());
                gameData.DataKey.Add(key);
                gameData.DataValue.Add(json);
            }

            string str = JsonUtility.ToJson(gameData);
            file.Write(str);
            file.Close();
        }

        public void LoadGame(string fileName)
        {
            if (fileName == null) //如果是空，证明是新存档
            {
                for (int i = 0; i < _saveModels.Count; i++)
                {
                    _saveModels[i].InitializeModel();
                }
            }
            else
            {
                StreamReader file = File.OpenText(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME + "/" +
                                                  fileName + ".txt");
                string fileContent = file.ReadToEnd();
                GameData gameData = JsonUtility.FromJson<GameData>(fileContent);
                Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
                for (int i = 0; i < gameData.DataKey.Count; i++)
                {
                    dataDictionary.Add(gameData.DataKey[i], gameData.DataValue[i]);
                }

                for (int i = 0; i < _saveModels.Count; i++)
                {
                    string key = _saveModels[i].ModelName();
                    _saveModels[i].LoadModel(dataDictionary[key]);
                }

                file.Close();
            }
        }

        public void DeleteGame(string fileName)
        {
            DeleteGameFileData(fileName);
            File.Delete(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME +
                        "/" + fileName + ".txt");
        }

        public void AddSaveModel(ISaveModel saveModel)
        {
            _saveModels.Add(saveModel);
        }

        /// <summary>
        /// 添加游戏存档的记录
        /// </summary>
        private void AddGameFileData(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME))
            {
                Directory.CreateDirectory(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME);
            }

            List<string> list = LoadGameFileList();
            list.Insert(0, fileName);
            SaveGameFileList(list);
        }

        public List<string> LoadGameFileList()
        {
            List<string> list = new List<string>();
            if (File.Exists(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME +
                            GameConstant.GAME_FILE_DATA))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME +
                                            GameConstant.GAME_FILE_DATA, FileMode.Open);
                list = JsonUtility.FromJson<GameFileData>((string)formatter.Deserialize(file)).fileData;
                file.Close();
            }

            return list;
        }

        /// <summary>
        /// 把游戏存档数据写入文件
        /// </summary>
        private void SaveGameFileList(List<string> list)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME +
                                          GameConstant.GAME_FILE_DATA);
            var json = JsonUtility.ToJson(new GameFileData { fileData = list });
            binaryFormatter.Serialize(file, json);
            file.Close();
        }

        /// <summary>
        /// 删除游戏存档的记录
        /// </summary>
        private void DeleteGameFileData(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME))
            {
                Directory.CreateDirectory(Application.persistentDataPath + GameConstant.SAVE_GAME_DATA_FOLDER_NAME);
            }

            List<string> list = LoadGameFileList();
            if (list.Remove(fileName))
            {
                SaveGameFileList(list);
            }
        }
    }

    /// <summary>
    /// 用于保存所有数据
    /// </summary>
    public class GameData
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public List<string> DataKey = new();

        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public List<string> DataValue = new();
    }

    /// <summary>
    /// 保存所有存档信息
    /// </summary>
    [Serializable]
    public class GameFileData
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public List<string> fileData = new();
    }
}