using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public enum GamePhase { Menu, Gameplay, GameOver }
    public class FrontDesk : EditorWindow
    {
        public static GamePhase phase = GamePhase.Menu;

        //Layout positions
        private bool statsFoldout = true;

        [MenuItem("Hotel/Front Desk")]
        private static void Init()
        {
            //Load data
            GameData.LoadData();

            //Show window
            FrontDesk window = (FrontDesk)GetWindow(typeof(FrontDesk), false, "Front Desk");
            window.Show();
        }

        private void OnGUI()
        {
            DisplayWelcomeMessage();
            DisplayGameStats();

            //Repeat button
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Feed Cat", GUILayout.Width(100), GUILayout.Height(100)))
            {
                DisplayCatFeeder();
            }

            if (GUILayout.Button("Forest", GUILayout.Width(100), GUILayout.Height(100)))
            {
                DisplayForest();
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Boogie", GUILayout.Width(100), GUILayout.Height(100)))
            {
                DisplayDDR();
            }

            if (GUILayout.Button("Combat", GUILayout.Width(100), GUILayout.Height(100)))
            {
                DisplayCombat();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(50);
            if (GUILayout.Button("Reset all data", GUILayout.Width(150), GUILayout.Height(20)))
            {
                GameData.ResetData();
            }

            if (GUILayout.Button("Close all windows", GUILayout.Width(150), GUILayout.Height(20)))
            {
                DisplayCatFeeder(false);
                DisplayCombat(false);
                DisplayDDR(false);
                DisplayForest(false);
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



        void DisplayCatFeeder(bool isOpen = true)
        {
            CatFeeder window = (CatFeeder)GetWindow(typeof(CatFeeder), false, "Cat feeder");
            window.Show();
            if (isOpen)
                window.Initialize();
            else
                window.Close();
        }

        void DisplayDDR(bool isOpen = true)
        {
            DDR window = (DDR)GetWindow(typeof(DDR), false, "DDR");
            window.Show();
            if (isOpen)
                window.Initialize();
            else
                window.Close();
        }

        void DisplayForest(bool isOpen = true)
        {
            //Forest window = (Forest)GetWindow(typeof(Forest), false, "Forest");
            //window.Show();
            //if (isOpen)
            //    window.OpenWindow();
            //else
            //    window.Close();
        }

        void DisplayCombat(bool isOpen = true)
        {
            Combat window = (Combat)GetWindow(typeof(Combat), false, "Combat");
            window.Show();
            if (isOpen)
                window.Initialize();
            else
                window.Close();
        }



        #region Minor methods
        void Label(string s1, string s2 = null)
        {
            EditorGUILayout.LabelField(s1, s2 == null ? "" : s2, GUILayout.ExpandWidth(false));
        }

        public static void RepaintWindow()
        {
            GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk").Repaint();
            //FrontDesk window = (FrontDesk)GetWindow(typeof(FrontDesk), false, "Procrastination Hotel - Front Desk");
            //window.Repaint();
        }

        private void OnDestroy()
        {
            GameData.SaveData();
        }
        #endregion

    }
}