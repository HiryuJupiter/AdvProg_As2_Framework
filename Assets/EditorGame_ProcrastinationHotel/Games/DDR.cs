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

        /// <summary>
        /// Initializes variables used in this window
        /// </summary>
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

        /// <summary>
        /// Draws the GUI stuff
        /// </summary>
        void OnGUI()
        {
            if (!initialized)
                Initialize();

            DrawHighScore();
            DrawNotes();
        }

        /// <summary>
        /// Update
        /// </summary>
        void OnInspectorUpdate()
        {
            if (!initialized)
                InitializeNotes();
            TickMoveTimer();
        }

        #region Move notes
        /// <summary>
        /// Initialize the initial note array that are to be displayed when the game starts
        /// </summary>
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

        /// <summary>
        /// Update the timer that moves the notes
        /// </summary>
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

        /// <summary>
        /// Move the notes down
        /// </summary>
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
        /// <summary>
        /// Display highscore on screen
        /// </summary>
        void DrawHighScore ()
        {
            EditorGUILayout.Space(20);
           
            EditorGUILayout.LabelField("High Score: " + GameData.DDRHighScore, centeredLabel);
            EditorGUILayout.LabelField("Current Score: " + currentScore, centeredLabel);
        }
        #endregion

        #region Draw notes
        /// <summary>
        /// Draw notes on screen with seperation lines and stuff
        /// </summary>
        void DrawNotes()
        {
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
            DrawButtonSection();
            DrawSeperationLine();
        }

        /// <summary>
        /// Draw the button section that the player can interact with
        /// </summary>
        void DrawButtonSection()
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

        /// <summary>
        /// Encapsulates the actual button drawing logic for the button-section
        /// </summary>
        /// <param name="xColumn"></param>
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

        /// <summary>
        /// Draws a seperation line on screen
        /// </summary>
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
        /// <summary>
        /// When the player hits a note
        /// </summary>
        void HitNote (int xColumn)
        {
            currentScore++;
            notes[xColumn, Rows - 1] = false;
            DrawNotes();
            Repaint();
            CheckAchievement();
        }

        /// <summary>
        /// Check if player has unlocked an achievement
        /// </summary>
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

        /// <summary>
        /// When the player misses a note
        /// </summary>
        void MissedNote ()
        {
            if (currentScore > GameData.DDRHighScore)
                GameData.DDRHighScore = currentScore;
            currentScore = 0;
            Repaint();
        }

        /// <summary>
        /// For easily getting an arrow string
        /// </summary>
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
