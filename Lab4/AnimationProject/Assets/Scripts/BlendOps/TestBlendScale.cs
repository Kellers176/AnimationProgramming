using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendScale : TestBlend
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

    public Transform pose_1 = null;

    BetterTransform pose_identity;

    [Range(0.0f,1.0f)]
    public float parameter = 1.0f;


    // Update is called once per frame
    void Update()
    {
        pose_identity.Init();
        
        //Logic: same as LERP operation as if pose_0 is pose_identity
        //SCALEp(A) = LERPp1,p(A)

        //translation: literal linear interpolaton
        pose_result.localPosition = Vector3.Lerp(pose_identity.pos, pose_1.localPosition, parameter);

        //scale: literal linear interpolation
        pose_result.localScale = Vector3.Lerp(pose_identity.scale, pose_1.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            pose_result.localRotation = Quaternion.Slerp(pose_identity.quat, pose_1.localRotation, parameter);
        }
        else
        {
            pose_result.localEulerAngles = Vector3.Lerp(pose_identity.eulerRot, pose_1.localEulerAngles, parameter);
        }
    }
}
