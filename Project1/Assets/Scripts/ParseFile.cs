using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//public struct Sample<valueType, keyframeType>
//{
//    valueType value;
//    keyframeType keyframe;
//}

public struct AnimationChannel
{
    //variables
    public string channelName;
    public List<float> value1;
    public List<float> keyframe1;



}

public class ParseFile : MonoBehaviour
{
    int ColumnLength;
    int RowHeight;
    public AnimationChannel temp;

    AnimationChannel[] ObjectHolder;
    string text = " ";
    string line = " ";
    int lineNumber;

    bool isChannelName;
    // Start is called before the first frame update
    void Start()
    {
        //this is to make sure that the struct is working
        StreamReader reader = new StreamReader("Assets/TxtFiles/pSphere1_KeyframeOutput.txt");
        ColumnLength = 4;
        RowHeight = 10;
        ObjectHolder = new AnimationChannel[ColumnLength];
        string tempS = "";
        lineNumber = 0;
        float[] floatValues;
        float[] floatKeyframes;
        string[] values;
        string[] keyframes;



        temp = new AnimationChannel();
        temp.channelName = "";
        temp.value1 = new List<float>();
        temp.keyframe1 = new List<float>();

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
                            if (float.TryParse((values[n]), out float num))
                            {
                                floatValues[n] = float.Parse((values[n]));
                                temp.value1.Add(floatValues[n]);
                            }
                        }
                    }
                    else if(lineNumber == 1)
                    {
                        keyframes = text.Split(',');
                        floatKeyframes = new float[keyframes.Length];
                        for (int n = 0; n < keyframes.Length; n++)
                        {
                            if(float.TryParse((keyframes[n]), out float num))
                            {
                                floatKeyframes[n] = float.Parse((keyframes[n]));
                                temp.keyframe1.Add(floatKeyframes[n]);
                            }
                        }
                    }
                    
                }
                //set the channel name from the temp string
                if (isChannelName)
                {
                    isChannelName = false;
                }
                temp.channelName = tempS;
                

                //set and read out the value for each channel name
                for (int i = 0; i < ColumnLength; i++)
                {
                    ObjectHolder[i] = temp;
                    Debug.Log("Channel Name: " + ObjectHolder[i].channelName);
                    for (int j = 0; j < ObjectHolder[i].value1.Count; j++)
                    {
                        Debug.Log("Value Name: " + ObjectHolder[i].value1[j]);
                    }
                    for (int j = 0; j < ObjectHolder[i].keyframe1.Count; j++)
                    {
                        Debug.Log("Keyframe Name: " + ObjectHolder[i].keyframe1[j]);
                    }
                }
                tempS = "";
                temp.keyframe1 = new List<float>();
                temp.value1 = new List<float>();
                temp.channelName = "";
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
