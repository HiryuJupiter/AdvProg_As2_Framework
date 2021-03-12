using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

//Json are good for sending to another device over server
//Slower than binary
//Supports MonoBehaviour, ScriptableObject, [Serializable] class/struct
//Dictionary are not supported
//Cannot directly send primitive types or array, you have to wrap such types in a class or struct

namespace HiryuTK.GameDataManagement
{
    
    public static class JsonSaver
    {
        public static void SaveAsJSON(GameDataSerializable saveObject)
        {
            string path = Application.persistentDataPath + "JsonSaveFile.json";

            string json = JsonUtility.ToJson(saveObject);
            using (StreamWriter sw = File.CreateText(path))
            {
                File.WriteAllText(path, json);
            }

            Debug.Log("Saving as JSON: " + json);

            //Load
            string jsonLoad = File.ReadAllText(path);
            GameDataSerializable playerData = JsonUtility.FromJson<GameDataSerializable>(jsonLoad);

            //Load (deserialize) into an already created object
            JsonUtility.FromJsonOverwrite(json, playerData);
        }

        public static void SaveJsonAsPlayerPrefs(GameDataSerializable saveObject)
        {
            //Save
            string jsonData = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString("PlayerData", jsonData);

            //Read
            var jsonFromPrefs = PlayerPrefs.GetString("PlayerData");
            GameDataSerializable loadedData = JsonUtility.FromJson<GameDataSerializable>(jsonFromPrefs);
        }
    }
}

