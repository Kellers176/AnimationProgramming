using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendAverage : TestBlend
{
    // Start is called before the first frame update
     public Transform pose_0 = null, pose_1 = null;
    //maybe?
    public Transform pose_identity;

    public Transform WeightedPose_0 = null, WeightedPose_1 = null;
    [Range(0.0f, 1.0f)]
    public float parameter = 1.0f;


    // Update is called once per frame
    void Update()
    {
        //similar to LERP but with multiple explicit weights
        //AVGp0,p1(Ao,A1) = ADDp0,p1( ) --> p0 = SCALEp0(a0), p1 = SCALEp1(A1)

        //get the scale of the first point
        WeightedPose_0.localPosition = Vector3.Lerp(pose_identity.localPosition, pose_0.localPosition, parameter);

        //scale: literal linear interpolation
        WeightedPose_0.localScale = Vector3.Lerp(pose_identity.localScale, pose_0.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            WeightedPose_0.localRotation = Quaternion.Slerp(pose_identity.localRotation, pose_0.localRotation, parameter);
        }
        else
        {
            WeightedPose_0.localEulerAngles = Vector3.Lerp(pose_identity.localEulerAngles, pose_0.localEulerAngles, parameter);
        }
        //-------------------------------------------------------------------------------
        //get the sclade of the second point
        WeightedPose_1.localPosition = Vector3.Lerp(pose_identity.localPosition, pose_1.localPosition, parameter);

        //scale: literal linear interpolation
        WeightedPose_1.localScale = Vector3.Lerp(pose_identity.localScale, pose_1.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            WeightedPose_1.localRotation = Quaternion.Slerp(pose_identity.localRotation, pose_1.localRotation, parameter);
        }
        else
        {
            WeightedPose_1.localEulerAngles = Vector3.Lerp(pose_identity.localEulerAngles, pose_1.localEulerAngles, parameter);
        }

        //--------------------------------------------------------------------------------
        //add these two together to get the average
        pose_result.localPosition = WeightedPose_0.localPosition + WeightedPose_1.localPosition;


        //for scale - need to multiply each scale together (component-wise multiplication)
        pose_result.localScale = WeightedPose_0.localScale;
        pose_result.localScale.Scale(WeightedPose_1.localScale);


        //rotation is just addition unless  using quaternion and then it will be multiplication
        if (usingQuaternionRotation)
        {
            pose_result.localRotation = WeightedPose_0.localRotation * WeightedPose_1.localRotation;
        }
        else
        {
            pose_result.localEulerAngles = WeightedPose_0.localEulerAngles + WeightedPose_1.localEulerAngles;
        }
    }
}
