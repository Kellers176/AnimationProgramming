using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        holder.channelName = "No";
        holder.value = 1;
        holder.keyFrame = 0;
        ColumnLength = 10;
        RowHeight = 10;
        ObjectHolder = new ObjectInformation[ColumnLength, RowHeight];

        for(int i = 0; i < ColumnLength; i++)
        {
            ObjectHolder[i, 0] = holder;
            for (int j = 0; j < RowHeight; j++)
            {
                ObjectHolder[i, j] = holder;
                Debug.Log("Channel Name: " + ObjectHolder[i, 0].channelName);
                Debug.Log("Value: " + ObjectHolder[i, 1].value);
                Debug.Log("Keyframe: " + ObjectHolder[i, 2].keyFrame);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
