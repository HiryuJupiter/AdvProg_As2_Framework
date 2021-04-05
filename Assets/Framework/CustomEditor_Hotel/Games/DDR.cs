using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public class DDR : EditorWindow
    {
        const int Columns = 4;
        const int Rows = 7;
        const string Up = "↑";
        const string Down = "↓";
        const string Left = "←";
        const string Right = "→";
        const int MoveInterval = 4;

        bool initialized = false;
        bool[,] notes;
        int currentScore = 0;
        int moveTimer;

        GUIStyle centeredLabel;

        public void Initialize()
        {
            initialized = true;
            centeredLabel = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
            currentScore = 0;
            InitializeNotes();
        }

        private void OnGUI()
        {
            if (!initialized || centeredLabel == null)
                CacheGUIStyle();

            DrawHighScore();
            DrawNotes();
        }

        void CacheGUIStyle ()
        {
            centeredLabel = new GUIStyle(GUI.skin.label);
            centeredLabel.alignment = TextAnchor.MiddleCenter;
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
            //If final row has a note, then wipe player score
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
           
            EditorGUILayout.LabelField("High Score: " + GameData.DDRHighScore, centeredLabel);
            EditorGUILayout.LabelField("Current Score: " + currentScore, centeredLabel);
        }
        #endregion

        #region Draw notes
        void DrawNotes()
        {
            if (!initialized || centeredLabel == null)
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
                    EditorGUILayout.LabelField(GetArrowString(notes[c, r] ? c : -1), centeredLabel, GUILayout.Width(20));
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
                EditorGUILayout.LabelField(GetArrowString(notes[c, Rows -1] ? c : -1), centeredLabel, GUILayout.Width(20));
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
                0 => Up,
                1 => Down,
                2 => Left,
                3 => Right,
                _ => "."
            };
        }

        private void OnDestroy()
        {
            GameData.SaveData();
        }
        #endregion
    }
}
