//using UnityEngine;
//using System;
//using System.IO;
//using System.Collections;
//using System.Collections.Generic;
//using LitJson;

//namespace HiryuTK.GameData
//{
//    static class LitJsonSaver
//    {
//        static Dictionary<int, string> resourceSubpath = new Dictionary<int, string>
//        {
//            {1, "/Resources/Event1.json" }
//        };

//        public static NarrativeEvent GetSceneNarrative(int sceneNumber)
//        {
//            string subpath = GetSubpath(sceneNumber);
//            string root = Application.dataPath;

//            if (IsValidJSON(subpath) == true)
//            {
//                //1. Get the json file's strings
//                string jsonString = File.ReadAllText(root + subpath);

//                //2. Map it to Narrative event
//                NarrativeEvent narrativeEvent = JsonMapper.ToObject<NarrativeEvent>(jsonString);

//                return narrativeEvent;
//            }
//            else //if not valid
//            {
//                throw new Exception("The JSON is not valid, check the schema and file extension.");
//            }
//        }

//        static string GetSubpath(int sceneNumber)
//        {
//            string pathResult;
//            if (resourceSubpath.TryGetValue(sceneNumber, out pathResult))
//            {
//                return pathResult;
//            }
//            else
//            {
//                throw new Exception("The scene number is not in the resource list.");
//            }

//        }

//        static bool IsValidJSON(string path)
//        {
//            return (WaypointPath.GetExtension(path) == ".json") ? true : false;
//        }
//    }

//}
