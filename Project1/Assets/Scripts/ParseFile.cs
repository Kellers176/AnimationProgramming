using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ParseFile : MonoBehaviour
{
    struct ObjectInformation
    {
        //variables
        public string channelName;
        public int value;
        public int keyFrame;

    }
    int ColumnLength;
    int RowHeight;
    ObjectInformation holder;

    ObjectInformation[,] ObjectHolder;
    string text = " ";
    string line = " ";

    bool channelNameBool;
    // Start is called before the first frame update
    void Start()
    {
        //this is to make sure that the struct is working
        StreamReader reader = new StreamReader("Assets/TxtFiles/pSphere1_KeyframeOutput.txt");
        holder.channelName = "No";
        holder.value = 1;
        holder.keyFrame = 0;
        ColumnLength = 10;
        RowHeight = 10;
        ObjectHolder = new ObjectInformation[ColumnLength, RowHeight];
        string tempS = "";
        ObjectInformation temp;
        temp.channelName = "";
        temp.value = 0;
        temp.keyFrame = 0;


        while (text != null)
        {
            text = reader.ReadLine().Trim();


            if (text[0] == '#')
                Debug.Log("comment");
            else
            {
                //for every char in the line
                for (int i = 0; i < text.Length; i++)
                {
                    temp.channelName = "";
                    temp.value = 0;
                    temp.keyFrame = 0;
                    if (text[i] == 'p')
                    {
                        channelNameBool = true;
                    }
                    if(channelNameBool == true)
                    {
                        tempS += text[i];
                    }
                }
                //set the channel name from the temp string
                temp.channelName = tempS;
                channelNameBool = false;
                tempS = "";

                //read out the value for each channel name
                for (int i = 0; i < ColumnLength; i++)
                {
                    for (int j = 0; j < RowHeight; j++)
                    {
                        ObjectHolder[i, j] = temp;
                        Debug.Log("Channel Name: " + ObjectHolder[i, 0].channelName);
                        //Debug.Log("Value: " + ObjectHolder[i, 1].value);
                        //Debug.Log("Keyframe: " + ObjectHolder[i, 2].keyFrame);
                    }
                }
            }
            text = " ";
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
