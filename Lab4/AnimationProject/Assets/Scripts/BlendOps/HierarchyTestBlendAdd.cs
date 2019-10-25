using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyTestBlendAdd : TestBlend
{

    public Transform[] poses;

    // Update is called once per frame
    void Update()
    {
        //translation concatination is just addition
        //pose_result.localPosition = pose_0.localPosition + pose_1.localPosition;

        for (int i = 0; i < poses.Length - 1; i++)
        {
            pose_result.localPosition = poses[i].localPosition + poses[i + 1].localPosition;
            //for scale - need to multiply each scale together (component-wise multiplication)
            pose_result.localScale = poses[i].localScale;
            pose_result.localScale.Scale(poses[i + 1].localScale);


            //rotation is just addition unless  using quaternion and then it will be multiplication
            if (usingQuaternionRotation)
            {
                pose_result.localRotation = poses[i].localRotation * poses[i + 1].localRotation;
            }
            else
            {
                pose_result.localEulerAngles = poses[i].localEulerAngles + poses[i + 1].localEulerAngles;
            }


        }
    }
}
