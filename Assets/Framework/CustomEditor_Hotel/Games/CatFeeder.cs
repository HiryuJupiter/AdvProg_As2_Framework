using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public class CatFeeder : EditorWindow
    {
        private const int PositionPhaseMax = 5;
        private const int FoodCost = 2;
        private const int WaterCost = 1;
        private const string CatFaceString = "[^._.^] ﾉ彡";

        private const string CatImg1 = "" +
        "          /^--^\\     \n" +
        "          \\_____/   \n" +
        "          /       \\  \n" +
        "         |         |  \n" +
        "          \\__ __/   \n" +
        "             \\ \\   \n" +
        "              \\ \\  \n" +
        "             / /     \n" +
        "             \\/     \n";

        private const string CatImg2 = "" +
       "          /^--^\\     \n" +
       "          \\_____/   \n" +
       "          /       \\  \n" +
       "         |         |  \n" +
       "          \\__ __/   \n" +
       "             / /   \n" +
       "           / /  \n" +
       "            \\ \\     \n" +
       "             \\/     \n";

        private string catImage = CatImg1;
        private bool isPlayingImageOne;
        private string givenCatName;

        //Timers
        private int hungerTimer = 24;
        private int thirstTimer = 16;
        private int moodTimer = 12;
        private int cleanlinessTimer = 48;

        //Cat location
        private int positionPhase = PositionPhaseMax;

        //Log
        private string logText;

        #region Start and end
        public void Initialize()
        {
            givenCatName = GameData.CatName;
            logText = "";
        }

        private void OnDestroy()
        {
            GameData.CatName = givenCatName;
            GameData.SaveData();
        }
        #endregion

        private void OnGUI()
        {
            //Cat info
            EditorGUILayout.Space(20);
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(catImage, GUILayout.Height(150), GUILayout.Width(100));

            GUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Cat name", EditorStyles.boldLabel);
                givenCatName = EditorGUILayout.TextField(givenCatName, GUILayout.Width(150));
                EditorGUILayout.Space(20);
                EditorGUILayout.LabelField("Food ------------------", GameData.CatHunger.ToString());
                EditorGUILayout.LabelField("Hydration ------------------", GameData.CatThirst.ToString());
                EditorGUILayout.LabelField("Mood ------------------", GameData.CatMood.ToString());
                EditorGUILayout.LabelField("Cleanliness ------------------", GameData.CatCleanliness.ToString());
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            //Cat scrub
            CatScrub();

            //Food buttons
            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"Buy food ($ {FoodCost})", GUILayout.Width(100), GUILayout.Height(40)))
            {
                BuyFood();
            }
            if (GUILayout.Button($"Buy water ($ {WaterCost})", GUILayout.Width(100), GUILayout.Height(40)))
            {
                BuyWater();
            }
            GUILayout.EndHorizontal();
            ShowCatButtons();

            if (logText != null)
            {
                EditorGUILayout.Space(20);
                var style = new GUIStyle(GUI.skin.label)
                { alignment = TextAnchor.MiddleCenter };
                EditorGUILayout.LabelField(logText, style);
            }
        }
              

        #region Play with cat
        void ShowCatButtons()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < PositionPhaseMax; i++)
            {
                if (i == positionPhase)
                {
                    if (GUILayout.Button(CatFaceString, GUILayout.Height(30)))
                    {
                        PlayedWithCat();
                    }
                }
                else
                {
                    if (GUILayout.Button("", GUILayout.Height(30)))
                    {
                        logText = "";
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        void PlayedWithCat()
        {
            if (GameData.CatMood < 100)
            {
                GameData.CatMood += 5;
                if (GameData.CatMood > 100)
                    GameData.CatMood = 100;
                CheckAchievement();
                logText = "You've chased the cat, cat is happy!";
            }
            else
            {
                logText = "You've played with the cat, cat is super happy!";
            }
        }

        int positionTimer;
        void TickPositionPhase()
        {
            //Timer
            if (positionTimer > 0)
            {
                positionTimer--;
                return;
            }
            positionTimer = 2;

            //Modify phase
            if (positionPhase <= 0)
            {
                positionPhase = PositionPhaseMax;
            }
            else
            {
                positionPhase--;
            }
            Repaint();
        }
        #endregion

        #region Scrub cat
        bool scrubbedLeft = true;
        float scrub;
        void CatScrub()
        {
            EditorGUILayout.LabelField("Scrub the cat!");
            scrub = EditorGUILayout.Slider(scrub, 0, 1);

            if ((scrub > 0.9f && scrubbedLeft) || (scrub < 0.1f && !scrubbedLeft))
            {
                scrubbedLeft = !scrubbedLeft;
                IncrementCleanliness();
                Repaint();
            }
        }

        void IncrementCleanliness()
        {
            if (GameData.CatCleanliness < 100)
            {
                GameData.CatCleanliness += 5;
                if (GameData.CatCleanliness > 100)
                    GameData.CatCleanliness = 100;
                CheckAchievement();

                logText = "Smelly cat!!";
            }
            else
            {
                if (Random.Range(0, 1) == 0)
                    logText = "The cat is thankful.";
                else
                    logText = "Cat has been clean!";
            }
        }
        #endregion

        #region Purchase item effects
        void BuyWater()
        {
            if (GameData.CatThirst == 100)
            {
                logText = "Cat is not thirsty ^_^";
            }
            else if (GameData.Money < WaterCost)
            {
                logText = "Come back with money ¯\\_(ツ)_/¯";
            }
            else
            {
                logText = "You fed the cat some water!";
                GameData.Money -= WaterCost;
                GameData.CatThirst += 20;
                CheckAchievement();

                if (GameData.CatThirst > 100)
                    GameData.CatThirst = 100;
                Repaint();
                FrontDesk.RepaintWindow();
            }
        }

        void BuyFood()
        {
            if (GameData.CatHunger == 100)
            {
                logText = "Cat is already full ^_^";
            }
            else if (GameData.Money < FoodCost)
            {
                logText = "Sorry you are broke ¯\\_(ツ)_/¯";
            }
            else
            {
                logText = "You fed the cat some food!";
                GameData.Money -= FoodCost;
                GameData.CatHunger += 20;
                CheckAchievement();

                if (GameData.CatHunger > 100)
                    GameData.CatHunger = 100;
                Repaint();
                FrontDesk.RepaintWindow();
            }
        }
        #endregion

        void OnInspectorUpdate()
        {
            ImageFlippingTimerUpdate();
            TickStats();
            TickPositionPhase();
        }

        #region Portrait
        int portraitTimer;
        void ImageFlippingTimerUpdate()
        {
            if (portraitTimer > 0)
            {
                portraitTimer--;
            }
            else
            {
                catImage = (isPlayingImageOne = !isPlayingImageOne) ? CatImg1 : CatImg2;
                Repaint();
                portraitTimer = 4;
            }
        }
        #endregion

        #region Cat stat ticks
        void TickStats()
        {
            TickStatTimer(ref GameData.CatHunger, ref hungerTimer, 25);
            TickStatTimer(ref GameData.CatThirst, ref thirstTimer, 15);
            TickStatTimer(ref GameData.CatMood, ref moodTimer, 12);
            TickStatTimer(ref GameData.CatCleanliness, ref cleanlinessTimer, 15);
        }

        void TickStatTimer(ref int value, ref int timer, int interval)
        {
            if (timer < 0)
            {
                timer = interval;
                if (value > 0)
                    value--;
            }
            else
            {
                timer--;
            }
        }

        #endregion

        #region Achievement
        void CheckAchievement()
        {
            if (GameData.CatHunger >= 100 && GameData.CatThirst >= 100 &&
                GameData.CatMood >= 100 && GameData.CatCleanliness >= 100)
            {
                GameData.Achivement_CatLover = true;
                GameData.SaveData();
                FrontDesk.RepaintWindow();
            }
        }
        #endregion
    }
}


/*
   // A stat with a tick timer used to make alterations
    public class TimeredStat
    {
        private readonly int depletionInterval;
        private readonly int statMin;
        private readonly int statMax;
        private int timer;
        private int value;

        public TimeredStat(int startingValue, int statMin, int statMax, int depletionInterval)
        {
            this.depletionInterval = depletionInterval;
            this.statMin = statMin;
            this.statMax = statMax;

            timer = 0;
            value = startingValue;
        }

        public int Value => value;

        public void Tick()
        {
            timer--;
            if (timer < 0)
            {
                timer = depletionInterval;
                if (value > statMin)
                    value--;
            }
        }

        public void Replenish (int amount)
        {
            value += amount;
            if (value > statMax)
                value = statMax;
        }
    }

 */