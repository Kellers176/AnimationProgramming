using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkSolver : MonoBehaviour
{
    //Fixed
    public Transform baseEffector;
    //Goal controlled
    public Transform endEffector;
    //Constraint locator changes with the player movement
    public Transform poleVector;

    float chainLength;
    float effectorDistance;

    //solved locations
    Vector3 constraintDisplacement; //c
    Vector3 effectorDisplacement; //d
    Vector3 normalPlane; //n-plane

    //positions
    Vector3 endPosition;    //e-end
    Vector3 basePosition;     //e-base
    Vector3 constrainPosition; //e-loc

    //normalized 
    Vector3 normalizedHeight;     //h-carrot
    Vector3 normalizedDisplacement;   //d carrot
    Vector3 normalizedNPlane;     //n-carrot

    //values
    float distanceAlongBase;
    float heightOfTriangle;

    //Heron's formula
    Vector3 baseLength;
    Vector3 a;
    Vector3 height;

    Vector3 CalculateDisplacement(Vector3 left, Vector3 right)
    {
        Vector3 temp;
        //elocal - ebase
        temp = left - right;
        return temp;
    }

    Vector3 CalculateCrossProductOfVector3(Vector3 left, Vector3 right)
    {
        Vector3 temp;
        temp = Vector3.Cross(left, right);
        return temp;
    }

    Vector3 FindNormalVec(Vector3 left)
    {
        //
        Vector3 temp;
        temp = left / Vector3.Magnitude(left);
        return temp;
    }


    // Start is called before the first frame update
    void Start()
    {
        //this needs to be fixed
        Transform[] allChildren = this.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.transform.GetComponent<BoxCollider>())
                //chainLength += Vector3.Distance(child.gameObject.transform.position, child.gameObject.transform.position);
                chainLength += child.gameObject.transform.localScale.y;
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        //check to see if effector sitance is greater than chain length
        //we dont have a valid solution
        effectorDistance = Vector3.Distance(baseEffector.transform.position, endEffector.transform.position);
        Debug.Log(effectorDistance);

        if( effectorDistance < chainLength)
        {
            //Step1
            //calculate constraint displacement
            constraintDisplacement = CalculateDisplacement(constrainPosition, basePosition);
            //calculate effector displacement
            effectorDisplacement = CalculateDisplacement(endPosition, basePosition);
            //calculate n-plane
            normalPlane = CalculateCrossProductOfVector3(effectorDisplacement, constraintDisplacement);

            //Step2
            //Find the normalized version of our NPlane
            normalizedNPlane = FindNormalVec(normalPlane);
            //find normalized version of Effector displacement
            normalizedDisplacement = FindNormalVec(effectorDisplacement);
            //find cross product of normal plane and effector displacement
            normalizedHeight = CalculateCrossProductOfVector3(normalizedNPlane, normalizedDisplacement);

            



        }

        






    }
}
