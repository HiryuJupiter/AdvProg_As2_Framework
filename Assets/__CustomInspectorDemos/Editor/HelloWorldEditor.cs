using UnityEngine;
using UnityEditor;

/*
 * Editor is a unity reserved folder name.
 * In this folder, scripts here will not be compiled when you create you game
 * Here we dont need compiler arguments anymore. 
 * Also note that code inside the asseet folder cannot access code inside the editor folder, but
 * scripts inside the editor folder can access scripts in the Asset folder.
 */

[CustomEditor(typeof(HelloWorld))]
public class HelloWorldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        HelloWorld script = target as HelloWorld;

        //Print members in inspector
        //DrawDefaultInspector();

        //A label (text) field in the inspector.
        EditorGUILayout.LabelField("LabelField", script.name);

        //A float field in the Inspector
        script.hp = EditorGUILayout.FloatField("Float Field", script.hp);

        //A slider control
        script.mana = EditorGUILayout.Slider("Slider", script.mana, 0, 10);

        //Object
        //true allows us to select the gameobject. False will limit the selection to asset folder
        //You can also limit the type of specific type of scripts (class)
        script.target = EditorGUILayout.ObjectField("GameObject ObjectField", script.target, typeof (GameObject), true) as GameObject;
        script.script = EditorGUILayout.ObjectField("Script ObjectField", script.script, typeof(HelloWorld), true) as HelloWorld;


        //Button
        if (GUILayout.Button("Simple button"))
        {
            Debug.Log(script.name);
        }

        //Ideally you stick to gui layout


        //Layout group
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Horizontal group");
        
        if (GUILayout.Button("Apple"))
        {
            Debug.Log("Apple");
        }
        if (GUILayout.Button("Orange"))
        {
            Debug.Log("Orange");
        }

        if (GUILayout.Button("Do it"))
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Open Custom EditorWindow"))
        {
            EditorWindow.GetWindow(typeof(EditorWindow_InputDebugger));
        }
    }
}