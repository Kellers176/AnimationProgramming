using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hierchary : MonoBehaviour
{
    public GameObject rootNode;

    GameObject parentNode;
    GameObject childNode;

    GameObject currentNode;

    const string NODE_TAG = "Node";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<GameObject> GetConnectedNodes(GameObject current)
    {
        List<GameObject> tempList = new List<GameObject>();

        for (int i = 0; i < current.transform.childCount; i++)
        {
            if (current.transform.GetChild(i).tag == NODE_TAG)
                tempList.Add(current.transform.GetChild(i).gameObject);
        }

        return tempList;
    }

    bool HasChildNode(GameObject current)
    {
        bool temp = false;

        if (GetConnectedNodes(current).Count != 0)
            temp = true;

        return temp;
    }

    bool HasParentNode(GameObject current)
    {
        bool temp = false;

        if (current.transform.parent.gameObject.tag == NODE_TAG)
            temp = true;

        return temp;
    }
}
