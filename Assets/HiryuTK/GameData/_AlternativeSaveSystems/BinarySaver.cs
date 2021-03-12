using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Binary files are good for local game saves

namespace HiryuTK.GameDataManagement
{
    public class BinarySaver : MonoBehaviour
    {
        const string FileName = "/playerSaveData.save";
        private string savePath;

        GameDataSerializable testSaveObject = new GameDataSerializable(new GameData());

        private void Awake()
        {
            savePath = Application.persistentDataPath + FileName;
            SaveGame(testSaveObject);
        }

        public void SaveGame(GameDataSerializable gameData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create(savePath)) //The extension doesn't matter
            {
                formatter.Serialize(stream, gameData);
                Debug.Log("Saved binary file to " + savePath);
            }
        }

        public GameDataSerializable TryLoadGame()
        {
            if (File.Exists(savePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = File.Open(savePath, FileMode.Open))
                {
                    GameDataSerializable gameData = (GameDataSerializable)formatter.Deserialize(stream);
                    Debug.Log("Loaded binary file from " + savePath);
                    return gameData;
                }
            }
            else
            {
                Debug.Log("Try loading binary save file failed, file does not exist at location " + savePath);
                return null;
            }
        }
    }

    public static class SaveLoad
    {
        const string FileName = "/savedGames.gd";

        public static List<GameDataSerializable> savedGames = new List<GameDataSerializable>();

        public static void Save(GameDataSerializable game)
        {
            savedGames.Add(game);
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream file = File.Create(Application.persistentDataPath + FileName))
            {
                formatter.Serialize(file, SaveLoad.savedGames);
            }
        }

        public static void Load()
        {
            if (File.Exists(Application.persistentDataPath + FileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream file = File.Open(Application.persistentDataPath + FileName, FileMode.Open))
                {
                    //Deserialize passes back an Object type, so we have to cast it.
                    SaveLoad.savedGames = (List<GameDataSerializable>)formatter.Deserialize(file);
                }
            }
        }
    }

}
