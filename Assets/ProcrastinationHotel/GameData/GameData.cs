using System.Collections;
using UnityEngine;

namespace HiryuTK.GameRoomService
{
    public static class GameData
    {
        //Has save file
        public static bool HasSaveFile;
        private static string keyHasSave = "HasSave";

        //Game stats
        public static int Money;
        public static bool Achivement_CatLover;
        public static bool Achivement_DanceFreak;
        public static bool Achivement_CombatLegend;
        private static string keyMoney = "Money";
        private static string keyCatLover = "CatLover";
        private static string keyDanceFreak = "DanceFreak";
        private static string keyCombatLegend = "CombatLegend";

        //Combat stats
        public static int Health = 100;
        public static int Attack = 1;
        public static int Defence = 1;
        private static string keyHealth = "Health";
        private static string keyAttack = "Attack";
        private static string keyDefence = "Defence";

        //Cat
        public static string CatName;
        public static int CatHunger = 0;
        public static int CatThirst = 0;
        public static int CatMood = 0;
        public static int CatCleanliness = 0;
        private static string keyCatName = "CatName";
        private static string keyHunger = "Hunger";
        private static string keyThirst = "Thirst";
        private static string keyIrritation = "Irritation";
        private static string keyCleanliness = "Cleanliness";

        //DDR
        public static int DDRHighScore = 0;
        private static string keyDDRHighScore = "DDRHighScore";

        public static void SaveData()
        {
            PlayerPrefs.SetInt(keyHasSave, HasSaveFile ? 0 : 1);
            PlayerPrefs.SetInt(keyMoney, Money);
            PlayerPrefs.SetInt(keyCatLover, Achivement_CatLover ? 1 : 0);
            PlayerPrefs.SetInt(keyDanceFreak, Achivement_DanceFreak ? 1 : 0);
            PlayerPrefs.SetInt(keyCombatLegend, Achivement_CombatLegend ? 1 : 0);

            PlayerPrefs.SetInt(keyHealth, Health);
            PlayerPrefs.SetInt(keyAttack, Attack);
            PlayerPrefs.SetInt(keyDefence, Defence);

            PlayerPrefs.SetString(keyThirst, CatName);
            PlayerPrefs.SetInt(keyHunger, CatHunger);
            PlayerPrefs.SetInt(keyThirst, CatThirst);
            PlayerPrefs.SetInt(keyIrritation, CatMood);
            PlayerPrefs.SetInt(keyCleanliness, CatCleanliness);
             
            PlayerPrefs.SetInt(keyDDRHighScore, DDRHighScore);
        }

        public static void LoadData()
        {
            HasSaveFile = PlayerPrefs.GetInt(keyHasSave, 0) == 0 ? false : true;
            Money = PlayerPrefs.GetInt(keyMoney, 999);
            Achivement_CatLover = PlayerPrefs.GetInt(keyCatLover, 0) == 0 ? false : true;
            Achivement_DanceFreak = PlayerPrefs.GetInt(keyDanceFreak, 0) == 0 ? false : true;
            Achivement_CombatLegend = PlayerPrefs.GetInt(keyCombatLegend, 0) == 0 ? false : true;

            Health = PlayerPrefs.GetInt(keyHealth, 100);
            Attack = PlayerPrefs.GetInt(keyAttack, 1);
            Defence = PlayerPrefs.GetInt(keyDefence, 1);

            CatName = PlayerPrefs.GetString(keyCatName, "MeowDog");
            CatHunger = PlayerPrefs.GetInt(keyHunger, 0);
            CatThirst = PlayerPrefs.GetInt(keyThirst, 0);
            CatMood = PlayerPrefs.GetInt(keyIrritation, 0);
            CatCleanliness = PlayerPrefs.GetInt(keyCleanliness, 0);

            DDRHighScore = PlayerPrefs.GetInt(keyDDRHighScore, 0);
        }

        public static void ResetData()
        {
            HasSaveFile = false;
            Money = 20;
            Achivement_CatLover = false;
            Achivement_DanceFreak = false;
            Achivement_CombatLegend = false;

            Health = 100;
            Attack = 1;
            Defence = 1;

            CatName = "MeowDog";
            CatHunger = 40;
            CatThirst = 40;
            CatMood = 40;
            CatCleanliness = 40;

            DDRHighScore = 0;
        }
    }
}