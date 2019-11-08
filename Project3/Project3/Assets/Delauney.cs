using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delauney : MonoBehaviour
{
    // Start is called before the first frame update

    //differnet poses
    public Hierarchy pose1;
    public Hierarchy pose2;
    public Hierarchy pose3;

    //point of the graph
    public Transform currentGraphPoint;


    float alpha, beta, gamma;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


        //theoretically this would work
        //we need to scale our poses with the corresponding parameters.
        //we need to set each of these to its own corresponding pose so that we can add them later
        // once each of these is scaled, we will add them to each other and get our prime result

        //TODO: These need to be set to a pose!
        //these need to be set!
        pose1.GetComponent<HierarchyBlendScale>().setParameter(alpha);
        pose1.GetComponent<HierarchyBlendScale>().Scale();

        pose2.GetComponent<HierarchyBlendScale>().setParameter(beta);
        pose2.GetComponent<HierarchyBlendScale>().Scale();

        pose3.GetComponent<HierarchyBlendScale>().setParameter(gamma);
        pose3.GetComponent<HierarchyBlendScale>().Scale();


    }




}
