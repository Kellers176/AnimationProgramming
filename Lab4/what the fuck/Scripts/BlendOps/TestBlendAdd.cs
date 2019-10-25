using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendAdd : TestBlend
{
    //Inputs
    public Transform pose_0 = null, pose_1 = null;


    // Update is called once per frame
    void Update()
    {
        //translation concatination is just addition
        pose_result.localPosition = pose_0.localPosition + pose_1.localPosition;


        //for scale - need to multiply each scale together (component-wise multiplication)
        pose_result.localScale = pose_0.localScale;
        pose_result.localScale.Scale(pose_1.localScale);


        //rotation is just addition unless  using quaternion and then it will be multiplication
        if(usingQuaternionRotation)
        {
            pose_result.localRotation = pose_0.localRotation * pose_1.localRotation;
        }
        else
        {
            pose_result.localEulerAngles = pose_0.localEulerAngles + pose_1.localEulerAngles;
        }


    }
}
