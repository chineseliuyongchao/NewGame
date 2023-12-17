using System.IO;
using Game.Game;
using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public class GameSaveSystem : AbstractSystem, IGameSaveSystem
    {
        protected override void OnInit()
        {
        }

        public void SaveGame(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME))
            {
                Directory.CreateDirectory(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME);
            }

            StreamWriter file = File.CreateText(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME + "/" +
                                                fileName + ".txt");
            file.Write("dataToWrite");
            file.Close();
        }

        public void LoadGame(string fileName)
        {
            StreamReader file = File.OpenText(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME + "/" +
                                              fileName + ".txt");
            string fileContent = file.ReadToEnd();
            Debug.Log(fileContent);
            file.Close();
        }

        public void DeleteGame(string fileName)
        {
            File.Delete(Application.persistentDataPath + GameConstant.SAVE_FOLDER_NAME +
                        "/" + fileName + ".txt");
        }
    }
}