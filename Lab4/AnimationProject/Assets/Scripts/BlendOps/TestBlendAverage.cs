using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendAverage : TestBlend
{
    struct BetterTransform
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
    public Transform pose_0 = null, pose_1 = null;
    //maybe?
    BetterTransform pose_identity;

    BetterTransform WeightedPose_0, WeightedPose_1;


    [Range(0.0f, 1.0f)]
    public float parameter = 1.0f;


    // Update is called once per frame
    void Update()
    {
        WeightedPose_0.Init();
        WeightedPose_1.Init();
        pose_identity.Init();

        //similar to LERP but with multiple explicit weights
        //AVGp0,p1(Ao,A1) = ADDp0,p1( ) --> p0 = SCALEp0(a0), p1 = SCALEp1(A1)

        //get the scale of the first point
        WeightedPose_0.pos = Vector3.Lerp(pose_identity.pos, pose_0.localPosition, parameter);
        //Vector3.Lerp(pose_identity.localPosition, pose_0.localPosition, parameter);

        //scale: literal linear interpolation
        WeightedPose_0.scale = Vector3.Lerp(pose_identity.scale, pose_0.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            WeightedPose_0.quat = Quaternion.Slerp(pose_identity.quat, pose_0.localRotation, parameter);
        }
        else
        {
            WeightedPose_0.eulerRot = Vector3.Lerp(pose_identity.eulerRot, pose_0.localEulerAngles, parameter);
        }
        //-------------------------------------------------------------------------------
        //get the sclade of the second point
        WeightedPose_1.pos = Vector3.Lerp(pose_identity.pos, pose_1.localPosition, parameter);
        
        //scale: literal linear interpolation
        WeightedPose_1.scale = Vector3.Lerp(pose_identity.scale, pose_1.localScale, parameter);
        
        
        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            WeightedPose_1.quat = Quaternion.Slerp(pose_identity.quat, pose_1.localRotation, parameter);
        }
        else
        {
            WeightedPose_1.eulerRot = Vector3.Lerp(pose_identity.eulerRot, pose_1.localEulerAngles, parameter);
        }
        
        //--------------------------------------------------------------------------------
        //add these two together to get the average
        pose_result.localPosition = (WeightedPose_0.pos + WeightedPose_1.pos)/2;
        
        
        //for scale - need to multiply each scale together (component-wise multiplication)
        pose_result.localScale = WeightedPose_0.scale;
        pose_result.localScale.Scale(WeightedPose_1.scale);
        //pose_result.localScale /= 2;
        
        
        //rotation is just addition unless  using quaternion and then it will be multiplication
        if (usingQuaternionRotation)
        {
            pose_result.localRotation = (WeightedPose_0.quat * WeightedPose_1.quat);
            pose_result.localRotation = QuaternionDivide(pose_result.localRotation, 2.0f);
        }
        else
        {
            pose_result.localEulerAngles = (WeightedPose_0.eulerRot + WeightedPose_1.eulerRot) / 2;
        }
    }

    Quaternion QuaternionDivide(Quaternion qu, float number)
    {
        Quaternion temp = qu;

        temp.x /= number;
        temp.y /= number;
        temp.z /= number;
        temp.w /= number;

        return temp;
    }
}
