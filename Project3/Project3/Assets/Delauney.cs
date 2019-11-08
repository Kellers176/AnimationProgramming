using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delauney : Hierarchy
{
    // Start is called before the first frame update

    //differnet poses
    public HierarchyBlendScale pose1;
    public HierarchyBlendScale pose2;
    public HierarchyBlendScale pose3;

    //point of the graph
    public Transform currentGraphPoint;
    Pose tempPos1;
    Pose tempPos2;
    Pose tempPos3;

    float alpha, beta, gamma;

    void Start()
    {
        tempPos1 = new Pose();

    }

    // Update is called once per frame
    void Update()
    {
        TriangularLerp(currentGraphPoint.position.x, currentGraphPoint.position.y, currentGraphPoint.position.z);
    }

    /// <summary>
    /// This gets the point at the center of the graph and calculate the distance and gets the alpha, beta, and gamma
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public float calculateDistance(Transform other, Transform centerPos)
    {   //          B1
        //          *
        //        / |-A\
        //       /  |   \
        //      /   *    \ 
        //     / G-/  \ -B\    
        //    B3----------B2
        //
        float distance = Vector3.Distance(other.position, centerPos.position);
        return distance;
    }

    void TriangularLerp(float a, float b, float g)
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
        alpha = calculateDistance(pose1.transform,currentGraphPoint.transform);
        beta = calculateDistance(pose2.transform,currentGraphPoint.transform);
        gamma = calculateDistance(pose3.transform,currentGraphPoint.transform);

        //set the parameter
        pose1.GetComponent<HierarchyBlendScale>().setParameter(alpha);
        pose2.GetComponent<HierarchyBlendScale>().setParameter(beta);
        pose3.GetComponent<HierarchyBlendScale>().setParameter(gamma);
        //theoretically this would work
        //we need to scale our poses with the corresponding parameters.
        //we need to set each of these to its own corresponding pose so that we can add them later
        // once each of these is scaled, we will add them to each other and get our prime result

        //TODO: These need to be set to a pose!
        //these need to be set!
//       tempPos1.skeletonHierarchy.Add(pose1.GetComponent<HierarchyBlendScale>().Scale());
//
//       tempPos2.skeletonHierarchy.Add(pose2.GetComponent<HierarchyBlendScale>().Scale());
//
//       tempPos3.skeletonHierarchy.Add(pose3.GetComponent<HierarchyBlendScale>().Scale());


    }




}
