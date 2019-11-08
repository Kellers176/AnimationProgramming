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
        // Loads an icon from an image stored at the specified path
        Texture icon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/God.png");
        // Create the instance of GUIContent to assign to the window. Gives the title "RBSettings" and the icon
        GUIContent titleContent = new GUIContent("God Among Mortals", icon);
        this.titleContent = titleContent;
        
        //GUILayout.Box(icon);
        //GUI.DrawTexture(new Rect(100, 100, 100, 100), icon, ScaleMode.ScaleAndCrop);

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        GUIContent tempContent = new GUIContent();
        tempContent.image = icon;
        tempContent.text = "Mouse Position ";
        GUILayoutOption[] tempLayout;
        tempLayout = new GUILayoutOption[]
        {
            GUILayout.Width(100),
            GUILayout.Height(40)
        };

        mousePos = EditorGUILayout.Vector2Field(tempContent, mousePos, tempLayout);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();

        //DrawGrid
        Texture gridTexture = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Sprites/Grid.jpg");
        float widthPosition = EditorGUIUtility.currentViewWidth / 2 - (EditorGUIUtility.currentViewWidth / 4);
        //https://forum.unity.com/threads/editorguilayout-get-width-of-inspector-window-area.82068/
        GUI.DrawTexture(new Rect(widthPosition, 0, 400, 400), gridTexture, ScaleMode.ScaleToFit);

        Event e = Event.current;
        if (e.mousePosition.x > 0 && e.mousePosition.y > 0 && e.mousePosition.x < Screen.width && e.mousePosition.y < Screen.height)
            Debug.Log(e.mousePosition);

        //Anim
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
        //GUI.DrawTexture(new Rect(0, 100, 400, 400), animFrame, ScaleMode.ScaleAndCrop);
        //
        //currFrame++;
        //if (currFrame > maxFrames)
        //    currFrame = 0;
    }

    void OnInspectorUpdate()
    {
        //Anim
        //timeStep--;
        //if (timeStep < 0)
        //{
        //    timeStep = 20;
        //    Repaint();
        //}
    }
}
