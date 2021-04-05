using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    /// <summary>
    /// Mini pet game
    /// </summary>
    public class CatFeeder : EditorWindow
    {
        const int PositionPhaseMax = 5;
        const int FoodCost = 2;
        const int WaterCost = 1;
        const string CatFaceString = "[^._.^] ﾉ彡";

        const string CatImg1 = "" +
        "          /^--^\\     \n" +
        "          \\_____/   \n" +
        "          /       \\  \n" +
        "         |         |  \n" +
        "          \\__ __/   \n" +
        "             \\ \\   \n" +
        "              \\ \\  \n" +
        "             / /     \n" +
        "             \\/     \n";

        const string CatImg2 = "" +
       "          /^--^\\     \n" +
       "          \\_____/   \n" +
       "          /       \\  \n" +
       "         |         |  \n" +
       "          \\__ __/   \n" +
       "             / /   \n" +
       "           / /  \n" +
       "            \\ \\     \n" +
       "             \\/     \n";

        //Status
        bool initialized;
        string catImage = CatImg1;
        bool isPlayingImageOne;
        string givenCatName;
        string logText;
        int positionPhase = PositionPhaseMax;

        //Timers
        int hungerTimer = 24;
        int thirstTimer = 16;
        int moodTimer = 12;
        int cleanlinessTimer = 48;

        //Cache
        GUIStyle centeredLabel;


        #region Start and end
        /// <summary>
        /// Initializes variables used in this window
        /// </summary>
        public void Initialize()
        {
            initialized = true;
            givenCatName = GameData.CatName;
            logText = "";

            centeredLabel = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
        }

        /// <summary>
        /// When the window closes
        /// </summary>
        void OnDestroy()
        {
            GameData.CatName = givenCatName;
            GameData.SaveData();
        }
        #endregion

        /// <summary>
        /// Draws the cat portrait and stats and other cat info
        /// </summary>
        void OnGUI()
        {
            if (!initialized)
                Initialize();

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
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField(logText, centeredLabel);
            }

            Repaint();
        }

        #region Play with cat
        /// <summary>
        /// Show the cat moving button that the player can click on to play with it
        /// </summary>
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

        /// <summary>
        /// Played with cat event - increase cat mood
        /// </summary>
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
                logText = "The cat is satisfied!";
            }
        }

        /// <summary>
        /// Update the cat positon
        /// </summary>
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
        }
        #endregion

        #region Scrub cat
        /// <summary>
        /// Scrub the cat by moving the slider let and right
        /// </summary>
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
            }
        }

        /// <summary>
        /// Increase the cleanliness stat
        /// </summary>
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
                logText = "Cat no longer smells!";
            }
        }
        #endregion

        #region Purchase item effects
        /// <summary>
        /// Purchase water for the cat's thirst
        /// </summary>
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
                FrontDesk.RepaintWindow();
            }
        }

        /// <summary>
        /// Purchase food for the cat';s hunger
        /// </summary>
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
                FrontDesk.RepaintWindow();
            }
        }
        #endregion

        /// <summary>
        /// Inspector update
        /// </summary>
        void OnInspectorUpdate()
        {
            ImageFlippingTimerUpdate();
            TickStats();
            TickPositionPhase();
        }

        #region Portrait
        /// <summary>
        /// Automatically flip the cat's on screen image.
        /// </summary>
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
                portraitTimer = 4;
            }
        }
        #endregion

        #region Cat stat ticks
        /// <summary>
        /// Tick the timer that automatically decreases the stats
        /// </summary>
        void TickStats()
        {
            TickStatTimer(ref GameData.CatHunger, ref hungerTimer, 25);
            TickStatTimer(ref GameData.CatThirst, ref thirstTimer, 15);
            TickStatTimer(ref GameData.CatMood, ref moodTimer, 12);
            TickStatTimer(ref GameData.CatCleanliness, ref cleanlinessTimer, 15);
        }

        /// <summary>
        /// Decrement a particular timer
        /// </summary>
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
        /// <summary>
        /// Check if player has unlocked an achievement
        /// </summary>
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