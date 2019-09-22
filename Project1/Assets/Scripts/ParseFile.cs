using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public struct ObjectInformation
{
    //variables
    public string channelName;
    public float value;
    public float keyFrame;

}
public class ParseFile : MonoBehaviour
{
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
        holder.value = 2.0f;
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
        lineNumber = 0;
        float[] floatValues;
        float[] floatKeyframes;
        string[] values;
        string[] keyframes;
        while (text != null)
        {
            text = reader.ReadLine().Trim();


            if (text[0] == '#')
                Debug.Log("comment");
            else
            {
                if (text[0] == 'p')
                {
                    isChannelName = true;
                }
                //for every char in the line
                for (int i = 0; i < text.Length; i++)
                {
                    temp.channelName = "";
                    temp.value = 0;
                    temp.keyFrame = 0;
                    if(isChannelName)
                    {
                        tempS += text[i];
                    }
                    else if(lineNumber == 0)
                    {
                        values = text.Split(',');
                        floatValues = new float[values.Length];

                        for (int n = 0; n < values.Length; n++)
                        {
                            //Debug.Log(values[i]);
                            if (float.TryParse((values[n]), out float num))
                            {
                                floatValues[n] = float.Parse((values[n]));
                                Debug.Log(floatValues[n]);
                            }
                        }
                        temp.value = floatValues[0];
                    }
                    else if(lineNumber == 1)
                    {
                        keyframes = text.Split(',');
                        floatKeyframes = new float[keyframes.Length];

                        for (int n = 0; n < keyframes.Length; n++)
                        {
                            //Debug.Log(values[i]);
                            if(float.TryParse((keyframes[n]), out float num))
                            {
                                floatKeyframes[n] = float.Parse((keyframes[n]));
                                Debug.Log(floatKeyframes[n]);
                            }
                        }
                        temp.keyFrame = floatKeyframes[0];

                    }
                    
                }
                //set the channel name from the temp string
                temp.channelName = tempS;
                if(isChannelName)
                {
                    isChannelName = false;

                }
                tempS = "";

                //set and read out the value for each channel name
                for (int i = 0; i < ColumnLength; i++)
                {
                    for (int j = 0; j < RowHeight; j++)
                    {
                        ObjectHolder[i, j] = temp;
                        Debug.Log("Channel Name: " + ObjectHolder[i, j].channelName);             
                        //Debug.Log("Value: " + ObjectHolder[i, j].value);
                        //Debug.Log("Keyframe: " + ObjectHolder[i, j].keyFrame);
                    }
                }
            }
            text = " ";

            lineNumber++;
            if (lineNumber > 2)
            {
                lineNumber = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
