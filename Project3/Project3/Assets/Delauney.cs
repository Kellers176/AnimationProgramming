using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delauney : TestBlend
{
    // Start is called before the first frame update

    public GameObject gameManager;


    //differnet poses
    public TestBlendScale pose1;
    public TestBlendScale pose2;
    public TestBlendScale pose3;

    public Transform deltapose1;
    public Transform deltapose2;
    public Transform deltapose3;
    //point of the graph
    public Vector2 currentGraphPoint;
    public Vector2 Pose1GraphTransform;
    public Vector2 Pose2GraphTransform;
    public Vector2 Pose3GraphTransform;


    Transform tempScalePos1;
    Transform tempScalePos2;
    Transform tempScalePos3;

    Transform tempAddPose1;
    Transform tempAddPose2;

    Transform finalPose;

    float alpha, beta, gamma;


    // Update is called once per frame
    void Update()
    {
        TriangularLerp();
    }

    /// <summary>
    /// This gets the point at the center of the graph and calculate the distance and gets the alpha, beta, and gamma
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public float calculateDistance(Vector2 other, Vector2 centerPos)
    {   //          B1
        //          *
        //        / |-A\
        //       /  |   \
        //      /   *    \ 
        //     / G-/  \ -B\    
        //    B3----------B2
        //
        float distance = Vector3.Distance(other, centerPos);
        return distance;
    }

    void TriangularLerp()
    {
        //                ---------------------
        //  pose1 -----> |  SCALE (with Alpha) |    \
        //                ---------------------      \      -------
        //                                            >    |  ADD  |  \
        //                --------------------       /      -------    \    -------
        //  pose2 -----> |  SCALE (with Beta) |     /                    > |  ADD  | ------> Pose'
        //                --------------------                         /    ------- 
        //                                                            /
        //                ---------------------                      /
        //  pose3 -----> |  SCALE (with gamma) | -------------------/
        //                ---------------------

        //set our beta, alpha, and gamma
        alpha = calculateDistance(Pose1GraphTransform, currentGraphPoint);
        beta = calculateDistance(Pose2GraphTransform, currentGraphPoint);
        gamma = calculateDistance(Pose3GraphTransform, currentGraphPoint);

        //set the parameter
        pose1.setParameter(alpha);
        pose2.setParameter(beta);
        pose3.setParameter(gamma);

        //scale each pose
        tempScalePos1 = pose1.Scale(deltapose1.transform);
        tempScalePos2 = pose2.Scale(deltapose2.transform);
        tempScalePos3 = pose3.Scale(deltapose3.transform);

        tempAddPose1 = gameManager.GetComponent<TestBlendAdd>().Add(tempScalePos1, tempScalePos2);

        finalPose = gameManager.GetComponent<TestBlendAdd>().Add(tempAddPose1, tempScalePos3);

        this.transform.position = finalPose.transform.position;
        this.transform.rotation = finalPose.transform.rotation;
        this.transform.localScale = finalPose.transform.localScale;

    }

    public void setPose1Transform(Vector2 pos)
    {
        Pose1GraphTransform = pos;
    }
    public void setPose2Transform(Vector2 pos)
    {
        Pose2GraphTransform = pos;
    }
    public void setPose3Transform(Vector2 pos)
    {
        Pose3GraphTransform = pos;
    }

    public void setCurrentGraphPoint(Vector2 pos)
    {
        currentGraphPoint = pos;
    }


}
