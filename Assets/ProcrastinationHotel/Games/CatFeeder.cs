using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public class CatFeeder : EditorWindow
    {
        const int PositionPhaseMax = 5;

        string cat = CatA;
        bool isImageOne;
        string givenCatName;

        //Timers
        int hungerTimer = 6;
        int thirstTimer = 4;
        int moodTimer = 3;
        int cleanlinessTimer = 12;

        //Food cost
        int foodCost = 2;
        int waterCost = 1;

        //Cat location
        int positionPhase = PositionPhaseMax;
        string catFaceString2 = "/ᐠ._.ᐟ\\";
        string catFaceString = "[^._.^] ﾉ彡";

        //Log
        string logText;

        //private static void Init()
        //{
        //    //CatFeeder window = (CatFeeder)GetWindow(typeof(CatFeeder), false, "Cat feeder");
        //    //window.Show();
        //}

        #region Open and closing window
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

            EditorGUILayout.LabelField(cat, GUILayout.Height(150), GUILayout.Width(100));

            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Cat name", EditorStyles.boldLabel);
            givenCatName = EditorGUILayout.TextField(givenCatName, GUILayout.Width(150));
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Food ------------------", GameData.CatHunger.ToString());
            EditorGUILayout.LabelField("Hydration ------------------", GameData.CatThirst.ToString());
            EditorGUILayout.LabelField("Mood ------------------", GameData.CatMood.ToString());
            EditorGUILayout.LabelField("Cleanliness ------------------", GameData.CatCleanliness.ToString());

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            //Cat scrub
            CatScrub();

            //Food buttons
            GUILayout.BeginHorizontal();
            if (GUILayout.Button($"Buy food ($ {foodCost})", GUILayout.Width(100), GUILayout.Height(40)))
            {
                BuyFood();
            }
            if (GUILayout.Button($"Buy water ($ {waterCost})", GUILayout.Width(100), GUILayout.Height(40)))
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

        void OnInspectorUpdate()
        {
            ImageFlippingTimerUpdate();
            TickStats();
            TickPositionPhase();
        }

        #region Play with cat
        void ShowCatButtons ()
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < PositionPhaseMax; i++)
            {
                if (i == positionPhase)
                {
                    if (GUILayout.Button(catFaceString, GUILayout.Height(30)))
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

        void PlayedWithCat ()
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
        void TickPositionPhase ()
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

        void IncrementCleanliness ()
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
            else if (GameData.Money < waterCost)
            {
                logText = "Come back with money ¯\\_(ツ)_/¯";
            }
            else
            {
                GameData.Money -= waterCost;
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
            else if (GameData.Money < foodCost)
            {
                logText = "Sorry you are broke ¯\\_(ツ)_/¯";
            }
            else
            {
                GameData.Money -= foodCost;
                GameData.CatHunger += 20;
                CheckAchievement();

                if (GameData.CatHunger > 100)
                    GameData.CatHunger = 100;
                Repaint();
                FrontDesk.RepaintWindow();
            }
        }

        public static void DoRepaint()
        {
            GetWindow(typeof(CatFeeder), false, "Cat Feeder").Repaint();
            //FrontDesk window = (FrontDesk)GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk");
            //window.Repaint();
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

        //void ResetCatStats()
        //{
        //    hungerTimer = 60;
        //    thirstTimer = 40;
        //    irritationTimer = 30;
        //    filthTimer = 120;
        //}
        #endregion

        #region Portrait
        int portraitTimer;
        void ImageFlippingTimerUpdate()
        {
            if (portraitTimer > 0)
            {
                portraitTimer --;
            }
            else
            {
                cat = (isImageOne = !isImageOne) ? CatA : CatB;
                Repaint();
                portraitTimer = 4;
            }
        }

        const string CatA = "" +
    "          /^--^\\     \n" +
    "          \\_____/   \n" +
    "          /       \\  \n" +
    "         |         |  \n" +
    "          \\__ __/   \n" +
    "             \\ \\   \n" +
    "              \\ \\  \n" +
    "             / /     \n" +
    "             \\/     \n";

        const string CatB = "" +
       "          /^--^\\     \n" +
       "          \\_____/   \n" +
       "          /       \\  \n" +
       "         |         |  \n" +
       "          \\__ __/   \n" +
       "             / /   \n" +
       "           / /  \n" +
       "            \\ \\     \n" +
       "             \\/     \n";
        #endregion

        #region Achievement
        void CheckAchievement ()
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