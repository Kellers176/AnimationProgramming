using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//Reference
//https://docs.unity3d.com/ScriptReference/EditorWindow.html

public class GraphWindowEditor : EditorWindow
{
    //Serialize all the fields that need to be saved
    [SerializeField]
    GameObject dScriptHolder;

    [SerializeField]
    Vector2 mousePos = Vector2.zero;

    [SerializeField]
    List<Vector2> delauneyPositions;

    //All other vars
    Vector2 oldMousePos = Vector2.zero;
    Vector2 gridLimits = new Vector2(400, 400);

    GUIContent windowTitleContent;
    GUILayoutOption[] tempLayout;

    Texture dotIcon;
    Texture delauneyDotIcon;

    // Add menu item to Window tab
    [MenuItem("Window/Delauney Blend Editor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(GraphWindowEditor));
    }

    void OnGUI()
    {
        oldMousePos = mousePos;

        //DrawGrid
        gridLimits = new Vector2(400, 400);
        Texture gridTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Grid.jpg");
        //https://forum.unity.com/threads/editorguilayout-get-width-of-inspector-window-area.82068/
        GUI.DrawTexture(new Rect(0, 0, gridLimits.x, gridLimits.y), gridTexture, ScaleMode.ScaleToFit);

        //Add space to position text under grid
        GUILayout.Space(gridLimits.y);

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);

        mousePos = EditorGUILayout.Vector2Field("Mouse Position: ", mousePos, tempLayout);

        //Get the Delauney script object
        dScriptHolder = EditorGUILayout.ObjectField("Delauney Script Holder", dScriptHolder, typeof(GameObject), true) as GameObject;

        GUILayout.Label("Nodes (Middle Click on Graph to Clear)", EditorStyles.boldLabel);

        for (int i = 0; i < delauneyPositions.Count; i++)
        {
            delauneyPositions[i] = EditorGUILayout.Vector2Field("Node Position " + (i + 1) + ": ", delauneyPositions[i], tempLayout);
        }

        //Get mouse click
        Event e = Event.current;
        if (e.mousePosition.x > 0 && e.mousePosition.y > 0 && e.mousePosition.x < gridLimits.x && e.mousePosition.y < gridLimits.y)
        {
            if (e.type == EventType.MouseDown)
            {
                if (e.button == 0)
                {
                    //Debug.Log("Mouse down at " + e.mousePosition);
                    mousePos = e.mousePosition / gridLimits.x;
                }
                else if (e.button == 1)
                {
                    if (delauneyPositions.Count < 3)
                    {
                        //Add Delauney node
                        delauneyPositions.Add(e.mousePosition / gridLimits.x);
                    }
                }
                else if (e.button == 2)
                {
                    delauneyPositions.Clear();
                }
            }

            else if (e.type == EventType.MouseDrag)
            {
                if (e.button == 0)
                {
                    //Debug.Log("Mouse drag at " + e.mousePosition);
                    mousePos = e.mousePosition / gridLimits.x;
                }
            }
        }

        if (oldMousePos != mousePos)
        {
            if (mousePos.x < 0)
                mousePos.x = 0;
            else if (mousePos.x > 1)
                mousePos.x = 1;

            if (mousePos.y < 0)
                mousePos.y = 0;
            else if (mousePos.y > 1)
                mousePos.y = 1;

            oldMousePos = mousePos;            

            //Stuff changed so repaint
            Repaint();
        }

        for (int i = 0; i < delauneyPositions.Count; i++)
        {
            float newX = delauneyPositions[i].x;
            float newY = delauneyPositions[i].y;

            if (newX < 0)
                newX = 0;
            else if (newX > 1)
                newX = 1;

            if (newY < 0)
                newY = 0;
            else if (newY > 1)
                newY = 1;

            delauneyPositions[i] = new Vector2(newX, newY);
        }

        GUI.DrawTexture(new Rect((oldMousePos.x) * gridLimits.x - 5, (oldMousePos.y) * gridLimits.x - 5, 10, 10), dotIcon, ScaleMode.ScaleToFit);

        for (int i = 0; i < delauneyPositions.Count; i++)
        {
            GUI.DrawTexture(new Rect((delauneyPositions[i].x) * gridLimits.x - 5, (delauneyPositions[i].y) * gridLimits.x - 5, 10, 10), delauneyDotIcon, ScaleMode.ScaleToFit);
        }

        //Pass data
        if (delauneyPositions.Count == 3)
        {
            Debug.Log(dScriptHolder.gameObject.name);
            Delauney passScript = dScriptHolder.gameObject.GetComponent<Delauney>();

            passScript.setCurrentGraphPoint(oldMousePos);

            passScript.setPose1Transform(delauneyPositions[0]);
            passScript.setPose2Transform(delauneyPositions[1]);
            passScript.setPose3Transform(delauneyPositions[2]);
        }
    }


    //Saving data is from this:
    //https://answers.unity.com/questions/119978/how-do-i-have-an-editorwindow-save-its-data-inbetw.html
    protected void OnEnable()
    {
        //Load objects
        //Create the window header
        windowTitleContent = new GUIContent("Delauney Blend Graph");
        this.titleContent = windowTitleContent;

        //Placed dot icon
        dotIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Dot.png");
        delauneyDotIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/DelauneyDot.png");

        //Set up layouts
        tempLayout = new GUILayoutOption[]
        {
            GUILayout.Width(120),
            GUILayout.Height(40)
        };

        //Save data
        // Here we retrieve the data if it exists or we save the default field initialisers we set above
        var data = EditorPrefs.GetString("Delauney Blend Graph", JsonUtility.ToJson(this, false));
        // Then we apply them to this window
        JsonUtility.FromJsonOverwrite(data, this);
    }

    protected void OnDisable()
    {
        // We get the Json data
        var data = JsonUtility.ToJson(this, false);
        // And we save it
        EditorPrefs.SetString("Delauney Blend Graph", data);

        // Et voilà !
    }
}
