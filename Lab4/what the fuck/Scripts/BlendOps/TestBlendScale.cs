using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlendScale : TestBlend
{
    public Transform pose_1 = null;

    //maybe?
    public Transform pose_identity;


    [Range(0.0f,1.0f)]
    public float parameter = 1.0f;



    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        //Logic: same as LERP operation as if pose_0 is pose_identity
        //SCALEp(A) = LERPp1,p(A)

        //translation: literal linear interpolaton
        pose_result.localPosition = Vector3.Lerp(pose_identity.localPosition, pose_1.localPosition, parameter);

        //scale: literal linear interpolation
        pose_result.localScale = Vector3.Lerp(pose_identity.localScale, pose_1.localScale, parameter);


        //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
        if (usingQuaternionRotation)
        {
            pose_result.localRotation = Quaternion.Slerp(pose_identity.localRotation, pose_1.localRotation, parameter);
        }
        else
        {
            pose_result.localEulerAngles = Vector3.Lerp(pose_identity.localEulerAngles, pose_1.localEulerAngles, parameter);
        }
    }
}
