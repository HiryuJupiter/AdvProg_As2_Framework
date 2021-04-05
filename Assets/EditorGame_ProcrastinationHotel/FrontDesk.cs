using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    /// <summary>
    /// Game phases that are inside the game
    /// </summary>
    public enum GamePhase { Menu, Gameplay, GameOver }

    /// <summary>
    /// For displaying things in the front desk window
    /// </summary>
    public class FrontDesk : EditorWindow
    {
        public static GamePhase phase = GamePhase.Menu;

        const int ButtonSize = 70;

        //Layout positions
        bool statsFoldout = true;

        [MenuItem("Hotel/Front Desk")]

        /// <summary>
        /// EditorWindow method that runs automatically when the window is clicked open
        /// </summary>
        static void Init()
        {
            //Load data
            GameData.LoadData();

            //Show window
            FrontDesk window = (FrontDesk)GetWindow(typeof(FrontDesk), false, "Front Desk");
            window.Show();
        }

        /// <summary>
        /// Displays GUI elements: welcome message, game stats, and menu buttons
        /// </summary>
        void OnGUI()
        {
            DisplayWelcomeMessage();
            DisplayGameStats();

            //Repeat button
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Feed Cat", GUILayout.Width(ButtonSize), GUILayout.Height(ButtonSize)))
            {
                DisplayCatFeeder();
            }

            if (GUILayout.Button("Boogie", GUILayout.Width(ButtonSize), GUILayout.Height(ButtonSize)))
            {
                DisplayDDR();
            }

            if (GUILayout.Button("Combat", GUILayout.Width(ButtonSize), GUILayout.Height(ButtonSize)))
            {
                DisplayCombat();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(50);
            if (GUILayout.Button("Reset all data", GUILayout.Width(150), GUILayout.Height(20)))
            {
                DisplayResetConfirmWindow();
            }
        }

        /// <summary>
        /// Displays a simple welcome message
        /// </summary>
        void DisplayWelcomeMessage()
        {
            var style = new GUIStyle(GUI.skin.label)
            { alignment = TextAnchor.MiddleCenter };
            EditorGUILayout.LabelField("Welcome to Procrastination Hotel!" +
                "\nHow may I help you today?", style, GUILayout.Height(50));

        }
        void DisplayGameStats()
        {
            statsFoldout = EditorGUILayout.Foldout(statsFoldout, "Stats");
            if (statsFoldout)
            {
                Label("Money ------------------", GameData.Money.ToString());
                EditorGUILayout.Space(5);
                if (GameData.Achivement_CatLover)
                    Label("Cat Lover ------------------", "Unlocked!");
                else
                    Label("**Hidden Achievement**");

                if (GameData.Achivement_DanceFreak)
                    Label("Dance Freak ------------------", "Unlocked!");
                else
                    Label("**Hidden Achievement**");

                if (GameData.Achivement_CombatLegend)
                    Label("Combat Legend ------------------", "Unlocked!");
                else
                    Label("**Hidden Achievement**");
            }
        }

        /// <summary>
        /// Display button for opening the cat minigame window
        /// </summary>
        void DisplayCatFeeder()
        {
            CatFeeder window = (CatFeeder)GetWindow(typeof(CatFeeder), false, "Cat feeder");
            window.Show();
            window.Initialize();
        }

        /// <summary>
        /// Display button for opening the dance minigame window
        /// </summary>
        void DisplayDDR()
        {
            DDR window = (DDR)GetWindow(typeof(DDR), false, "DDR");
            window.Show();
            window.Initialize();
        }

        /// <summary>
        /// Display button for opening the combat minigame window
        /// </summary>
        void DisplayCombat()
        {
            Combat window = (Combat)GetWindow(typeof(Combat), false, "Combat");
            window.Show();
            window.Initialize();
        }

        /// <summary>
        /// Display button for opening the minigame window
        /// </summary>
        void DisplayResetConfirmWindow ()
        {
            ResetConfirm window = (ResetConfirm)GetWindow(typeof(ResetConfirm), false, "Reset confirm");
            window.maxSize = new Vector2(300f, 50f);
            window.minSize = window.maxSize;
            window.Show();
        }

        #region Minor methods
        /// <summary>
        /// For drawing a label in the window
        /// </summary>
        void Label(string s1, string s2 = null)
        {
            EditorGUILayout.LabelField(s1, s2 == null ? "" : s2);
        }

        /// <summary>
        /// Refresh the drawn elements in the windown
        /// </summary>
        public static void RepaintWindow()
        {
            GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk").Repaint();
        }

        /// <summary>
        /// When the window closes
        /// </summary>
        void OnDestroy()
        {
            GameData.SaveData();
        }
        #endregion

    }
}