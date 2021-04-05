using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public class DDR : EditorWindow
    {
        const int Columns = 4;
        const int Rows = 7;
        const int CellSize = 30;
        const string U = "↑";
        const string D = "↓";
        const string L = "←";
        const string R = "→";
        const int MoveInterval = 4;

        bool[,] notes;

        bool initialized = false;

        GUIStyle middleLable;
        int currentScore = 0;
        int moveTimer;

        private static void Init()
        {
            //DDR window = (DDR)GetWindow(typeof(DDR), false, "DDR");
            //window.Show();
        }

        public void Initialize()
        {
            initialized = true;
            //middleAlignment = new GUIStyle(GUI.skin.label)
            //{
            //    stretchWidth = true,
            //    alignment = TextAnchor.MiddleCenter
            //};
            currentScore = 0;
            InitializeNotes();
        }

        private void OnGUI()
        {
            if (!initialized || middleLable == null)
                CacheGUIStyle();

            DrawHighScore();
            DrawNotes();
        }

        void CacheGUIStyle ()
        {
            middleLable = new GUIStyle(GUI.skin.label);
            middleLable.alignment = TextAnchor.MiddleCenter;
        }

        void OnInspectorUpdate()
        {
            if (notes == null)
                InitializeNotes();
            TickMoveTimer();
        }

        #region Move notes
        void InitializeNotes()
        {
            notes = new bool[Columns, Rows];
            for (int r = 0; r < Rows; r++)
            {
                int col = Random.Range(0, Columns);
                for (int c = 0; c < Columns; c++)
                {
                    notes[c, r] = c == col ? true : false;
                }
            }
        }


        void TickMoveTimer ()
        {
            if (moveTimer > 0)
            {
                moveTimer--;
            }
            else
            {
                moveTimer = MoveInterval;
                MoveNotes();
            }
        }

        void MoveNotes ()
        {
            //If final row has a note, then reset progress
            for (int c = 0; c < Columns; c++)
            {
                if (notes[c, Rows - 1] == true)
                {
                    MissedNote();
                    break;
                }
            }

            //Move all the notes down by 1
            for (int r = Rows - 1; r > 0; r--)
            {
                for (int c = 0; c < Columns; c++)
                {
                    notes[c, r] = notes[c, r - 1];
                }
            }

            //Then generate a random note at row 0
            int i = Random.Range(0, Columns);
            for (int c = 0; c < Columns; c++)
            {
                notes[c, 0] = (c == i) ? true : false;
            }
            Repaint();
        }
        #endregion

        #region Highscore
        void DrawHighScore ()
        {
            EditorGUILayout.Space(20);
           
            EditorGUILayout.LabelField("High Score: " + GameData.DDRHighScore, middleLable);
            EditorGUILayout.LabelField("Current Score: " + currentScore, middleLable);
        }
        #endregion

        #region Draw notes
        void DrawNotes()
        {
            if (!initialized || middleLable == null)
                CacheGUIStyle();

            DrawSeperationLine();

            //First zone
            GUILayout.BeginVertical();
            for (int r = 0; r < Rows - 1; r++)
            {
                GUILayout.BeginHorizontal();
                for (int c = 0; c < Columns; c++)
                {
                    EditorGUILayout.LabelField("|", GUILayout.Width(5));
                    EditorGUILayout.LabelField(GetArrowString(notes[c, r] ? c : -1), middleLable, GUILayout.Width(20));
                    EditorGUILayout.LabelField("|", GUILayout.Width(5));
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            //Seperation line
            DrawSeperationLine();

            //Hit zone
            GUILayout.BeginHorizontal();
            for (int c = 0; c < Columns; c++)
            {
                EditorGUILayout.LabelField("|", GUILayout.Width(5));
                EditorGUILayout.LabelField(GetArrowString(notes[c, Rows -1] ? c : -1), middleLable, GUILayout.Width(20));
                EditorGUILayout.LabelField("|", GUILayout.Width(5));
            }
            GUILayout.EndHorizontal();

            DrawSeperationLine();
            DrawSeperationLine();
            DrawButtonText();
            DrawSeperationLine();
        }

        void DrawButtonText()
        {
            GUILayout.BeginHorizontal();
            for (int c = 0; c < Columns; c++)
            {
                EditorGUILayout.LabelField("|", GUILayout.Width(5));
                DrawButton(c);
                EditorGUILayout.LabelField("|", GUILayout.Width(5));
            }
            GUILayout.EndHorizontal();
        }

        void DrawButton(int xColumn)
        {
            if (GUILayout.Button(GetArrowString(xColumn), GUILayout.Width(20)))
            {
                if (notes[xColumn, Rows - 1])
                {
                    HitNote(xColumn);
                }
                else
                {
                    MissedNote();
                }
            }
        }

        void DrawSeperationLine()
        {
            GUILayout.BeginHorizontal();
            for (int c = 0; c < Columns; c++)
            {
                EditorGUILayout.LabelField("------", GUILayout.Width(36), GUILayout.Height(5));
            }
            GUILayout.EndHorizontal();
        }
        #endregion

        #region Minor methods
        void HitNote (int xColumn)
        {
            currentScore++;
            notes[xColumn, Rows - 1] = false;
            DrawNotes();
            Repaint();
            CheckAchievement();
        }

        void CheckAchievement()
        {
            if (currentScore >= 22)
            {
                GameData.Achivement_DanceFreak = true;
                GameData.DDRHighScore = currentScore;
                GameData.SaveData();
                FrontDesk.RepaintWindow();
            }
        }

        void MissedNote ()
        {
            if (currentScore > GameData.DDRHighScore)
                GameData.DDRHighScore = currentScore;
            currentScore = 0;
            Repaint();
        }

        string GetArrowString(int index)
        {
            return index switch
            {
                0 => U,
                1 => D,
                2 => L,
                3 => R,
                _ => "."
            };
        }

        string GetRandomArrowString ()
        {
            return GetArrowString(GetRandomArrowIndex());
        }

        int GetRandomArrowIndex () => Random.Range(0, 4);

        void Label(string s1, string s2 = null)
        {
            EditorGUILayout.LabelField(s1, s2 == null ? "" : s2, GUILayout.ExpandWidth(false));
        }

        public static void RepaintWindow()
        {
            GetWindow(typeof(FrontDesk), false, "Dance Machine").Repaint();
        }

        private void OnDestroy()
        {
            GameData.SaveData();
        }
        #endregion
    }
}
