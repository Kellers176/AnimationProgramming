using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyTestBlendScale : Hierchary
{

    public Transform[] poses;

    BetterTransform passTrans;  

    [Range(0, 1)]
    public float parameter;

    // Update is called once per frame
    void Update()
    {
        passTrans.Init();

        //translation concatination is just addition
        //pose_result.localPosition = pose_0.localPosition + pose_1.localPosition;

        //Get nodes in hierarchy
        int amountOfNodes = 1;
        Transform root = poses[0];

        while (root.childCount != 0)
        {
            amountOfNodes++;

            for (int q = 0; q < root.childCount; q++)
            {
                if (root.GetChild(q).gameObject.tag == NODE_TAG)
                {
                    root = root.GetChild(q);
                    q = 0;
                }
            }
        }

        for (int i = 0; i < poses.Length - 1; i++)
        {
            Transform node1 = poses[i];
            Transform node2 = poses[i + 1];

            //Do this for each node in the hierachy
            for (int j = 0; j < amountOfNodes; j++)
            {
                for (int p = 0; p < node1.childCount; p++)
                {
                    if (node1.GetChild(p).gameObject.tag == NODE_TAG)
                    {
                        node1 = node1.GetChild(p);
                        break;
                    }
                }

                for (int k = 0; k < node2.childCount; k++)
                {
                    if (node2.GetChild(k).gameObject.tag == NODE_TAG)
                    {
                        node2 = node2.GetChild(k);
                        break;
                    }
                }

                //translation: literal linear interpolaton
                passTrans.pos = Vector3.Lerp(node1.localPosition, node2.localPosition, parameter);

                //scale: literal linear interpolation
                passTrans.scale = Vector3.Lerp(node1.localScale, node2.localScale, parameter);


                //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
                if (usingQuaternionRotation)
                {
                    passTrans.quat = Quaternion.Slerp(node1.localRotation, node2.localRotation, parameter);
                }
                else
                {
                    passTrans.eulerRot = Vector3.Lerp(node1.localEulerAngles, node2.localEulerAngles, parameter);
                }

                SetPoseResult(passTrans, j);
            }
        }
    }
}
