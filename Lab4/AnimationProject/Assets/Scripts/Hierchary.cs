using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hierchary : MonoBehaviour
{
    protected const string NODE_TAG = "Node";

    protected Transform pose_result;

    Transform rootNode;

    protected bool usingQuaternionRotation = true;

    protected struct BetterTransform
    {
        public Vector3 pos;
        public Vector3 eulerRot;
        public Vector3 scale;
        public Quaternion quat;

        public void Init()
        {
            //Hard coded jazz
            pos = Vector3.zero;
            eulerRot = Vector3.zero;
            scale = Vector3.one;
            quat = Quaternion.identity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pose_result = this.gameObject.transform;
        rootNode = this.gameObject.transform;
    }

    protected void SetPoseResult(BetterTransform newTrans, int downHierchary)
    {
        Transform temp = rootNode;

        //Getting the current node via the down var
        for (int i = 0; i <= downHierchary; i++)
        {
            for (int p = 0; p < temp.childCount; p++)
            {
                if (temp.GetChild(p).gameObject.tag == NODE_TAG)
                {
                    temp = temp.GetChild(p);
                    break;
                }
            }
        }

        temp.localPosition = newTrans.pos;
        temp.localScale = newTrans.scale;

        if (usingQuaternionRotation)
            temp.localRotation = newTrans.quat;

        else
            temp.localEulerAngles = newTrans.eulerRot;
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
