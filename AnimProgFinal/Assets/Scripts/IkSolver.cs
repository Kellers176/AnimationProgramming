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

    public Transform tochange;

    public GameObject bone1;
    public GameObject bone2;

    float length1;
    float length2;

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

    //Heron's formula
    float baseLength;
    float area;
    float height;

    //pythagorean theroem
    float distance;

    //final postion
    Vector3 finalPosition;

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


    //this is causing errors
    Vector3 FindNormalVec(Vector3 left)
    {
        Vector3 temp;
        temp = left / Vector3.Magnitude(left);
        return temp;
    }

    void HeronsFormula()
    {
        //get the base length 
        baseLength = effectorDisplacement.magnitude;
        //calculate s
        float s = 0.5f * (baseLength + length1 + length2);
        //get the sqrt for the area
        area = Mathf.Sqrt(s * (s - baseLength) * (s - length1) * (s - length2));
        //calculate the height
        height = (2 * area) / baseLength;

    }
    float PythagoreanTheorem(float lhs, float rhs)
    {
        float D = Mathf.Sqrt((lhs * lhs) - (rhs * rhs));
        return D;
    }


    // Start is called before the first frame update
    void Start()
    {
        //this needs to be fixed
        //        Transform[] allChildren = this.GetComponentsInChildren<Transform>();
        //        foreach (Transform child in allChildren)
        //        {
        //            if (child.gameObject.transform.GetComponent<BoxCollider>())
        //                //chainLength += Vector3.Distance(child.gameObject.transform.position, child.gameObject.transform.position);
        //                chainLength += child.gameObject.transform.localScale.y;
        //        }

        length1 = bone1.transform.localScale.y;
        length2 = bone2.transform.localScale.y;
        chainLength = 2;
    }

    // Update is called once per frame
    void Update()
    {

        //check to see if effector sitance is greater than chain length
        //we dont have a valid solution
        effectorDistance = Vector3.Distance(baseEffector.transform.position, endEffector.transform.position);
        Debug.Log(effectorDistance);
        Debug.Log(chainLength);
        constrainPosition = poleVector.position;
        basePosition = baseEffector.position;
        endPosition = endEffector.position;
        if( effectorDistance < chainLength)
        {
            //=============================================
            //solve for positions!
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

            //Step3
            //calculate herons formula
            HeronsFormula();

            //Step 4
            //calculate pythagorean theorem
            distance = PythagoreanTheorem(length1, height);

            //Step 5
            //calculate final location
            finalPosition = basePosition + (distance * normalizedDisplacement) + (height * normalizedHeight);

            Vector3 changedPos = finalPosition - tochange.position;
            endEffector.position -= changedPos;

            tochange.position = finalPosition;
            //===========================================
            //Solve for Orientations!

            //Debug.Log(finalPosition.x + " " + finalPosition.y + " " + finalPosition.z);


        }



    }
}
