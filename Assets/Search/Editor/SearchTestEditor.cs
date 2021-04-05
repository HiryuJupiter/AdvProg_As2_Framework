using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Search
{
    [CustomEditor(typeof(SearchTest))]
    public class SearchTestEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.BeginHorizontal();

            SearchTest searchTest = (SearchTest)target;

            if (GUILayout.Button("Perform Search"))
            {
                searchTest.PerformSearch();
            }

            GUILayout.EndHorizontal();
        }
    }
}