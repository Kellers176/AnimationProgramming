using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//Reference
//https://docs.unity3d.com/ScriptReference/EditorWindow.html

public class GraphWindowEditor : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    Vector2 mousePos = Vector2.zero;
    Vector2 oldMousePos = Vector2.zero;
    Vector2 gridLimits = new Vector2(400, 400);

    //Anim
    int maxFrames = 64;
    int timeStep = 20;
    int currFrame = 0;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/Sick Ass Blend Tree")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(GraphWindowEditor));
    }

    void OnGUI()
    {
        oldMousePos = mousePos;
        
        //Create the window header
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/God.png");
        GUIContent titleContent = new GUIContent("God Among Mortals", icon);
        this.titleContent = titleContent;
        
        //Placed dot icon
        Texture dotIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Dot.png");
        
        //Set up Content
        GUIContent tempContent = new GUIContent();
        tempContent.image = icon;
        tempContent.text = "Mouse Position ";
        
        //Set up layouts
        GUILayoutOption[] tempLayout;
        tempLayout = new GUILayoutOption[]
        {
            GUILayout.Width(120),
            GUILayout.Height(40)
        };
        
        //DrawGrid
        gridLimits = new Vector2(400, 400);
        Texture gridTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Grid.jpg");
        float widthPosition = EditorGUIUtility.currentViewWidth / 2 - (EditorGUIUtility.currentViewWidth / 4);
        //https://forum.unity.com/threads/editorguilayout-get-width-of-inspector-window-area.82068/
        GUI.DrawTexture(new Rect(0, 0, gridLimits.x, gridLimits.y), gridTexture, ScaleMode.ScaleToFit);
        
        //Add space
        GUILayout.Space(gridLimits.y);
        
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);
        
        mousePos = EditorGUILayout.Vector2Field(tempContent, mousePos, tempLayout);
        
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();        
        
        //Get mouse click
        Event e = Event.current;
        if (e.mousePosition.x > 0 && e.mousePosition.y > 0 && e.mousePosition.x < gridLimits.x && e.mousePosition.y < gridLimits.y)
        {
            if (e.type == EventType.MouseDown)
            {
                Debug.Log("Mouse down at " + e.mousePosition);
                //GUI.DrawTexture(new Rect(e.mousePosition.x, e.mousePosition.y, 100, 100), dotIcon, ScaleMode.ScaleToFit);
                mousePos = e.mousePosition / gridLimits.x;
            }
        
            else if (e.type == EventType.MouseDrag)
            {
                Debug.Log("Mouse drag at " + e.mousePosition);
        
                //mousePos = e.mousePosition;
                mousePos = e.mousePosition / gridLimits.x;
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
        }
        
        GUI.DrawTexture(new Rect((oldMousePos.x) * gridLimits.x - 5, (oldMousePos.y) * gridLimits.x - 5, 10, 10), dotIcon, ScaleMode.ScaleToFit);
        Repaint();

        ////Anim
        //string tempName = "Assets/Sprites/Anim/frame_";
        //if (currFrame < 10)
        //    tempName += "0" + currFrame;
        //else
        //    tempName += currFrame;
        //
        //tempName += "_delay-0.1s.gif";
        //
        //Debug.Log(tempName);
        //
        //Texture animFrame = AssetDatabase.LoadAssetAtPath<Texture>(tempName);
        //
        ////GUILayout.Box(icon);
        //GUI.DrawTexture(new Rect(0, 00, 400, 400), animFrame, ScaleMode.ScaleAndCrop);
        //
        //currFrame++;
        //if (currFrame > maxFrames)
        //    currFrame = 0;
    }

    void Update()
    {
        ////Anim
        //timeStep--;
        //if (timeStep < 0)
        //{
        //    timeStep = 20;
        //    Repaint();
        //}
    }
}
