using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendLerp : TestBlend
{

    public Transform pose_0 = null, pose_1 = null;

    [Range(0.0f, 1.0f)]
    public float parameter = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //translation: literal linear interpolaton
        pose_result.localPosition = Vector3.Lerp(pose_0.localPosition, pose_1.localPosition, parameter);

        //scale: literal linear interpolation
        pose_result.localScale = Vector3.Lerp(pose_0.localScale, pose_1.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if(usingQuaternionRotation)
        {
            pose_result.localRotation = Quaternion.Slerp(pose_0.localRotation, pose_1.localRotation, parameter);
        }
        else
        {
            pose_result.localEulerAngles = Vector3.Lerp(pose_0.localEulerAngles, pose_1.localEulerAngles, parameter);
        }
    }
}
