using System.Collections;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public static class GameData
    {
        //Has save file
        public static bool HasSaveFile;
        private static string keyHasSave = "HasSave";

        //Front desk
        public static int Money;
        public static bool Achivement_CatLover;
        public static bool Achivement_DanceFreak;
        public static bool Achivement_CombatLegend;
        private static string keyMoney = "Money";
        private static string keyCatLover = "CatLover";
        private static string keyDanceFreak = "DanceFreak";
        private static string keyCombatLegend = "CombatLegend";

        //Combat 
        public static int Health = 100;
        private static string keyHealth = "Health";

        //Cat
        public static string CatName;
        public static int CatHunger = 0;
        public static int CatThirst = 0;
        public static int CatMood = 0;
        public static int CatCleanliness = 0;
        private static string keyCatName = "CatName";
        private static string keyHunger = "Hunger";
        private static string keyThirst = "Thirst";
        private static string keyMood = "Irritation";
        private static string keyCleanliness = "Cleanliness";

        //DDR
        public static int DDRHighScore = 0;
        private static string keyDDRHighScore = "DDRHighScore";

        public static void SaveData()
        {
            //Front desk
            PlayerPrefs.SetInt(keyHasSave, HasSaveFile ? 0 : 1);
            PlayerPrefs.SetInt(keyMoney, Money);
            PlayerPrefs.SetInt(keyCatLover, Achivement_CatLover ? 1 : 0);
            PlayerPrefs.SetInt(keyDanceFreak, Achivement_DanceFreak ? 1 : 0);
            PlayerPrefs.SetInt(keyCombatLegend, Achivement_CombatLegend ? 1 : 0);

            //Combat
            PlayerPrefs.SetInt(keyHealth, Health);

            //Cat
            PlayerPrefs.SetString(keyCatName, CatName);
            PlayerPrefs.SetInt(keyHunger, CatHunger);
            PlayerPrefs.SetInt(keyThirst, CatThirst);
            PlayerPrefs.SetInt(keyMood, CatMood);
            PlayerPrefs.SetInt(keyCleanliness, CatCleanliness);

            //DDR
            PlayerPrefs.SetInt(keyDDRHighScore, DDRHighScore);
        }

        public static void LoadData()
        {
            //Front desk
            HasSaveFile = PlayerPrefs.GetInt(keyHasSave, 0) == 0 ? false : true;
            Money = PlayerPrefs.GetInt(keyMoney, 0);
            Achivement_CatLover = PlayerPrefs.GetInt(keyCatLover, 0) == 0 ? false : true;
            Achivement_DanceFreak = PlayerPrefs.GetInt(keyDanceFreak, 0) == 0 ? false : true;
            Achivement_CombatLegend = PlayerPrefs.GetInt(keyCombatLegend, 0) == 0 ? false : true;

            //Combat 
            Health = PlayerPrefs.GetInt(keyHealth, 100);

            //Cat
            CatName = PlayerPrefs.GetString(keyCatName, "MeowBox");
            CatHunger = PlayerPrefs.GetInt(keyHunger, 40);
            CatThirst = PlayerPrefs.GetInt(keyThirst, 40);
            CatMood = PlayerPrefs.GetInt(keyMood, 40);
            CatCleanliness = PlayerPrefs.GetInt(keyCleanliness, 40);

            //DDR
            DDRHighScore = PlayerPrefs.GetInt(keyDDRHighScore, 0);
             
            Debug.Log("Loaded data. Achivement_CatLover: "  + Achivement_CatLover);
        }

        public static void ResetData()
        {
            HasSaveFile = false;
            Money = 20;
            Achivement_CatLover = false;
            Achivement_DanceFreak = false;
            Achivement_CombatLegend = false;

            Health = 100;

            CatName = "MeowDog";
            CatHunger = 40;
            CatThirst = 40;
            CatMood = 40;
            CatCleanliness = 40;

            DDRHighScore = 0;
        }
    }
}