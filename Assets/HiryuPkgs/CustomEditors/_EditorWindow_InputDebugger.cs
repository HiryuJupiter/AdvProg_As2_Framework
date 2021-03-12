using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UIElements;

public class EditorWindow_InputDebugger : EditorWindow
{
    GameObject currentSelection;
    string fileName = "testEditorWindow";
    int fileIndex = 0;

    [MenuItem("Tools/Label Field")]
    public static void Init()
    {
        //Creates a reference to this class
        //If you don't need the reference then just have (EditorWindow_InputDebugger)GetWindow(typeof(EditorWindow_InputDebugger));
        EditorWindow_InputDebugger window = (EditorWindow_InputDebugger)GetWindow(typeof(EditorWindow_InputDebugger));

        //This changes the title of the window from the name of this class to a string.
        GUIContent content = new GUIContent();
        content.text = "GameObject Debugger";

        //Set icon
        var icon = new Texture2D(16, 16);
        content.image = icon;

        //Sets the contents
        window.titleContent = content;
    }

    private void OnFocus()
    {
        currentSelection = Selection.activeGameObject;
    }

    private void OnGUI()
    {
        if (currentSelection != null)
        {
            EditorGUILayout.LabelField("Selected Object = ", currentSelection.name);
            EditorGUILayout.Vector3Field("Selected Object = ", currentSelection.transform.position);
        }
        else
        {
            EditorGUILayout.LabelField("Selected a game object then click here");
        }
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Horizontal Input", Input.GetAxis("Horizontal").ToString());
        EditorGUILayout.Space();

        fileName = EditorGUILayout.TextField("Screenshot file name = ", fileName);

        //Standard button
        if (GUILayout.Button("Pres to take a game screenshot"))
        {
            ScreenCapture.CaptureScreenshot(fileName + " " + fileIndex + ".png");
            fileIndex++;
        }
        EditorGUILayout.Space();


        //Repeat button
        GUILayout.BeginHorizontal();
        bool repeatPressed = GUILayout.RepeatButton("Is Repeat Button\nPressed");
        EditorGUILayout.LabelField(repeatPressed.ToString());

        GUILayout.EndHorizontal();

        DropAreaGUI();

        //We need this in order to update OnGUI
        this.Repaint();
    }

    void DropAreaGUI ()
    {
        var e = Event.current.type; //Reference to current event

        if (e == EventType.DragUpdated ) 
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy; //Change the visual mode to let the cursor reflect we're about toa ccept dragging onto the window.
        }
        else if (e == EventType.DragPerform) //Mouse has being released and we can accept the item onto the window
        {
            DragAndDrop.AcceptDrag();

            foreach(Object draggedObject in DragAndDrop.objectReferences)
            {
                if (draggedObject is GameObject)
                {
                    Debug.Log(draggedObject.name);
                }
            }

        }
    }

    private void DrawDatabaseEntries()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Box("", EditorStyles.toolbar, GUILayout.ExpandWidth(true));
                GUILayout.Space((EditorGUIUtility.singleLineHeight * -1f) - 2.5f);
                //Sorter Tabs
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("ID"))
                    {
                    }
                    if (GUILayout.Button("Name"))
                    {
                    }
                }
                GUILayout.EndHorizontal();

                //EditorGUILayout.BeginHorizontal(AreaStyleNoMarginNoPadding);
                //EditorGUIUtility.labelWidth = 1;
                //EditorGUILayout.LabelField(dbE.ID, AreaStyleNoMargin, GUILayout.MaxWidth(60), GUILayout.MinWidth(60), GUILayout.MinHeight(EditorGUIUtility.singleLineHeight + 2), GUILayout.ExpandWidth(false));
                //EditorGUILayout.LabelField(dbE.Name, AreaStyleNoMargin, GUILayout.MaxWidth(60), GUILayout.MinWidth(60), GUILayout.MinHeight(EditorGUIUtility.singleLineHeight + 2), GUILayout.ExpandWidth(false));
                //EditorGUIUtility.labelWidth = 0;
                //EditorGUILayout.EndHorizontal();

                //Draw Lines
                Handles.BeginGUI();
                Handles.color = Color.gray;
                for (int i = 0; i < 7; i++)
                {
                    Handles.DrawLine(new Vector2((100 * (i + 1)) - 2, - 20), new Vector2((100 * (i + 1)) - 2, 0));
                }
                Handles.EndGUI();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private GUIStyle AreaStyleNoMargin
    {
        get
        {
            GUIStyle s = new GUIStyle(EditorStyles.textArea)
            {
                margin = new RectOffset(0, 0, 0, 0),
            };
            return s;
        }
    }
    private GUIStyle AreaStyleNoPadding
    {
        get
        {
            GUIStyle s = new GUIStyle(EditorStyles.textArea)
            {
                padding = new RectOffset(0, 0, 0, 0)
            };
            return s;
        }
    }
    private GUIStyle AreaStyleNoMarginNoPadding
    {
        get
        {
            GUIStyle s = new GUIStyle(EditorStyles.textArea)
            {
                margin = new RectOffset(0, 0, 0, 0),
                padding = new RectOffset(0, 0, 0, 0),

            };
            return s;
        }
    }
}