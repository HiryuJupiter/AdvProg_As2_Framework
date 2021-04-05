using System.Collections;
using UnityEngine;

namespace HiryuTK.Assets.InspectorRPG
{
    public static class GUIStyles
    {
        public static GUIStyle TitleStyle;

        public static void Initialize ()
        {
            TitleStyle = GUI.skin.GetStyle("Label");
        }
    }
}