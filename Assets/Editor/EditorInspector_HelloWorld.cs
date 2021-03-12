using UnityEngine;
using System.Collections;

//This shows how to do custom inspector without placing the script in Editor. 

//Without this you'll get an error when building as editor code cannot be compiled
//Ideally, you get around this by placing this in the Asset/Editor folder.
#if UNITY_EDITOR 
using UnityEditor;
#endif


//Note that currently, this script will get overriden by the one in Editor class
#if UNITY_EDITOR
//[CustomEditor(typeof(HelloWorld))]
//public class ExampleCustomEDtiro : Editor
//{
//    public override void OnInspectorGUI()
//    {

//    }
//}
#endif