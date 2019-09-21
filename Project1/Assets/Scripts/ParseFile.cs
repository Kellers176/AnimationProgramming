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
    int lineNumber;

    bool isChannelName;
    bool isValueName;
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
        int value;
        lineNumber = 0;

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
                        isChannelName = true;
                    }
                    else
                    {
                        lineNumber++;
                    }
                    if(isChannelName)
                    {
                        tempS += text[i];
                    }
                    else if(lineNumber == 1)
                    {
                        isValueName = true;
                    }
                    if(isValueName)
                    {
                        string[] values = text.Split(',');
                        int[] integerValues = new int[values.Length];
                        for (int n = 0; n < values.Length; n++)
                        {
                            integerValues[n] = int.Parse(values[n]);
                            Debug.Log(integerValues);
                        }
                    }
                }
                //set the channel name from the temp string
                temp.channelName = tempS;
                if(isChannelName)
                {
                    isChannelName = false;
                    
                }
                else if(isValueName)
                {
                    isValueName = false;
                }
                
                tempS = "";

                //set and read out the value for each channel name
                for (int i = 0; i < ColumnLength; i++)
                {
                    ObjectHolder[i, 0] = temp;
                    for (int j = 0; j < RowHeight; j++)
                    {
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
