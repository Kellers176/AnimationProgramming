using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyBlendAvg : Hierarchy
{
    public Transform[] poses;

    BetterTransform passTrans;

    BetterTransform WeightedPose_0, WeightedPose_1;
    BetterTransform pose_identity;

    [Range(0, 1)]
    public float parameter;

    // Update is called once per frame
    void Update()
    {
        WeightedPose_0.Init();
        WeightedPose_1.Init();
        pose_identity.Init();
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

                //similar to LERP but with multiple explicit weights
                //AVGp0,p1(Ao,A1) = ADDp0,p1( ) --> p0 = SCALEp0(a0), p1 = SCALEp1(A1)

                //get the scale of the first point
                WeightedPose_0.pos = Vector3.Lerp(pose_identity.pos, node1.localPosition, parameter);
                //Vector3.Lerp(pose_identity.localPosition, pose_0.localPosition, parameter);

                //scale: literal linear interpolation
                WeightedPose_0.scale = Vector3.Lerp(pose_identity.scale, node1.localScale, parameter);


                //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
                if (usingQuaternionRotation)
                {
                    WeightedPose_0.quat = Quaternion.Slerp(pose_identity.quat, node1.localRotation, parameter);
                }
                else
                {
                    WeightedPose_0.eulerRot = Vector3.Lerp(pose_identity.eulerRot, node1.localEulerAngles, parameter);
                }
                //-------------------------------------------------------------------------------
                //get the sclade of the second point
                WeightedPose_1.pos = Vector3.Lerp(pose_identity.pos, node2.localPosition, parameter);

                //scale: literal linear interpolation
                WeightedPose_1.scale = Vector3.Lerp(pose_identity.scale, node2.localScale, parameter);


                //rotation: SLERP (or NLERP) if using quaternions otherwise regular literal linaer interpolation
                if (usingQuaternionRotation)
                {
                    WeightedPose_1.quat = Quaternion.Slerp(pose_identity.quat, node2.localRotation, parameter);
                }
                else
                {
                    WeightedPose_1.eulerRot = Vector3.Lerp(pose_identity.eulerRot, node2.localEulerAngles, parameter);
                }

                //--------------------------------------------------------------------------------
                //add these two together to get the average
                passTrans.pos = (WeightedPose_0.pos + WeightedPose_1.pos) / 2;


                //for scale - need to multiply each scale together (component-wise multiplication)
                passTrans.scale = WeightedPose_0.scale;
                passTrans.scale.Scale(WeightedPose_1.scale);
                //pose_result.localScale /= 2;


                //rotation is just addition unless  using quaternion and then it will be multiplication
                if (usingQuaternionRotation)
                {
                    passTrans.quat = (WeightedPose_0.quat * WeightedPose_1.quat);
                    passTrans.quat = QuaternionDivide(pose_result.localRotation, 2.0f);
                }
                else
                {
                    passTrans.eulerRot = (WeightedPose_0.eulerRot + WeightedPose_1.eulerRot) / 2;
                }

                SetPoseResult(passTrans, j);
            }
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
