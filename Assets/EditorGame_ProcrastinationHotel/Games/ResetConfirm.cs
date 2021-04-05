﻿using System.Collections;
using UnityEngine;
using UnityEditor;

namespace HiryuTK.GameRoomService
{
    public class ResetConfirm : EditorWindow
    {
        static void Init() {}

        /// <summary>
        /// Draws the reset confirm window with a Yes and a No button.
        /// </summary>
        void OnGUI()
        {
            EditorGUILayout.LabelField("Are you sure you want to reset all data?");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("YES"))
            {
                GameData.ResetData();
                FrontDesk.RepaintWindow();
                CloseWindow();
            }

            if (GUILayout.Button("NO"))
            {
                CloseWindow();
            }

            GUILayout.EndHorizontal();
        }

        void CloseWindow()
        {
            GetWindow(typeof(ResetConfirm), false, "Reset confirm").Close();
        }
    }
}