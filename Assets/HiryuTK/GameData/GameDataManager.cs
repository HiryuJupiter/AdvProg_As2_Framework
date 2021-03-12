using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HiryuTK.GameDataManagement
{
    /// <summary>
    /// A persistent game object that loads  GameData
    /// </summary>
    public class GameDataManager : Singleton<GameDataManager>
    {
        private GameData data;
        public GameData Data => data;
        private static string SavePath => Application.persistentDataPath + "/player.save";
        public static bool HasSaveFile() => File.Exists(SavePath);



        public void SaveGameData()
        {
            //Formatter converts a class into a stream, FileStream writes the stream to file
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(SavePath, FileMode.Create);

            GameDataSerializable serializableData = new GameDataSerializable(data);

            formatter.Serialize(stream, serializableData);
            stream.Close();
        }

        public void LoadGameData(int defaultScene = 2)
        {
            if (HasSaveFile())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(SavePath, FileMode.Open);

                GameDataSerializable serializableData = formatter.Deserialize(stream) as GameDataSerializable;
                stream.Close();

                data = serializableData.ConvertToGameData();
            }
            else
            {
                Debug.LogWarning("Saved game data doesn't exist. Player shouldn't have been able to load in the first place.");

                data.currentSceneIndex = defaultScene;
                data.playerLevel = 1;
                data.playerHealth = 100;
                data.playerPosition = new Vector3(0f, 0f, 0f);
            }
        }

        void Awake()
        {
            //Singleton
            DeleteDuplicateSingleton();
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void ClearSave()
        {
            if (HasSaveFile())
            {
                File.Delete(SavePath);
            }
        }
    }
}