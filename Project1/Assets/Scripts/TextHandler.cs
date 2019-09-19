using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TextHandler : MonoBehaviour
{
    string text = " ";
    string line = " ";
    string[] types;
    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = new StreamReader("Assets/TxtFiles/GraphInfo.txt");
        types = new string[100];
        int j = 0;
        while(text != null)
        {
            text = reader.ReadLine();
            //Debug.Log(text);
            if(text[0] == '@')
            {
                for(int i = 1; i < text.Length; i++)
                {
                    line += text[i];
                }
                types[j] = line;
                Debug.Log(types[j]);
                line = " ";
            }
            j++;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
