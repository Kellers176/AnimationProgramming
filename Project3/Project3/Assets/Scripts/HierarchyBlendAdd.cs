using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyBlendAdd : Hierarchy
{
    public Transform[] poses;

    BetterTransform passTrans;

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

                passTrans.pos = (node1.localPosition + node2.localPosition) / 2; //Do we average idk dude

                //for scale - need to multiply each scale together (component-wise multiplication)
                passTrans.scale = node1.localScale;
                passTrans.scale.Scale(node2.localScale);

                //rotation is just addition unless  using quaternion and then it will be multiplication
                if (usingQuaternionRotation)
                {
                    passTrans.quat = node1.localRotation * node2.localRotation;
                }
                else
                {
                    passTrans.eulerRot = node1.localEulerAngles + node2.localEulerAngles;
                }

                SetPoseResult(passTrans, j);
            }
        }
    }
}
