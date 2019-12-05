using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSolver : MonoBehaviour
{
    //Fixed
    Transform baseEffector;
    //Goal controlled
    Transform endEffector;
    //Constraint locator changes with the player movement
    Transform poleVector;

    float chainLength;
    float effectorDistance;

    //solved locations
    Vector3 constraintDisplacement; //c
    Vector3 effectorDisplacement; //d
    Vector3 normalPlane; //n-plane

    //positions
    Vector3 endPosition;    //e-end
    Vector3 basePosion;     //e-base
    Vector3 constrainPosition; //e-loc

    //normalized 
    float normalizedHeight;     //h-carrot
    float normalizedDisplacement;   //d carrot
    float normalizedNormal;     //n-carrot

    //values
    float distanceAlongBase;
    float heightOfTriangle;

    //Heron's formula
    Vector3 baseLength;
    Vector3 a;
    Vector3 height;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
