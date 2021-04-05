using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public enum GamePhase { Menu, Gameplay, GameOver }
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

        void DisplayCatFeeder()
        {
            CatFeeder window = (CatFeeder)GetWindow(typeof(CatFeeder), false, "Cat feeder");
            window.Show();
            window.Initialize();
        }

        void DisplayDDR()
        {
            DDR window = (DDR)GetWindow(typeof(DDR), false, "DDR");
            window.Show();
            window.Initialize();
        }

        void DisplayCombat()
        {
            Combat window = (Combat)GetWindow(typeof(Combat), false, "Combat");
            window.Show();
            window.Initialize();
        }

        void DisplayResetConfirmWindow ()
        {
            ResetConfirm window = (ResetConfirm)GetWindow(typeof(ResetConfirm), false, "Reset confirm");
            window.maxSize = new Vector2(300f, 50f);
            window.minSize = window.maxSize;
            window.Show();
        }

        #region Minor methods
        void Label(string s1, string s2 = null)
        {
            EditorGUILayout.LabelField(s1, s2 == null ? "" : s2);
        }

        public static void RepaintWindow()
        {
            GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk").Repaint();
            //FrontDesk window = (FrontDesk)GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk");
            //window.Repaint();
        }

        void OnDestroy()
        {
            GameData.SaveData();
        }
        #endregion

    }
}